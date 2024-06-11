using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories;

public class ScanFormFactory
{
    public static ScanForm Create()
    {
        var scanForm = new ScanForm()
        {
            ContactName = "Test Contact Name",
            ContactEmail = "Test@email.com"
        };
        
        return scanForm;
    }
}