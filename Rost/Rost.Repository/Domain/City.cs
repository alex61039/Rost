using System.Collections.Generic;

namespace Rost.Repository.Domain
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        
        public virtual ICollection<District> Districts { get; set; }
    }
}