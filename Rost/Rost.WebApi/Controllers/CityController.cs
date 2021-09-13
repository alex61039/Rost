using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;
using Rost.WebApi.Models.City;

namespace Rost.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cityService"></param>
        /// <param name="mapper"></param>
        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Возвращает список всех городов
        /// </summary>
        /// <returns></returns>
        [Route("List/All")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityModel>>> List()
        {
            var cities = await _cityService.ListAsync();

            if (cities == null || !cities.Any())
            {
                return NotFound("Не найдено ни одного города");
            }

            var model = _mapper.Map<List<CityModel>>(cities);

            return Ok(model);
        }

        /// <summary>
        /// Добавляет в список новый город
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> AddCity([FromBody] CityModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name))
            {
                return BadRequest("Наименование не должно быть пустым");
            }

            var city = await _cityService.GetByNameAsync(model.Name);

            if (city != null)
            {
                return BadRequest($"Город с названием {model.Name} уже существует");
            }

            city = _mapper.Map<City>(model);

            await _cityService.AddAsync(city);

            return Ok();
        }
    }
}