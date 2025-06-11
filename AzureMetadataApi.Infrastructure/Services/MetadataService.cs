using AzureMetadataApi.Application.DTOs;
using AzureMetadataApi.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace AzureMetadataApi.Infrastructure.Services
{
    public class MetadataService : IMetadataService
    {
        private const string MetadataUrl = "http://169.254.169.254/metadata/instance?api-version=2021-02-01&format=json";
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger<MetadataService> _logger;

        public MetadataService(HttpClient httpClient, IMemoryCache cache, ILogger<MetadataService> logger)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Metadata", "true");
            _cache = cache;
            _logger = logger;
        }

        public async Task<MetadataResponse> GetAllMetadataAsync()
        {
            return await _cache.GetOrCreateAsync("metadata", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                _logger.LogInformation("Fetching all metadata from Azure IMDS...");
                var response = await _httpClient.GetStringAsync(MetadataUrl);
                var json = JObject.Parse(response);
                _logger.LogInformation("Metadata fetched and parsed.");
                return json.ToObject<MetadataResponse>();
            });
        }

        public async Task<string> GetMetadataByPathAsync(string keyPath)
        {
            var json = await _cache.GetOrCreateAsync("metadata_raw", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                _logger.LogInformation("Fetching metadata JSON for key path lookup...");
                return await _httpClient.GetStringAsync(MetadataUrl);
            });

            var token = JObject.Parse(json).SelectToken(keyPath);
            return token?.ToString();
        }
    }
}