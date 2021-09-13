using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rost.WebApi.Models.Invitation
{
    public class InvitationAcceptModel
    {
        public int Id { get; set; }

        public int[] Children { get; set; }
    }
}
