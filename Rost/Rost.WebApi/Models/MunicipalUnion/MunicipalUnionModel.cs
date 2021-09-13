namespace Rost.WebApi.Models.MunicipalUnion
{
    /// <summary>
    /// Модель отображения муниципального округа
    /// </summary>
    public class MunicipalUnionModel
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