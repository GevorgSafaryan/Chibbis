using Chibbis.Model;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chibbis.Tests
{
    public class TestScenarios : BaseTest
    {
        [Test]
        public static async Task TestResponseStatusCode()
        {
            var response = await HttpClient.GetAsync(URL);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public static async Task TestResponseContentType()
        {
            var response = await HttpClient.GetAsync(URL);
            Assert.IsTrue(response.Content.Headers.ContentType.MediaType.Equals("application/json"));
        }

        [Test]
        public static async Task TestResponseObject()
        {
            var response = await HttpClient.GetAsync(URL);
            string jsonString = await response.Content.ReadAsStringAsync();
            List<Reviews> reviews = JsonConvert.DeserializeObject<List<Reviews>>(jsonString);
            foreach (var review in reviews)
            {
                Assert.IsNotNull(review.IsPositive);
                Assert.IsNotNull(review.Message);
                Assert.IsNotNull(review.DateAdded);
                Assert.IsNotNull(review.UserFIO);
                Assert.IsNotNull(review.RestaurantName);
            }
        }

        //Test is failing. Should be possible to get a single review. 404 error code is returned
        [TestCase("1")]
        public static async Task GetReviewByID(string id)
        {
            var response = await HttpClient.GetAsync(URL + $"/{id}");
            string jsonString = await response.Content.ReadAsStringAsync();
            List<Reviews> reviews = JsonConvert.DeserializeObject<List<Reviews>>(jsonString);
            Assert.IsTrue((response.StatusCode == HttpStatusCode.OK) && (reviews.Count == 1));
        }

        [Test]
        public static async Task TestTheMethodType()
        {
            var newReview = new Reviews
            {
                IsPositive = true,
                Message = "Test",
                DateAdded = DateTime.Now.ToString(),
                UserFIO = "Test User",
                RestaurantName = "Test restaurant"
            };
            var jsonData = JsonConvert.SerializeObject(newReview);
            var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(URL, data);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.MethodNotAllowed);
        }
    }
}
