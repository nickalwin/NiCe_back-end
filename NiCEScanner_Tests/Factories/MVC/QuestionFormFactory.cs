using NiCeScanner.Models;

namespace NiCEScanner_Tests.Factories.MVC;

public class QuestionFormFactory
{
    public static QuestionForm Create()
    {
        var questionForm = new QuestionForm()
        {
            Headers = new Dictionary<string, string>()
            {
                { "nl", "Energieniveau" },
                { "en", "Energy level" }
            },
            Questions = new Dictionary<string, string>()
            {
                { "nl", "Recover; Laagste niveau circulariteit: Primaire bedrijfsprocessen zorgen ervoor dat producten zo worden ontworpen dat materialen aan het einde van de levensduur veilig worden verbrand met terugwinning van energie?" },
                { "en", "Recover; Lowest circularity level: Primary business processes ensure that products are designed so that materials at the end of their lifespan can be safely incinerated with energy recovery?" }
            },
            Tooltips = new Dictionary<string, string>()
            {
                { "nl", "Bij diensten: wordt dmv van uw diensten vermeden dat poducten en materialen niet meer veilig kunnen worden verbrand met terugwinning van energie?" },
                { "en", "For services: does the use of your services prevent products and materials from being incinerated with energy recovery?" }
            },
            CategoryId = new Random().Next(1, 5),
            Weight = 1,
            Statement = false,
            Show = true,
            ImageId = new Random().Next(1, 40),
        };
        
        return questionForm;
    }
}