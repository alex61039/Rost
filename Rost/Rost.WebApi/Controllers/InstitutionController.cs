using System;
using System.Collections.Generic;
using System.Threading;
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
using Rost.WebApi.Models.Institution;
using Rost.WebApi.Models.Structure;

namespace Rost.WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService _institutionService;
        private readonly IStructureService _structureService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="institutionService"></param>
        /// <param name="userService"></param>
        /// <param name="mapper"></param>
        /// <param name="structureService"></param>
        /// <param name="userManager"></param>
        public InstitutionController(IInstitutionService institutionService, IStructureService structureService, 
            IUserService userService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _institutionService = institutionService;
            _structureService = structureService;
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
        }

        /// <summary>
        /// Метод получения деталей учреждений
        /// </summary>
        /// <param name="id">Идентификатор учреждения</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
            {
                var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                if (appUser.InstitutionId != null)
                {
                    id = appUser.InstitutionId.Value;
                }
            }
            
            var institutions = await _institutionService.GetAsync(id, CancellationToken.None);
            var model = _mapper.Map<InstitutionDetailsModel>(institutions);

            return Ok(model);
        }
        
        /// <summary>
        /// Метод получения списка учреждений
        /// </summary>
        /// <param name="structureId">Идентификатор структуры</param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List(int structureId)
        {
            var institutions = await _institutionService.GetByStructureIdAsync(structureId, CancellationToken.None);
            var model = _mapper.Map<InstitutionListModel>(institutions);

            return Ok(model);
        }

        /// <summary>
        /// Метод создания учреждения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(InstitutionEditModel model)
        {
            var institution = _mapper.Map<Institution>(model);

            try
            {
                await _institutionService.AddAsync(institution);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel { Status = ResponseErrors.Error, Message = ex.Message });
            }

            var parentStructure = await _structureService.GetParentAsync(model.StructureId, CancellationToken.None);
            var (currentStructureWithParents, children) = await _structureService.GetStructureHierarchyAsync(parentStructure?.Id, CancellationToken.None);

            var parentStructures = _mapper.Map<List<ParentStructure>>(currentStructureWithParents);
            var childStructures = _mapper.Map<List<SubStucture>>(children);

            foreach (var parent in parentStructures)
            {
                if (parent.Id != null)
                {
                    var parentInstitutions =
                        await _institutionService.GetByStructureIdAsync(parent.Id.Value, CancellationToken.None);
                    parent.Institutions = _mapper.Map<List<InstitutionListModel>>(parentInstitutions);
                }
            }
            
            foreach (var child in childStructures)
            {
                var childInstitutions =
                    await _institutionService.GetByStructureIdAsync(child.Id, CancellationToken.None);
                child.Institutions = _mapper.Map<List<InstitutionListModel>>(childInstitutions);
            }
            
            return Ok(new StructureModel { Children = childStructures, CurrentWithParents = parentStructures });
        }

        /// <summary>
        /// Метод добавления сотрудника к учреждению
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("employees/add")]
        public async Task<IActionResult> AddEmployee(EmployeeAddModel model)
        {
            ApplicationUser user;
                
            if (model.User.Contains('@'))
            {
                user = await _userService.GetByEmailAsync(model.User);
            }
            else
            {
                user = await _userService.GetByNumberAsync(model.User);
            }
            
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.UserEmailIsNotExists(model.User) });
            }

            if (user.InstitutionId.HasValue)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = "Пользователь уже привязан учреждению" });
            }
            
            var institution = await _institutionService.GetAsync(model.InstitutionId, CancellationToken.None);

            if (institution == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.NotFoundWithParameter("Учреждение", "Id", model.InstitutionId.ToString()) });
            }
            
            user.EmployeeRole = model.EmployeeRole;
            user.Position = model.Position;
            
            try
            {
                institution.Employees.Add(user);
                await _institutionService.UpdateAsync(institution);
                await _userService.UpdateAsync(user);
                
                var userRoles = await _userManager.GetRolesAsync(user);
            
                if (!userRoles.Contains(Constants.RoleEmployee))
                {
                    await _userManager.AddToRoleAsync(user, Constants.RoleEmployee);  
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel { Status = ResponseErrors.Error, Message = ex.Message });
            }

            if (model.EmployeeRole == EmployeeRole.GlobalAdmin)
            {
                return Ok(_mapper.Map<InstitutionDetailsModel>(institution));
            }
            
            var employees = await _userService.ListByInstitutionId(model.InstitutionId);

            return Ok(_mapper.Map<List<EmployeeListModel>>(employees));
        }

        /// <summary>
        /// Метод обновления сотрудника
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("employees/update")]
        public async Task<IActionResult> UpdateEmployee(EmployeeAddModel model)
        {
            ApplicationUser user;
                
            if (model.User.Contains('@'))
            {
                user = await _userService.GetByEmailAsync(model.User);
            }
            else
            {
                user = await _userService.GetByNumberAsync(model.User);
            }
            
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.UserEmailIsNotExists(model.User) });
            }

            user.EmployeeRole = model.EmployeeRole;
            user.Position = model.Position;
            
            try
            {
                await _userService.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel { Status = ResponseErrors.Error, Message = ex.Message });
            }
            
            var institution = await _institutionService.GetAsync(model.InstitutionId, CancellationToken.None);

            if (institution == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.NotFoundWithParameter("Учреждение", "Id", model.InstitutionId.ToString()) });
            }
            
            return Ok(_mapper.Map<InstitutionDetailsModel>(institution));
        }
        
        /// <summary>
        /// Метод получения списка сотрудников учреждения
        /// </summary>
        /// <param name="structureId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("employees/list")]
        public async Task<IActionResult> ListEmployees(int structureId)
        {
            if (structureId == 0)
            {
                var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                if (appUser.InstitutionId != null)
                {
                    structureId = appUser.InstitutionId.Value;
                }
            }
            
            var institution = await _institutionService.GetAsync(structureId, CancellationToken.None);

            if (institution == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.NotFoundWithParameter("Учреждение", "Id", structureId.ToString()) });
            }
            
            var employees = await _userService.ListByInstitutionId(structureId);

            return Ok(_mapper.Map<List<EmployeeListModel>>(employees));
        }
        
        /// <summary>
        /// Метод удаления сотрудника из учреждения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("employees/delete")]
        public async Task<IActionResult> DeleteEmployee(EmployeeAddModel model)
        {
            var institution = await _institutionService.GetAsync(model.InstitutionId, CancellationToken.None);

            if (institution == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.NotFoundWithParameter("Учреждение", "Id", model.InstitutionId.ToString()) });
            }

            var user = await _userService.GetAsync(model.User);
            
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = ValidationErrorMessages.NotFoundWithParameter("Пользователь", "Id", model.User) });
            }

            try
            {
                var result = institution.Employees.Remove(user);

                if (result)
                {
                    user.EmployeeRole = null;
                    user.Position = string.Empty;
                    await _institutionService.UpdateAsync(institution);
                    await _userService.UpdateAsync(user);
                    
                    var userRoles = await _userManager.GetRolesAsync(user);
            
                    if (userRoles.Contains(Constants.RoleEmployee))
                    {
                        await _userManager.RemoveFromRoleAsync(user, Constants.RoleEmployee);  
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel { Status = ResponseErrors.Error, Message = ex.Message });
            }

            var employees = await _userService.ListByInstitutionId(institution.Id);

            return Ok(_mapper.Map<List<EmployeeListModel>>(employees));
        }
        
        /// <summary>
        /// Метод редактирования учреждения
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(InstitutionEditModel model)
        {
            var institution = await _institutionService.GetAsync(model.Id);

            if (institution == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = "Не найдено учреждение" });
            }
            
            //institution = _mapper.Map<Institution>(model);
            institution.Address = model.Address;
            institution.Description = model.Description;
            institution.Email = model.Email;
            institution.Name = model.Name;
            institution.Phone = model.Phone;
            institution.CityId = model.CityId;
            institution.DistrictId = model.DistrictId;
            institution.EducationId = model.EducationId;
            institution.MunicipalUnionId = model.MunicipalUnionId;
            
            try
            {
                await _institutionService.UpdateAsync(institution);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel { Status = ResponseErrors.Error, Message = ex.Message });
            }
            
            return Ok(_mapper.Map<InstitutionDetailsModel>(institution));
        }
        
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var institution = await _institutionService.GetAsync(id);
            
            if (institution == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = "Не найдено учреждение" });
            }
            
            try
            {
                await _institutionService.DeleteAsync(institution);
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