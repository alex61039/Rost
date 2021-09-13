using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rost.Repository.Domain
{
    public class Invitation : BaseEntity
    {
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        
        public int CommunityId { get; set; }
        
        [ForeignKey("CommunityId")]
        public virtual Community Community { get; set; }
        
        public DateTime Updated { get; set; }
        
        public bool IsActive { get; set; }
    }
}