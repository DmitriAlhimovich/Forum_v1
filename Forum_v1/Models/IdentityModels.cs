using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum_v1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ClientName { set; get; }
        public string CompanyName { set; get; }
        public DateTime DateOfRegistration { set; get; }
        public bool Ban { set; get; }

        public ApplicationUser() 
        {
            DateOfRegistration = DateTime.Now;
            Ban = false;       
        }
    }



    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();  
        }

    }
}
