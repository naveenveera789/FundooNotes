using CommonLayer.User;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FundooDbContext dbContext;
        public UserRL(FundooDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void RegisterUser(UserPostModel userPostModel)
        {
            try
            {
                User user = new User();
                user.userId = new User().userId;
                user.firstName = userPostModel.firstName;
                user.lastName = userPostModel.lastName;
                user.email = userPostModel.email;
                user.phoneNumber = userPostModel.phoneNumber;
                user.address = userPostModel.address;
                user.password = StringCipher.Encrypt(userPostModel.password);
                user.cPassword = StringCipher.Encrypt(userPostModel.cPassword);
                user.registeredDate = DateTime.Now;
                user.modifiedDate = DateTime.Now;
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
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
                User user = new User();
                var result = dbContext.Users.Where(x => x.email == userLogIn.email && x.password == userLogIn.password).FirstOrDefault();
                if (result != null)
                    return GenerateJWTToken(userLogIn.email, user.userId);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private static string GenerateJWTToken(string email, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public void ResetPassword(string email, string password, string cPassword)
        {
            try
            {
                User user = new User();
                var result = dbContext.Users.FirstOrDefault(x => x.email == email);
                if (result != null)
                {
                    result.password = password;
                    result.cPassword = cPassword;
                    dbContext.SaveChanges();
                }
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
                User user = new User();
                var result = dbContext.Users.Where(x => x.email == email).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<User> GetAllUsers()
        {
            try
            {
                var result = dbContext.Users.ToList();
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}