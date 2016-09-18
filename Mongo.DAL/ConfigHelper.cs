using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.DAL
{
    public class ConfigHelper
    {
        private static string mongoMdbaseUrl;
        /// <summary>
        /// MongoDB 群组
        /// </summary>
        public static string MongoMdbaseUrl
        {
            get
            {
                if (string.IsNullOrEmpty(mongoMdbaseUrl))
                {
                    mongoMdbaseUrl = ConfigurationManager.AppSettings["MD.MongoGroup.Servers"];
                }
                return mongoMdbaseUrl;
            }
        }
    }
}
