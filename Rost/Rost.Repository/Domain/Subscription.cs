using System;
using System.ComponentModel.DataAnnotations.Schema;
using Rost.Common.Enums;

namespace Rost.Repository.Domain
{
    public class Subscription : BaseEntity
    {
        public int ChildId { get; set; }
        public int CommunityId { get; set; }
        public SubscriptionStatus Status { get; set; }
        public DateTime Updated { get; set; }
        
        [ForeignKey("ChildId")]
        public virtual Child Child { get; set; }
        
        [ForeignKey("CommunityId")]
        public virtual Community Community { get; set; }
    }
}