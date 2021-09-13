namespace Rost.WebApi.Models.Account
{
    /// <summary>
    /// Модель для сброса пароля
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Код для сброса пароля
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}