namespace PizzaNavigator.API.Models.Entities
{
    public class Pizzeria
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Delivery { get; set; }
        public string PhoneNumber { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Pizza> Pizzas { get; set; }
    }
}
