using AntiScrape.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AntiScrape.Support;
using AntiScrape.Core.Interfaces;
using Moq;
using System.Net;
using System.Web;
using System.IO;

namespace AntiScrape.Tests
{
    
    
    /// <summary>
    ///This is a test class for AntiScrapeModuleTest and is intended
    ///to contain all AntiScrapeModuleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AntiScrapeModuleTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Init
        ///</summary>
        [TestMethod()]
        public void InitTest()
        {
            var mockConfig = GetMockConfiguration();
            var mockApplication = GetMockApplication();

            var target = new AntiScrapeModule(mockConfig.Object, obj => mockApplication.Object);

            target.Init(mockApplication.Object);          
        }

        [TestMethod()]
        public void PipelineEventsTest()
        {
            var mockConfig = GetMockConfiguration();
            var mockApplication = GetMockApplication();

            var target = new AntiScrapeModule(mockConfig.Object, obj => mockApplication.Object);

            target.Init(mockApplication.Object);

            mockApplication.Raise(x => x.BeginRequest += null, EventArgs.Empty);
            mockApplication.Raise(x => x.PostMapRequestHandler += null, EventArgs.Empty);
        }


        #region Test Helper Methods


        private Mock<IAntiScrapeConfiguration> GetMockConfiguration()
        {
            var mockSettings = new Mock<IAntiScrapeConfiguration>();

            mockSettings.SetupGet(x => x.Action).Returns(AntiScrapeAction.Delay);
            mockSettings.SetupGet(x => x.ClassNameSalt).Returns("test-class-salt");
            mockSettings.SetupGet(x => x.ContentVirtualPath).Returns("~/content.html");
            mockSettings.SetupGet(x => x.ErrorCode).Returns(HttpStatusCode.NotFound);
            mockSettings.SetupGet(x => x.HoneypotRelativeUrl).Returns("/honey/pot/");
            mockSettings.SetupGet(x => x.MaxDelay).Returns(30000);
            mockSettings.SetupGet(x => x.MinDelay).Returns(500);

            return mockSettings;
        }

        private Mock<IHttpApplication> GetMockApplication()
        {
            var mockApplication = new Mock<IHttpApplication>();

            mockApplication.SetupGet(x => x.Context).Returns(() => new HttpContext(new HttpRequest("test.html", "http://localhost/", ""), new HttpResponse(new StringWriter())));

            return mockApplication;
        }

        #endregion
    }
}
