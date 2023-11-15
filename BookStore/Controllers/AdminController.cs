using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepoLayer.Models;
using System.Linq;
using System;

namespace BookStore.Controllers
{
    /// <summary>
    /// Admin Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminUserBusiness adminBusiness;
        private readonly BookStoreDBContext bookStoreDBContext;
        private readonly ILogger<AdminController> logger;
        public AdminController(IAdminUserBusiness adminBusiness,BookStoreDBContext bookStoreDBContext,ILogger<AdminController> logger)
        {
            this.adminBusiness = adminBusiness;
            this.bookStoreDBContext = bookStoreDBContext;
            this.logger = logger;
        }
        /// <summary>
        /// Admin Registration
        /// </summary>
        /// <param name="userRegisModel"> Admin Info for new Registration</param>
        /// <returns> SMD(Status,Message,Data(Admin info)) </returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("register")]
        public IActionResult AdminRegister(UserRegisModel userRegisModel)
        {
            try
            {
                var result = adminBusiness.Register(userRegisModel);
                if (result != null)
                {
                    return Ok(new { success = true, Message = "Admin Registered Successfully.", data = result});
                }
                else
                {
                    return BadRequest(new { success = false, Message = "Admin Registration Failed."});
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex ,"Error during Admin registration");
                return BadRequest(new { Success = false, Message = "Admin registration can not be completed" });
            }
        }
        /// <summary>
        /// Admin Login
        /// </summary>
        /// <param name="userLoginModel"> Login info </param>
        /// <returns> SMD(Status,Message,Data(Admin info)) </returns>
        [HttpPost]
        [Route("login")]
        public IActionResult AdminLogin(UserLoginModel userLoginModel)
        {
            try
            {
                var user = bookStoreDBContext.UserTable.FirstOrDefault(u => u.UserEmailId == userLoginModel.Email);
                if (user != null && user.UserRole == "Admin")
                {
                    var result = adminBusiness.Login(userLoginModel);
                    if(result != null)
                    {
                        return Ok(new { success = true, Message = "Admin Login Successfully.", data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, Message = "Admin Login Failed." });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, Message = "Admin Login Failed. User does not have the Admin role." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during Admin Login");
                return BadRequest(new { Success = false, Message = "Admin login cannot be completed."});
            }
        }
    }
}
