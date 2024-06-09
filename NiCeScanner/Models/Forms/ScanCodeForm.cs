using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class ScanCodeForm
	{
		public long Id { get; set; }
		
		public required Guid Code { get; set; }

		public required bool CanEdit { get; set; }

		[Range(1, long.MaxValue)]
		public required long ScanId { get; set; }
	}

}
