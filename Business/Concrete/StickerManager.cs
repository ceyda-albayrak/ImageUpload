using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Utilities.Business;
using Business.Utilities.Messages;
using Business.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using ExcelDataReader;

namespace Business.Concrete
{
    public class StickerManager : IStickerService
    {
        private IStickerDal _stickerDal;
        private IStickerTemplateDal _stickerTemplateDal;

        public StickerManager(IStickerDal stickerDal, IStickerTemplateDal stickerTemplateDal)
        {
            _stickerDal = stickerDal;
            _stickerTemplateDal = stickerTemplateDal;
        }
     
        public Result Add(ProductSticker sticker)
        {
            var tip = _stickerTemplateDal.Get(sticker.TempId);
            var result = BusinessRules.Rol(CheckProductImageCount(sticker.ProductId));
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            if (tip != null)
            {
                _stickerDal.Add(sticker);
                return new SuccessResult("Sticker"+Messages.Added);
            }

            return new ErrorResult("Böyle bir template yok!");
        }

        public Result Delete(ProductSticker sticker)
        {
            _stickerDal.Delete(sticker);
            return new SuccessResult(Messages.Deleted);
        }

        public Result ExcelUpload(ProductSticker sticker)
        {

            var _customRoot = Environment.CurrentDirectory+ "\\wwwroot";
            var _folderName = "\\Excel\\cyda.xlsx";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(_customRoot + _folderName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    reader.Read();
                    if (reader.GetValue(0).ToString() == "ProductId" && reader.GetValue(1).ToString() == "MerchantId")
                    {

                        while (reader.Read())
                        {
                            if (reader.GetValue(0).ToString() == null || reader.GetValue(1) == null)
                            {
                                return new ErrorResult("Boş hücre olamaz!");
                            }
                            sticker.ProductId = int.Parse(reader.GetValue(0).ToString());
                            sticker.MerchantId = int.Parse(reader.GetValue(1).ToString());
                            sticker.UpdateDate = null;
                            var tip = _stickerTemplateDal.Get(sticker.TempId);
                            var result = BusinessRules.Rol(CheckProductImageCount(sticker.ProductId));
                            if (tip == null)
                            {
                                return new ErrorResult("Böyle bir template yok!");
                            }
                           
                            if (result != null)
                            {

                                return new ErrorResult(result.Message);
                            }
                            _stickerDal.Add(sticker);
                        }
                        
                    }
                    else
                    {
                        return new ErrorResult("Kolon isimleri hatalı!");
                    }
                    


                }
                
            }
            return new SuccessResult("başarılı");





        }

        public DataResult<List<ProductSticker>> GetAll()
        {
            return new SuccessDataResult<List<ProductSticker>>(_stickerDal.GetAll(),Messages.Listed);
        }


        public Result Update(ProductSticker sticker)
        {
            _stickerDal.Update(sticker);
            return new SuccessResult(Messages.Updated);
        }

        private IResult CheckProductImageCount(int productId)
        {
            var sonuc = _stickerDal.GetProductsbyId(productId);
            if (sonuc.Count > 2)
            {
                return new ErrorResult("Ürünün en fazla 3 resmi olabilir");
            }

            return new SuccessResult();

        }
    }
}
