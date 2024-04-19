using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NiCeScanner.Models
{
	public class Scan
	{
		public Scan()
		{
			Uuid = Guid.NewGuid();
		}

		[Key]
		public long Id { get; set; }

		public Guid Uuid { get; set; }

		public string ContactName { get; set; }

		public string ContactEmail { get; set; }

		[ForeignKey("Sector")]
		public long SectorId { get; set; }

		private string _results;
		public string Results
		{
			get { return _results; }
			set { _results = value ?? "N/A"; } 
		}

		public DateTime CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }


		public Sector Sector { get; set; }

		public ICollection<Answer> Answers { get; set; }


	}
}
