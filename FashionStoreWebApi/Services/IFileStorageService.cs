namespace FashionStoreWebApi.Services
{
    public interface IFileStorageService
    {
        string saveImageToPath(IFormFile file);
    }
}
