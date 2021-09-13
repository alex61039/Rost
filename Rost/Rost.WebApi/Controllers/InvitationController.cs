using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Rost.Common;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;
using Rost.WebApi.Models;
using Rost.WebApi.Models.Invitation;
using Rost.WebApi.Models.Community;

namespace Rost.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInvitationService _invitationService;
        private readonly IMapper _mapper;

        public InvitationController(UserManager<ApplicationUser> userManager, IInvitationService invitationService, IMapper mapper)
        {
            _userManager = userManager;
            _invitationService = invitationService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List()
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }

            var invitations = await _invitationService.ListByUserId(appUser.Id, true);

            var model = _mapper.Map<List<InvitationListModel>>(invitations);

            return Ok(model);
        }

        [HttpPost]
        [Route("accept")]
        public async Task<IActionResult> AcceptInvitation([FromBody]InvitationAcceptModel invitationModel)
        {
            var invitation = await _invitationService.GetAsync(invitationModel.Id);

            if (invitation == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = "Invalid Invitation Id" });
            }

            await _invitationService.Accept(invitation, invitationModel.Children);

            return Ok();
        }
    }
}
