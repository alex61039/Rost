namespace Rost.WebApi.Models.Child
{
    /// <summary>
    /// Модель для отображения списка детей
    /// </summary>
    public class ChildrenListModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Персональный номер
        /// </summary>
        public string PersonalNumber { get; set; }
        
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Возраст (количество полных лет)
        /// </summary>
        public string Age { get; set; }
        
        /// <summary>
        /// Профориентация
        /// </summary>
        public string Careers { get; set; }
    }
}