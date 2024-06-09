using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class ScanForm
	{
		public long Id { get; set; }
		
		[MaxLength(400)]
		public required string ContactName { get; set; }

		[MaxLength(255)]
		[EmailAddress]
		public required string ContactEmail { get; set; }
	}

}
