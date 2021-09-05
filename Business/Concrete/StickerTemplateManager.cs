using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Utilities.Business;
using Business.Utilities.Helper;
using Business.Utilities.Messages;
using Business.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace Business.Concrete
{
    public class StickerTemplateManager : IStickerTemplateService
    {
        private IStickerTemplateDal _templateDal;
        private IStickerDal _stickerDal;
        private IImageHelper _imageHelper;

        public StickerTemplateManager(IStickerTemplateDal templateDal,IStickerDal stickerDal,IImageHelper imageHelper)
        {
            _templateDal = templateDal;
            _stickerDal = stickerDal;
            _imageHelper = imageHelper;
        }
        public Result Add(IFormFile file, ProductStickerTemplate template)
        {
            var templateResult = _imageHelper.Upload(file);
            if (!templateResult.Success)
            {
                return new ErrorResult(templateResult.Message);
            }

            template.ImagePath = templateResult.Message;
            template.CreateDate = DateTime.Now;
            template.UpdateDate = null;
            template.IsActive = true;
          
            if (!(template.TempText == null || template.ImagePath == null))
            {
                return new ErrorResult("Ürünün yalnızca texti ya da image'i olabilir!");
            }
            _templateDal.Add(template);
            return new SuccessResult(Messages.Added);
        }

        public Result Delete(ProductStickerTemplate template)
        {
            var image = _templateDal.Get(template.Id);
            if (image == null)
            {
                return new ErrorResult("Image not found!");
            }

            _imageHelper.Delete(image.ImagePath);
            _templateDal.Delete(template);
            return new SuccessResult(Messages.Deleted);
        }

        public DataResult<List<ProductStickerTemplate>> GetAll()
        {
            return new SuccessDataResult<List<ProductStickerTemplate>>(_templateDal.GetAll().ToList(), Messages.Listed);
        }

        public DataResult<List<ProductStickerTemplate>> GetById(int id)
        {
            return new SuccessDataResult<List<ProductStickerTemplate>>(_templateDal.GetAll().ToList(), Messages.Listed);
        }

        public Result Update(IFormFile file, ProductStickerTemplate template)
        {
            var image = _templateDal.Get(template.Id);
            if (image == null)
            {
                return new ErrorResult("Image not found!");
            }
            var uploadImage = _imageHelper.Update(file, image.ImagePath);
            if (!uploadImage.Success)
            {
                return new ErrorResult(uploadImage.Message);
            }
            template.UpdateDate=DateTime.Now;
            template.ImagePath = uploadImage.Message;
            template.TempText = null;
            return new SuccessResult(Messages.Updated);
        }

       
        


    }
}
