using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntiScrape.Core.Interfaces;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Data.Common;

namespace AntiScrape.DataStorage
{
    public class SQLDataStorage : IDataStorage
    {
        public void StoreScrapingRequest(HttpRequest request)
        {
            using (var db = GetDB())
            {
                db.Execute("insert into ScrapeRequests ([IP], [HostName], [UserAgent], [Referrer], [Params], [Headers], [Timestamp]) values (@IP, @HostName, @UserAgent, @Referrer, @Params, @Headers, @Timestamp)", 
                    ScrapeRequest.FromHttpRequest(request));
            }
        }

        public bool IsKnownScraper(HttpRequest request)
        {
            using (var db = GetDB())
            {
                var count = db.Query<int>(@"select count([IP]) from ScrapeRequests where IP = @IP and UserAgent = @UserAgent", ScrapeRequest.FromHttpRequest(request)).Single();

                return count > 0;
            }
        }

        public IEnumerable<dynamic> GetScrapers()
        {
            using (var db = GetDB())
            {
                return db.Query("select * from ScrapeRequests");
            }
        }

        private DataRepository GetDB()
        {
            return DataRepository.Init(GetConnection(), commandTimeout: 0);
        }

        private DbConnection GetConnection()
        {
            var dbProviderFactory = DbProviderFactories.GetFactory("AntiScrape.DataStorage.DBProvider");

            var cn = dbProviderFactory.CreateConnection();

            cn.ConnectionString = ConfigurationManager.ConnectionStrings["AntiScrape.DataStorage.SQLDataStorage"].ConnectionString;

            cn.Open();

            return cn;
        }
    }
}
