using Newtonsoft.Json;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories;

public class CategoryFormFactory
{
    public static CategoryForm Create()
    {
        var colors = CategoryColors.colors;

        var categoryForm = new CategoryForm()
        {
            Languages = new Dictionary<string, string>()
            {
                { "nl", "Facilitair2123" },
                { "en", "Facility123123" }
            },
            Color = colors[new Random().Next(0, colors.Length)],
            Show = true,
        };
        
        return categoryForm;
    }
}