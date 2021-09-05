using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.Dapper;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IStickerDal : IDapperRepository<ProductSticker>
    {
        ProductSticker GetProductId(int ProductId);
        List<ProductSticker> GetProductsbyId(int ProductId);
    }
}
