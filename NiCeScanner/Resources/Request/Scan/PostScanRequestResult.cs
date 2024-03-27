namespace NiCeScanner.Resources.Request.Scan
{
	public class PostScanRequestResult
	{
		public required Guid Uuid { get; set; }

		public required Guid Edit_code { get; set; }

		public required Guid View_code { get; set; }
	}
}
