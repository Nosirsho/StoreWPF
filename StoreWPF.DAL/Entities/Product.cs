using StoreWPF.DAL.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreWPF.DAL.Entities
{
    public class Product : NamedEntityDescription
    {
        public virtual Category Category { get; set; }
        public virtual UOM UOM { get; set; }
        public string Barcode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SellingPrice { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
