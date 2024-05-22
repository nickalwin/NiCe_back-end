using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class LinkForm
	{
		[MaxLength(400)]
		public required string Name { get; set; }

		[MaxLength(400)]
		public required string Href { get; set; }

		public long CategoryId { get; set; }
	}

}
