using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{
    public class UserLogIn
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
