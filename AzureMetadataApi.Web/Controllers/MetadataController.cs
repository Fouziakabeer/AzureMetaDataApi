using Microsoft.AspNetCore.Mvc;
using AzureMetadataApi.Application.Interfaces;
using System.Threading.Tasks;


namespace AzureMetadataApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetadataController : ControllerBase
    {
        private readonly IMetadataService _metadataService;

        public MetadataController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _metadataService.GetAllMetadataAsync();
            return Ok(result);
        }

        [HttpGet("{*keyPath}")]
        public async Task<IActionResult> GetByPath(string keyPath)
        {
            var value = await _metadataService.GetMetadataByPathAsync(keyPath);
            if (value == null)
                return NotFound(new { message = $"Key path '{keyPath}' not found." });

            return Ok(new { keyPath, value });
        }
    }
}