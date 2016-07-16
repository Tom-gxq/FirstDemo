using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace SequencerDemo.Note
{
    public class Score
    {
        private string name;
        private string author;//作者
        private Stack<NoteBar> barList =new Stack<NoteBar>();//小节
        private int fewShot;//一小节有几拍
        private NoteType oneShotNote;//以什么样的音符为一拍
        private int bitTime;//一拍的时间（单位tick）
        private int headLen;//midi头长


        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
            }
        }
        public int FewShot
        {
            get
            {
                return this.fewShot;
            }
            set
            {
                this.fewShot = value;
            }
        }

        public NoteType OneShotNote
        {
            get
            {
                return this.oneShotNote;
            }
            set
            {
                this.oneShotNote = value;
            }
        }

        public int BitTime
        {
            get
            {
                return this.bitTime;
            }
            set
            {
                this.bitTime = value;
            }
        }
        
        public int HeadLen
        {
            get
            {
                return this.headLen;
            }
            set
            {
                this.headLen = value;
            }
        }
        public List<NoteBar> NoteBars
        {
            get
            {
                return new List<NoteBar>(this.barList.ToArray());
            }
            set
            {
                if (value != null)
                {
                    value.ForEach(x => this.barList.Push(x));
                }
            }
        }
        public void AddNoteBars(NoteBar noteBar)
        {
            this.barList.Push(noteBar);
        }
        
        public void AddNote(int data1, ChannelCommand command,int ticks)
        {
            if(command == ChannelCommand.NoteOn)
            {
                NoteBar bar = null;
                if (this.barList.Count > 0)
                {
                    bar = this.barList.Pop();
                    int paiCnt = bar.BarTicks % this.BitTime;
                    if (((bar.BarTicks / this.BitTime ) >=this.fewShot)
                        ||((paiCnt >0) &&(bar.BarTicks > this.bitTime) && (( this.BitTime % paiCnt > 0)) ))
                    {
                        this.barList.Push(bar);
                        bar = new NoteBar(this.bitTime);
                    }
                }
                else
                {
                    bar = new NoteBar(this.bitTime);
                }

                if ((ticks > 0) && (this.barList.Count <= 0))
                {
                    int data2 = 0;
                    if (ticks / this.bitTime == this.fewShot)
                    {
                        data2 = (int)StopNoteVal.AllStop;
                    }
                    else if (ticks / this.bitTime == this.fewShot/2)
                    {
                        data2 = (int)StopNoteVal.MinimsStop;
                    }
                    else if (ticks / this.bitTime == this.fewShot / 4)
                    {
                        data2 = (int)StopNoteVal.CrotchetsCStop;
                    }
                    else if (ticks / this.bitTime == this.fewShot / 8)
                    {
                        data2 = (int)StopNoteVal.QuaversStop;
                    }

                    bar.AddNote(data2, command, ticks);
                    if (data2 == (int)StopNoteVal.AllStop)
                    {
                        this.barList.Push(bar);
                        bar = new NoteBar(this.bitTime);
                    }
                }
                bar.AddNote(data1, command, ticks);
                this.barList.Push(bar);
            }
            else
            {
                if (this.barList.Count > 0)
                {
                    var bar = this.barList.Pop();
                    bar.AddNote(data1, command, ticks);
                    this.barList.Push(bar);
                }
            }
        }
    }
}
