namespace NiCeScanner.Models
{
	public class QuestionForm
	{
		public long Id { get; set; }
		public Guid Uuid { get; set; }
		public string Data { get; set; }
		public long CategoryId { get; set; }
		public short Weight { get; set; }
		public bool Statement { get; set; }
		public bool Show { get; set; }
		public long ImageId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}

}
