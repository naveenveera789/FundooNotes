using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        void RegisterUser(UserPostModel userPostModel);
        string LogInUser(UserLogIn userLogIn);
        void ResetPassword(string email, string password, string cPassword);
        void ForgetPassword(string email);
        List<User> GetAllUsers();
    }
}
