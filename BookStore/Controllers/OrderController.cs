using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer.Models;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness orderBusiness;
        public OrderController(IOrderBusiness orderBusiness)
        {
            this.orderBusiness = orderBusiness;
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("add")]
        public IActionResult AddOrder(OrderResultModel order)
        {
            try
            {
                var userIdClaim = User.FindFirst("userAdminId");
                int userId = int.Parse(userIdClaim.Value);
                if (userId != 0)
                {
                    var addOrder = orderBusiness.AddOrder(order, userId);
                    if (addOrder != null)
                    {
                        return Ok(new { success = true, Message = "User Order Added", Data = addOrder });
                    }
                    else
                    {
                        return BadRequest(new { Success = false, Message = "Order can't be placed." });
                    }
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Admin can't place order" });
                }
                

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("view")]
        public IActionResult ViewOrder()
        {
            try
            {
                var userIdClaim = User.FindFirst("userAdminId");
                int userId = int.Parse(userIdClaim.Value);
                if(userId != 0)
                {
                    var result = orderBusiness.ViewOrder(userId);
                    if(result != null)
                    {
                        return Ok(new { success = true, Message = "All order for user" + userId, Data = result });
                    }
                    else
                    {
                        return BadRequest(new { Success = false, Message = "Can't fetch orders" });
                    }
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Admin can't fetch orders " });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize(Roles = "User")]
        [HttpDelete]
        [Route("cancel")]
        public IActionResult CancelOrder(int orderId)
        {
            try
            {
                var userIdClaim = User.FindFirst("userAdminId");
                int userId = int.Parse(userIdClaim.Value);
                if(userId != 0)
                {
                    bool result = orderBusiness.CancelOrder(orderId, userId);
                    if(result)
                    {
                        return Ok(new { success = true, Message = "Order Cancelled!! " });
                    }
                    else { return BadRequest(new { success = false, Message = "Something went wrong!" }); }
                }
                else
                {
                    return BadRequest(new { Success = false, Message = "Admin can't cancel order " });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
