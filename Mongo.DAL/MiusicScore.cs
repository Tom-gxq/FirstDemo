using MD.DAL.Mongo;
using Mongo.Entity;
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

    }
}
