using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rost.WebApi.Models.Community;

namespace Rost.WebApi.Models.Invitation
{
    public class InvitationListModel
    {
        public int Id { get; set; }

        public CommunityListModel Community { get; set; }
    }
}
