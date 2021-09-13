using System.Collections;
using System.Collections.Generic;
using Rost.WebApi.Models.Institution;

namespace Rost.WebApi.Models.Structure
{
    /// <summary>
    /// Модель представляет структуру текущего и вышестоящих уровней.
    /// </summary>
    public class ParentStructure
    {
        /// <summary>
        /// Идентификатор структуры.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Наименование структуры.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Флаг, указывает, является ли эта структура структурой текущего уровня иерархии.
        /// </summary>
        public bool IsCurrentLevel { get; set; }
        
        /// <summary>
        /// Список учреждений
        /// </summary>
        public IList<InstitutionListModel> Institutions { get; set; }
    }
}