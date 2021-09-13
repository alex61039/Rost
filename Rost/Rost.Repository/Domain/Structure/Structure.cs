using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rost.Repository.Domain
{
    /// <summary>
    /// Модель представляет сущность "Структура РОСТ" в БД.
    /// </summary>
    public class Structure : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public Structure Parent { get; set; }

        public ICollection<Structure> Children { get; set; }

        public ICollection<Institution> Institutions { get; set; }
    }
}