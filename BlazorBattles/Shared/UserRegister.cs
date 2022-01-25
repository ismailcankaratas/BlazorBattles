using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattles.Shared
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Email giriniz."), EmailAddress(ErrorMessage = "E-posta alanı geçerli bir e-posta adresi değil.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Kullanıcıadı giriniz."), StringLength(16, ErrorMessage = "Kullanıcı adınız çok uzun (en fazla 16 karakter).")]
        public string Username { get; set; }
        public string Bio { get; set; }
        [Required(ErrorMessage = "Şifre giriniz."), StringLength(100, MinimumLength = 6, ErrorMessage = "Parola alanı, minimum uzunluğu 6 ve maksimum uzunluğu 100 olan bir dize olmalıdır.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Şifre eşleşmiyor")]
        public string ConfirmPassword { get; set; }
        public int StartUnitId { get; set; } = 1;
        [Range(0, 1000, ErrorMessage = "Lütfen 0 ile 1000 arasında bir sayı seçin.")]
        public int Bananas { get; set; } = 100;
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Range(typeof(bool), "true", "true", ErrorMessage = "Sadece onaylayan kullanıcılar oynayabilir!")]
        public bool IsConfirmed { get; set; } = true;


    }
}
