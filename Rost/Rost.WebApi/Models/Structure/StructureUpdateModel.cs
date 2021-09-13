using System.ComponentModel.DataAnnotations;

namespace Rost.WebApi.Models.Structure
{
    /// <summary>
    /// Можель редактирования структуры
    /// </summary>
    public class StructureUpdateModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Название структуры.
        /// </summary>
        public string Name { get; set; }
    }
}