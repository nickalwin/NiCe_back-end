using Newtonsoft.Json;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories.MVC;

public class SectorFactory
{
    public static Sector Create()
    {
        var sector = new Sector()
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
        };
        
        return sector;
    }
    
    public static Sector CreateAndSubmit(ApplicationDbContext context)
    {
        var sector = Create();
        
        context.Sectors.Add(sector);
        context.SaveChanges();
        
        return sector;
    }
}