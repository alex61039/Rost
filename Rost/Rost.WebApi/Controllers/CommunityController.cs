using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rost.Common;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;
using Rost.WebApi.Models;
using Rost.WebApi.Models.Community;

namespace Rost.WebApi.Controllers
{
    /// <summary>
    /// Методы для работы с сообществами
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CommunityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICommunityService _communityService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;
        
        public CommunityController(UserManager<ApplicationUser> userManager,
            ICommunityService communityService, ISubscriptionService subscriptionService, IMapper mapper)
        {
            _userManager = userManager;
            _communityService = communityService;
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод получения деталей сообщества по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("get")]
        [HttpGet]        
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.FieldIsRequired("Id") });
            }

            var community = await _communityService.GetAsync(id);

            return Ok(community);
        }

        /// <summary>
        /// Метод получения списка сообществ для текущего сотрудника
        /// </summary>
        /// <param name="isActive">Активные или архивные</param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List(bool isActive)
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }

            var communities = await _communityService.ListByUserIdAsync(appUser.Id, isActive);

            var model = _mapper.Map<List<CommunityListModel>>(communities);

            return Ok(model);
        }
    }
}