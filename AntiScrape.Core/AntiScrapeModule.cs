using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;
using System.Net;
using System.Web.UI;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using Microsoft.Practices.Unity;
using UnityConfiguration;
using AntiScrape.Core.IoC;
using AntiScrape.Core.Interfaces;

namespace AntiScrape.Core
{
    public class AntiScrapeModule : IHttpModule
    {
        private IDataStorage _storage;
        
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            Debug.WriteLine("AntiScrapeModule.Init");

            using (var container = new UnityContainer())
            {
                container.Configure(x =>
                {
                    x.AddRegistry<IoCRegistry>();
                });

                _storage = container.Resolve<IDataStorage>();
            }

            context.BeginRequest += OnBeginRequest;
            context.PostMapRequestHandler += OnPostMapRequestHandler;
        }

        void OnPostMapRequestHandler(object sender, EventArgs e)
        {
            Debug.WriteLine("AntiScrapeModule.OnPostMapRequestHandler");

            HttpContext context = ((HttpApplication)sender).Context;
            Page page = HttpContext.Current.CurrentHandler as Page;
            if (page != null)
            {
                page.PreRenderComplete += new EventHandler(OnPreRenderComplete);
            }
        }

        void OnPreRenderComplete(object sender, EventArgs e)
        {
            Debug.WriteLine("AntiScrapeModule.OnPreRenderComplete");

            Page page = (Page)sender;

            var linkUri = new Uri(AntiScrapeConfiguration.Settings.HoneypotRelativeUrl, UriKind.Relative);

            Debug.WriteLine("Link Uri: {0}", linkUri);

            page.Form.Controls.Add(new LiteralControl(string.Format("<a href=\"{0}\" style=\"display: none;\">{0}</a>", linkUri)));
        }

        void OnBeginRequest(object sender, EventArgs e)
        {
            Debug.WriteLine("AntiScrapeModule.OnBeginRequest");

            var application = sender as HttpApplication;

            if (application != null)
            {                
                if (application.Context.Request.Url.AbsolutePath.Contains(AntiScrapeConfiguration.Settings.HoneypotRelativeUrl))
                {
                    Debug.WriteLine("Request.Url.AbsolutePath '{0}' contains '{1}'", 
                                        application.Context.Request.Url.AbsolutePath, 
                                        AntiScrapeConfiguration.Settings.HoneypotRelativeUrl);

                    _storage.StoreScrapingRequest(application.Context.Request);
                }
                else if (!_storage.IsKnownScraper(application.Context.Request))
                    return;

                Debug.WriteLine("Known Scraper!");

                var content = "<html><head><title></title></head><body></body></html>";

                if(string.IsNullOrWhiteSpace(AntiScrapeConfiguration.Settings.ContentVirtualPath))
                {
                    var contentPath = application.Context.Server.MapPath(AntiScrapeConfiguration.Settings.ContentVirtualPath);

                    if (File.Exists(contentPath))
                    {
                        content = File.ReadAllText(contentPath);
                    }
                }

                var action = AntiScrapeConfiguration.Settings.Action;

                Debug.WriteLine("AntiScrapeConfiguration.Settings.Action = {0}", action);

                switch (action)
                {
                    case AntiScrapeAction.Delay:

                        var rng = new Random();

                        var delay = rng.Next(AntiScrapeConfiguration.Settings.MinDelay, AntiScrapeConfiguration.Settings.MaxDelay);

                        Debug.WriteLine("Waiting {0}", delay);

                        Thread.Sleep(delay);

                        application.Context.Response.StatusCode = (int)HttpStatusCode.OK;
                        application.Context.Response.Write(content);
                        application.Context.Response.End();

                        break;

                    case AntiScrapeAction.Error:

                        Debug.WriteLine("AntiScrapeConfiguration.Settings.ErrorCode = {0}", AntiScrapeConfiguration.Settings.ErrorCode);

                        application.Context.Response.StatusCode = (int)AntiScrapeConfiguration.Settings.ErrorCode;
                        application.Context.Response.SuppressContent = true;
                        application.Context.Response.End();

                        break;

                    case AntiScrapeAction.NoResponse:

                        application.Context.Response.StatusCode = (int)HttpStatusCode.OK;
                        application.Context.Response.SuppressContent = true;
                        application.Context.Response.End();

                        break;

                    case AntiScrapeAction.None:
                    default:
                        //do nothing
                        break;
                }
            }
        }
    }
}
