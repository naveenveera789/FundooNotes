using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.User
{
    public class UserPostModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string cPassword { get; set; }
    }
}
