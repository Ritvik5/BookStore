using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class LoginResultModel
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
