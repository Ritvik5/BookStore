using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IOrderRepo
    {
        OrderModel AddOrder(OrderResultModel order,int userId);
        List<ViewOrderModel> ViewOrder(int userId);
        bool CancelOrder(int orderId, int userId);
    }
}
