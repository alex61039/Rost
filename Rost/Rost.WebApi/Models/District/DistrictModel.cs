namespace Rost.WebApi.Models.District
{
    /// <summary>
    /// Модель отображения района
    /// </summary>
    public class DistrictModel
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}