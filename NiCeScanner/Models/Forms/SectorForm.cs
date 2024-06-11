namespace NiCeScanner.Models
{
	public class SectorForm
	{
		public long Id { get; set; }

		public Dictionary<string, string> Languages { get; set; } = new Dictionary<string, string>();
	}
}
