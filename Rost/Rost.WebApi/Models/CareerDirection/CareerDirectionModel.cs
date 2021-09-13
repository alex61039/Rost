using System.Collections.Generic;
using Rost.WebApi.Models.Career;

namespace Rost.WebApi.Models.CareerDirection
{
    /// <summary>
    /// Модель направлений профориентации
    /// </summary>
    public class CareerDirectionModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Список профориентаций
        /// </summary>
        public IList<CareerModel> Careers { get; set; } = new List<CareerModel>();
    }
}