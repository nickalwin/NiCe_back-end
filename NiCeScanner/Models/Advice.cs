using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NiCeScanner.Models
{
	public class Advice
	{
		[Key]
		public long Id { get; set; }
		public string Data { get; set; } = default!;

		public string AdditionalLink { get; set; } = default!;

		public string AdditionalLinkName { get; set; } = default!;

		public int Condition { get; set; }

		public long QuestionId { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		[ForeignKey("QuestionId")]
		public Question Question { get; set; } = default!;
	}

}
