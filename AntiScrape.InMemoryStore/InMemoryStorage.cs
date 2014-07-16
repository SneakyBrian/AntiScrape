using System;
using System.Web;
using System.Linq;
using AntiScrape.Core.Interfaces;
using System.Collections.Generic;
using System.Collections;

namespace AntiScrape.InMemoryStore
{
    public class InMemoryStorage : IDataStorage
    {
        public void StoreScrapingRequest(HttpRequest request)
        {
            var key = GetCacheKey("scraper", request);

            HttpContext.Current.Cache.Insert(key, Tuple.Create(request.UserHostAddress, request.UserAgent));
        }

        public bool IsKnownScraper(HttpRequest request)
        {
            var key = GetCacheKey("scraper", request);

            var cacheItem = HttpContext.Current.Cache.Get(key);

            return cacheItem != null;
        }

        private static string GetCacheKey(string type, HttpRequest request)
        {
            return string.Format("{0}-{1}-{2}", type, request.UserHostAddress, request.UserAgent);
        }

        public static IEnumerable<Tuple<string, string>> GetScrapers()
        {
            return HttpContext.Current.Cache.OfType<DictionaryEntry>().Select(de => de.Value).OfType<Tuple<string, string>>();
        }

        public void StoreValidRequest(HttpRequest request)
        {
            var key = GetCacheKey("valid", request);

            HttpContext.Current.Cache.Insert(key, Tuple.Create(request.UserHostAddress, request.UserAgent));
        }

        public bool IsKnownValidClient(HttpRequest request)
        {
            var key = GetCacheKey("valid", request);

            var cacheItem = HttpContext.Current.Cache.Get(key);

            return cacheItem != null;
        }
    }
}
