using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NiCeScanner.Models
{
	public class Answer
	{
		[Key]
		public long Id { get; set; }

		[ForeignKey("Scan")]
		public long ScanId { get; set; }

		[ForeignKey("Question")]
		public long QuestionId { get; set; }

		public short Score { get; set; }

		public string Comment { get; set; }


		public Scan Scan { get; set; }

		public Question Question { get; set; }
	}
}
