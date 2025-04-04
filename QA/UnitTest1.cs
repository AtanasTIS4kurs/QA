using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QA
{
    public class UserResponce 
    {
        public int id { get; set; } 
        public string name { get; set; } 
        public string email { get; set; } 
        public string gender { get; set; } 
        public string status { get; set; } 
    }
    public class Tests
    {
        private HttpClient _httpClient;
        private string _url = "https://gorest.co.in/public/v2/users";
        [SetUp]

        public void Setup()
        {
            _httpClient = new HttpClient();
        }
        [TearDown]
        public void Teardown() 
        { 
        _httpClient.Dispose();
        }
        [Test]
        public async Task GetAllUsers()
        {
            //responce message
           var responce = await _httpClient.GetAsync(_url);
            //Assert the responce status code
            Assert.True(responce.IsSuccessStatusCode);
            var content = await responce.Content.ReadAsStringAsync();
            var userList = JsonConvert.DeserializeObject<List<UserResponce>>(content);
            var actualUser = userList[0];
            Assert.That(actualUser.id, Is.EqualTo(3), "The id doesnt match");

        }
    }
}