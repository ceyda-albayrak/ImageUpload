using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Entities.Abstract;

namespace Entities.Concrete
{
    [Table("ProductStickerTemplates")]
    public class ProductStickerTemplate : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get ; set; }
        public DateTime? UpdateDate { get; set; }
        public string TempName { get; set; }
        public string ImagePath { get; set; }
        public Boolean IsActive { get; set; }
        public string TempText { get; set; }
    }
}
