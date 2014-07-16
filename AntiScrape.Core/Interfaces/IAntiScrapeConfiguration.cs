using System;
using System.Net;

namespace AntiScrape.Core.Interfaces
{
    public interface IAntiScrapeConfiguration
    {
        AntiScrapeAction Action { get; }
        string ClassNameSalt { get; }
        string ContentVirtualPath { get; }
        HttpStatusCode ErrorCode { get; }
        string HoneypotRelativeUrl { get; }
        string ReverseHoneypotRelativeUrl { get; }
        int MaxDelay { get; }
        int MinDelay { get; }
    }
}
