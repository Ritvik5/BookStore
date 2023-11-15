using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class OrderBusiness : IOrderBusiness
    {
        public IOrderRepo orderRepo;
        public OrderBusiness(IOrderRepo orderRepo)
        {
            this.orderRepo = orderRepo;
        }

        public OrderModel AddOrder(OrderResultModel order, int userId)
        {
            try
            {
                return orderRepo.AddOrder(order,userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CancelOrder(int orderId, int userId)
        {
            try
            {
                return orderRepo.CancelOrder(orderId, userId);
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
                return orderRepo.ViewOrder(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
