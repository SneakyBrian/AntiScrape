using System;
using System.Net;

namespace AntiScrape.Core.Interfaces
{
    public interface IAntiScrapeConfiguration
    {
        AntiScrapeAction Action { get; set; }
        string ClassNameSalt { get; set; }
        string ContentVirtualPath { get; set; }
        HttpStatusCode ErrorCode { get; set; }
        string HoneypotRelativeUrl { get; set; }
        int MaxDelay { get; set; }
        int MinDelay { get; set; }
    }
}
