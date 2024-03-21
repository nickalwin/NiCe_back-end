namespace NiCeScanner.Resources.API
{
	class ScanResultElement
	{
		public required string Category_name { get; set; }

		public required Guid Category_uuid { get; set; }

		public required float Mean { get; set; }
	}

	public class GroupedCategoryQuestionsResource
	{
		public required string Question_data { get; set; }

		public required Guid Question_uuid { get; set; }
		
		public required short Answer { get; set; }

		public required string Comment { get; set; }

	}

	public class ScanResultDataResource
	{
		public required Guid Category_uuid { get; set; }

		public required IEnumerable<GroupedCategoryQuestionsResource> Grouped_answers { get; set; }
	}

	public class ScanResource
	{
		public required Guid Uuid { get; set; }

		public required string Contact_name { get; set; }

		public required string Contact_email { get; set; }

		public required string Results { get; set; }

		public required string Average_results { get; set; }

		public required DateTime Created_at { get; set; }

		public required DateTime? Updated_at { get; set; }

		public required IEnumerable<ScanResultDataResource> Data { get; set; }
	}
}
