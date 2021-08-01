using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum_v1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum_v1.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        


        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }




        [Authorize(Roles = "admin")]
        // GET: Admin
        public ActionResult Index()
        {
            return View(_userManager.Users);
        }




        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Info(string Id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                IList<string> _rolelist = await _userManager.GetRolesAsync(user);

                AdminInfo model = new AdminInfo { UserName = user.ClientName, UserEmail = user.Email, UserRoleList = _rolelist, CompanyName = user.CompanyName };

                ViewBag.id = user.Id;

                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Что-то пошло не так");
            }

            return RedirectToAction("Index");

        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(string Id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                IList<string> _userrolelist = await _userManager.GetRolesAsync(user);


                List<IdentityRole> _appprolelist = _roleManager.Roles.ToList();


                IList<string> _availableroles = new List<string>();

                foreach (IdentityRole r in _appprolelist)
                {
                    _availableroles.Add(r.Name);
                }


                AdminEdit model = new AdminEdit
                {
                    UserName = user.ClientName,
                    UserEmail = user.Email,
                    UserRoleList = _userrolelist,
                    RoleList = _availableroles,
                    CompanyName = user.CompanyName
                };

                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Что-то пошло не так");
            }

            return RedirectToAction("Index");

        }




        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddRoleToUser(string Email, string Rolename)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(Email);

            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, Rolename);
                return RedirectToAction("Edit", "Admin", new { Id = user.Id });
            }
            else
            {
                ModelState.AddModelError("", "Что-то пошло не так");
            }

            return RedirectToAction("Index");
        }



        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteRoleFromUser(string Email, string Rolename)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(Email);

            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, Rolename);
                return RedirectToAction("Edit", "Admin", new { Id = user.Id });
            }
            else
            {
                ModelState.AddModelError("", "Что-то пошло не так");
            }

            return RedirectToAction("Index");

        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteUser()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string Id)
        {

            ApplicationUser user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                //если удаляемый пользователь - админ удаляет сам себя, то вызывается метод AdminSelfDelete() контролера Account
                if (user.Email == User.Identity.Name)
                {
                    return RedirectToAction("AdminSelfDelete", "Account");
                }
                

                user = await _userManager.FindByIdAsync(Id);

                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }


            }
            return RedirectToAction("CantDeleteUser", new { name = user.Email });

        }



        [Authorize(Roles = "admin")]
        public string CantDeleteUser(string name)
        {
            return "Не могу удалить пользователя:  " + name + "  Что-то пошло не так!";
        }




        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }



        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, ClientName = model.ClientName, CompanyName = model.CompanyName };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    // если создание прошло успешно, то добавляем роль пользователя
                    await _userManager.AddToRoleAsync(user, "user");

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }



        [Authorize(Roles = "admin")]
        public async Task<ActionResult> FindByEmail(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return RedirectToAction("Edit", "Admin", new { Id = user.Id });
            }
            else
            {
                return RedirectToAction("NoUserEmail", "Admin");
            }
        }


        [Authorize(Roles = "admin")]
        public ActionResult NoUserEmail()
        {
            return View();
        }
    }
}