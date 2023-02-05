using StoreWPF.DAL.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreWPF.DAL.Entities
{
    public class Order : Entity
    {
        public virtual Operation Operation { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
