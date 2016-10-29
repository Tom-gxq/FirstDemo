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
    public class NoteBlock
    {
        private string nbid = string.Empty;
        [BsonElement("nbid")]
        public string NBID
        {
            get { return nbid; }
            set { nbid = value; }
        }

        private List<NoteGroup> noteList = new List<NoteGroup>();
        [BsonElement("Notes")]
        public List<NoteGroup> Notes
        {
            get { return noteList; }
            set { noteList = value; }
        }
    }
}
