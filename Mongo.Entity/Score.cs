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
    public class Score
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId _id { get; set; }
        
        private string sid = string.Empty;
        [BsonElement("sid")]
        public string SID
        {
            get { return sid; }
            set { sid = value; }
        }

        private string name = string.Empty;
        [BsonElement("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string author = string.Empty;
        [BsonElement("author")]
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        private int fewShot = 0;
        [BsonElement("fewShot")]
        public int FewShot
        {
            get { return fewShot; }
            set { fewShot = value; }
        }

        private int oneShot = 0;
        [BsonElement("oneShot")]
        public int OneShot
        {
            get { return oneShot; }
            set { oneShot = value; }
        }

        private int oneShotNote = 0;
        [BsonElement("oneShotNote")]
        public int OneShotNote
        {
            get { return oneShotNote; }
            set { oneShotNote = value; }
        }

        private DateTime createTime = DateTime.Now;
        [BsonElement("ctime")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        private DateTime updateTime = DateTime.Now;
        [BsonElement("utime")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }

        private DateTime lastTime = DateTime.Now;
        [BsonElement("ltime")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime LastTime
        {
            get { return lastTime; }
            set { lastTime = value; }
        }

        private List<Measure> measureList = new List<Measure>();
        [BsonElement("measureList")]
        public List<Measure> MeasureList
        {
            get { return measureList; }
            set { measureList = value; }
        }
    }
}
