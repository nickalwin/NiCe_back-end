using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories;

public class ScanCodeFormFactory
{
    public static ScanCodeForm Create()
    {
        var scanForm = new ScanCodeForm()
        {
            Code = Guid.NewGuid(),
            ScanId = 1,
            CanEdit = false
        };
        
        return scanForm;
    }
}