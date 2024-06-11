namespace NiCeScanner.Models
{
	public class QuestionForm
	{
		public long Id { get; set; }

		public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string> Questions { get; set; } = new Dictionary<string, string>();
		public Dictionary<string, string> Tooltips { get; set; } = new Dictionary<string, string>();
		public long CategoryId { get; set; }
		public short Weight { get; set; }
		public bool Statement { get; set; }
		public bool Show { get; set; }
		public long? ImageId { get; set; }
	}

}
