using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.Dapper;
using Dapper;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class StickerTemplateDal : DapperRepositoryBase<ProductStickerTemplate>, IStickerTemplateDal
    {
     
       
    }
}
