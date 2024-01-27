namespace PizzaNavigator.API.Models.Dto
{
    public class NewPizzeriaDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Delivery { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

    }
}
