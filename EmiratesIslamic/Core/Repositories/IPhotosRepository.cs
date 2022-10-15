using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmiratesIslamic.Core.Repositories;

public interface IPhotosRepository
{
    Task<string> SaveAsync(IFormFile file, string categoryFolder);
    bool Delete(string relativePath);
}