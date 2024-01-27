using PizzaNavigator.API.Models.Entities;

namespace PizzaNavigator.API.Models.Dto
{
    public class InfoPizzeriaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Delivery { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string User { get; set; }
        public List<InfoPizzaDto> Pizzas { get; set; }
    }
}
