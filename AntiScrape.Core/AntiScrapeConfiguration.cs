using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;

namespace AntiScrape.Core
{
    public class AntiScrapeConfiguration : ConfigurationSection
    {

        public static AntiScrapeConfiguration Settings { get; private set; }

        static AntiScrapeConfiguration()
        {
            Settings = ConfigurationManager.GetSection("AntiScrape") as AntiScrapeConfiguration;
        }

        [ConfigurationProperty("honeypotRelativeUrl", IsRequired = true)]
        public string HoneypotRelativeUrl
        {
            get { return (string)this["honeypotRelativeUrl"]; }
            set { this["honeypotRelativeUrl"] = value; }
        }

        [ConfigurationProperty("action", IsRequired = true)]
        public AntiScrapeAction Action
        {
            get { return (AntiScrapeAction)this["action"]; }
            set { this["action"] = value; }
        }

        [ConfigurationProperty("minDelay", DefaultValue = 500, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 30000)]
        public int MinDelay
        {
            get { return (int)this["minDelay"]; }
            set { this["minDelay"] = value; }
        }

        [ConfigurationProperty("maxDelay", DefaultValue = 30000, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 60000)]
        public int MaxDelay
        {
            get { return (int)this["maxDelay"]; }
            set { this["maxDelay"] = value; }
        }

        [ConfigurationProperty("contentVirtualPath", DefaultValue = "", IsRequired = false)]
        public string ContentVirtualPath
        {
            get { return (string)this["contentVirtualPath"]; }
            set { this["contentVirtualPath"] = value; }
        }

        [ConfigurationProperty("errorCode", DefaultValue = HttpStatusCode.NotFound, IsRequired = false)]
        public HttpStatusCode ErrorCode
        {
            get { return (HttpStatusCode)this["errorCode"]; }
            set { this["errorCode"] = value; }
        }

        [ConfigurationProperty("classNameSalt", DefaultValue = "anti-scrape-salt", IsRequired = false)]
        public string ClassNameSalt
        {
            get { return (string)this["classNameSalt"]; }
            set { this["classNameSalt"] = value; }
        }

    }
}
