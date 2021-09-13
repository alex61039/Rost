using Rost.Common.Enums;

namespace Rost.WebApi.Models.Institution
{
    /// <summary>
    /// Можель элемента списка сотрудников учреждения
    /// </summary>
    public class EmployeeListModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Персональный номер
        /// </summary>
        public string PersonalNumber { get; set; }
        
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Фото в профиле
        /// </summary>
        public string MainPhoto { get; set; }
        
        /// <summary>
        /// Роль
        /// </summary>
        public EmployeeRole? Role { get; set; }
        
        /// <summary>
        /// Роль сотрудника
        /// </summary>
        public string RoleName { get; set; }
        
        /// <summary>
        /// Email сотрудника
        /// </summary>
        public string Email { get; set; }
    }
}