using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using MakoIoT.Device.Services.Interface;
using MakoIoT.Device.Utilities.Invoker;

namespace MakoIoT.Device.Services.Logging.Sinks.SdCard
{
    internal sealed class ElasticsearchSink : ILogSink
    {
        private readonly IConfigurationService _configService;
        private readonly HttpClient _httpClient;

        public ElasticsearchSink(IConfigurationService configService)
        {
            _configService = configService;
            _httpClient = CreateClient();
        }
        
        public void Log(string message)
        {
            var config = GetConfig();
            using (var content = new StringContent(message, Encoding.UTF8, "application/json"))
            {
                Invoker.Retry(() =>
                {
                    using (var response = _httpClient.Post(config.ServiceUrl, content))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"[Critical] Unable to write to Elasticsearch. Response code: {response.StatusCode}");
                            Console.WriteLine($"[Critical] Response: {response.Content.ReadAsString()} || Request: {message}");
                        }

                        response.EnsureSuccessStatusCode();
                    }
                }, 3);
            }
        }

        private HttpClient CreateClient()
        {
            var config = GetConfig();
            var c = new HttpClient();

            if (!string.IsNullOrEmpty(config.ServiceCertificate))
            {
                c.HttpsAuthentCert = new X509Certificate(config.ServiceCertificate);
            }

            if (!string.IsNullOrEmpty(config.ServiceAuthorization))
            {
                c.DefaultRequestHeaders.Add("Authorization", config.ServiceAuthorization);
            }

            return c;
        }

        private ElasticsearchSinkConfig GetConfig()
        {
            return (ElasticsearchSinkConfig)_configService.GetConfigSection(ElasticsearchSinkConfig.SectionName,
                typeof(ElasticsearchSinkConfig));
        }
    }
}