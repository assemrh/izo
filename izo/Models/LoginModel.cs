using System;

namespace izo.Models
{
    public class LoginModel
    {
        public String Email { get; set; }
        public String Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
