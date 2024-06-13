﻿using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository.IRepository
{
    public interface IOrderDeailRepository:IRepository<OrderDetail>
    {
        void Update(OrderDetail obj);
  
    }
}
