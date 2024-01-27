namespace PizzaNavigator.API.Models.Dto
{
    public class NewPizzaaDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PriceSmall { get; set; }
        public decimal PriceMedium { get; set; }
        public decimal PriceLarge { get; set; }
    }
}
