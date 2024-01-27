using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaNavigator.API.Db;
using PizzaNavigator.API.Models.Dto;
using PizzaNavigator.API.Models.Entities;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace PizzaNavigator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PizzeriaController : ControllerBase
    {
        private readonly PizzaNavigatorContext _dbContext;
        private readonly IMapper _mapper;
        public PizzeriaController(PizzaNavigatorContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<InfoPizzeriaDto>> GetPizzerias()
        {
            var pizzerias = _dbContext.Pizzerias.Include(p => p.Address).Include(p => p.User).ToList();

            var pizzeriasDto = _mapper.Map<List<InfoPizzeriaDto>>(pizzerias);

            return Ok(pizzeriasDto);
        }

        [AllowAnonymous]
        [HttpGet("s")]
        public ActionResult<IEnumerable<InfoPizzeriaDto>> GetPizzeriasByCity([FromQuery] string value)
        {
            var pizzerias = _dbContext.Pizzerias.Include(p => p.Address).Include(p => p.User).Where(p => p.Name.Contains(value) || p.Address.City.Contains(value)).ToList();

            var pizzeriasDto = _mapper.Map<List<InfoPizzeriaDto>>(pizzerias);

            return Ok(pizzeriasDto);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<InfoPizzeriaDto> GetPizzeria([FromRoute] int id)
        {
            var pizzeria = _dbContext.Pizzerias.Include(p => p.Address).FirstOrDefault(p => p.Id == id);
            if (pizzeria != null)
            {
                var pizzeriaDto = _mapper.Map<List<InfoPizzeriaDto>>(pizzeria);
                return Ok(pizzeriaDto);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult AddPizzerria([FromBody] NewPizzeriaDto dto)
        {
            // if (dto is null || dto.Name == "" || dto.Description == "" || dto.PhoneNumber == "" || dto.Street == "" || dto.PostalCode == "" || dto.City == "") { }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var newPizzeria = new Pizzeria()
            {
                Name = dto.Name,
                Description = dto.Description,
                Delivery = dto.Delivery,
                PhoneNumber = dto.PhoneNumber,
                Address = new Address()
                {
                    Street = dto.Street,
                    PostalCode = dto.PostalCode,
                    City = dto.City,
                },
                UserId = new Guid(userId)
            };

            _dbContext.Pizzerias.Add(newPizzeria);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePizzeria([FromRoute] int id, [FromBody] NewPizzeriaDto dto) 
        {
          //  if (dto is null || dto.Name == "" || dto.Description == "" || dto.PhoneNumber == "" || dto.Street == "" || dto.PostalCode == "" || dto.City == "") { }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var pizzeria = _dbContext.Pizzerias.Include(p => p.Address).FirstOrDefault(p => p.Id == id && p.UserId.ToString() == userId);

            if (pizzeria is null) return BadRequest("Not allowed");

            pizzeria.Name = dto.Name;
            pizzeria.Description = dto.Description;
            pizzeria.Delivery = dto.Delivery;
            pizzeria.Address.Street = dto.Street;
            pizzeria.Address.PostalCode = dto.PostalCode;
            pizzeria.Address.City = dto.City;

            _dbContext.Pizzerias.Update(pizzeria);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult RemovePizzeria([FromRoute] int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var pizzeria = _dbContext.Pizzerias.FirstOrDefault(p => p.Id == id && p.UserId.ToString() == userId);

            if (pizzeria == null) 
            {
                return BadRequest();
            }

            var pizzas = _dbContext.Pizzas.Where(p => p.PizzeriaId == pizzeria.Id);

            _dbContext.Pizzerias.Remove(pizzeria);
            _dbContext.Pizzas.RemoveRange(pizzas);
            _dbContext.SaveChanges();
            return Ok(pizzeria);
        }
    }
}
