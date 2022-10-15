using EmiratesIslamic.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EmiratesIslamic.Infrastructure.Services;

public class PhotosRepository : IPhotosRepository
{
    private readonly string _webRootPath;

    public PhotosRepository(IWebHostEnvironment hostingEnvironment)
    {
        _webRootPath = hostingEnvironment.WebRootPath;
    }

    public async Task<string> SaveAsync(IFormFile file, string categoryFolder)
    {
        var relativePath = Path.Combine("images", categoryFolder);
        relativePath = Path.Combine(relativePath, Guid.NewGuid().ToString());
        relativePath = string.Concat(relativePath, Path.GetExtension(file.FileName));
        var fullPath = Path.Combine(_webRootPath, relativePath);
        try
        {
            await using Stream fileStream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return relativePath;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public bool Delete(string relativePath)
    {
        try
        {
            var fullPath = Path.Combine(_webRootPath, relativePath);
            File.Delete(fullPath);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}