using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class Advice
	{
		[Key]
		public long Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }
	}

}
