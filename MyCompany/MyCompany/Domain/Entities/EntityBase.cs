using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.Domain.Entities
{
    public abstract class EntityBase
    {

        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Назва (Заголовок)")]
        public virtual string Title { get; set; }

        [Display(Name = "Краткое описание")]
        public virtual string Subtitle { get; set; }

        [Display(Name = "Полное описание")]
        public virtual string Text { get; set; }

        [Display(Name = "Назва (Заголовок)")]
        public virtual string TitleImagePath { get; set; }

        [Display(Name = "SEO  (Description")]
        public string MetaDescription { get; set; }

        [Display(Name = "SEO (Keywords)")]
        public virtual string MetaKeywords { get; set; }

        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}
