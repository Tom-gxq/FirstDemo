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
    public class Note
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId _id { get; set; }

        private string nid = string.Empty;
        [BsonElement("nid")]
        public string NID
        {
            get { return nid; }
            set { nid = value; }
        }

        private int lift = 0;
        [BsonElement("lift")]
        public int Lift
        {
            get { return lift; }
            set { lift = value; }
        }

        private string name = string.Empty;
        [BsonElement("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int data = 0;
        [BsonElement("data")]
        public int Data
        {
            get { return data; }
            set { data = value; }
        }

        private int noteType = 0;
        [BsonElement("noteType")]
        public int NoteType
        {
            get { return noteType; }
            set { noteType = value; }
        }

        private int crochetType = 0;
        [BsonElement("crochetType")]
        public int CrochetType
        {
            get { return crochetType; }
            set { crochetType = value; }
        }

        private NoteLocation noteLocation = new NoteLocation();
        [BsonElement("noteLocation")]
        public NoteLocation NoteLocation
        {
            get { return noteLocation; }
            set { noteLocation = value; }
        }

        private int defaultX = 0;
        [BsonElement("defaultX")]
        public int DefaultX
        {
            get { return defaultX; }
            set { defaultX = value; }
        }

        private int defaultY = 0;
        [BsonElement("defaultY")]
        public int DefaultY
        {
            get { return defaultY; }
            set { defaultY = value; }
        }

        private int staff = 0;
        [BsonElement("staff")]
        public int Staff
        {
            get { return staff; }
            set { staff = value; }
        }

        private Dictionary<string, string> beams = new Dictionary<string, string>();
        [BsonElement("beams")]
        public Dictionary<string, string> Beams
        {
            get { return beams; }
            set { beams = value; }
        }

        private int voice = 0;
        [BsonElement("voice")]
        public int Voice
        {
            get { return voice; }
            set { voice = value; }
        }

        private int octave = 0;
        [BsonElement("octave")]
        public int Octave
        {
            get { return octave; }
            set { octave = value; }
        }
    }
}
