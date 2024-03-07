using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class Opinion
	{
		[Key]
		public long Id { get; set; }
		
		public short Score { get; set; }
		
		public string? Feedback { get; set; }
		
		public DateTime CreatedAt { get; set; }
	}
}
