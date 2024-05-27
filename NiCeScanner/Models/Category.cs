using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NiCeScanner.Models
{
	public class Category
	{
		public Category()
		{
			Uuid = Guid.NewGuid();
		}

		[Key]
		public long Id { get; set; }

		public Guid Uuid { get; set; }

		public string Data { get; set; }

		public string Color { get; set; }

		public bool Show { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }


		public ICollection<Question> Questions { get; set; }

		public ICollection<Link> Links { get; set; }
	}

}
