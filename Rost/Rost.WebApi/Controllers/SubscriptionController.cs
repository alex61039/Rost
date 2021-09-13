using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rost.Common;
using Rost.Common.Enums;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;
using Rost.WebApi.Models;
using Rost.WebApi.Models.Community;
using Rost.WebApi.Models.Subscription;

namespace Rost.WebApi.Controllers
{
    /// <summary>
    /// Методы для подписок на сообщества
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        
        public SubscriptionController(UserManager<ApplicationUser> userManager,
            ISubscriptionService subscriptionService, IUserService userService, IMapper mapper)
        {
            _userManager = userManager;
            _subscriptionService = subscriptionService;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод получения списка сообществ для текущего пользователя
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List([FromQuery] CommunityFilterModel filterModel)
        {
            try
            {
                var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                List<int> children;

                if (appUser == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
                }

                if (filterModel.Children.HasValue)
                {
                    children = new List<int>() { filterModel.Children.Value };
                }
                else
                {
                    var user = await _userService.GetAsync(appUser.Id);
                    children = appUser.Children.Select(x => x.Id).ToList();
                }


                var subscriptions =
                    await _subscriptionService.ListByChildrenAsync(children, filterModel.Status);

                var model = _mapper.Map<List<CommunityListModel>>(subscriptions);

                return Ok(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("unsubscribe")]
        public async Task<IActionResult> Unsubscribe([FromBody]UnsubscribeModel model)
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }
            var subscriptions = await _subscriptionService.ListByCommunity(model.Id, SubscriptionStatus.Accepted);

            foreach (var subscr in subscriptions)
            {
                await _subscriptionService.Unsubscribe(subscr);
            }

            return Ok();
        }

        //TODO: Добавить метод для добавления подписки. Добавление подписки - это отправка приглашениия в сообщество
        //TODO: ДОбавить метод для изменения подписки. Изменение статуса подписки
    }
}