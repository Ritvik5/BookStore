using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Models;
using System.Linq;

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
        public AdminController(IAdminUserBusiness adminBusiness,BookStoreDBContext bookStoreDBContext)
        {
            this.adminBusiness = adminBusiness;
            this.bookStoreDBContext = bookStoreDBContext;
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
            catch (System.Exception)
            {

                throw;
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
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
