using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NiCeScanner.Models
{
	public class Sector
	{
		[Key]
		public long Id { get; set; }

		public string Name { get; set; }


		public Scan Scan { get; set; }
	}
}
