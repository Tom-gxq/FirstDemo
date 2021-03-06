#region License

/* Copyright (c) 2005 Leslie Sanford
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 * THE SOFTWARE.
 */

#endregion

#region Contact

/*
 * Leslie Sanford
 * Email: jabberdabber@hotmail.com
 */

#endregion

using System;
using System.IO;

namespace Sanford.Multimedia.Midi
{
	/// <summary>
	/// Reads a track from a stream.
	/// </summary>
	internal class TrackReader
	{
        private Track track = new Track();

        private Track newTrack = new Track();

        private ChannelMessageBuilder cmBuilder = new ChannelMessageBuilder();

        private SysCommonMessageBuilder scBuilder = new SysCommonMessageBuilder();

        private Stream stream;

        private byte[] trackData;

        private int trackIndex;

        private int previousTicks;

        private int ticks;

        private int status;

        private int runningStatus;
        private SequencerDemo.Note.Score score = null;



        public TrackReader(SequencerDemo.Note.Score score)
		{
            this.score = score;
		}

        public void Read(Stream strm)
        { 
            stream = strm;     
            FindTrack();

            int trackLength = GetTrackLength();
            trackData = new byte[trackLength];

            int result = strm.Read(trackData, 0, trackLength);

            if(result < 0)
            {
                throw new MidiFileException("End of MIDI file unexpectedly reached.");
            }
            
            newTrack = new Track();

            ParseTrackData();

            track = newTrack;
        }

        /// <summary>
        /// 寻找MIDI文件头
        /// </summary>
        private void FindTrack()
        {
            bool found = false;
            int result;

            while(!found)
            {
                result = stream.ReadByte();

                if(result == 'M')
                {
                    result = stream.ReadByte();

                    if(result == 'T')
                    {
                        result = stream.ReadByte();

                        if(result == 'r')
                        {
                            result = stream.ReadByte();

                            if(result == 'k')
                            {
                                found = true;
                            }
                        }
                    }
                }

                if(result < 0)
                {
                    throw new MidiFileException("Unable to find track in MIDI file.");
                }
            }
        }

        /// <summary>
        /// 获取音轨数据长度
        /// </summary>
        /// <returns></returns>
        private int GetTrackLength()
        {
            //音轨头后四个字节表示该音轨长度
            byte[] trackLength = new byte[4];

            int result = stream.Read(trackLength, 0, trackLength.Length);
            
            if(result < trackLength.Length)
            {
                throw new MidiFileException("End of MIDI file unexpectedly reached.");
            }

            if(BitConverter.IsLittleEndian)
            {
                Array.Reverse(trackLength);
            }

            return BitConverter.ToInt32(trackLength, 0);
        }

        /// <summary>
        /// 解析音轨数据
        /// </summary>
        private void ParseTrackData()
        {
            trackIndex = ticks = runningStatus = 0;

            while(trackIndex < trackData.Length)
            {
                previousTicks = ticks;
                
                ticks += ReadVariableLengthValue();

                if((trackData[trackIndex] & 0x80) == 0x80)//midi命令最小是80
                {
                    status = trackData[trackIndex];
                    trackIndex++;
                }
                else
                {
                    status = runningStatus;
                }  
              
                ParseMessage(ticks- previousTicks);                
            }
        }

        /// <summary>
        /// 分析音轨块数据
        /// </summary>
        private void ParseMessage(int ticks=0)
        {
            // If this is a channel message MIDI事件命令
            if (status >= (int)ChannelCommand.NoteOff && 
                status <= (int)ChannelCommand.PitchWheel + 
                ChannelMessage.MidiChannelMaxValue)
            {
                ParseChannelMessage(ticks);
            }
            // Else if this is a meta message.
            else if(status == 0xFF)
            {
                ParseMetaMessage();                
            }
            // Else if this is the start of a system exclusive message.
            else if(status == (int)SysExType.Start)
            {
                ParseSysExMessageStart();
            }
            // Else if this is a continuation of a system exclusive message.
            else if(status == (int)SysExType.Continuation)
            {
                ParseSysExMessageContinue();
            }
            // Else if this is a system common message.
            else if(status >= (int)SysCommonType.MidiTimeCode &&
                status <= (int)SysCommonType.TuneRequest)
            {
                ParseSysCommonMessage();
            }
            // Else if this is a system realtime message.
            else if(status >= (int)SysRealtimeType.Clock &&
                status <= (int)SysRealtimeType.Reset)
            {
                ParseSysRealtimeMessage();                
            }
        }

