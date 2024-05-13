namespace NiCeScanner.Resources.API
{
	public class QuestionResource
	{
		public required Guid Uuid { get; set; }
		public required string Data { get; set; }
		public required Guid Category_uuid { get; set; }
		public required string Category_data { get; set; }

		public required string Category_color { get; set; }
		public required bool Statement { get; set; }
		public string? Image_data { get; set; }
	}
}
