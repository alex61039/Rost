using Rost.Common.Enums;

namespace Rost.WebApi.Models.Institution
{
    /// <summary>
    /// Модель добавления администратора
    /// </summary>
    public class EmployeeAddModel
    {
        /// <summary>
        /// Ижентификатор учреждения
        /// </summary>
        public int InstitutionId { get; set; }
        
        /// <summary>
        /// Email или номер пользователя
        /// </summary>
        public string User { get; set; }
        
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        
        /// <summary>
        /// Роль сотрудника
        /// </summary>
        public EmployeeRole EmployeeRole { get; set; }
    }
}