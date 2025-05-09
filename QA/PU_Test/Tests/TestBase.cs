using PU_Test.Framework.HttpClientFactory;

namespace PU_Test.Tests
{
    public class TestBase
    {
        protected HttpClient Client;

        [SetUp]
        public void Setup()
        {
            Client = HttpClientProvider.Client;
        }
    }
}
