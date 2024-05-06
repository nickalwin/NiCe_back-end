using System.ComponentModel.DataAnnotations;

namespace NiCeScanner.Models
{
	public class ImageModel
	{
		public ImageModel()
		{
			Uuid = Guid.NewGuid();
		}

		[Key]
		public long Id { get; set; }
		public Guid Uuid { get; set; }

		public string FileName { get; set; }
		public byte[] ImageData { get; set; }

		public ICollection<Question> Questions { get; set; }

	}

}
