namespace Rost.WebApi.Models.Institution
{
    /// <summary>
    /// Модель списка учреждений
    /// </summary>
    public class InstitutionListModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}