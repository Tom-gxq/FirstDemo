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

        public NoteBar(int bitTime)
        {
            this.bitTime = bitTime;
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

        
        public void AddNote(int data1, ChannelCommand command, int ticks)
        {
            if (command == ChannelCommand.NoteOn)
            {
                NoteBlock block = null;
                if (this.noteList.Count > 0)
                {
                    block = this.noteList.Pop();
                    if (block.BlockTicks >= this.bitTime)
                    {
                        this.noteList.Push(block);
                        block = new NoteBlock();
                    }
                }
                else
                {
                    block = new NoteBlock();
                }
                var note = NoteScoreTable.Instance.GetNoteLocation(data1);
                if (note != null)
                {
                    note.Ticks = ticks;
                    block.AddNote(note);
                }

                this.noteList.Push(block);
            }
            else if (command == ChannelCommand.NoteOff)
            {
                if (noteList.Count > 0)
                {
                    var block = this.noteList.Pop();
                    if (block.Count > 0)
                    {
                        var note = block.GetLast();
                        int noteTypeVal = (int)Math.Floor(((double)this.bitTime / ticks));
                        switch(noteTypeVal)
                        {
                            case 1:
                                note.NoteType = NoteType.CrotchetsC;
                                break;
                            case 2:
                                note.NoteType = NoteType.Quavers;
                                break;
                            case 4:
                                note.NoteType = NoteType.Demiquaver;
                                break;
                            case 8:
                                note.NoteType = NoteType.Demisemiquaver;
                                break;
                        }
                        note.Ticks = ticks;                        
                    }
                    this.noteList.Push(block);
                }
            }
         }
    }
}
