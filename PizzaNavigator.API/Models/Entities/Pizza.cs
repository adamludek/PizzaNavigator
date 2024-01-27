namespace PizzaNavigator.API.Models.Entities
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PriceSmall { get; set; }
        public decimal PriceMedium { get; set; }
        public decimal PriceLarge { get; set; }
        public int PizzeriaId { get; set; }
        public Pizzeria Pizzeria { get; set; }
    }
}
