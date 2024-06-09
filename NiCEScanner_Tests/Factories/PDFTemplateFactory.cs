using Newtonsoft.Json;

namespace NiCEScanner_Tests.Factories;

public class PDFTemplateFactory
{
    public static string CreateTitle()
    {
        return JsonConvert.SerializeObject(new
        {
            nl = new
            {
                data = "Circulaire scan PDF-rapport",
            },
            en = new
            {
                data = "Circularity scan PDF report",
            }
        });
    }

    public static string CreateIntroduction()
    {
        return JsonConvert.SerializeObject(new
        {
            nl = new
            {
                data =
                    "Bedankt voor het invullen van de circulaire scan. In dit pdf-document kunt u uw resultaten vinden. Hoe hoger u scoort op een categorie, hoe beter u al bezig bent met de circulaire economie in dat gebied.",
            },
            en = new
            {
                data =
                    "Thank you for completing the circularity scan. In this pdf document you can find your results. The higher you score on a category, the better you are already doing with the circular economy in that area.",
            }
        });
    }

    public static byte[] CreateImage()
    {
        return System.IO.File.ReadAllBytes("../../../../NiCeScanner/wwwroot/images/pdf.png");
    }

    public static string CreateAfterPlotText()
    {
        return JsonConvert.SerializeObject(new
        {
            nl = new
            {
                data =
                    "Op de volgende pagina's kunt u de resultaten van de scan in detail vinden samen met tips en adviezen over hoe u uw score kunt verbeteren.",
            },
            en = new
            {
                data =
                    "On the following pages you can find the results of the scan in detail along with tips and advice on how to improve your score.",
            }
        });
    }
    
    public static string CreateBeforePlotText()
    {
        return JsonConvert.SerializeObject(new
        {
            nl = new
            {
                data =
                    "Op de onderstaande plot kunt u de resultaten van de scan zien. Hoe hoger u scoort op een categorie, hoe beter u al bezig bent met de circulaire economie in dat gebied.",
            },
            en = new
            {
                data =
                    "On the plot below you can see the results of the scan. The higher you score on a category, the better you are already doing with the circular economy in that area.",
            }
        });
    }
}