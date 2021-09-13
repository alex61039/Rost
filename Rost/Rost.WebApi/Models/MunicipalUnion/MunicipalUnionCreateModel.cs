namespace Rost.WebApi.Models.MunicipalUnion
{
    /// <summary>
    /// Модель для добавления муниципального образования
    /// </summary>
    public class MunicipalUnionCreateModel
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Идентификатор района
        /// </summary>
        public int DistrictId { get; set; }
    }
}