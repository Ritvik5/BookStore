using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Interface;
using RepoLayer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepoLayer.Service
{
    /// <summary>
    /// Admin and User Login/Registration/JwtToken/Forgotpass/Resetpass
    /// </summary>
    public class AdminUserRepo : IAdminUserRepo
    {
        public readonly BookStoreDBContext bookStoreDBContext;
        public readonly IConfiguration configuration;

        public AdminUserRepo(BookStoreDBContext bookStoreDBContext, IConfiguration configuration)
        {
            this.bookStoreDBContext = bookStoreDBContext;
            this.configuration = configuration;
        }
        /// <summary>
        /// Registration 
        /// </summary>
        /// <param name="registerModel"> Registration Model</param>
        /// <returns></returns>
        public UserTable Register(UserRegisModel registerModel)
        {
            try
            {
                UserTable userTable = new UserTable();
                userTable.UserName = registerModel.Name;
                userTable.UserEmailId = registerModel.Email;
                userTable.UserPassword = EncryptedPassword(registerModel.Password);
                userTable.UserPhoneNumber = registerModel.PhoneNumber;
                userTable.UserRole = registerModel.Role;

                bookStoreDBContext.UserTable.Add(userTable);
                bookStoreDBContext.SaveChanges();
                if (userTable != null)
                {
                    return userTable;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Generating JWT(Json Web Token)
        /// </summary>
        /// <param name="email"> Email id </param>
        /// <param name="userAdminId"> User or Admin Id </param>
        /// <param name="role">User or Admin </param>
        /// <returns> JWT token </returns>
        public string GenerateJwtToken(string email, int userAdminId, string role)
        {
            try
            {
                var tokenhandler = new JwtSecurityTokenHandler();

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

                var tokenDescriptions = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Email, email),
                            new Claim("userAdminId",userAdminId.ToString()),
                            new Claim(ClaimTypes.Role, role),
                        }),
                    Expires = DateTime.Now.AddMinutes(120),
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                };

                var token = tokenhandler.CreateToken(tokenDescriptions);
                return tokenhandler.WriteToken(token);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Encrypted User or Admin password
        /// </summary>
        /// <param name="password"> User or Admin password </param>
        /// <returns> Encrypted password </returns>
        public static string EncryptedPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return null;
                }
                else
                {
                    byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                    string encryptedPassword = Convert.ToBase64String(storePassword);
                    return encryptedPassword;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Decrypte User Or Admin Password
        /// </summary>
        /// <param name="password"> User or Admin password </param>
        /// <returns> Encrypted password </returns>
        public static string DecryptedPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return null;
                }
                else
                {
                    byte[] encryptedPassword = Convert.FromBase64String(password);
                    string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                    return decryptedPassword;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// User or Admin Login
        /// </summary>
        /// <param name="userLoginModel"> Login Info </param>
        /// <returns> User or Admin info with JWT Token </returns>
        public LoginResultModel Login(UserLoginModel userLoginModel)
        {
            try
            {
                UserTable user = bookStoreDBContext.UserTable.FirstOrDefault(u => u.UserEmailId == userLoginModel.Email);

                if (user == null)
                {
                    return null;
                }

                string decryptedPassword = DecryptedPassword(user.UserPassword);

                if (decryptedPassword == userLoginModel.Password)
                {
                    var token = GenerateJwtToken(userLoginModel.Email, user.UserId, user.UserRole);
                    LoginResultModel loginResult = new LoginResultModel
                    {
                        Token = token,
                        Name = user.UserName,
                        Email = user.UserEmailId,
                        Password = user.UserPassword,
                        PhoneNumber = user.UserPhoneNumber,
                        Role = user.UserRole,
                    };
                    return loginResult;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="forgotPassModel"> Email Id </param>
        /// <returns> JWT Token </returns>
        public string ForgotPassword(ForgotPassModel forgotPassModel)
        {
            try
            {
                UserTable user = bookStoreDBContext.UserTable.FirstOrDefault(u => u.UserEmailId == forgotPassModel.Email);

                if (user == null)
                {
                    return null;
                }

                var token = GenerateJwtToken(forgotPassModel.Email, user.UserId, user.UserRole);
                return token;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="resetPassModel"> new and confirm password </param>
        /// <param name="email"> User or Admin Email </param>
        /// <returns> Boolean Value </returns>
        public bool ResetPassword(ResetPassModel resetPassModel, string email)
        {
            try
            {
                UserTable user = bookStoreDBContext.UserTable.FirstOrDefault(u => u.UserEmailId == email);
                if (user != null && resetPassModel.NewPassword == resetPassModel.ConfirmPassword)
                {
                    user.UserPassword = EncryptedPassword(resetPassModel.NewPassword);
                    bookStoreDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
