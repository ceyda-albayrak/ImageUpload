using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace Business.Utilities.Helper
{
    public interface IImageHelper
    {
        public IResult Upload(IFormFile file);
        public IResult Update(IFormFile file, string imagePath);
        public IResult Delete(string path);
        public IResult CheckFileExists(IFormFile file);
        public IResult CheckFileTypeValid(string type);
    }
}
