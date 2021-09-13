namespace Rost.WebApi.Models.Account
{
    /// <summary>
    /// Модель для изменения пароля
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Старый пароль
        /// </summary>
        public string OldPassword { get; set; }
        
        /// <summary>
        /// Новый пароль
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}