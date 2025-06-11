using AzureMetadataApi.Domain.Models;


namespace AzureMetadataApi.Application.DTOs
{
    public class MetadataResponse
    {
        public ComputeInfo Compute { get; set; } = new();
    }
}