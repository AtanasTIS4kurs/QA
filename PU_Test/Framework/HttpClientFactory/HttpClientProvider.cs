using System.Net.Http.Headers;

namespace PU_Test.Framework.HttpClientFactory
{
    public class HttpClientProvider
    {
        public HttpClient Client { get; }

        public HttpClientProvider(TestConfiguration config)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(config.BaseUrl)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.Token);

            Client = client;
        }
    }
}
