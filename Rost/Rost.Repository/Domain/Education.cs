using System.Collections.Generic;

namespace Rost.Repository.Domain
{
    public class Education : BaseEntity
    {
        public string Name { get; set; }
        
        public ICollection<School> Schools { get; set; }
    }
}