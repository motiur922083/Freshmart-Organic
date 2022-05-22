using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Organic_Food_01_EXM.Data;
using Organic_Food_01_EXM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Organic_Food_01_EXM.Areas.Customer.Controllers
{
    [Area("Dashboard")]
    public class UserController : Controller
    {
        ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        
        public UserController(ApplicationDbContext db,UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,ILogger<RegisterModel> logger,IEmailSender emailSender)
        {
            _userManager = userManager;
            _db = db;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View(_db.ApplicationUsers.ToList());
        }

        ////Create Register Page
        //public async Task<IActionResult> Register(string returnUrl = null)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(ApplicationUser user, string returnUrl = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _userManager.CreateAsync(user, user.PasswordHash);
        //        if (result.Succeeded)
        //        {
        //            var isSaveRole = await _userManager.AddToRoleAsync(user, "User");
        //            _logger.LogInformation("User created a new account with password.");

        //            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //            var callbackUrl = Url.Page(
        //                "/Account/ConfirmEmail",
        //                pageHandler: null,
        //                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
        //                protocol: Request.Scheme);

        //            await _emailSender.SendEmailAsync(user.UserName, "Confirm your email",
        //                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //            if (_userManager.Options.SignIn.RequireConfirmedAccount)
        //            {
        //                return RedirectToPage("RegisterConfirmation", new { email = user.UserName, returnUrl = returnUrl });
        //            }
        //            else
        //            {
        //                await _signInManager.SignInAsync(user, isPersistent: false);
        //                return LocalRedirect(returnUrl);
        //            }
        //            //TempData["save"] = "User has been create Successfully";
        //            //return RedirectToAction(nameof(Index));
        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }
        //    return View();
        //}

        //Edit Action User
        public async Task<IActionResult> Edit(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            var userinfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userinfo == null)
            {
                return NotFound();
            }
            userinfo.FirstName = user.FirstName;
            userinfo.LastName = user.LastName;
            var result = await _userManager.UpdateAsync(userinfo);
            if (result.Succeeded)
            {
                TempData["save"] = "User has been updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userinfo);
        }
        //Details Action User
        public async Task<IActionResult> Details(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        //Lockout Action User
        public async Task<IActionResult> Lockout(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Lockout(ApplicationUser user)
        {
            var userinfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userinfo == null)
            {
                return NotFound();
            }
            userinfo.LockoutEnd = DateTime.Now.AddYears(10);
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {
                TempData["save"] = "User has been updated lockout successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userinfo);
        }
        //Active User Action User
        public async Task<IActionResult> Active(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Active(ApplicationUser user)
        {
            var userinfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userinfo == null)
            {
                return NotFound();
            }
            userinfo.LockoutEnd = null; //DateTime.Now.AddDays(-1)
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {
                TempData["save"] = "User has been updated Active successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userinfo);
        }
        //Delete User Action User
        public async Task<IActionResult> Delete(string id)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser user)
        {
            var userinfo = _db.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userinfo == null)
            {
                return NotFound();
            }
            _db.ApplicationUsers.Remove(userinfo);
            int rowAffected = _db.SaveChanges();
            if (rowAffected > 0)
            {
                TempData["save"] = "User has been Delete successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userinfo);
        }
    }
}
