using System.Collections.Generic;
using Rost.WebApi.Models.Child;

namespace Rost.WebApi.Models.Profile
{
    /// <summary>
    /// Модель деталей профиля
    /// </summary>
    public class ProfileDetailModel
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
        public string City { get; set; }
        
        /// <summary>
        /// Фото в профиле
        /// </summary>
        public string MainPhoto { get; set; }
        
        /// <summary>
        /// Почта подтверждена
        /// </summary>
        public bool EmailConfirmed { get; set; }
        
        /// <summary>
        /// Первый вход в профиль
        /// </summary>
        public bool IsFirstLogin { get; set; }
        
        /// <summary>
        /// Показывать подсказку
        /// </summary>
        public bool IsDisplayHint { get; set; }
        
        /// <summary>
        /// Список детей
        /// </summary>
        public IList<ChildrenListModel> Children { get; set; }
    }
}