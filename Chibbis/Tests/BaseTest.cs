using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Chibbis.Tests
{
    public class BaseTest
    {
        protected static string URL { get; set; }
        protected static HttpClient HttpClient { get; set; }

        [SetUp]
        public void Init()
        {
            URL = ConfigurationManager.AppSettings["URL"];
            HttpClient = new HttpClient();
        }

        [TearDown]
        public void TearDown()
        {
            HttpClient.Dispose();
        }
    }
}
