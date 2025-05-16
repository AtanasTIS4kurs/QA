using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PU_Test.Framework.HttpClientFactory;

namespace PU_Test.Framework.Models.Service
{
    public static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Deserialize the TestConfiguration section and register as singleton
            var testConfig = configuration.GetSection("TestConfiguration").Get<TestConfiguration>();
            services.AddSingleton(testConfig);

            // Register HttpClientProvider as singleton, injected with TestConfiguration
            services.AddSingleton<HttpClientProvider>();

            return services;
        }
    }
}
