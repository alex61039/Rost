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
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;
using Rost.WebApi.Models;
using Rost.WebApi.Models.Career;
using Rost.WebApi.Models.Child;

namespace Rost.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICareerService _careerService;
        private readonly IChildService _childService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ChildController(UserManager<ApplicationUser> userManager, ICareerService careerService, IChildService childService, IUserService userService, IMapper mapper)
        {
            _userManager = userManager;
            _careerService = careerService;
            _childService = childService;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод получения списка детей по Id пользователя
        /// Если Id не указан, то возвращается список для текущего пользователя
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("List")]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }

            var children = await _childService.ListByUserId(appUser.Id);
            var model = _mapper.Map<List<ChildrenListModel>>(children);

            return Ok(model);
        }

        /// <summary>
        /// Метод получения деталей ребенка
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.FieldIsRequired("Id") });
            }
            
            var child = await _childService.GetAsync(id);

            if (child == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.ChildWithIdNotFound(id) });
            }

            var model = _mapper.Map<ChildEditModel>(child);
            model.Careers = child.Careers?.Select(t => new CareerModel { Id = t.Id, IsSelected = true, Name = t.Name }).ToList();
            
            return Ok(model);
        }
        
        /// <summary>
        /// Метод добавления ребенка
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ChildAddModel model)
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }

            if (await _childService.IsAlreadyExistsAsync(appUser.Id, model.Name, model.Sex))
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.ChildWithNameAlreadyExists(model.Name) });
            }

            var child = _mapper.Map<Child>(model);
            child.PersonalNumber = await _userService.GetNextPersonalNumber();
            child.UserId = appUser.Id;
            
            if (model.Careers!.Any())
            {
                child.Careers = new List<Career>();
                foreach (var career in model.Careers)
                {
                    var careerDto = await _careerService.GetAsync(career.Id);
                    if (careerDto != null)
                    {
                        child.Careers.Add(careerDto);
                    }
                }
            }

            try
            {
                await _childService.AddAsync(child);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel { Status = ResponseErrors.Error, Message = ex.Message });
            }

            var children = await _childService.ListByUserId(appUser.Id);

            return Ok(_mapper.Map<List<ChildrenListModel>>(children));
        }
        
        /// <summary>
        /// Метод редактирования ребенка
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("edit")]
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] ChildEditModel model)
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }

            var child = await _childService.GetAsync(model.Id);
            
            if (child == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.ChildWithIdNotFound(model.Id) });
            }

            if (!appUser.Children.Contains(child))
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = "Недостаточно прав для редактирования" });
            }

            child.Name = model.Name;
            child.BirthDay = model.BirthDay;
            child.Sex = model.Sex;
            
            if (model.Careers!.Any())
            {
                child.Careers = new List<Career>();
                foreach (var career in model.Careers)
                {
                    var careerDto = await _careerService.GetAsync(career.Id);
                    if (careerDto != null)
                    {
                        child.Careers.Add(careerDto);
                    }
                }
            }

            try
            {
                await _childService.UpdateAsync(child);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel { Status = ResponseErrors.Error, Message = ex.Message });
            }

            var children = await _childService.ListByUserId(appUser.Id);

            return Ok(_mapper.Map<List<ChildrenListModel>>(children));
        }

        /// <summary>
        /// Метод удаления профиля ребенка
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var child = await _childService.GetAsync(id);

            if (child == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.ChildWithIdNotFound(id) });
            }

            try
            {
                await _childService.DeleteAsync(child);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel { Status = ResponseErrors.Error, Message = ex.Message });
            }

            return Ok();
        }

        private async Task<ApplicationUser> GetUserAsync(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _userManager.FindByIdAsync(userId);
            }

            return await _userManager.FindByNameAsync(User.Identity.Name);
        }
    }
}