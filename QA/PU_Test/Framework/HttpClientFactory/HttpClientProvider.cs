using System.Net.Http.Headers;

namespace PU_Test.Framework.HttpClientFactory
{
    public class HttpClientProvider
    {
        private static readonly HttpClient _httpClient = new HttpClient();  
        public HttpClientProvider(TestConfiguration config)
        {
            _httpClient.BaseAddress = new System.Uri(config.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.Token);
        }
        public static HttpClient Client => _httpClient;
    }
}
