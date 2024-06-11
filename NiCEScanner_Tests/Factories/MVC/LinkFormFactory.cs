using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories;

public class LinkFormFactory
{
    public static LinkForm Create()
    {
        var linkForm = new LinkForm()
        {
            Name = "Test Link",
            Href = "https://www.test.com",
            CategoryId = new Random().Next(1, 5)
        };
        
        return linkForm;
    }
}