using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;
using Rost.WebApi.Models.District;

namespace Rost.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _districtService;
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="districtService"></param>
        /// <param name="cityService"></param>
        /// <param name="mapper"></param>
        public DistrictController(IDistrictService districtService, ICityService cityService, IMapper mapper)
        {
            _districtService = districtService;
            _cityService = cityService;
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список районов
        /// </summary>
        /// <param name="cityId">Если параметр указан, то возвращаются районы для выбранного города</param>
        /// <returns></returns>
        [Route("List/All")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictModel>>> List(int cityId)
        {
            IEnumerable<District> districts;

            if (cityId == 0)
            {
                districts = await _districtService.ListAsync();
            }
            else
            {
                districts = await _districtService.ListByCityIdAsync(cityId);
            }

            if (districts == null || !districts.Any())
            {
                return BadRequest("Не найдено ни одного района");
            }

            var model = _mapper.Map<List<DistrictModel>>(districts);

            return Ok(model);
        }

        /// <summary>
        /// Метод для добавления нового района
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult<string>> Add([FromBody] DistrictCreateModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Наименование не должно быть пустым");
            }

            var city = await _cityService.GetAsync(model.CityId);

            if (city == null)
            {
                return BadRequest($"Город с идентификатором {model.CityId} не найден");
            }   
            
            var district = await _districtService.GetByNameAsync(model.Name);

            if (district != null)
            {
                return BadRequest($"Район с названием {model.Name} уже существует");
            }

            district = _mapper.Map<District>(model);

            await _districtService.AddAsync(district);

            return Ok();
        }
    }
}