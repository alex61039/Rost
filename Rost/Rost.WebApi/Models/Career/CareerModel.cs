namespace Rost.WebApi.Models.Career
{
    /// <summary>
    /// Модель профориентации
    /// </summary>
    public class CareerModel
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Выбран
        /// </summary>
        public bool IsSelected { get; set; }
    }
}