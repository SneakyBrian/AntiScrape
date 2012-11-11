using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace AntiScrape.DataStorage
{
    class ASDB : Database<ASDB>
    {
        public Table<ScrapeRequest> ScrapeRequests { get; set; }
    }
}
