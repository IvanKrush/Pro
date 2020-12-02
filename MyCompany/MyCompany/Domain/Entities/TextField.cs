using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompany.Domain.Entities
{
    public class TextField : EntityBase
    {
        [Required]
        public string CodeWord { get; set; }

        [Display(Name = "Назва сторінки (заголовок) ")]
        public override string Title { get; set; } = "Інформаційне середовище";

        [Display(Name = "Наповнення сторінки")]
        public override string Text { get; set; } = "Заповнення змінюється адміністратором";

    }
}
