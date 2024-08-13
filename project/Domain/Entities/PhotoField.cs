using System.ComponentModel.DataAnnotations;

namespace project.Domain.Entities
{
	public class PhotoField :EntityBase
	{
		[Required(ErrorMessage = "Заповніть назву розділу")]
		[Display(Name = "Назва розділу(заголовок)")]
		public override string? Title { get; set; }

		[Display(Name = "Титульне зображення")]
		public virtual string? TitleImagePath { get; set; }
		public byte[]? PhotoData { get; set; }
	}
}
