namespace AuthServiceBulgakov.Application.Dto.Users
{
    /// <summary>
    /// DTO модель для получения списка пользователей
    /// </summary>
    public class UserListDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }
}