        /// <summary>
        /// 分析MIDI事件命令
        /// </summary>
        private void ParseChannelMessage(int noteTicks=0)
        {
            if(trackIndex >= trackData.Length)
            {
                throw new MidiFileException("End of track unexpectedly reached.");
            }

            cmBuilder.Command = ChannelMessage.UnpackCommand(status);
            cmBuilder.MidiChannel = ChannelMessage.UnpackMidiChannel(status);
            cmBuilder.Data1 = trackData[trackIndex];

            //添加音符到描画结构
            this.score.AddNote(cmBuilder.Data1, cmBuilder.Command, noteTicks);

            trackIndex++;

            if(ChannelMessage.DataBytesPerType(cmBuilder.Command) == 2)
            {
                if(trackIndex >= trackData.Length)
                {
                    throw new MidiFileException("End of track unexpectedly reached.");
                }
                        
                cmBuilder.Data2 = trackData[trackIndex];

                trackIndex++;
            }  

            cmBuilder.Build();            
            newTrack.Insert(ticks, cmBuilder.Result, noteTicks);
            runningStatus = status;
        }

        private void ParseMetaMessage()
        {
            if(trackIndex >= trackData.Length)
            {
                throw new MidiFileException("End of track unexpectedly reached.");
            }

            MetaType type = (MetaType)trackData[trackIndex];

            trackIndex++;

            if(trackIndex >= trackData.Length)
            {
                throw new MidiFileException("End of track unexpectedly reached.");
            }

            if(type == MetaType.EndOfTrack)
            {
                newTrack.EndOfTrackOffset = ticks - previousTicks;

                trackIndex++;
            }
            else
            {
                byte[] data = new byte[ReadVariableLengthValue()];
                Array.Copy(trackData, trackIndex, data, 0, data.Length);
                newTrack.Insert(ticks, new MetaMessage(type, data));        

                trackIndex += data.Length;
                switch (type)
                {
                    case MetaType.TimeSignature:
                        {
                            //记录拍子记号
                            this.score.FewShot = data[0];
                            //以什么类型音符为一拍
                            int tem = data[1];
                            switch (tem)
                            {
                                case 0:
                                    this.score.OneShotNote = SequencerDemo.Note.NoteType.Whole;
                                    break;
                                case 1:
                                    this.score.OneShotNote = SequencerDemo.Note.NoteType.Minims;
                                    break;
                                case 2:
                                    this.score.OneShotNote = SequencerDemo.Note.NoteType.CrotchetsC;
                                    break;
                                case 3:
                                    this.score.OneShotNote = SequencerDemo.Note.NoteType.Quavers;
                                    break;
                                case 4:
                                    this.score.OneShotNote = SequencerDemo.Note.NoteType.Demiquaver;
                                    break;
                                case 5:
                                    this.score.OneShotNote = SequencerDemo.Note.NoteType.Demisemiquaver;
                                    break;
                            }
                        }
                        break;
                    case MetaType.Text:
                        //作者
                        this.score.Author = System.Text.Encoding.Default.GetString(data);
                        break;
                    case MetaType.TrackName:
                        //曲名
                        this.score.Name = System.Text.Encoding.Default.GetString(data);
                        break;
                }
                

            }
        }

        private void ParseSysExMessageStart()
        {
            // System exclusive cancels running status.
            runningStatus = 0;

            byte[] data = new byte[ReadVariableLengthValue() + 1];
            data[0] = (byte)SysExType.Start;

            Array.Copy(trackData, trackIndex, data, 1, data.Length - 1);
            newTrack.Insert(ticks, new SysExMessage(data));

            trackIndex += data.Length - 1;
        }

