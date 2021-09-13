using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rost.Repository.Domain
{
    public class District : BaseEntity
    {
        public string Name { get; set; }
        
        public int CityId { get; set; }
        
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        
        public virtual ICollection<District> Districts { get; set; }
    }
}