using StoreWPF.DAL.Entities.Base;
using System;

namespace StoreWPF.DAL.Entities
{
    public class Operation : Entity
    {
        public string Number { get; set; }
        public string DocumentNumber { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual OperationType OperationType { get; set; }
    }
}
