using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NDCLondonDemo2;

namespace NDCLondonDemo2Tests
{
    [TestClass]
    public class ArticleApiTest
    {
        private HttpClient _client;

        [TestInitialize]
        public void Init()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:50265/api/");
        }

        [TestMethod]
        public async Task ShouldGetAllArticlesByCallingGet()
        {
            var response = await _client.GetAsync("articles");
            response.EnsureSuccessStatusCode();

            var articles = await response.Content.ReadAsAsync<List<ArticleListDto>>();
            Assert.AreEqual(articles.Count, 4);
        }

        [TestMethod]
        public async Task ShouldInsertArticle()
        {
            var a = new ArticleListDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Article",
                Description = "Test description"
            };

            var response = await _client.PostAsJsonAsync("articles", a);
            Assert.AreEqual(HttpStatusCode.OK, response.EnsureSuccessStatusCode());
        }
    }
}
