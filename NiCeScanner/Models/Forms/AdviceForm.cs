namespace NiCeScanner.Models
{
	public class AdviceForm
	{
		public long Id { get; set; }

		public Dictionary<string, string> Languages { get; set; } = new Dictionary<string, string>();

		public string AdditionalLink { get; set; } = default!;

		public string? AdditionalLinkName { get; set; } = default!;

		public int Condition { get; set; }

		public long QuestionId { get; set; }
	}
}
