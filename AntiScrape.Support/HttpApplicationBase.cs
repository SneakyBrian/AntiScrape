using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AntiScrape.Support
{
    public abstract class HttpApplicationBase
    {
        public event EventHandler BeginRequest;
        public event EventHandler PostMapRequestHandler;

        public abstract HttpContext Context { get; }
    }
}
