using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Entity
{
    public class Measure
    {
        private string mid = string.Empty;
        [BsonElement("mid")]
        public string MID
        {
            get { return mid; }
            set { mid = value; }
        }

        private int beats = 0;
        [BsonElement("beats")]
        public int Beats
        {
            get { return beats; }
            set { beats = value; }
        }

        private int beatType = 0;
        [BsonElement("beatType")]
        public int BeatType
        {
            get { return beatType; }
            set { beatType = value; }
        }

        private int barLine = 0;
        [BsonElement("barLine")]
        public int BarLine
        {
            get { return barLine; }
            set { barLine = value; }
        }

        private List<NoteBlock> blockList  = new List<NoteBlock>();
        [BsonElement("Blocks")]
        public List<NoteBlock> BlockList
        {
            get { return blockList; }
            set { blockList = value; }
        }

        private List<NoteBlock> lowBlockList = new List<NoteBlock>();
        [BsonElement("LowBlocks")]
        public List<NoteBlock> LowBlocks
        {
            get { return lowBlockList; }
            set { lowBlockList = value; }
        }
    }
}
