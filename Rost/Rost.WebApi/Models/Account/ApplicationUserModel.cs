namespace Rost.WebApi.Models.Account
{
    /// <summary>
    /// Модель для вывода информации о пользователе.
    /// </summary>
    public class ApplicationUserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}