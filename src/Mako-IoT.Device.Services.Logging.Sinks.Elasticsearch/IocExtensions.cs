using MakoIoT.Device.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace MakoIoT.Device.Services.Logging.Sinks.SdCard
{
    public static class IocExtensions
    {
        public static void AddElasticsearchSink(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(ILogSink), typeof(ElasticsearchSink));
        }
    }
}