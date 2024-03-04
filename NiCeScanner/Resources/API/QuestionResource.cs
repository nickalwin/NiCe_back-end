namespace NiCeScanner.Resources.API
{
	public class QuestionResource
	{
		public Guid Uuid { get; set; }
		public string Data { get; set; }
		public Guid Category_uuid { get; set; }
		public string Category_name { get; set; }
		public bool Statement { get; set; }
		public string? Image { get; set; }
	}
}
