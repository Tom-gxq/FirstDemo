using System;
using System.Collections.Generic;
using System.Text;

namespace SequencerDemo.Note
{
    public class NoteBlock
    {
        private List<Note> noteList = new List<Note>();//小节中包含的音符数据
        private long id;//小节Id
        private double maxLine = 0;//离第三条线最远距离，用于确定符杆方向

        public List<Note> Notes
        {
            get
            {
                return this.noteList;
            }
            set
            {
                this.noteList = value;
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

        public long Count
        {
            get
            {
                return this.noteList?.Count??0;
            }
        }

        public int BlockTicks
        {
            get
            {
                int ticks = 0;
                this.noteList.ForEach(x => ticks += x.Ticks);
                return ticks;
            }
        }
        public void AddNote(Note note)
        {
            double baseLine = Math.Abs((note.Location.line+(note.Location.offset!=0?0.5:0) ) - 3);
            if(this.noteList.Count == 0)
            {
                this.maxLine = (note.Location.line + (note.Location.offset != 0 ? 0.5 : 0));
            }
            else if(Math.Abs(this.maxLine-3) < baseLine)
            {
                this.maxLine = (note.Location.line + (note.Location.offset != 0 ? 0.5 : 0));
            }
            this.noteList.Add(note);
            SetSymbolBarDirection();
        }

        protected void SetSymbolBarDirection()
        {
            //大于3,符杆向下
            if(this.maxLine > 3)
            {
                this.noteList.ForEach(x => x.CrochetType = CrochetType.Down);
            }
            else
            {
                this.noteList.ForEach(x => x.CrochetType = CrochetType.Up);
            }
        }
        public void RemoveNote(Note note)
        {
            this.noteList.Remove(note);
        }
        public void RemoveNote(int index)
        {
            this.noteList.RemoveAt(index);
        }

        public Note GetLast()
        {
            if(this.noteList.Count >0)
            {
                return this.noteList[(int)this.Count -1];
            }
            else
            {
                return null;
            }
        }

        public Note GetPreLast()
        {
            if (this.noteList.Count > 1)
            {
                return this.noteList[(int)this.Count - 2];
            }
            else
            {
                return null;
            }
        }

    }
}
