using System;
using System.Collections.Generic;
using System.IO;
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
using Rost.WebApi.Models.Profile;

namespace Rost.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        
        public ProfileController(UserManager<ApplicationUser> userManager, IMapper mapper, IUserService userService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userService = userService;
        }
        
        /// <summary>
        /// Метод получения деталей профиля
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("details")]
        public async Task<IActionResult> Index()
        {
            //var currentUser = User;
            //var id = _userManager.GetUserId(User);
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var user = await _userService.GetAsync(appUser.Id);
            
            var model = _mapper.Map<ProfileDetailModel>(user);
            

            return Ok(model);
        }

        /// <summary>
        /// Метод загрузки фото в профиль
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] ImageModel model)
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }
            
            if (model == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.ImageIsNullOrEmpty });
            }
            
            var basePath = $"{Directory.GetCurrentDirectory()}/wwwroot/{appUser.Id}";
            var unixTimestamp = (Int32)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var path = $"{basePath}/{unixTimestamp}{model.FileName}";

            Directory.CreateDirectory($"{basePath}/");
            
            var user = await _userService.GetAsync(appUser.Id);
            if (user.Photos == null)
            {
                user.Photos = new List<UserPhoto>();
            }
            else
            {
                var photo = user.Photos.FirstOrDefault(t => t.IsMain);
                if (photo != null && !string.IsNullOrEmpty(photo.Path) && System.IO.File.Exists(photo.Path))
                {
                    try
                    {
                        System.IO.File.Delete(photo.Path);
                        user.Photos.Remove(photo);
                    }
                    catch (Exception)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            new ResponseModel { Status = ResponseErrors.Error, Message = ResponseErrors.FileDeleteFailed });
                    }
                }
            }
            
            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.Image.CopyToAsync(fileStream);
            }
            
            var userPhoto = new UserPhoto { IsMain = true, UserId = appUser.Id, Url = $"{Constants.BaseUrlAddress}{appUser.Id}/{unixTimestamp}{model.FileName}", Path = path };
            
            user.Photos.Add(userPhoto);
            await _userService.UpdateAsync(user);
            var result = _mapper.Map<ProfileDetailModel>(user);
            
            return Ok(result);
        }

        /// <summary>
        /// Метод скрывает подсказку для текущего пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("hint/hide")]
        public async Task<IActionResult> HideHint()
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }

            appUser.IsDisplayHint = false;

            await _userService.UpdateAsync(appUser);

            return Ok();
        }
        
        /// <summary>
        /// Метод скрывает выбор ролей при первом вхоже в систему
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("role/select")]
        public async Task<IActionResult> SelectRole()
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }

            appUser.IsFirstLogin = false;

            await _userService.UpdateAsync(appUser);

            return Ok();
        }

        /// <summary>
        /// Метод получения деталей профиля для редактирования
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("edit/get")]
        public async Task<IActionResult> GetEdit()
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }

            var profileDetails = await _userService.GetAsync(appUser.Id);
            var model = _mapper.Map<ProfileEditModel>(profileDetails);

            return Ok(model);
        }

        /// <summary>
        /// Метод сохранения деталей профиля пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("edit/post")]
        public async Task<IActionResult> PostEdit(ProfileEditModel model)
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ResponseModel { Status = ResponseErrors.BadRequest, Message = ValidationErrorMessages.UserEmailIsNotExists(User.Identity.Name) });
            }

            var profileDetails = await _userService.GetAsync(appUser.Id);
            profileDetails.Name = model.Name;
            profileDetails.Surname = model.Surname;
            profileDetails.PhoneNumber = model.Phone;
            profileDetails.Email = model.Email;
            profileDetails.CityId = model.CityId;
            profileDetails.DistrictId = model.DistrictId;
            profileDetails.MunicipalUnionId = model.MunicipalUnionId;

            try
            {
                await _userService.UpdateAsync(profileDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel { Status = ResponseErrors.Error, Message = ex.Message });
            }
            
            return Ok();
        }
    }
}