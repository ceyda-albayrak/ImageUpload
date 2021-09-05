using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IStickerTemplateService
    {
        DataResult<List<ProductStickerTemplate>> GetById(int id);
        DataResult<List<ProductStickerTemplate>> GetAll();
        Result Add(IFormFile file, ProductStickerTemplate template);
        Result Update(IFormFile file, ProductStickerTemplate template);
        Result Delete(ProductStickerTemplate template);
    }
}
