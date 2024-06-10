namespace NiCeScanner.Models
{
	public class CategoryForm
	{
		public long Id { get; set; }
		
		public Dictionary<string, string> Languages { get; set; } = new Dictionary<string, string>();
		
		public string Color { get; set; }

		public bool Show { get; set; }
	}
}
