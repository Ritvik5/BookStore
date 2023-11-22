using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoLayer.Interface;
using RepoLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace RepoLayer.Service
{
    /// <summary>
    /// Order Repository
    /// </summary>
    public class OrderRepo : IOrderRepo
    {
        private readonly BookStoreDBContext bookStoreDBContext;
        private readonly IConfiguration configuration;
        public OrderRepo(BookStoreDBContext bookStoreDBContext,IConfiguration configuration)
        {
            this.bookStoreDBContext = bookStoreDBContext;
            this.configuration = configuration;
        }
        public OrderModel AddOrder(OrderResultModel order,int userId)
        {
            try
            {
                var user = bookStoreDBContext.UserTable.FirstOrDefault(u => u.UserId == userId);
                if(user != null)
                {
                    OrderTable newOrder = new OrderTable
                    {
                        OrderPrice = order.OrderPrice,
                        OrderQuantity = order.OrderQuantity,
                        AddressId = order.AddressId,
                        UserId = userId,
                    };

                    bookStoreDBContext.OrderTable.Add(newOrder);
                    bookStoreDBContext.SaveChanges();
                    OrderModel orderModel = new OrderModel
                    {
                        OrderId = newOrder.OrderId,
                        OrderPrice= order.OrderPrice,
                        OrderQuantity= order.OrderQuantity,
                        AddressId= order.AddressId,
                        UserId = userId,
                    };
                    return orderModel;
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
        public List<ViewOrderModel> ViewOrder(int userId)
        {
            try
            {
                List<ViewOrderModel> viewOrders = new List<ViewOrderModel>();
                var orders = bookStoreDBContext.OrderTable.Where(u => u.UserId == userId).ToList();

                foreach (var item in orders)
                {
                    ViewOrderModel viewOrder = new ViewOrderModel
                    {
                        OrderId = item.OrderId,
                        OrderPrice = item.OrderPrice,
                        OrderQuantity = item.OrderQuantity,
                        AddressId = item.AddressId,
                        UserId = item.UserId
                    };

                    viewOrders.Add(viewOrder);
                }
                return viewOrders;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool CancelOrder(int orderId,int userId)
        {
            try
            {
                var orders = bookStoreDBContext.OrderTable.FirstOrDefault(u => u.OrderId == orderId && u.UserId == userId);
                if(orders != null)
                {
                    bookStoreDBContext.Remove(orders);
                    bookStoreDBContext.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
