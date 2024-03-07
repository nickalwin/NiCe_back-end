namespace NiCeScanner.Resources.Request
{
	public class AnswerElement
	{
		public Guid Question_uuid { get; set; }

		public short Answer { get; set; }

		public string? Comment { get; set; }
	}

	public class PostScanRequest
	{
		public long Sector_id { get; set; }

		public string Contact_name { get; set; } = default!;

		public string Contact_email { get; set; } = default!;

		public IEnumerable<AnswerElement> Answers { get; set; } = default!;
	}
}
