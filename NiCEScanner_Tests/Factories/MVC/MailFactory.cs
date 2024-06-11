using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories.MVC;

public class MailFactory
{
    public static Mail Create()
    {
        var mail = new Mail()
        {  
            FirsName = "John",
            LastName = "Doe",
            Email = "john@doe.gmail.com",
            Subject = "Test",
            Message = "This is a test message",
            Phone = "1234567890",
        };
        
        return mail;
    }
    
    public static Mail CreateAndSubmit(ApplicationDbContext context)
    {
        var mail = Create();
        
        context.Mail.Add(mail);
        context.SaveChanges();
        
        return mail;
    }
}