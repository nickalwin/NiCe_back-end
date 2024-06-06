using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class AdviceForm
	{
		public required string Data { get; set; }

		public string AdditionalLink { get; set; } = default!;

		public string? AdditionalLinkName { get; set; } = default!;

		public int Condition { get; set; }

		public long QuestionId { get; set; }
	}
}
