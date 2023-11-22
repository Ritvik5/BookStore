using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using RepoLayer.Models;
using System.Security.Claims;

namespace BookStore.Controllers
{
    /// <summary>
    /// Order Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness orderBusiness;
        private readonly ILogger<OrderController> logger;
        public OrderController(IOrderBusiness orderBusiness,ILogger<OrderController> logger)
        {
            this.orderBusiness = orderBusiness;
            this.logger = logger;
        }
        /// <summary>
        /// Add Order authorized by user
        /// </summary>
        /// <param name="order"> Order Details </param>
        /// <returns> SMD(Status,Message,Data(Order info)) </returns>
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error come during adding order");
                return BadRequest(new { Success = false, Message = "Adding order could not be completed" });
            }
        }
        /// <summary>
        /// View Order authorize by user
        /// </summary>
        /// <returns> SMD(Status,Message,Data(List of Orders)) </returns>
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error come during view order");
                return BadRequest(new { Success = false, Message = "Viewing Orders could not be completed" });
            }
        }
        /// <summary>
        /// Cancel Order authorized by User
        /// </summary>
        /// <param name="orderId"> Order Id </param>
        /// <returns> SMD(Status,Message,Data(Boolean value)) </returns>
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
            catch (Exception ex)
            {
                logger.LogError(ex, "Error come during cancel order");
                return BadRequest(new { Success = false, Message = "Cancel Order could not completed" });
            }
        }
    }
}
