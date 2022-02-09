using BusinessLayer.Interface;
using CommonLayer.User;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
                if(result != null)
                {
                    return this.Ok(new { success = true, message = $"LogIn Successful {userLogIn.email}, data = {result}" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = $"Enter Valid Email and Password" });
                }
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
                var result = dbContext.Users.FirstOrDefault(x => x.email == email);
                if(result == null)
                {
                    return this.BadRequest(new { success = false, message = "Email is invalid" });
                }
                else
                {
                    this.userBL.ForgetPassword(email);
                    return this.Ok(new { success = true, message = "Token sent succesfully to email for password reset" });
                }              
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [AllowAnonymous]
        [HttpPut("resetpassword")]
        public ActionResult ResetPassword(string password, string cPassword)
        {
            try
            {
                if (password != cPassword)
                {
                    return BadRequest(new { success = false, message = $"Paswords are not same" });
                }
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var UserEmailObject = claims.Where(p => p.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").FirstOrDefault()?.Value;
                    if(UserEmailObject != null)
                    {
                        this.userBL.ResetPassword(UserEmailObject, password, cPassword);
                        return Ok(new { success = true, message = "Password Changed Sucessfully" });
                    }
                    else
                    {
                        return this.BadRequest(new { success = false, message = $"Password not changed Successfully" });
                    }
                }
                return this.BadRequest(new { success = false, message = $"Password not changed Successfully" });
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