using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Business.Abstract;
using Business.Utilities.Results;
using Entities.Concrete;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Office.Interop.Excel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StickerController : ControllerBase
    {
        private IStickerService _stickerService;

        public StickerController(IStickerService stickerService)
        {
            _stickerService = stickerService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _stickerService.GetAll();
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add( ProductSticker sticker)
        {
            sticker.UpdateDate = null;
            var result = _stickerService.Add(sticker);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(ProductSticker sticker)
        {
            var result = _stickerService.Delete(sticker);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPut("update")]
        public IActionResult Update(ProductSticker sticker)
        {
            var result = _stickerService.Update(sticker);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("excel")]
        public IActionResult ExcelUpload(ProductSticker sticker)
        {

            var result = _stickerService.ExcelUpload(sticker);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);

        }

    }
}
