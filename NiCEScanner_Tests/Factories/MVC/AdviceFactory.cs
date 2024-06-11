using Newtonsoft.Json;
using NiCeScanner.Data;
using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories.MVC;

public class AdviceFactory
{
    public static Advice Create()
    {
        var advice = new Advice()
        {
            Data = JsonConvert.SerializeObject(new
            {
                nl = new
                {
                    data = "Kijk tijdens het ontwerpen van producten dat producten op het eind van hun levensfase in ieder geval veilig verbrand kunnen worden. Wat voor materialen gebruikt u voor uw producten en welke stoffen komen er vrij bij het verbranden van dit materiaal?",
                },
                en = new
                {
                    data = "When designing products, ensure that products can at least be safely incinerated at the end of their life cycle. What materials do you use for your products and what substances are released when this material is burned?",
                }
            }),
            AdditionalLink = "https://www.google.com",
            AdditionalLinkName = "Google",
            QuestionId = new Random().Next(1, 40),
            Condition = new Random().Next(1, 5),
            CreatedAt = DateTime.Now
        };
        
        return advice;
    }
    
    public static Advice CreateAndSubmit(ApplicationDbContext context)
    {
        var advice = Create();
        
        context.Advices.Add(advice);
        context.SaveChanges();
        
        return advice;
    }
}