using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rost.Common;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;
using Rost.WebApi.Models;
using Rost.WebApi.Models.Institution;
using Rost.WebApi.Models.Structure;

namespace Rost.WebApi.Controllers
{
    /// <summary>
    /// Методы для работы со структурой РОСТ.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class StructureController : ControllerBase
    {
        private readonly IStructureService _structureService;
        private readonly IInstitutionService _institutionService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="structureService"></param>
        /// <param name="mapper"></param>
        /// <param name="institutionService"></param>
        public StructureController(IStructureService structureService, IMapper mapper, IInstitutionService institutionService)
        {
            _structureService = structureService;
            _mapper = mapper;
            _institutionService = institutionService;
        }

        /// <summary>
        /// Метод возвращает список структ
        /// </summary>
        /// <param name="structureId">Идентификатор структуры, выбранной в качестве текущего уровня иерархии.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Route("Get/ByCurrentStructure")]
        [HttpGet]
        public async Task<ActionResult<StructureModel>> GetByCurrentStructure(int? structureId, CancellationToken cancellationToken)
        {
            var (currentStructureWithParents, children) = await _structureService.GetStructureHierarchyAsync(structureId, cancellationToken);

            var parentStructures = _mapper.Map<List<ParentStructure>>(currentStructureWithParents);
            var childStructures = _mapper.Map<List<SubStucture>>(children);

            foreach (var parent in parentStructures)
            {
                if (parent.Id != null)
                {
                    var parentInstitutions =
                        await _institutionService.GetByStructureIdAsync(parent.Id.Value, cancellationToken);
                    parent.Institutions = _mapper.Map<List<InstitutionListModel>>(parentInstitutions);
                }
            }
            
            foreach (var child in childStructures)
            {
                var childInstitutions =
                    await _institutionService.GetByStructureIdAsync(child.Id, cancellationToken);
                child.Institutions = _mapper.Map<List<InstitutionListModel>>(childInstitutions);
            }
            
            return Ok(new StructureModel { Children = childStructures, CurrentWithParents = parentStructures });
        }

        /// <summary>
        /// Метод для добавления новой структуры РОСТ.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] StructureCreateModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var structure = _mapper.Map<Structure>(model);
            if (model.ParentId.HasValue)
            {
                var parentExists = await _structureService.ExistsAsync(s => s.Id == structure.ParentId, cancellationToken);
                if (!parentExists)
                {
                    ModelState.AddModelError(nameof(StructureCreateModel.ParentId), $"Родительская структура с идентификатором {model.ParentId} не найдена.");
                    return ValidationProblem(ModelState);
                }
            }

            await _structureService.AddAsync(structure, cancellationToken);
            var (currentStructureWithParents, children) = await _structureService.GetStructureHierarchyAsync(structure.ParentId, cancellationToken);

            var parentStructures = _mapper.Map<List<ParentStructure>>(currentStructureWithParents);
            var childStructures = _mapper.Map<List<SubStucture>>(children);

            foreach (var parent in parentStructures)
            {
                if (parent.Id != null)
                {
                    var parentInstitutions =
                        await _institutionService.GetByStructureIdAsync(parent.Id.Value, cancellationToken);
                    parent.Institutions = _mapper.Map<List<InstitutionListModel>>(parentInstitutions);
                }
            }
            
            foreach (var child in childStructures)
            {
                var childInstitutions =
                    await _institutionService.GetByStructureIdAsync(child.Id, cancellationToken);
                child.Institutions = _mapper.Map<List<InstitutionListModel>>(childInstitutions);
            }
            
            return Ok(new StructureModel { Children = childStructures, CurrentWithParents = parentStructures });
        }

        /// <summary>
        /// Метод для редактирования структуры РОСТ.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<ActionResult> Edit(StructureUpdateModel model, CancellationToken cancellationToken)
        {
            var structure = await _structureService.GetAsync(model.Id, cancellationToken);

            if (structure == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseModel { Status = ResponseErrors.NotFound, Message = $"Не найдено структуры с Id = {model.Id}" });
            }
            
            structure.Name = model.Name;

            try
            {
                await _structureService.UpdateAsync(structure, cancellationToken);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseModel { Status = ResponseErrors.Error, Message = ex.Message });
            }
            
            var (currentStructureWithParents, children) = await _structureService.GetStructureHierarchyAsync(structure.ParentId, cancellationToken);

            var parentStructures = _mapper.Map<List<ParentStructure>>(currentStructureWithParents);
            var childStructures = _mapper.Map<List<SubStucture>>(children);

            foreach (var parent in parentStructures)
            {
                if (parent.Id != null)
                {
                    var parentInstitutions =
                        await _institutionService.GetByStructureIdAsync(parent.Id.Value, cancellationToken);
                    parent.Institutions = _mapper.Map<List<InstitutionListModel>>(parentInstitutions);
                }
            }
            
            foreach (var child in childStructures)
            {
                var childInstitutions =
                    await _institutionService.GetByStructureIdAsync(child.Id, cancellationToken);
                child.Institutions = _mapper.Map<List<InstitutionListModel>>(childInstitutions);
            }
            
            return Ok(new StructureModel { Children = childStructures, CurrentWithParents = parentStructures });
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var structure = await _structureService.GetWithChildrenAsync(id, cancellationToken);
            if (structure != null)
            {
                if (structure.Children.Any())
                {
                    ModelState.AddModelError(nameof(id), $"Структура с идентификатором {id} не может быть удалена, т.к. содержит дочерние структуры.");
                    return ValidationProblem(ModelState);
                }

                try
                {
                    await _structureService.DeleteAsync(structure, cancellationToken);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new ResponseModel { Status = ResponseErrors.Error, Message = ex.Message });
                }
            }

            return Ok();
        }

        /// <summary>
        /// Метод возвращает список учреждений, находящихся в структуре с ID <paramref name="structureId"/>.
        /// </summary>
        /// <param name="structureId">Идентификатор структуры, выбранной в качестве текущего уровня иерархии.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Route("Institution/List/ByStructure")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<InstitutionAdminModel>>> GetInstitutionsByStructure(int structureId, CancellationToken cancellationToken)
        {
            var structureExists = await _structureService.ExistsAsync(s => s.Id == structureId, cancellationToken);
            if (!structureExists)
            {
                ModelState.AddModelError(nameof(structureId), $"Структура с идентификатором {structureId} не найдена.");
                return ValidationProblem(ModelState);
            }

            var institutions = await _institutionService.GetByStructureIdAsync(structureId, cancellationToken);
            var institutionModels = _mapper.Map<List<InstitutionAdminModel>>(institutions);
            return Ok(institutionModels);
        }
    }
}