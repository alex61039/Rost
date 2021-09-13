using System.ComponentModel.DataAnnotations.Schema;

namespace Rost.Repository.Domain
{
    public class Career : BaseEntity
    {
        public string Name { get; set; }
        
        public int CareerDirectionId { get; set; }
        
        [ForeignKey("CareerDirectionId")]
        public virtual CareerDirection CareerDirection { get; set; }
    }
}