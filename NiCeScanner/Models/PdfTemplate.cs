using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class PdfTemplate
	{
		[Key]
		public int Id { get; set; }
		
		public string Title { get; set; }
		
		public string Introduction { get; set; }
		
		public byte[] ImageData { get; set; }
		
		public string? BeforePlotText { get; set; }
		
		public string? AfterPlotText { get; set; }
	}
}
