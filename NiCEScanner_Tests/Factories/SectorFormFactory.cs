using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories;

public class SectorFormFactory
{
    public static SectorForm Create()
    {
        var sectorForm = new SectorForm()
        {
            Languages = new Dictionary<string, string>()
            {
                { "nl", "Facilitair2123" },
                { "en", "Facility123123" }
            },
        };
        
        return sectorForm;
    }
}