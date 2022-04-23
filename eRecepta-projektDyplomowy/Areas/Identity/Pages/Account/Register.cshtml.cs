using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using eRecepta_projektDyplomowy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace eRecepta_projektDyplomowy.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [EmailAddress (ErrorMessage = "Nieprawid�owy adres Email.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [StringLength(100, ErrorMessage = "{0} musi mie� co najmniej {2} i maksymalnie {1} znak�w.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Has�o")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Powt�rz has�o")]
            [Compare("Password", ErrorMessage = "Wpisane has�a s� niezgodne.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [DataType(DataType.Text)]
            [Display(Name = "Imi�")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [DataType(DataType.Text)]
            [Display(Name = "Nazwisko")]
            public string Surname { get; set; }

            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [RegularExpression(@"^[\d]{11}$", ErrorMessage = "{0} musi si� sk�ada� z 11 cyfr.")]
            [DataType(DataType.Text)]
            [Display(Name = "PESEL")]
            public string PESEL { get; set; }

            [Phone(ErrorMessage = "Nieprawid�owy numer telefonu.")]
            [Display(Name = "Numer telefonu")]
            public string PhoneNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Name = Input.Name, Surname = Input.Surname, PESEL = Input.PESEL, PhoneNumber = Input.PhoneNumber };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "eRecepta - zatwierdzenie adresu Email",
                        $"Aby potwierdzi� sw�j adres email i aktywowa� swoje konto w serwisie eRecepta, prosz� kliknij <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>tutaj</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
