using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        void RegisterUser(UserPostModel userPostModel);
        string LogInUser(UserLogIn userLogIn);
        void ResetPassword(string email, string password, string cPassword);
        void ForgetPassword(string email);
    }
}
