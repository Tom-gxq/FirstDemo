using SequencerDemo.Note;
using System;
using System.Collections.Generic;
using System.Text;

namespace SequencerDemo.Note
{
    public enum BarLineType
    {
        None=0,
        /// <summary>
        /// 单条实线
        /// </summary>
        Regular,
        /// <summary>
        /// 单条点线
        /// </summary>
        Dotted,
        /// <summary>
        /// 单条虚线
        /// </summary>
        Dashed,
        /// <summary>
        /// 单条粗线
        /// </summary>
        Heavy,
        /// <summary>
        /// 两条实线
        /// </summary>
        DobuleLight,
        /// <summary>
        /// 一条实线一条粗线
        /// </summary>
        LightHeavy,
        /// <summary>
        /// 一条粗线一条实线
        /// </summary>
        HeavyLight,
        /// <summary>
        /// 两条粗线
        /// </summary>
        DobuleHeavy,
        /// <summary>
        /// -------+---
        /// -----------
        /// -----------
        /// -----------
        /// -----------
        /// </summary>
        Tick,
        /// <summary>
        /// -----------
        /// -------+---
        /// -------+---
        /// -------+---
        /// -----------
        /// </summary>
        Short
    }
    public class Measure
    {
        private List<NoteBlock> blockList = new List<NoteBlock>();//小节中包含的音符数据
        private List<NoteBlock> lowBlockList = new List<NoteBlock>();//小节中包含的音符数据
        private long id;//小节Id
        public int Beats { get; set; }
        public int Beat_Type { get; set; }

        public List<NoteBlock> Blocks
        {
            get
            {
                return this.blockList;
            }
            set
            {
                if (value != null)
                {
                    value.ForEach(x => this.blockList.Add(x));
                }
            }
        }

        public List<NoteBlock> LowBlocks
        {
            get
            {
                return this.lowBlockList;
            }
            set
            {
                if (value != null)
                {
                    value.ForEach(x => this.lowBlockList.Add(x));
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

        public void AddNote(SequencerDemo.Note.Note note)
        {
            if (note.Staff == 1)
            {
                NoteBlock lastBlock = null;
                if ((this.blockList.Count == 0) || (note.Beams.Count == 0))
                {
                    if (this.blockList.Count > 0)
                    {
                        lastBlock = this.blockList[this.blockList.Count - 1];
                        if (lastBlock.IsContianerDefaultX(note.DefaultX))
                        {
                            lastBlock.AddNote(note);
                        }
                        else
                        {
                            NoteBlock block = new NoteBlock();
                            block.AddNote(note);
                            this.blockList.Add(block);
                        }
                    }
                    else
                    {
                        NoteBlock block = new NoteBlock();
                        block.AddNote(note);
                        this.blockList.Add(block);
                    }
                }
                else
                {
                    var beamFlag = note.Beams["1"];
                    
                    if (beamFlag == "begin")
                    {
                        lastBlock = new NoteBlock();
                        this.blockList.Add(lastBlock);
                    }
                    else
                    {
                        lastBlock = this.blockList[this.blockList.Count - 1];
                    }
                    lastBlock.AddNote(note);
                }
            }
            else
            {
                if ((this.lowBlockList.Count == 0) || (note.Beams.Count == 0))
                {
                    NoteBlock block = new NoteBlock();
                    block.AddNote(note);
                    this.lowBlockList.Add(block);
                }
                else
                {
                    var beamFlag = note.Beams["1"];
                    NoteBlock lastBlock = null;
                    if (beamFlag == "begin")
                    {
                        lastBlock = new NoteBlock();
                        this.lowBlockList.Add(lastBlock);
                    }
                    else
                    {
                        lastBlock = this.lowBlockList[this.lowBlockList.Count - 1];
                    }
                    lastBlock.AddNote(note);
                }
            }    
        }

        

        public long NoteNum
        {
            get
            {
                long cnt = 0;
                foreach (var item in this.blockList)
                {
                    cnt += item.NoteCount;
                }
                return cnt;
            }
        }

        public BarLineType BarLine { get; set; }
    }
}
