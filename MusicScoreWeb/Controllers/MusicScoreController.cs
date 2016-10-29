using Mongo.Business;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicScoreWeb.Controllers
{
    public class MusicScoreController : ApiController
    {
        private JObject JsonResult = new JObject();
        // GET: api/MusicScore/5
        public JObject Get(string sid)
        {            
            var score = MusicScoreBusiness.GetMusicScoreByID(sid);
            string response = JsonConvert.SerializeObject(score);
            JsonResult.Add("score", JsonConvert.DeserializeObject<JObject>(response));
            return JsonResult;
        }

        public JObject GetAll(int pageIndex, int pageSize)
        {
            var scoreList = MusicScoreBusiness.GetMusicScores(pageIndex, pageSize);
            JArray jsonList = new JArray();
            foreach(var item in scoreList)
            {
                JObject jobj = new JObject();
                jobj.Add("SID", item.SID);
                jobj.Add("Name", item.Name);
                jsonList.Add(jobj);
            }
            JsonResult.Add("scores", jsonList);
            return JsonResult;
        }

        public JObject GetMusicScoreCount()
        {
            var count = MusicScoreBusiness.GetMusicScoreCount();            
            JsonResult.Add("result", count);
            return JsonResult;
        }


    }
}