        private void ParseSysExMessageContinue()
        {
            trackIndex++;

            if(trackIndex >= trackData.Length)
            {
                throw new MidiFileException("End of track unexpectedly reached.");
            }

            // System exclusive cancels running status.
            runningStatus = 0;
           
            // If this is an escaped message rather than a system exclusive 
            // continuation message.
            if((trackData[trackIndex] & 0x80) == 0x80)
            {
                status = trackData[trackIndex];
                trackIndex++;

                ParseMessage();
            }
            else
            {
                byte[] data = new byte[ReadVariableLengthValue() + 1];
                data[0] = (byte)SysExType.Continuation;

                Array.Copy(trackData, trackIndex, data, 1, data.Length - 1);
                newTrack.Insert(ticks, new SysExMessage(data));

                trackIndex += data.Length - 1;
            }
        }

        private void ParseSysCommonMessage()
        {
            if(trackIndex >= trackData.Length)
            {
                throw new MidiFileException("End of track unexpectedly reached.");
            }

            // System common cancels running status.
            runningStatus = 0;

            scBuilder.Type = (SysCommonType)status;

            switch((SysCommonType)status)
            {
                case SysCommonType.MidiTimeCode:
                    scBuilder.Data1 = trackData[trackIndex];
                    trackIndex++;
                    break;

                case SysCommonType.SongPositionPointer:
                    scBuilder.Data1 = trackData[trackIndex];
                    trackIndex++;

                    if(trackIndex >= trackData.Length)
                    {
                        throw new MidiFileException("End of track unexpectedly reached.");
                    }

                    scBuilder.Data2 = trackData[trackIndex];
                    trackIndex++;
                    break;

                case SysCommonType.SongSelect:
                    scBuilder.Data1 = trackData[trackIndex];
                    trackIndex++;
                    break;

                case SysCommonType.TuneRequest:
                    // Nothing to do here.
                    break;
            }

            scBuilder.Build();

            newTrack.Insert(ticks, scBuilder.Result);
        }

        private void ParseSysRealtimeMessage()
        {
            SysRealtimeMessage e = null;

            switch((SysRealtimeType)status)
            {
                case SysRealtimeType.ActiveSense:
                    e = SysRealtimeMessage.ActiveSenseMessage;
                    break;

                case SysRealtimeType.Clock:
                    e = SysRealtimeMessage.ClockMessage;
                    break;

                case SysRealtimeType.Continue:
                    e = SysRealtimeMessage.ContinueMessage;
                    break;

                case SysRealtimeType.Reset:
                    e = SysRealtimeMessage.ResetMessage;
                    break;

                case SysRealtimeType.Start:
                    e = SysRealtimeMessage.StartMessage;
                    break;

                case SysRealtimeType.Stop:
                    e = SysRealtimeMessage.StopMessage;
                    break;

                case SysRealtimeType.Tick:
                    e = SysRealtimeMessage.TickMessage;
                    break;
            }

            newTrack.Insert(ticks, e);
        }

        /// <summary>
        /// 获取音符前的tick数
        /// </summary>
        /// <returns></returns>
        private int ReadVariableLengthValue()
        {
            if(trackIndex >= trackData.Length)
            {
                throw new MidiFileException("End of track unexpectedly reached.");
            }

            int result = 0;

            result = trackData[trackIndex];

            trackIndex++;

            //超过127的话长度首位为1，作为标志
            if((result & 0x80) == 0x80)
            {
                result &= 0x7F;//去掉首位标志，取得实际长度值

                int temp;
                //循环获取长度值，知道首位标志为0，音符长度结束
                do
                {
                    if(trackIndex >= trackData.Length)
                    {
                        throw new MidiFileException("End of track unexpectedly reached.");
                    }

                    temp = trackData[trackIndex];
                    trackIndex++;
                    result <<= 7;
                    result |= temp & 0x7F;
                }while((temp & 0x80) == 0x80);
            }

            return result;            
        }

        public Track Track
        {
            get
            {
                return track;
            }
        }
	}
}
