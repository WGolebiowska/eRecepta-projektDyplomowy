using System.ComponentModel.DataAnnotations;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class UserModel
    {
        public string Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression(@"^[\d]{11}$", ErrorMessage = "{0} musi się składać z 11 cyfr.")]
        [DataType(DataType.Text)]
        [Display(Name = "PESEL")]
        public string PESEL { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [EmailAddress()]
        [Display(Name = "Adres Email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Nieprawidłowy numer telefonu.")]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [StringLength(100, ErrorMessage = "{0} musi mieć co najmniej {2} i maksymalnie {1} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Wpisane hasła są niezgodne.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Rola")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Zatwierdzone")]
        public bool Approved { get; set; }
    }

    public class UpdateUserModel
    {
        public string Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [RegularExpression(@"^[\d]{11}$", ErrorMessage = "{0} musi się składać z 11 cyfr.")]
        [DataType(DataType.Text)]
        [Display(Name = "PESEL")]
        public string PESEL { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [EmailAddress()]
        [Display(Name = "Adres Email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Nieprawidłowy numer telefonu.")]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Rola")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Pole {0} jest wymagane.")]
        [Display(Name = "Zatwierdzone")]
        public bool Approved { get; set; }
    }
}
