namespace MakoIoT.Device.Services.Logging.Sinks.SdCard
{
    public class ElasticsearchSinkConfig
    {
        /// <summary>
        /// URL of the service to send logs to
        /// </summary>
        public string ServiceUrl { get; set; }
        
        /// <summary>
        /// HTTPS certificate of the service
        /// </summary>
        public string ServiceCertificate { get; set; }
        
        /// <summary>
        /// Authorization string (HTTP request header) 
        /// </summary>
        public string ServiceAuthorization { get; set; }
        
        /// <summary>
        /// Device ID
        /// </summary>
        public string DeviceId { get; set; }
        
        public static string SectionName => "LogStorage";
    }
}