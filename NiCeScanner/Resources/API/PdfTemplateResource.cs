namespace NiCeScanner.Resources.API
{
	public class PdfTemplateResource
	{
		public required string Title { get; set; }
		
		public required string Introduction { get; set; }
		
		public required string ImageData { get; set; }
		
		public required string? BeforePlotText { get; set; }
		
		public required string? AfterPlotText { get; set; }
	}
}
