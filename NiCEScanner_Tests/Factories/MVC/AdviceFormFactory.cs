using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories.MVC;

public class AdviceFormFactory
{
    public static AdviceForm Create()
    {
        var adviceForm = new AdviceForm()
        {
            Languages = new Dictionary<string, string>()
            {
                { "nl", "Kijk tijdens het ontwerpen van producten dat producten op het eind van hun levensfase in ieder geval veilig verbrand kunnen worden. Wat voor materialen gebruikt u voor uw producten en welke stoffen komen er vrij bij het verbranden van dit materiaal?" }, 
                { "en", "When designing products, ensure that products can at least be safely incinerated at the end of their life cycle. What materials do you use for your products and what substances are released when this material is burned?" }
            },
            AdditionalLink = "https://www.google.com",
            AdditionalLinkName = "Google",
            QuestionId = new Random().Next(1, 40),
            Condition = new Random().Next(1, 5),
        };
        
        return adviceForm;
    }
}