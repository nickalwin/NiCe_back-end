using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NiCeScanner.Models
{
	public class AdviceCondition
	{
		[Key]
		public long Id { get; set; }

		public int Condition { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		[ForeignKey("AdviceId")]
		public Advice Advice { get; set; } = default!;

		[ForeignKey("QuestionId")]
		public Question Question { get; set; } = default!;
	}

}
