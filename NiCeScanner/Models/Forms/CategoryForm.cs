using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class CategoryForm
	{
		public long Id { get; set; }
		
		public string Data { get; set; }

		public string Color { get; set; }

		public bool Show { get; set; }
	}
}
