﻿using Microsoft.AspNetCore.Cors.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CustmeWebApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Назва роботи обов'язкова")]
        [Display(Name = "Назва")]
        [MaxLength(100)]
        public string? Title { get; set; }

        [Display(Name = "Опис")]
        [MaxLength(500)]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата створення")]
        public DateTime DateCompleted { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Ціна")]
        public decimal? Price { get; set; }

        [Display(Name = "Посилання на фото")]
        public string? ImagesUrl { get; set; }

        [Required(ErrorMessage = "Ціна обов'язкова")]
        [Display(Name = "Ціна")]
        [DataType(DataType.Currency)]

        public int ServiceId { get; set; }

        public virtual Service Service { get; set; } = null!;
    }
}
