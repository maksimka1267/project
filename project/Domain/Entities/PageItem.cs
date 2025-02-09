using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace project.Domain.Entities
{
    public class PageItem
    {
        public Guid Id { get; set; }

        [Display(Name = "Назва сторінки")]
        [Required(ErrorMessage = "Обов'язково заповніть назву сторінки")]
        public string Title { get; set; }

        [Display(Name = "Зміст сторінки")]
        public Guid Text { get; set; }

        [Display(Name = "Батьківська сторінка")]
        public Guid? Father { get; set; }

        [Display(Name = "Виберіть шаблон сторінки")]
		[Required(ErrorMessage = "Обов'язково виберіть шаблон сторінки")]
		public string View { get; set; }

		[Display(Name = "Чи є дочерні сторінки")]
		public bool? Child { get; set; }

        [Display(Name =" Порядок вивидення")]
        public int Number {  get; set; }
        public bool Index {  get; set; } = false;

        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; } = DateTime.Now;

    }
}
