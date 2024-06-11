using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;

namespace NiCEScanner_Tests.Factories.API;

public class DummyDatabaseFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Filename=:memory:")
            .Options;

        var context = new ApplicationDbContext(options);
        
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        DatabaseSeeder.Seed(context, "../../../../NiCeScanner/wwwroot/images/");
        
        return context;
    } 
}