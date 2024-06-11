using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class LinkForm
	{
		public long Id { get; set; }

		[MaxLength(400)]
		public string Name { get; set; }

		[MaxLength(400)]
		public string Href { get; set; }

		public long CategoryId { get; set; }
	}

}
