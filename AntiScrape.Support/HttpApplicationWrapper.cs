using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AntiScrape.Support
{
    public class HttpApplicationWrapper : HttpApplicationBase
    {
        private readonly HttpApplication _application;

        public event EventHandler BeginRequest;
        public event EventHandler PostMapRequestHandler;

        public override HttpContext Context { get { return _application.Context; } }
        
        public HttpApplicationWrapper(HttpApplication application)
        {
            _application = application;

            _application.BeginRequest += application_BeginRequest;
            _application.PostMapRequestHandler += application_PostMapRequestHandler;            
        }

        void application_PostMapRequestHandler(object sender, EventArgs e)
        {
            var handler = PostMapRequestHandler;
            if (handler != null)
                handler(sender, e);
        }

        void application_BeginRequest(object sender, EventArgs e)
        {
            var handler = BeginRequest;
            if (handler != null)
                handler(sender, e);
        }
    }
}
