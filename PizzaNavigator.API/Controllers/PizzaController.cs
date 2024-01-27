using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaNavigator.API.Db;
using PizzaNavigator.API.Models.Dto;
using PizzaNavigator.API.Models.Entities;
using System.Security.Claims;

namespace PizzaNavigator.API.Controllers
{
    [Route("api/pizzeria/{pizzeriaId}/[controller]")]
    [ApiController]
    [Authorize]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaNavigatorContext _dbContext;
        public PizzaController(PizzaNavigatorContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Pizza>> GetPizzas([FromRoute] int pizzeriaId) 
        {
            var pizzas = _dbContext.Pizzas.Where(p => p.PizzeriaId == pizzeriaId).ToList();
            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        public ActionResult<Pizza> GetPizza([FromRoute] int pizzeriaId, [FromRoute] int id)
        {
            var pizzas = _dbContext.Pizzas.Where(p => p.Id == id && p.PizzeriaId == pizzeriaId);
            return Ok(pizzas);
        }

        [HttpPost]
        public ActionResult AddPizza([FromRoute] int pizzeriaId, [FromBody] NewPizzaaDto dto)
        {
            var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var pizzeria = _dbContext.Pizzerias.FirstOrDefault(p => p.Id == pizzeriaId && p.UserId.ToString() == userId);

            if (pizzeria is null) return BadRequest();

            var newPizza = new Pizza()
            {
                Name = dto.Name,
                Description = dto.Description,
                PriceSmall = dto.PriceSmall,
                PriceMedium = dto.PriceMedium,
                PriceLarge = dto.PriceLarge,

            };

            _dbContext.Pizzas.Add(newPizza);
            _dbContext.SaveChanges();
            return Ok(newPizza);
        }

        [HttpPut("edit/{id}")]
        public ActionResult UpdatePizza([FromRoute] int pizzeriaId, [FromRoute] int id, [FromBody] NewPizzaaDto dto)
        {
            var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var pizzeria = _dbContext.Pizzerias.FirstOrDefault(p => p.Id == pizzeriaId && p.UserId.ToString() == userId);

            if (pizzeria is null) return BadRequest();

            var pizza = _dbContext.Pizzas.FirstOrDefault(p => p.PizzeriaId == pizzeriaId && p.Id == id);

            if (pizza is null) return BadRequest();

            pizza.Name = dto.Name;
            pizza.Description = dto.Description;
            pizza.PriceSmall = dto.PriceSmall;
            pizza.PriceMedium = dto.PriceMedium;
            pizza.PriceLarge = dto.PriceLarge;

            _dbContext.Pizzas.Update(pizza);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public ActionResult RemovePizzas([FromRoute] int pizzeriaId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var pizzeria = _dbContext.Pizzerias.FirstOrDefault(p => p.Id == pizzeriaId && p.UserId.ToString() == userId);

            if (pizzeria is null) return BadRequest();

            var pizzas = _dbContext.Pizzas.Where(p => p.PizzeriaId == pizzeriaId);

            _dbContext.Pizzas.RemoveRange(pizzas);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult RemovePizza([FromRoute] int pizzeriaId, [FromRoute] int id)
        {
            var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var pizzeria = _dbContext.Pizzerias.FirstOrDefault(p => p.Id == pizzeriaId && p.UserId.ToString() == userId);

            if (pizzeria is null) return BadRequest();

            var pizza = _dbContext.Pizzas.Where(p => p.PizzeriaId == pizzeriaId && p.Id == id);

            _dbContext.Remove(pizza);
            _dbContext.SaveChanges();
            return Ok();
        }
    }

}
