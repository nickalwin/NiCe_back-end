using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NiCeScanner.Models
{
	public class Link
	{
		[Key]
		public long Id { get; set; }

		[MaxLength(400)]
		public string Name { get; set; } = default!;

		[MaxLength(400)]
		public string Href { get; set; } = default!;

		public long CategoryId { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		[ForeignKey("CategoryId")]
		public Category Category { get; set; } = default!;
	}

}
