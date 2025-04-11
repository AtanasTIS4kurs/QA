using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace QA;


[TestFixture]
public class CreateUserTest
{
    private readonly string _accessToken = "dbd62db8abdba4ac503739553e7303aa8d3ec2c76c4be97120751d6266afedc0";
    private HttpClient _client;
    private int _userID = 7824113;
    private HttpClient _httpClient;
    private string _url = "https://gorest.co.in/public/v2/users";

    [SetUp]
    public void Setup()
    {
        _client = new HttpClient();
        _client.BaseAddress = new System.Uri("https://gorest.co.in/");
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
    }
    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }
    [Test]
    public async Task CreateUser_ShouldBe201()
    {
        var requestBody = new
        {
            name = "pu-student1",
            gender = "male",
            email = $"pu-sdfhsfhdsfmiro.{System.Guid.NewGuid()}@example.com",
            status = "active"
        };
        //deserializaciq ot jason kym c# i validaciq na edno properti
        //

        var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("public/v2/users", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        var user = JsonConvert.DeserializeObject<UserResponce>(responseBody);

        _userID = user.id;

        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created), $"Unexpected response: {responseBody}");
        Assert.That(user.id, Is.LessThan(0), "The id is not valid");

    }
    [Test]
    public async Task UpdateUser()
    {

        //Method -> Get ALL Users
        //Take the first one user from the list
        //take user.ID
        var responce = await _httpClient.GetAsync(_url);
        var contentAll = await responce.Content.ReadAsStringAsync();
        var userList = JsonConvert.DeserializeObject<List<UserResponce>>(contentAll);
        var actualUser = userList[0];
        _userID = actualUser.id;
        var requestBody = new
        {
            name = "TEst"
        };

        var json = System.Text.Json.JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"public/v2/users/.{_userID}", content);
        var responseBody = await response.Content.ReadAsStringAsync();
    }
}
