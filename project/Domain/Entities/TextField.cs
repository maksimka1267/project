﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace project.Domain.Entities
{
    public class TextField : EntityBase
    {
        [Required(ErrorMessage = "Обов'язково заповніть кодове слово")]
        public string CodeWord { get; set; }

        [Display(Name = "Назва сторінки")]
        [Required(ErrorMessage = "Обов'язково заповніть назву сторінки")]
        public override string Title { get; set; }

        [Display(Name = "Зміст сторінки")]
        [Required(ErrorMessage = "Обов'язково заповніть зміст сторінки")]
        public override string Text { get; set; }

        [Display(Name = "Батьківська сторінка")]
        public string? Father { get; set; }
        [Display(Name = "Виберіть шаблон сторінки")]
		[Required(ErrorMessage = "Обов'язково виберіть шаблон сторінки")]
		public string View { get; set; }

		[Display(Name = "Чи є дочерні сторінки")]
		public bool? Child { get; set; }

        [Display(Name =" Порядок вивидення")]
        public int Number {  get; set; }
	}
}
