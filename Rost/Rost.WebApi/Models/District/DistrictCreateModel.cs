namespace Rost.WebApi.Models.District
{
    /// <summary>
    /// Модель для добавления нвого района
    /// </summary>
    public class DistrictCreateModel
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Идентификатор города
        /// </summary>
        public int CityId { get; set; }
    }
}