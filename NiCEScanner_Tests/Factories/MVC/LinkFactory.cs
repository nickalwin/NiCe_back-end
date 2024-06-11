using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories.MVC;

public class LinkFactory
{
    public static Link CreateAndSubmit(ApplicationDbContext context)
    {
        Category category = CategoryFactory.CreateAndSubmit(context);
        
        var link = new Link()
        {
            Name = "Test Link",
            Href = "https://www.test.com",
            CategoryId = category.Id,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
        
        context.Links.Add(link);
        context.SaveChanges();
        
        return link;
    }
}