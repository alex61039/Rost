namespace Rost.WebApi.Models.Profile
{
    /// <summary>
    /// Модель редактирования профиля
    /// </summary>
    public class ProfileEditModel
    {
        /// <summary>
        /// Персональный номер пользователя
        /// </summary>
        public string PersonalNumber { get; set; }
        
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// Город
        /// </summary>
        public int CityId { get; set; }
        
        /// <summary>
        /// Район
        /// </summary>
        public int DistrictId { get; set; }
        
        /// <summary>
        /// Муниципальное образование
        /// </summary>
        public int MunicipalUnionId { get; set; }
        
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }
    }
}