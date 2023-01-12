using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDDotNet6.Exceptions;
using CRUDDotNet6.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDDotNet6.Controllers.Blob
{
    [Route("api/[controller]")]
    public class BlobController : Controller
    {
        private readonly IBlobStorage _storage;
        private readonly string _connectionString;
        private readonly string _container;

        public BlobController(IBlobStorage storage, IConfiguration iConfig)
        {
            _storage = storage;
            _connectionString = iConfig.GetValue<string>("MyConfig:StorageConnection");
            _container = iConfig.GetValue<string>("MyConfig:ContainerName");
        }

        [HttpGet("DownloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            try
            {
                var content = await _storage.GetDocument(_connectionString, _container, fileName);
                return File(content, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch(Exception e)
            {
                if (e.GetType() == typeof(BusinessException))
                    return BadRequest(e.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [Route("InsertFile")]
        [HttpPost]
        public async Task<ActionResult<object>> InsertFile(IFormFile asset)
        {
            try
            {
                Console.WriteLine("InsertFle");
                Console.WriteLine(asset);
                if (asset != null)
                {
                    Stream stream = asset.OpenReadStream();
                    await _storage.UploadDocument(_connectionString, _container, asset.FileName, stream);
                    return Ok("File uploaded successfully.");
                }

                return BadRequest("Asset is null");
            }
            catch(Exception e)
            {
                if (e.GetType() == typeof(BusinessException))
                    return BadRequest(e.Message);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
    }
}

