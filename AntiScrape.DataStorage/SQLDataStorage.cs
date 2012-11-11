using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntiScrape.Core.Interfaces;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace AntiScrape.DataStorage
{
    

    public class SQLDataStorage : IDataStorage
    {
        public void StoreScrapingRequest(HttpRequest request)
        {
            using (var db = GetDB())
            {
                db.ScrapeRequests.Insert(ScrapeRequest.FromHttpRequest(request));
            }
        }

        public bool IsKnownScraper(HttpRequest request)
        {
            using (var db = GetDB())
            {
                var count = db.Query<int>(@"select count(IP) from ScrapeRequests where IP = @IP and UserAgent = @UserAgent", ScrapeRequest.FromHttpRequest(request)).Single();

                return count > 0;
            }
        }

        private ASDB GetDB()
        {
            return ASDB.Init(GetConnection(), commandTimeout: 100);
        }

        private SqlConnection GetConnection()
        {
            var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["AntiScrape.DataStorage.SQLDataStorage"].ConnectionString);
            cn.Open();
            return cn;
        }
    }
}
