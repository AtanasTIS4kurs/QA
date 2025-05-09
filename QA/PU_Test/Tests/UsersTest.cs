using Newtonsoft.Json;
using PU_Test.Framework.Models;

namespace PU_Test.Tests
{
    [TestFixture]
    public class UsersTest : TestBase
    {
        [Test]
        public async Task GetUsers()
        {
            var response = await Client.GetAsync("users");
            string responseData = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserResponse>>(responseData);
        }
    }
}
