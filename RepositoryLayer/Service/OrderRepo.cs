using Microsoft.Extensions.Configuration;
using RepoLayer.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RepoLayer.Service
{
    public class OrderRepo
    {
        private readonly BookStoreDBContext bookStoreDBContext;
        private readonly IConfiguration configuration;
        public OrderRepo(BookStoreDBContext bookStoreDBContext,IConfiguration configuration)
        {
            this.bookStoreDBContext = bookStoreDBContext;
            this.configuration = configuration;
        }

    }
}
