using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Organic_Food_01_EXM.Areas.Admin.Models;
using Organic_Food_01_EXM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organic_Food_01_EXM.Areas.Admin.Controllers
{
    [Area("Dashboard")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }
        //Create New Role 
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole role = new IdentityRole();
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.message = "This Role is already exist!!";
                ViewBag.name = name;
            }
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "New Role Add successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //Edit Role 
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role==null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.message = "This Role is already exist!!";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "User has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //Delete Role 
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["delete"] = "Role delete successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //Assign Role To User Page 
        public async Task<IActionResult> Assign(string id)
        {
            ViewData["UserId"] = new SelectList(_db.ApplicationUsers.Where(c=>c.LockoutEnd<DateTime.Now || c.LockoutEnd==null).ToList(), "Id", "UserName");
            ViewData["RoleId"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Assign(RoleUserVM roleUser)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c=>c.Id==roleUser.UserId);
            var isCheckRoleAssign =await _userManager.IsInRoleAsync(user,roleUser.RoleId);
            if (isCheckRoleAssign)
            {
                ViewBag.message = "This user already assign this role!";
                ViewData["UserId"] = new SelectList(_db.ApplicationUsers.Where(c => c.LockoutEnd < DateTime.Now || c.LockoutEnd == null).ToList(), "Id", "UserName");
                ViewData["RoleId"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
                return View();
            }
            var role = await _userManager.AddToRoleAsync(user, roleUser.RoleId);
            if (role.Succeeded)
            {
                TempData["save"] = "Role assign successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
     
        //User & Role & UserRole Table Join Linq
        public ActionResult AssignUserRole()
        {
            var result = from ur in _db.UserRoles
                         join r in _db.Roles on ur.RoleId equals r.Id
                         join a in _db.ApplicationUsers on ur.UserId equals a.Id
                         select new UserRoleMapping()
                         {
                             UserId = ur.UserId,
                             RoleId = ur.RoleId,
                             UserName = a.UserName,
                             RoleName=r.Name
                         };
            ViewBag.UserRoles = result;
            return View();
        }
    }
}
