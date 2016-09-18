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
    public class NoteLocation
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId _id { get; set; }

        private string lid = string.Empty;
        [BsonElement("lid")]
        public string LID
        {
            get { return lid; }
            set { lid = value; }
        }

        private int soundType = 0;
        [BsonElement("soundType")]
        public int SoundType
        {
            get { return soundType; }
            set { soundType = value; }
        }

        private int line = 0;
        [BsonElement("line")]
        public int Line
        {
            get { return line; }
            set { line = value; }
        }

        private int offset = 0;
        [BsonElement("offset")]
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }
    }
}
