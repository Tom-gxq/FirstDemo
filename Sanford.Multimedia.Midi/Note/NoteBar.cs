using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace SequencerDemo.Note
{
    /// <summary>
    /// 小节数据
    /// </summary>
    public class NoteBar
    {
        private Stack<NoteBlock> noteList = new Stack<NoteBlock>();//小节中包含的音符数据
        private long id;//小节Id
        private int bitTime;//一拍的时间（单位tick）
        private Score score = null;

        public NoteBar(int bitTime, Score score)
        {
            this.bitTime = bitTime;
            this.score = score;
        }
        public List<NoteBlock> Notes
        {
            get
            {
                return new List<NoteBlock>(this.noteList.ToArray());
            }
            set
            {
                if (value != null)
                {
                    value.ForEach(x => this.noteList.Push(x));
                }
            }
        }

        public long Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public int BarTicks
        {
            get
            {
                int ticks = 0;
                new List<NoteBlock>(this.noteList.ToArray()).ForEach(x => ticks += x.BlockTicks);
                return ticks;
            }
        }

        public long NoteNum
        {
            get
            {
                long cnt = 0;
                foreach(var item in this.noteList)
                {
                    cnt += item.NoteCount;
                }
                return cnt;
            }
        }

        
        public void AddNote(int data1, ChannelCommand command, int ticks)
        {
            //if (command == ChannelCommand.NoteOn)
            //{
            //    NoteBlock block = null;
            //    SequencerDemo.Note.Note preNote = null;
            //    SequencerDemo.Note.Note prePreNote = null;
            //    if (this.noteList.Count > 0)
            //    {
            //        //向前寻找前两个音符，为判断黑键升降号
            //        block = this.noteList.Pop();
            //        if(block.Count >0)
            //        {
            //            preNote = block.GetLast();
            //        }
            //        if (block.Count > 1)
            //        {
            //            prePreNote = block.GetPreLast();
            //        }
            //        else if(noteList.Count > 0)
            //        {
            //           var temBlock = this.noteList.Pop();
            //            prePreNote = temBlock.GetLast();
            //            this.noteList.Push(temBlock);
            //        }


            //        if (block.BlockTicks >= this.bitTime)
            //        {
            //            this.noteList.Push(block);
            //            block = new NoteBlock();
            //        }
            //    }
            //    else
            //    {
            //        //每小节第一个block
            //        block = new NoteBlock();

            //        //向前寻找前两个音符，为判断黑键升降号
            //        var bar = this.score.GetLastBar(data1);
            //        if((bar != null)&&(bar.Notes != null)&&(bar.Notes.Count > 0))
            //        {
            //            var lastBlock = bar.Notes[0];
            //            preNote = lastBlock.GetLast();
            //            if (lastBlock.Count > 1)
            //            {
            //                prePreNote = lastBlock.GetPreLast();
            //            }
            //            else if (bar.Notes.Count > 1)
            //            {
            //                var temBlock = bar.Notes[1];
            //                prePreNote = temBlock.GetLast();
            //            }
            //        }
            //    }
            //    var note = NoteScoreTable.Instance.GetNoteLocation(data1);
            //    if (note != null)
            //    {
            //        note.Ticks = ticks;                   

            //        bool ret = NoteScoreTable.Instance.IsBlackNote(preNote);
            //        if((ret)&&(prePreNote != null)&&(preNote != null))
            //        {
            //            //对于黑键的位置，要判断前一个和后一个的位置差，
            //            //如果位置的落差大于1，那么黑键是本身的位置，加上降号符
            //            //如果位置的落差小于1，且后一个相对于前一个是升高，那么黑键是前一个的位置，加上升号符
            //            //后一个相对于前一个是降低，那么黑键是前一个的位置，加上降号符
            //            double lineDiff = (note.Location.line+ note.Location.offset*-0.1) - (prePreNote.Location.line+ prePreNote.Location.offset * -0.1);
            //            if (Math.Abs(lineDiff) < 1)
            //            {
            //                preNote.Location = prePreNote.Location;
            //                if (lineDiff > 0.1)
            //                {                                
            //                    preNote.Lift = NoteLift.Sharp;
            //                }
            //                else
            //                {
            //                    preNote.Lift = NoteLift.Flat;
            //                }
                        
            //            }
            //            else
            //            {
            //                preNote.Location = new NoteLocation() { line = preNote.Location.line, soundType = preNote.Location.soundType, offset = 0 };
            //                preNote.Lift = NoteLift.Flat;
            //            }
            //        }

            //        block.AddNote(note);
            //    }

            //    this.noteList.Push(block);
            //}
            //else if (command == ChannelCommand.NoteOff)
            //{
            //    if (noteList.Count > 0)
            //    {
            //        var block = this.noteList.Pop();
            //        if (block.Count > 0)
            //        {
            //            var note = block.GetLast();
            //            if (ticks <=this.bitTime)
            //            {
            //                int noteTypeVal = (int)Math.Floor(((double)this.bitTime / ticks));
            //                switch (noteTypeVal)
            //                {
            //                    case 1:
            //                        note.NoteType = NoteType.CrotchetsC;
            //                        break;
            //                    case 2:
            //                        note.NoteType = NoteType.Quavers;
            //                        break;
            //                    case 4:
            //                        note.NoteType = NoteType.Demiquaver;
            //                        break;
            //                    case 8:
            //                        note.NoteType = NoteType.Demisemiquaver;
            //                        break;
            //                }
            //            }
            //            else
            //            {
            //                int value = ticks / this.bitTime;
            //                if(value == 4)
            //                {
            //                    note.NoteType = NoteType.Whole;
            //                }
            //                else if(value == 2)
            //                {
            //                    note.NoteType = NoteType.Minims;
            //                }
            //            }
            //            note.Ticks = ticks;                        
            //        }
            //        this.noteList.Push(block);
            //    }
            //}
         }
    }
}
