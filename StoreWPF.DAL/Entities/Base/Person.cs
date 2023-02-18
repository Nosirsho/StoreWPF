using StoreWPF.DAL.Entities.Base;

namespace StoreWPF.DAL.Entities.Base
{
    public abstract class Person : NamedEntity
    {
        public string Surname { get; set; }
        public string SurnamePatronymic { get; set; }
    }
}
