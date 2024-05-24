namespace NiCeScanner.Models
{
	public class UserRolesViewModel
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string Roles { get; set; }
		public bool IsPendingRoleRequest { get; set; }
		public string? RequestReason { get; internal set; }
		public string? RequestedRole { get; internal set; }
	}
}
