using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public interface IFIleUploader
    {
        string Upload(IFormFile file,string path);
    }
}