using Microsoft.AspNetCore.Identity;
using PizzaNavigator.API.Models.Entities;

namespace PizzaNavigator.API.Db
{
    public class Seeder
    {
        private readonly PizzaNavigatorContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public Seeder(PizzaNavigatorContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public void Seed()
        {
            if (_context.Database.CanConnect()) 
            {
                if(!_context.Users.Any())
                {
                    var data = GetData();
                    _context.Users.AddRange(data);
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<User> GetData()
        {
            var data = new List<User>();
            var user1 = new User()
            {
                FirstName = "Konstanty",
                LastName = "Nowak",
                Email = "demo@demo.pl",
                Username = "demo",
                Pizzerias = new List<Pizzeria> { new Pizzeria()
                {
                    Name = "Pizzeria Prima",
                    Description = "description lorem lorem",
                    Delivery = true,
                    PhoneNumber = "1234567890",
                    Address = new Address
                    {
                        Street = "Mickiewicza 80",
                        City = "Gdańsk",
                        PostalCode = "12-345",
                    }

                },
                new Pizzeria()
                {
                    Name = "Pizzeria Prima",
                    Description = "description lorem lorem lorem",
                    Delivery = true,
                    PhoneNumber = "1234567891",
                    Address = new Address
                    {
                        Street = "Promowa 55",
                        City = "Gdynia",
                        PostalCode = "12-347",
                    }

                },
                new Pizzeria()
                {
                    Name = "Pizzeria Italliana",
                    Description = "description lorem lorem ipsum",
                    Delivery = false,
                    PhoneNumber = "1234567890",
                    Address = new Address
                    {
                        Street = "Słowackiego 4",
                        City = "Gdynia",
                        PostalCode = "12-405",
                    }

                }
                }
            };
            user1.Password = _passwordHasher.HashPassword(user1, "Demo123");
            var user2 = new User()
            {
                FirstName = "Robert",
                LastName = "Kowalski",
                Email = "demo2@demo.pl",
                Username = "demo_2",
                Pizzerias = new List<Pizzeria> { new Pizzeria()
                {
                    Name = "Pizzeria Gusto",
                    Description = "description lorem lorem",
                    Delivery = true,
                    PhoneNumber = "1234567800",
                    Address = new Address
                    {
                        Street = "Pokątna 20",
                        City = "Poznań",
                        PostalCode = "18-435",
                    }

                },
                new Pizzeria()
                {
                    Name = "Pizza Hot",
                    Description = "description lorem lorem lorem",
                    Delivery = true,
                    PhoneNumber = "1234567891",
                    Address = new Address
                    {
                        Street = "Kopernika 93",
                        City = "Katowice",
                        PostalCode = "82-412",
                    }

                },
                new Pizzeria()
                {
                    Name = "Pizza Hot",
                    Description = "description lorem lorem ipsum",
                    Delivery = true,
                    PhoneNumber = "1234567890",
                    Address = new Address
                    {
                        Street = "Krucza 7",
                        City = "Wrocław",
                        PostalCode = "12-409",
                    }

                }
                }
            };
            user1.Password = _passwordHasher.HashPassword(user1, "Demo123");
            data.Add(user2);
            data.Add(user1);
            return data;
        }
    }
}
