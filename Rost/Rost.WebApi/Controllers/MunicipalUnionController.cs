using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;
using Rost.WebApi.Models.MunicipalUnion;

namespace Rost.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MunicipalUnionController : ControllerBase
    {
        private readonly IDistrictService _districtService;
        private readonly IMunicipalUnionService _municipalUnionService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="municipalUnionService"></param>
        /// <param name="districtService"></param>
        /// <param name="mapper"></param>
        public MunicipalUnionController(IDistrictService districtService, IMunicipalUnionService municipalUnionService, IMapper mapper)
        {
            _districtService = districtService;
            _municipalUnionService = municipalUnionService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Возвращает список муниципальных округов
        /// </summary>
        /// <param name="id">Если параметр указан, то возвращаются округа для выбранного района</param>
        /// <returns></returns>
        [Route("List/All")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MunicipalUnionModel>>> List(int id)
        {
            IEnumerable<MunicipalUnion> municipalUnions;

            if (id == 0)
            {
                municipalUnions = await _municipalUnionService.ListAsync();
            }
            else
            {
                municipalUnions = await _municipalUnionService.ListByDisrtictId(id);
            }

            if (municipalUnions == null || !municipalUnions.Any())
            {
                return BadRequest("Не найдено ни одного района");
            }

            var model = _mapper.Map<List<MunicipalUnion>>(municipalUnions);

            return Ok(model);
        }
        
        /// <summary>
        /// Метод для добавления нового муниципального образования
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult<string>> Add([FromBody] MunicipalUnionCreateModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Наименование не должно быть пустым");
            }

            var district = await _districtService.GetAsync(model.DistrictId);

            if (district == null)
            {
                return BadRequest($"Район с идентификатором {model.DistrictId} не найден");
            }   
            
            var municipalUnion = await _municipalUnionService.GetByNameAsync(model.Name);

            if (municipalUnion != null)
            {
                return BadRequest($"Муниципальное объединение с названием {model.Name} уже существует");
            }

            municipalUnion = _mapper.Map<MunicipalUnion>(model);

            await _municipalUnionService.AddAsync(municipalUnion);

            return Ok();
        }
    }
}