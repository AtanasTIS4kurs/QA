using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PU_Test.Framework.HttpClientFactory;
using PU_Test.Framework.Models.Service;

namespace PU_Test.Tests
{
    public class TestBase
    {
        protected HttpClient Client;

        private HttpClientProvider _httpClientProvider;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json") 
                .Build();

            services.RegisterServices(config);

            var provider = services.BuildServiceProvider();
            _httpClientProvider = provider.GetRequiredService<HttpClientProvider>();
            Client = _httpClientProvider.Client;
        }
    }
}
