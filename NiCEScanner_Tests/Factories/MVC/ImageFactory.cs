using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories;

public class ImageFactory
{
    public static ImageModel Create()
    {
        var image = new ImageModel()
        {
            FileName = "Innovation_XX.jpg",
            ImageData = System.IO.File.ReadAllBytes("../../../../NiCeScanner/wwwroot/images/Innovation/Innovation_1.jpg"),
        };
        
        return image;
    }
    
    public static ImageModel CreateAndSubmit(ApplicationDbContext context)
    {
        var image = Create();
        
        context.Images.Add(image);
        context.SaveChanges();
        
        return image;
    }
}