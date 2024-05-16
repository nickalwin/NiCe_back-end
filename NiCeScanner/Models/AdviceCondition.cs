using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class AdviceCondition
	{
		[Key]
		public long Id { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }
	}

}
