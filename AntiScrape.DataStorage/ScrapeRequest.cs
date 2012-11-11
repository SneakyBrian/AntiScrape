﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AntiScrape.DataStorage
{
    class ScrapeRequest
    {
        public string IP { get; set; }
        public string HostName { get; set; }
        public string UserAgent { get; set; }
        public string Referrer { get; set; }
        public string Params { get; set; }
        public string Headers { get; set; }

        public static ScrapeRequest FromHttpRequest(HttpRequest request)
        {
            return new ScrapeRequest
            {
                IP = request.UserHostAddress,
                HostName = request.UserHostName,
                UserAgent = request.UserAgent,
                Referrer = request.UrlReferrer.ToString(),
                Params = request.Params.AllKeys.Select(key => string.Format("{0}={1}", key, request.Params[key])).Aggregate((a, b) => string.Format("{0};{1}", a, b)),
                Headers = request.Headers.AllKeys.Select(key => string.Format("{0}={1}", key, request.Headers[key])).Aggregate((a, b) => string.Format("{0};{1}", a, b)),
            };
        }
    }
}
