using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepoLayer.Models;
using System.Linq;
using System.Security.Claims;
using System;

namespace BookStore.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAdminUserBusiness userBusiness;
        private readonly BookStoreDBContext dbContext;
        private readonly ILogger logger;
        public UserController(IAdminUserBusiness userBusiness, BookStoreDBContext dbContext,ILogger logger)
        {
            this.userBusiness = userBusiness;
            this.dbContext = dbContext;
            this.logger = logger;
        }
        /// <summary>
        /// User Registration
        /// </summary>
        /// <param name="userRegisModel"> Info for new User</param>
        /// <returns> SMD(Status,Message,Data(User info)) </returns>
        [HttpPost]
        [Route("register")]
        public IActionResult UserRegister(UserRegisModel userRegisModel)
        {
            try
            {
                var result = userBusiness.Register(userRegisModel);
                if(result != null)
                {
                    return Ok(new {Success = true , Message = "User Registered Successfully.", Data = result});
                }
                else
                {
                    return BadRequest(new {Success = false, Message = "User Registration Failed."});
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error come during the User registration");
                return BadRequest(new { Success = false, Message = "User registration could not completed" });
            }
        }
        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="userLoginModel"> login info </param>
        /// <returns>SMD(Status,Message,Data(JWT Token and User info ))</returns>
        [HttpPost]
        [Route("login")]
        public IActionResult UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                var user = dbContext.UserTable.FirstOrDefault(u => u.UserEmailId == userLoginModel.Email);

                if (user != null && user.UserRole == "User")
                {
                    var result = userBusiness.Login(userLoginModel);
                    if (result != null)
                    {
                        return Ok(new { Success = true, Message = "User Login Successfully", Data = result });
                    }
                    else
                    {
                        return BadRequest(new { Success = false, Message = "User login failed." });
                    }
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "User Login Failed. Admin does not have the User role." });
                }
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error come during user login");
                return BadRequest(new { Success = false, Message = "User Login could not be completed" });
            }
        }
        /// <summary>
        /// Forgot password 
        /// </summary>
        /// <param name="model"> email of the user </param>
        /// <returns> JWT Token </returns>
        [HttpPost]
        [Route("forgotpassword")]
        public IActionResult ForgotPass(ForgotPassModel model)
        {
            try
            {
                var result = userBusiness.ForgotPassword(model);
                if (result != null)
                { 
                    return Ok(new { Success = true, Message = "Token sent Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Something went wrong...." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error come during Forget password");
                return BadRequest(new { Success = false, Message = "Forget password could not be completed" });
            }
        }
        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="model"> new password info </param>
        /// <returns> SMD(Status,Message,Data) Format </returns>
        [Authorize(Roles = "User")]
        [HttpPut]
        [Route("resetpassword")]
        public IActionResult ResetPass(ResetPassModel model)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                bool resetPassword = userBusiness.ResetPassword(model,email);
                if (resetPassword)
                {
                    return Ok(new { Success = true, Message = "Password has been updated" });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Password cannot be updated" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error come during Reset Password");
                return BadRequest(new { Success = false, Message = "Reset Password could not be completed" });
            }
        }
    }
}
