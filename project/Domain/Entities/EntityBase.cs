using System.ComponentModel.DataAnnotations;

namespace project.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            DateAdded = DateTime.UtcNow;
        }

        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Назва(заголовок)")]
        public virtual string Title { get; set; } = "Зміст заповнюється адміністратором";


		[Display(Name = "Повний опис")]
        public virtual string Text { get; set; } = "Зміст заповнюється адміністратором";


		[DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}
