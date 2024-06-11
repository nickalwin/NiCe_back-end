namespace NiCEScanner_Tests.Factories.MVC;

public class PDFTemplateFactory
{
    public static Dictionary<string, string> CreateTitle()
    {
        return new Dictionary<string, string>
        {
            {"nl", "Circulaire scan PDF-rapport"},
            {"en", "Circularity scan PDF report"}
        };
    }

    public static Dictionary<string, string> CreateIntroduction()
    {
        return new Dictionary<string, string>
        {
            {"nl", "Bedankt voor het invullen van de circulaire scan. In dit pdf-document kunt u uw resultaten vinden. Hoe hoger u scoort op een categorie, hoe beter u al bezig bent met de circulaire economie in dat gebied."},
            {"en", "Thank you for completing the circularity scan. In this pdf document you can find your results. The higher you score on a category, the better you are already doing with the circular economy in that area."}
        };
    }

    public static byte[] CreateImage()
    {
        return System.IO.File.ReadAllBytes("../../../../NiCeScanner/wwwroot/images/pdf.png");
    }

    public static Dictionary<string, string> CreateAfterPlotText()
    {
        return new Dictionary<string, string>
        {
            {"nl", "Op de volgende pagina's kunt u de resultaten van de scan in detail vinden samen met tips en adviezen over hoe u uw score kunt verbeteren."},
            {"en", "On the following pages you can find the results of the scan in detail along with tips and advice on how to improve your score."}
        };
    }
    
    public static Dictionary<string, string> CreateBeforePlotText()
    {
        return new Dictionary<string, string>
        {
            {"nl", "Op de onderstaande plot kunt u de resultaten van de scan zien. Hoe hoger u scoort op een categorie, hoe beter u al bezig bent met de circulaire economie in dat gebied."},
            {"en", "On the plot below you can see the results of the scan. The higher you score on a category, the better you are already doing with the circular economy in that area."}
        };
    }
}