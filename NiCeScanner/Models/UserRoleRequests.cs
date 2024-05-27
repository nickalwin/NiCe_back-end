using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class UserRoleRequests
	{
		[Key]
		public string Id { get; set; } = new Guid().ToString();
		public string User { get; set; }
		public string Role { get; set; }
		public string? HandledBy { get; set; }
		public DateTime RequestedOn { get; set; }
		public bool? Status { get; set; }
		public string Reason { get; set; }
	}
}
