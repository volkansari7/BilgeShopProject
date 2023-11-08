using System.ComponentModel.DataAnnotations;

namespace BilgeShop.WebUI.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "Ad")]
        [MaxLength(25, ErrorMessage = "İsim maksimum 25 karakter uzunluğunda olabbilir.")]
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        public string FirstName { get; set; }
        [MaxLength(25, ErrorMessage = "Soyad maksimum 25 karakter uzunluğunda olabbilir.")]
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
        [Display(Name = "Eposta")]
        [Required(ErrorMessage = "Mail alanı boş bırakılamaz.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Şifre Tekrar")]
        [Required(ErrorMessage = "Şifre Tekrar alanı boş bırakılamaz.")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmiyor")]
        public string PasswordConfirm { get; set; }
    }
}
