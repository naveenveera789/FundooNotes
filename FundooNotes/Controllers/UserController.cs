using BusinessLayer.Interface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        FundooDbContext dbContext;
        public UserController(IUserBL userBL, FundooDbContext fundooDb)
        {
            this.userBL = userBL;
            this.dbContext = fundooDb;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.RegisterUser(userPostModel);
                return this.Ok(new { success = true, message = $"Registration Successful {userPostModel.email}" });
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost("login")]
        public ActionResult LogInUser(UserLogIn userLogIn)
        {
            try
            {
                string result = this.userBL.LogInUser(userLogIn);
                return this.Ok(new { success = true, message = $"LogIn Successful {userLogIn.email}, data = {result}" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut("forgetpassword")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                this.userBL.ForgetPassword(email);
                return this.Ok(new { success = true, message = "Token sent succesfully to email for password reset" });
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [AllowAnonymous]
        [HttpPut("resetpassword")]
        public ActionResult ResetPassword(string email, string password, string cPassword)
        {
            try
            {
                if (password != cPassword)
                {
                    return BadRequest(new { success = false, message = $"Paswords are not same" });
                }
                this.userBL.ResetPassword(email, password, cPassword);
                return this.Ok(new { success = true, message = $"Password changed Successfully {email}" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("getallusers")]
        public ActionResult GetAllUsers()
        {
            try
            {
                var result = this.userBL.GetAllUsers();
                return this.Ok(new { success = true, message = $"Below are the User data", data = result });
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}