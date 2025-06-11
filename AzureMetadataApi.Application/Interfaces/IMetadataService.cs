using System.Threading;
using System.Threading.Tasks;
using AzureMetadataApi.Domain.Models;
using AzureMetadataApi.Application.DTOs;


namespace AzureMetadataApi.Application.Interfaces
{
    public interface IMetadataService
    {
        Task<MetadataResponse> GetAllMetadataAsync();
        Task<string> GetMetadataByPathAsync(string keyPath);
    }
}