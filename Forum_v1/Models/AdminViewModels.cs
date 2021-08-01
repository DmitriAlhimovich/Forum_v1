using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum_v1.Models
{
    public class AdminEdit
    {
        [DisplayName("Имя:")]
        public string UserName { set; get; }


        [Display(Name = "Компания:")]
        public string CompanyName { set; get; }


        [DisplayName("Email:")]
        public string UserEmail { set; get; }


        [DisplayName("Роли пользователя:")]
        public IList<string> UserRoleList { set; get; }


        [DisplayName("Доступные роли:")]
        public IList<string> RoleList { set; get; }


        public AdminEdit()
        {
            UserRoleList = new List<string>();
            RoleList = new List<string>();
        }
    }




    public class AdminInfo
    {
        [Display(Name = "Имя:")]
        public string UserName { set; get; }


        [Display(Name = "Компания:")]
        public string CompanyName { set; get; }


        [Display(Name = "Email:")]
        public string UserEmail { set; get; }


        [Display(Name = "Роли пользователя:")]
        public IList<string> UserRoleList { set; get; }


        public AdminInfo()
        {
            UserRoleList = new List<string>();
        }
    }
}
