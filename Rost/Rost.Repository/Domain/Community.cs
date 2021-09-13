using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Rost.Common.Enums;

namespace Rost.Repository.Domain
{
    public class Community : BaseEntity
    {
        public string Name { get; set; }
        
        public int? CommunityTypeId { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? MunicipalUnionId { get; set; }
        public int? EducationId { get; set; }
        public int? CareerId { get; set; }
        
        public string Image { get; set; }
        
        public bool IsActive { get; set; }
        
        [ForeignKey("CareerId")]
        public virtual Career Career { get; set; }
        
        [ForeignKey("CommunityTypeId")]
        public virtual CommunityType CommunityType { get; set; }
        
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
        
        [ForeignKey("MunicipalUnionId")]
        public virtual MunicipalUnion MunicipalUnion { get; set; }
        
        [ForeignKey("EducationId")]
        public virtual Education Education { get; set; }

        public AccessType AccessType { get; set; } = AccessType.Public;
        
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        
        public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}