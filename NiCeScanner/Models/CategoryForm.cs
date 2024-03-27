using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class CategoryForm
	{
		public CategoryForm()
		{
			CreatedAt = DateTime.Now;
			UpdatedAt = DateTime.Now;
			Uuid = Guid.NewGuid();
		}

		[Key]
		public long Id { get; set; }

		public Guid Uuid { get; set; }

		[MaxLength(255)]
		public string Name { get; set; }

		public bool Show { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }
	}
}
