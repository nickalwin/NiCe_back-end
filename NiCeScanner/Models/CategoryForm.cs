using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class CategoryForm
	{
		public required long Id { get; set; }
		
		public required string Data { get; set; }

		public required string Color { get; set; }

		public required bool Show { get; set; }
	}
}
