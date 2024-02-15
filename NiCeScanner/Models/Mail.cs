using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class Mail
	{
		[Key]
		public int Id { get; set; }
		public string FirsName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string? Phone { get; set; } = null;
		public string Subject { get; set; }
		public string Message { get; set; }
	}
}
