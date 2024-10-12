using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustmeWebApp.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ім'я обов'язкове")]
        [Display(Name = "Назва")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Тип послуги обов'язковий")]
        [Display(Name = "Тип")]
        public string? Type { get; set; } //shirt, bag, skirt etc.

        [Display(Name = "Опис")]
        [StringLength(1000, ErrorMessage ="Текст опису не повинен перевищувати 1 тис. символів")]
        public string? Description { get; set; }

        [Required(ErrorMessage ="Ціна обов'язкова")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [Display(Name = "Посилання на зображення")]
        public string? ImageUrl { get; set; }

        //Slug read only property
        public string Slug => $"(Name)-(Type)".ToLower().Replace(" ", "-");

    }
}
