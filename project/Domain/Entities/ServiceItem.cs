﻿using System.ComponentModel.DataAnnotations;

namespace project.Domain.Entities
{
	public class ServiceItem : EntityBase
	{
		[Required(ErrorMessage = "Заповніть назву розділу")]
		[Display(Name = "Назва розділу(заголовок)")]
		public override string? Title { get; set; }

		[Display(Name = "Короткий опис")]
		public virtual string? Subtitle { get; set; }

		[Display(Name = "Повний опис")]
		public override string? Text { get; set; }

		[Display(Name = "Батьківська сторінка")]
		[Required(ErrorMessage = "Обов'язково вибиріть батьківську сторінку")]
		public string Father { get; set; }

		[Display(Name = "Стаття для банеру")]
		public bool ShowBanners { get; set; } = false;

		[Display(Name = "Титульне зображення")]
		public byte[]? TitleImage { get; set; }  // Поле для хранения изображения

		[Display(Name = "Створювати сторінки для цієї статті?")]
		public bool MakePage { get; set; }
	}
}
