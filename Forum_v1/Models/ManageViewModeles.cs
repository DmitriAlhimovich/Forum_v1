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



    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }


}
