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
    public class NoteGroup
    {
        private string ngid = string.Empty;
        [BsonElement("ngid")]
        public string NGID
        {
            get { return ngid; }
            set { ngid = value; }
        }

        private List<Note> noteList = new List<Note>();
        [BsonElement("noteList")]
        public List<Note> NoteList
        {
            get { return noteList; }
            set { noteList = value; }
        }

        
    }
}
