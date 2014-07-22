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
            StoreRequest(request, ClientType.Scraper);
        }

        public void StoreValidRequest(HttpRequest request)
        {
            StoreRequest(request, ClientType.Legitimate);
        }

        public bool IsKnownScraper(HttpRequest request)
        {
            return IsKnownType(request, ClientType.Scraper);
        }

        public bool IsKnownValidClient(HttpRequest request)
        {
            return IsKnownType(request, ClientType.Legitimate);
        }

        public IEnumerable<dynamic> GetScrapers(int count)
        {
            return GetRequestsOfType(count, ClientType.Scraper);
        }

        public IEnumerable<dynamic> GetValidUsers(int count)
        {
            return GetRequestsOfType(count, ClientType.Legitimate);
        }

        private void StoreRequest(HttpRequest request, ClientType clientType)
        {
            //if we've already got this recorded
            if (IsKnownType(request, clientType))
            {
                //bail
                return;
            }

            using (var db = GetDB())
            {
                db.Execute("insert into ClientRequests ([IP], [HostName], [UserAgent], [Referrer], [Params], [Headers], [Timestamp], [UserType]) values (@IP, @HostName, @UserAgent, @Referrer, @Params, @Headers, @Timestamp, @ClientType)",
                    ClientRequest.FromHttpRequest(request, clientType));
            }
        }

        private bool IsKnownType(HttpRequest request, ClientType clientType)
        {
            using (var db = GetDB())
            {
                var count = db.Query<int>(@"select count([IP]) from ClientRequests where IP = @IP and UserAgent = @UserAgent and UserType = @ClientType", ClientRequest.FromHttpRequest(request, clientType)).Single();

                return count > 0;
            }
        }

        private IEnumerable<dynamic> GetRequestsOfType(int count, ClientType clientType)
        {
            using (var db = GetDB())
            {
                return db.Query("select top (@Count) * from ClientRequests where UserType = @ClientType order by [Timestamp] desc", new { Count = count, ClientType = clientType });
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
