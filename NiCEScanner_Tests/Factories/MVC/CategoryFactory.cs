using Newtonsoft.Json;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories.MVC;

public class CategoryFactory
{
    public static Category Create()
    {
        var colors = CategoryColors.colors;
        
        var category = new Category()
        {
            Data = JsonConvert.SerializeObject(new
            {
                nl = new
                {
                    name = "Facilitair",
                },
                en = new
                {
                    name = "Facility",
                }
            }),
            Color = colors[new Random().Next(0, colors.Length)],
            Show = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
        
        return category;
    }
    
    public static Category CreateAndSubmit(ApplicationDbContext context)
    {
        var category = Create();
        
        context.Categories.Add(category);
        context.SaveChanges();
        
        return category;
    }
}