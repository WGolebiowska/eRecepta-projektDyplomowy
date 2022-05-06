using eRecepta_projektDyplomowy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Login")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone(ErrorMessage = "Nieprawidłowy numer telefonu.")]
            [Display(Name = "Numer telefonu")]
            public string PhoneNumber { get; set; }
            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [DataType(DataType.Text)]
            [Display(Name = "Imię")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [DataType(DataType.Text)]
            [Display(Name = "Nazwisko")]
            public string Surname { get; set; }
            [Required(ErrorMessage = "Pole {0} jest wymagane.")]
            [RegularExpression(@"^[\d]{11}$", ErrorMessage = "{0} musi się składać z 11 cyfr.")]
            [DataType(DataType.Text)]
            [Display(Name = "PESEL")]
            public string PESEL { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Name = user.Name,
                Surname = user.Surname,
                PESEL = user.PESEL,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            if (Input.Name != user.Name)
            {
                user.Name = Input.Name;
            }

            if (Input.Surname != user.Surname)
            {
                user.Surname = Input.Surname;
            }

            if (Input.PESEL != user.PESEL)
            {
                user.PESEL = Input.PESEL;
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Twój profil został zaktualizowany.";
            return RedirectToPage();
        }
    }
}
