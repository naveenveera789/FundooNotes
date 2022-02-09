﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.User
{
    public class UserLogIn
    {
        [Required]
        [RegularExpression(@"^[a-z0-9]+(.[a-z0-9]+)?@[a-z]+[.][a-z]{3}$",
         ErrorMessage = "Please enter correct Email address")]
        public string email { get; set; }
        [Required]
        [RegularExpression("^(?=.*[A-Z]).{8,}$",
         ErrorMessage = "Password Have minimum 8 Characters, Should have at least 1 Upper Case and Should have at least 1 numeric number and Has exactly 1 Special Character")]
        public string password { get; set; }
    }
}
