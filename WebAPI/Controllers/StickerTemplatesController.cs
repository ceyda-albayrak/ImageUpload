using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StickerTemplatesController : ControllerBase
    {
        private IStickerTemplateService _templateService;

        public StickerTemplatesController(IStickerTemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _templateService.GetAll();
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = "Image")] IFormFile file, [FromForm]ProductStickerTemplate template)
        {
            var result = _templateService.Add(file,template);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(ProductStickerTemplate template)
        {
            var result = _templateService.Delete(template);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm(Name = ("ImagePath"))] IFormFile file, ProductStickerTemplate template)
        {
            var result = _templateService.Update(file, template);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

    }
}
