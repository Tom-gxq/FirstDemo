using Sanford.Multimedia.Midi.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace SequencerDemo.Note
{
    public class NoteGroup
    {
        private List<Note> noteList = new List<Note>();//小节中包含的音符数据

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

        public int DefaultX
        {
            get
            {
                if (this.noteList.Count > 0)
                {
                    return this.noteList[0].DefaultX;
                }
                else
                {
                    return 0;
                }
            }
        }

        public NoteType NoteType
        {
            get
            {
                if (this.noteList.Count > 0)
                {
                    return this.noteList[0].NoteType;
                }
                else
                {
                    return  NoteType.Whole;
                }
            }
        }

        public CrochetType CrochetType
        {
            get
            {
                if (this.noteList.Count > 0)
                {
                    return this.noteList[0].CrochetType;
                }
                else
                {
                    return CrochetType.Down;
                }
            }
        }

        public NoteLocation Location
        {
            get
            {
                return this.noteList[0].Location;
            }
        }

        public void AddNote(Note note)
        {
            //if (this.Notes.Count > 0)
            //{
            //    var firstNote = this.Notes[0];
            //    if (note.CrochetType != firstNote.CrochetType)
            //    {
            //        note.CrochetType = firstNote.CrochetType;
            //    }
            //}
            this.Notes.Add(note);
        }

        
    }
}
