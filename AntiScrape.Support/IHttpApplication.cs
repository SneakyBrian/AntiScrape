using System;
using System.Web;

namespace AntiScrape.Support
{
    public interface IHttpApplication
    {
        event EventHandler BeginRequest;
        event EventHandler PostMapRequestHandler;

        HttpContext Context { get; }
    }
}
