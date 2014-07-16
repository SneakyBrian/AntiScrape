using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AntiScrape.Core.Interfaces
{
    public interface IDataStorage
    {
        void StoreScrapingRequest(HttpRequest request);
        void StoreValidRequest(HttpRequest request);

        bool IsKnownScraper(HttpRequest request);
        bool IsKnownValidClient(HttpRequest request);
    }
}
