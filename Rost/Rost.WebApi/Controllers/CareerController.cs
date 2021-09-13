using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rost.Services.Infrastructure;
using Rost.WebApi.Models.Career;
using Rost.WebApi.Models.CareerDirection;

namespace Rost.WebApi.Controllers
{
    /// <summary>
    /// Методы для работы с профориентацией
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CareerController : ControllerBase
    {
        private readonly ICareerService _careerService;
        private readonly ICareerDirectionService _careerDirectionService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="careerService"></param>
        /// <param name="careerDirectionService"></param>
        /// <param name="mapper"></param>
        public CareerController(ICareerService careerService, ICareerDirectionService careerDirectionService, IMapper mapper)
        {
            _careerService = careerService;
            _careerDirectionService = careerDirectionService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Метод возвращает все доступные профориентации 
        /// </summary>
        /// <returns></returns>
        [Route("List/All")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CareerModel>>> List()
        {
            var careers = await _careerService.ListAsync();

            if (careers == null || !careers.Any())
            {
                return NotFound();
            }

            var model = _mapper.Map<List<CareerModel>>(careers);

            return Ok(model);
        }

        /// <summary>
        /// Метод возвращает список профориентаций в выбранном направлении
        /// </summary>
        /// <param name="directionId">Иденитификатор направления</param>
        /// <returns></returns>
        [Route("List/ByDirection")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CareerModel>>> ListByDirectionId(int directionId)
        {
            if (directionId == 0)
            {
                return BadRequest("Не выбрано направление профориентации");
            }

            var careers = await _careerService.ListByDirectionId(directionId);
            
            if (careers == null || !careers.Any())
            {
                return NotFound();
            }

            var model = _mapper.Map<List<CareerModel>>(careers);

            return Ok(model);
        }

        /// <summary>
        /// Метод для получения направлений профориентаций со списком профориентаций
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("direction/list")]
        public async Task<ActionResult<IEnumerable<CareerDirectionModel>>> ListDirections()
        {
            var careerDirections = await _careerDirectionService.ListWithCareersAsync();
            
            if (careerDirections == null || !careerDirections.Any())
            {
                return NotFound();
            }

            var model = _mapper.Map<List<CareerDirectionModel>>(careerDirections);

            return Ok(model);
        }
    }
}