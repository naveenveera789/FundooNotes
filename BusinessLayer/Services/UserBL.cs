using BusinessLayer.Interface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                userRL.RegisterUser(userPostModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string LogInUser(UserLogIn userLogIn)
        {
            try
            {
                return userRL.LogInUser(userLogIn);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public void ResetPassword(string email, string password, string cPassword)
        {
            try
            {
                userRL.ResetPassword(email, password, cPassword);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void ForgetPassword(string email)
        {
            try
            {
                userRL.ForgetPassword(email);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}