using AntiScrape.Core.Interfaces;

namespace AntiScrape.Tests
{
    public class TestDataStorage : IDataStorage
    {
        public void StoreScrapingRequest(System.Web.HttpRequest request)
        {
            
        }

        public bool IsKnownScraper(System.Web.HttpRequest request)
        {
            return true;
        }

        public void StoreValidRequest(System.Web.HttpRequest request)
        {

        }

        public bool IsKnownValidClient(System.Web.HttpRequest request)
        {
            return true;
        }
    }
}
