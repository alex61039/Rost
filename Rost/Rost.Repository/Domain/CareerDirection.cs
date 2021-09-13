using System.Collections.Generic;

namespace Rost.Repository.Domain
{
    public class CareerDirection : BaseEntity
    {
        public string Name { get; set; }
        
        public ICollection<Career> Careers { get; set; }
    }
}