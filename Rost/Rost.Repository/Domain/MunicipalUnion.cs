using System.ComponentModel.DataAnnotations.Schema;

namespace Rost.Repository.Domain
{
    public class MunicipalUnion : BaseEntity
    {
        public string Name { get; set; }
        
        public int DistrictId { get; set; }
        
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
    }
}