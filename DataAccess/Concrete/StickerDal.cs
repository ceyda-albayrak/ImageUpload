using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.Dapper;
using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class StickerDal : DapperRepositoryBase<ProductSticker>, IStickerDal
    {
        public ProductSticker GetProductId(int ProductId)
        {
            using (var connection = new SqlConnection(Db.connection))
            {
                return connection.Query<ProductSticker>("SELECT * FROM ProductStickers WHERE ProductId=@ProductId",
                    new {ProductId = ProductId}).FirstOrDefault();

            }
        }

        public List<ProductSticker> GetProductsbyId(int ProductId)
        {

            using (var connection = new SqlConnection(Db.connection))
            {
                return connection.Query<ProductSticker>("SELECT * FROM ProductStickers WHERE ProductId=@ProductId", new { ProductId = ProductId }).ToList();
            }
        }
    }
}
