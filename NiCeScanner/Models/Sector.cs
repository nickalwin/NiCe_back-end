using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class Sector
	{
		[Key]
		public long Id { get; set; }

		public string Data { get; set; }


		public Scan Scan { get; set; }
	}
}
