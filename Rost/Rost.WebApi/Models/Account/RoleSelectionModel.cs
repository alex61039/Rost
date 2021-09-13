namespace Rost.WebApi.Models.Account
{
    /// <summary>
    /// Модель для выбора ролей
    /// </summary>
    public class RoleSelectionModel
    {
        /// <summary>
        /// Роль родителя
        /// </summary>
        public bool IsParentRole { get; set; }
        
        /// <summary>
        /// Роль сотрудника
        /// </summary>
        public bool IsEmployeeRole { get; set; }
        
        /// <summary>
        /// Роль самозанятого
        /// </summary>
        public bool IsSelfEmployeeRole { get; set; }
    }
}