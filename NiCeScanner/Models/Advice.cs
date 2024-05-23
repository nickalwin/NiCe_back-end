using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NiCeScanner.Models
{
	public class Advice
	{
		[Key]
		public long Id { get; set; }
		public string Data { get; set; } = default!;

		public string AdditionalLink { get; set; } = default!;

		public string AdditionalLinkName { get; set; } = default!;

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		public ICollection<AdviceCondition> AdviceConditions { get; set; } = default!;
	}

}
