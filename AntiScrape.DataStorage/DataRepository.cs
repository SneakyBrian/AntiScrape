using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace AntiScrape.DataStorage
{
    class DataRepository : Database<DataRepository>
    {
        public Table<ClientRequest> ScrapeRequests { get; set; }
    }
}
