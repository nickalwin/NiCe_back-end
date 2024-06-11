using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories;

public class ScanCodeFactory
{
    public static ScanCode CreateAndSubmit(ApplicationDbContext context)
    {
		ScanCode scd1 = new ScanCode()
		{
			Code = Guid.NewGuid(),
			ScanId = 1,
			CanEdit = false
		};
		
		context.ScanCodes.Add(scd1);
		context.SaveChanges();
        
        return scd1;
    }
}