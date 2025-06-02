namespace DainikBazar.UI.ApiServices.Interfaces;

public interface IImageService
{
    public Task<string> ImageMappingAsync(IFormFile? file);
}
