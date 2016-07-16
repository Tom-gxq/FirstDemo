﻿using Sanford.Multimedia.Midi.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace SequencerDemo.Note
{
    public enum NoteType
    {
        /// <summary>
        /// 全音符
        /// </summary>
        Semibreve = 0,
        /// <summary>
        /// 二分音符
        /// </summary>
        Minims,
        /// <summary>
        /// 四分音符
        /// </summary>
        CrotchetsC,
        /// <summary>
        /// 八分音符
        /// </summary>
        Quavers,
        /// <summary>
        /// 十六分音符
        /// </summary>
        Demiquaver,
        /// <summary>
        /// 三十二分音符
        /// </summary>
        Demisemiquaver,
        /// <summary>
        /// 全休止符
        /// </summary>
        AllStop,
        /// <summary>
        /// 二分休止符
        /// </summary>
        MinimsStop,
        /// <summary>
        /// 四分休止符
        /// </summary>
        CrotchetsCStop,
        /// <summary>
        /// 八分休止符
        /// </summary>
        QuaversStop
    }

    public enum CrochetType
    {
        Up,
        Down
    }
    /// <summary>
    /// 音符数据
    /// </summary>
    public class Note : ICloneable
    {
        private long id;//音符ID
        
        private string name;//音符名
        private NoteType noteType;//音符类型
        private CrochetType noteCrochetType;//符杆方向
        private NoteLocation location;//音符位置
        private int ticks;

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
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        //音符值
        public int Data
        {
            get; set;
        }
        public NoteType NoteType
        {
            get
            {
                return this.noteType;
            }
            set
            {
                noteType = value;
            }
        }

        public CrochetType CrochetType
        {
            get
            {
                return this.noteCrochetType;
            }
            set
            {
                this.noteCrochetType = value;
            }
        }

        public NoteLocation Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
            }
        }

        public NoteLocation EndLocation { get; set; }

        public int Ticks
        {
            get
            {
                return this.ticks;
            }
            set
            {
                this.ticks = value;
            }
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}