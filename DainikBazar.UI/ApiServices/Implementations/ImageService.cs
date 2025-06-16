using DainikBazar.UI.ApiServices.Interfaces;

namespace DainikBazar.UI.ApiServices.Implementations;

public class ImageService : IImageService
{
    public async Task<string> ImageMappingAsync(IFormFile? imagefile)
    {
        if (imagefile == null || imagefile.Length == 0)
        {
            return "Images/NoImageFound.jpg";
        }
        var fileName = $"{Guid.NewGuid()}_{imagefile.FileName}";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imagefile.CopyToAsync(stream);
        }
        return $"Images/{fileName}";
    }


}
