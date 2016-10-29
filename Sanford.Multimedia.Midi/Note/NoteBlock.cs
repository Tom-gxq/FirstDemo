using System;
using System.Collections.Generic;
using System.Text;

namespace SequencerDemo.Note
{
    public class NoteBlock : IComparable
    {
        private List<NoteGroup> noteList = new List<NoteGroup>();//小节中包含的音符数据
        private long id;//小节Id
        private double maxLine = 0;//离第三条线最远距离，用于确定符杆方向

        public string NBID { get; set; }
        public List<NoteGroup> Notes
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

        public long NoteCount
        {
            get
            {
                int cnt = 0;
                foreach(var group in this.noteList)
                {
                    foreach (var item in group.Notes)
                    {
                        switch (item.NoteType)
                        {
                            case NoteType.Whole:
                            case NoteType.AllStop:
                                cnt += 4;
                                break;
                            case NoteType.Minims:
                            case NoteType.MinimsStop:
                                cnt += 2;
                                break;
                            default:
                                cnt++;
                                break;
                        }
                    }
                }
                return cnt;
            }
        }

        public int BlockTicks
        {
            get
            {
                int ticks = 0;
                foreach (var item in this.noteList)
                {
                    item.Notes.ForEach(x => ticks += x.Ticks);
                }
                return ticks;
            }
        }
        public void AddNote(Note note)
        {
            //double baseLine = Math.Abs((note.Location.line+(note.Location.offset!=0?0.5:0) ) - 3);
            NoteGroup group = null;
            foreach (var item in this.noteList)
            {
                //var list = item.Notes.FindAll(x => { return (x.NoteType < NoteType.AllStop); });
                //if (list.Count == 0)
                //{
                //    this.maxLine = (note.Location.line + (note.Location.offset != 0 ? 0.5 : 0));
                //}
                //else if (Math.Abs(this.maxLine - 3) < baseLine)
                //{
                //    this.maxLine = (note.Location.line + (note.Location.offset != 0 ? 0.5 : 0));
                //}
                if (item.DefaultX == note.DefaultX)
                {
                    group = item;
                    break;
                }
                //SetSymbolBarDirection();
            }
            if (group != null)
            {
                group.AddNote(note);
            }
            else
            {
                group = new NoteGroup();
                this.noteList.Add(group);
                group.AddNote(note);
            }
        }

        public bool IsContianerDefaultX(int defaultX)
        {
            NoteGroup obj = null;
            foreach(var item in this.noteList)
            {
                var list  = item.Notes.FindAll(x => x.DefaultX == defaultX);
                if(list.Count > 0)
                {
                    obj = item;
                }
            }
            return obj != null;
        }

        protected void SetSymbolBarDirection()
        {
            //大于3,符杆向下
            if(this.maxLine > 3)
            {
                foreach (var item in this.noteList)
                {
                    item.Notes.ForEach(x => x.CrochetType = CrochetType.Down);
                }
            }
            else
            {
                foreach (var item in this.noteList)
                {
                    item.Notes.ForEach(x => x.CrochetType = CrochetType.Up);
                }
            }
        }
       

        public NoteGroup GetLast()
        {
            if(this.noteList.Count >0)
            {                
                return this.noteList[(int)this.Count - 1];
            }
            else
            {
                return null;
            }
        }

        public NoteGroup GetPreLast()
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

        public int CompareTo(object obj)
        {
            int res = 0;
            try
            {
                if(obj == null)
                {
                    return 1;
                }
                NoteBlock sObj = (NoteBlock)obj;
                if ((this.Notes == null)||(this.Notes.Count <= 0)|| this.Notes[0] == null)
                {
                    return -1;
                }
                if ((sObj.Notes == null) || (sObj.Notes.Count <= 0) || sObj.Notes[0] == null)
                {
                    return 1;
                }
                
                if (this.Notes[0].DefaultX > sObj.Notes[0].DefaultX)
                {
                    res = 1;
                }
                else if (this.Notes[0].DefaultX < sObj.Notes[0].DefaultX)
                {
                    res = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("比较异常", ex.InnerException);
            }
            return res;
        }

    }
}
