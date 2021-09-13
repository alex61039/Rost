using System.ComponentModel.DataAnnotations.Schema;

namespace Rost.Repository.Domain
{
    public class School : BaseEntity
    {
        public string Name { get; set; }
        
        public int EducationId { get; set; }
        
        [ForeignKey("EducationId")]
        public virtual Education Education { get; set; }
    }
}