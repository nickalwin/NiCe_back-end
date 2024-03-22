namespace NiCeScanner.Resources.API
{
	public class ValidatedScanCodeResource
	{
		public required Guid Scan_uuid { get; set; }

		public required bool Editable { get; set; }
	}
}
