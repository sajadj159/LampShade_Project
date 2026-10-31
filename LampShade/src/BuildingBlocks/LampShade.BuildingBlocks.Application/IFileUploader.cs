using Microsoft.AspNetCore.Http;

namespace LampShade.BuildingBlocks.Application
{
    public interface IFileUploader
    {
        string Upload(IFormFile file,string path);
    }
}