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
            Data = JsonConvert.SerializeObject(new
            {
                nl = new
                {
                    name = "Facilitair2123",
                },
                en = new
                {
                    name = "Facility123123",
                }
            }),
            Color = colors[new Random().Next(0, colors.Length)],
            Show = true,
        };
        
        return categoryForm;
    }
}