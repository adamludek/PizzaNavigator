using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using PizzaNavigator.API.Models.Entities;

namespace PizzaNavigator.API.Db
{
    public class PizzaNavigatorContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Pizzeria> Pizzerias { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<User> Users { get; set; }
        public PizzaNavigatorContext(DbContextOptions<PizzaNavigatorContext> options) : base(options)
        {
            var dbCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (dbCreater != null)
            {
                // Create Database 
                if (!dbCreater.CanConnect())
                {
                    dbCreater.Create();
                }

                // Create Tables
                if (!dbCreater.HasTables())
                {
                    dbCreater.CreateTables();
                }
            }
        }
    }
}
