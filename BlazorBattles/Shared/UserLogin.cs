using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattles.Shared
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Kullanıcıadı giriniz.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifre giriniz.")]
        public string Password { get; set; }
    }
}
