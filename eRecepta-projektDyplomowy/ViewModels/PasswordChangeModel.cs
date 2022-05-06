using System.ComponentModel.DataAnnotations;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class PasswordChangeModel
    {
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [StringLength(100, ErrorMessage = "{0} musi mieć co najmniej {2} i maksymalnie {1} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Wpisane hasła są niezgodne.")]
        public string ConfirmPassword { get; set; }
    }
}