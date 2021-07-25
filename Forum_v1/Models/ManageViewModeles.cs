using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum_v1.Models
{
    public class ManageIndexViewModel
    {
        [Display(Name = "Ваше имя")]
        public string ClientName { get; set; }

        [Display(Name = "Компания:")]
        public string CompanyName { get; set; }

        [EmailAddress]
        [Display(Name = "Адрес электронной почты:")]
        public string Email { get; set; }
    }


    public class ChangeRegisterDataViewModel
    {
        [Required]
        [Display(Name = "Ваше имя")]
        public string ClientName { get; set; }

        [Display(Name = "Компания (не обязательно)")]
        public string CompanyName { get; set; }


        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
    }


}
