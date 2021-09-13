using System.ComponentModel.DataAnnotations;

namespace Rost.WebApi.Models.Structure
{
    /// <summary>
    /// Модель для добавления структуры РОСТ.
    /// </summary>
    public class StructureCreateModel
    {
        /// <summary>
        /// Идентификатор родительского уровня иерархии.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Название структуры.
        /// </summary>
        [Required(ErrorMessage = "Название не должно быть пустым.")]
        public string Name { get; set; }
    }
}