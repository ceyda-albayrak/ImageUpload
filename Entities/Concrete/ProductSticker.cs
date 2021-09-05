using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Entities.Abstract;

namespace Entities.Concrete
{
    [Table("ProductStickers")]
    public class ProductSticker : IEntity<int>
    {
       [Key]
        public int Id { get ; set ; }
        public DateTime CreateDate { get ; set; }
        public DateTime? UpdateDate { get ; set ; }
        public int ProductId { get; set; }
        public int TempId { get; set; }
        public int MerchantId { get; set; }
    }
}
