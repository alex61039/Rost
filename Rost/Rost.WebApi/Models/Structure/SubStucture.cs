using System.Collections.Generic;
using Rost.WebApi.Models.Institution;

namespace Rost.WebApi.Models.Structure
{
    /// <summary>
    /// Представляет структуру подчиненных уровней (дочернюю).
    /// </summary>
    public class SubStucture
    {
        /// <summary>
        /// Идентификатор структуры.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование структуры.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Флаг, указывает, есть ли у данной структуры подчиненные/дочерние структуры.
        /// </summary>
        public bool HasChildren { get; set; }
        
        /// <summary>
        /// Список учреждений
        /// </summary>
        public IList<InstitutionListModel> Institutions { get; set; }
    }
}