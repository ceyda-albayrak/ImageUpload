using System;
using System.IO;
using System.Net.Mime;
using Business.Utilities.Helper;
using Business.Utilities.Results;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

public class ImageHelper : IImageHelper
{

    private static string _customRoot = Environment.CurrentDirectory + "\\wwwroot";
    private static string _folderName = "\\Images\\";
    private static string _txtfolderName = "\\Texts\\";

    public  IResult Upload(IFormFile file)
    {
        var fileExists = CheckFileExists(file);
        if (fileExists.Message != null)
        {
            return new ErrorResult(fileExists.Message);
        }




        var type = Path.GetExtension(file.FileName);
        var typeValid = CheckFileTypeValid(type);
        var randomName = Guid.NewGuid().ToString();

        if (typeValid.Message != null)
        {
            return new ErrorResult(typeValid.Message);
        }




        var path = Path.Combine(Directory.GetCurrentDirectory(), _customRoot + _folderName + randomName + type);
        using var image = Image.Load(file.OpenReadStream());
        //100: height
        //100: width
        image.Mutate(x => x.Resize(25, 25));
        image.Save(path);









        CheckDirectoryExists(_customRoot + _folderName);


        return new SuccessResult((_folderName + randomName + type).Replace("\\", "/"));



    }

    public  IResult Update(IFormFile file, string imagePath)
    {
        var fileExists = CheckFileExists(file);
        if (fileExists.Message != null)
        {
            return new ErrorResult(fileExists.Message);
        }

        var type = Path.GetExtension(file.FileName);
        var typeValid = CheckFileTypeValid(type);
        var randomName = Guid.NewGuid().ToString();

        if (typeValid.Message != null)
        {
            return new ErrorResult(typeValid.Message);
        }

        DeleteOldImageFile((_customRoot + imagePath).Replace("/", "\\"));
        CheckDirectoryExists(_customRoot + _folderName);
        var path = Path.Combine(Directory.GetCurrentDirectory(), _customRoot + _folderName + randomName + type);
        using var image = Image.Load(file.OpenReadStream());
        //100: height
        //100: width
        image.Mutate(x => x.Resize(25, 25));
        image.Save(path);
        return new SuccessResult((_folderName + randomName + type).Replace("\\", "/"));
    }

    public  IResult Delete(string path)
    {
        DeleteOldImageFile((_customRoot + path).Replace("/", "\\"));
        return new SuccessResult();
    }




    public  IResult CheckFileExists(IFormFile file)
    {
        if (file != null && file.Length > 0 && file.Length < 1715200)
        {
            return new SuccessResult();
        }
        return new ErrorResult("File doesn't exists.");
    }


    public  IResult CheckFileTypeValid(string type)
    {
        if (type != ".jpeg" && type != ".png" && type != ".jpg")
        {
            return new ErrorResult("Wrong file type.");
        }
        return new SuccessResult();
    }

    private static void CheckDirectoryExists(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }


    }
 

    private static void DeleteOldImageFile(string directory)
    {
        if (File.Exists(directory.Replace("/", "\\")))
        {
            File.Delete(directory.Replace("/", "\\"));
        }

    }
}