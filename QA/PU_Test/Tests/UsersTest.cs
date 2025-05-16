using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using PU_Test.Framework.Models;
using static System.Net.WebRequestMethods;

namespace PU_Test.Tests
{
    [TestFixture]
    public class UsersTest : TestBase
    {
        private string _url = "https://gorest.co.in/public/v2";
        private string _userEndpoint = "/user/7892574";
        private int _id = 7892574;


        [Test]
        //public async Task GetUsers()
        //{
        //    var response = await Client.GetAsync("users");
        //    string responseData = await response.Content.ReadAsStringAsync();
        //    var users = JsonConvert.DeserializeObject<List<UserResponse>>(responseData);
        //}
        public async Task GetUserByID()
        {
            var response = await Client.GetAsync("https://gorest.co.in/public/v2/users/7892574");
            string responseData = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<UserResponse>(responseData);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), $"Unexpected response: {responseData}");
            
        }
        public async Task CreateUser()
        {
            var requestBody = new
            {
                name = "test",
                gender = "male",
                email = $"pu-sddsf.{System.Guid.NewGuid()}@example.com",
                status = "active"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("https://gorest.co.in/public/v2/", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created), $"Unexpected response: {responseBody}");
          
        }
    }
}
