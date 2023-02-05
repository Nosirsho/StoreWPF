using StoreWPF.DAL.Entities;
using StoreWPF.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWPF.DAL.Entities
{
    public class OrderProduct : Entity
    {
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
