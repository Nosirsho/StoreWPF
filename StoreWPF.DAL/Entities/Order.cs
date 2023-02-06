using StoreWPF.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreWPF.DAL.Entities
{
    public class Order : Entity
    {
        [Column(TypeName = "Date")]
        public DateTime OperationDate { get; set; }
        public virtual Operation Operation { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        
    }
}
