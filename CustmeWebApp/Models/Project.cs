using Microsoft.AspNetCore.Cors.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CustmeWebApp.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва проєкту обов'язкова")]
        [Display(Name = "Проєкт")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Display(Name = "Опис")]
        [MaxLength(500)]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата створення")]
        public DateTime? DateCompleted { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Ціна")]
        public decimal? Price { get; set; }

        [Display(Name = "Посилання на фото")]
        public string? ImagesUrl { get; set; }

        
        //[Required(ErrorMessage="Послуга є обов'язковою")]
        [Display(Name = "Послуга")]
        public int ServiceId { get; set; }

        [Display(Name = "Послуга")]
        public virtual Service Service { get; set; }
    }
}
