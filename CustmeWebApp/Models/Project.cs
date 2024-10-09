using Microsoft.AspNetCore.Cors.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CustmeWebApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Назва роботи обов'язкова")]
        [Display(Name = "Назва")]
        public string? Title { get; set; }

        [Display(Name = "Опис")]
        public string? Description { get; set; }

        [Display(Name = "Тип")]
        public string? Type { get; set; }

        [Display(Name = "Дата створення")]
        public DateTime DateCompleted { get; set; }

        [Display(Name = "Посилання на фото")]
        public string ImagesUrl { get; set; }

        public int ServiceId { get; set; }


        public virtual Service Service { get; set; } = null!;
    }
}
