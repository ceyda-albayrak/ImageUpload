using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IStickerService
    {
        Result Add(ProductSticker sticker);
        Result Update(ProductSticker sticker);
        Result Delete(ProductSticker sticker);
        DataResult<List<ProductSticker>> GetAll();
        Result ExcelUpload(ProductSticker sticker);

    }
}
