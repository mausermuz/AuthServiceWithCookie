using AuthServiceBulgakov.Domain.Seedwork;

namespace AuthServiceBulgakov.Domain.Entites
{
    public class Role : BaseEntity
    {
        public string Name { get; private set; }

        public List<User> Users { get; set; } = [];

        #region Конструктор
        protected Role()
        {
            
        }
        public Role(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        #endregion

        #region DDD-методы
        public void SetName(string name) => Name = name;
        #endregion
    }
}
