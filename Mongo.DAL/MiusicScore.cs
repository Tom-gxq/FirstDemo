using MD.DAL.Mongo;
using Mongo.Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.DAL
{
    public class MusicScore : MongoBaseProvider<Score>
    {
        private static string conUrl = ConfigHelper.MongoMdbaseUrl;
        public MusicScore()
            : base(conUrl)
        {
            collectionName = "Scores";
            databaseName = "MusicScore";
        }

        public string AddMusicScore(Score insertObj)
        {
            string newGroupId = Guid.NewGuid().ToString().ToLower();
            insertObj.SID = newGroupId;
            InsertOneModel<Score> model = new InsertOneModel<Score>(insertObj);
            var inserResult = BulkWrite(new[] { model });
            if (inserResult != null && inserResult.InsertedCount == 1)
                return newGroupId;
            return string.Empty;
        }

        public Score GetMusicScoreByID(string sid)
        {
            var query = new QueryDocument();
            query.Add("sid", sid);
            return this.FindOne(query);
        }

        public List<Score> GetMusicScores(int pageIndex, int pageSize)
        {
            var query = new QueryDocument();
            

            var searchOptions = new FindOptions<Score, Score>()
            {
                Limit = pageSize,
                Skip = (pageIndex - 1) * pageSize,
                Projection = new BsonDocument() {
                    {"name", 1},
                    {"sid",1}
                },
                Sort = new BsonDocument("ctime", -1)
            };
            return this.Find(query, searchOptions);
        }

        public int GetMusicScoreCount()
        {
            var pipeline = new[]{
                new BsonDocument("$group",new BsonDocument(){
                    {"_id",1},
                    {"total",new BsonDocument("$sum",1)}
                })
            };
            List<BsonDocument> result = this.Aggregate<BsonDocument>(pipeline);
            if (result.Count > 0)
                return int.Parse(result.First()["total"].ToString());
            return 0;
        }
    }
}
