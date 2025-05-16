using System.Net.Http;
using System.Text;
using System.Text.Json;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using PU_Test.Framework.Models;
using PU_Test.Framework.Models.Validator;
using static System.Net.WebRequestMethods;

namespace PU_Test.Tests
{
    [TestFixture]
    public class UsersTest : TestBase
    {
        private string _userEndpoint = "users";
        private string _id = "/7892574";
       
        [Test]
        public async Task GetUserByID()
        {
            var response = await Client.GetAsync($"{_userEndpoint}{_id}");
            string responseData = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Unexpected status code: {response.StatusCode}, body: {responseData}");
            }
            var user = JsonConvert.DeserializeObject<UserResponse>(responseData);
            var validator = new UserResponseValidator();
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                throw new Exception("Validation failed: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }
        }

        [Test]
        public async Task CreateUser()
        {
            var user = new UserResponse()
            {
                name = "sds",
                gender = "male",
                email = $"pu-sddsf.{System.Guid.NewGuid()}@example.com",
                status = "active"
            };
            var validator = new UserResponseValidator();
            ValidationResult result = validator.Validate(user);

            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"User data is invalid: {errors}");
            }
            var json = System.Text.Json.JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync($"{_userEndpoint}", content);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new Exception($"API Error: Status {response.StatusCode}, Response: {responseBody}");
            }
        }
    }
}
