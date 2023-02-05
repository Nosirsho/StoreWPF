using StoreWPF.DAL.Entities.Base;

namespace StoreWPF.DAL.Entities
{
    public class Provider : NamedEntityDescription
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string INN { get; set; }
    }
}
