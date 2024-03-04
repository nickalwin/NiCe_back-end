using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NiCeScanner.Models
{
	public class Question
	{
		public Question()
		{
			Uuid = Guid.NewGuid();
		}

		[Key]
		public long Id { get; set; }

		public Guid Uuid { get; set; }

		public string Data { get; set; }

		[ForeignKey("Category")]
		public long CategoryId { get; set; }

		public short Weight { get; set; }

		public bool Statement { get; set; }

		public bool Show { get; set; }

		public string Image { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }


		public Category Category { get; set; }
	}
}
