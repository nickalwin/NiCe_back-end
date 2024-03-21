using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class ScanCode
	{
		[Key]
		public long Id { get; set; }
		
		public Guid Code { get; set; }

		public bool CanEdit { get; set; }

		public long ScanId { get; set; }


		public Scan Scan { get; set; }
	}
}
