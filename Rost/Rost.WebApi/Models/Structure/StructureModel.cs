using System.Collections.Generic;

namespace Rost.WebApi.Models.Structure
{
    /// <summary>
    /// Модель структуры РОСТ. Содержит два уровня иерархии: нисходящую (все подчиненные структуры) и восходящую (текущая структура + все родительские/головные структуры).
    /// </summary>
    public class StructureModel
    {
        /// <summary>
        /// Все подчиненные структуры.
        /// </summary>
        public IReadOnlyCollection<SubStucture> Children { get; set; }

        /// <summary>
        /// Текущая структура + все родительские/головные структуры.
        /// </summary>
        public IReadOnlyCollection<ParentStructure> CurrentWithParents { get; set; }
    }
}