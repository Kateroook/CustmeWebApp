using Microsoft.AspNetCore.Cors.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CustmeWebApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Назва роботи обов'язкова")]
        [Display(Name = "Назва")]
        [MaxLength(100)]
        public string? Title { get; set; }

        [Display(Name = "Опис")]
        [MaxLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Тип")]
        [MaxLength(50)]
        public string? Type { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата створення")]
        public DateTime DateCompleted { get; set; }

        [Display(Name = "Посилання на фото")]
        public string ImagesUrl { get; set; }

        public int ServiceId { get; set; }


        public virtual Service Service { get; set; } = null!;
    }
}
