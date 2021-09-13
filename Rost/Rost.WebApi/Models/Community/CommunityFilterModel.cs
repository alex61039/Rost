using System.Collections.Generic;
using Rost.Common.Enums;

namespace Rost.WebApi.Models.Community
{
    public class CommunityFilterModel
    {
        public int? Children { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}