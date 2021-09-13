using System;
using System.Collections.Generic;
using Rost.Common.Enums;
using Rost.WebApi.Models.Career;

namespace Rost.WebApi.Models.Child
{
    /// <summary>
    /// Модель создания ребенка
    /// </summary>
    public class ChildAddModel
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// Отчество
        /// </summary>
        public string SecondName { get; set; }
        
        /// <summary>
        /// Дата рожения
        /// </summary>
        public DateTime BirthDay { get; set; }
        
        /// <summary>
        /// Пол
        /// </summary>
        public Sex Sex { get; set; }
        
        /// <summary>
        /// Профориентация
        /// </summary>
        public IList<CareerModel> Careers { get; set; }
    }
}