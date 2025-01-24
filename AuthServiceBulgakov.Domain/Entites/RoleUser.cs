namespace AuthServiceBulgakov.Domain.Entites
{
    public class RoleUser
    {
        public Guid RoleId { get; private set; }
        public Role? Role { get; private set; }
        public Guid UserId { get; private set; }
        public User? User { get; private set; }

        #region Конструктор
        protected RoleUser()
        {
            
        }
        public RoleUser(Guid roleId, Guid userId)
        {
            RoleId = roleId;
            UserId = userId;
        }
        #endregion
    }
}
