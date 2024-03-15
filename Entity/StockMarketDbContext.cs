using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class StockMarketDbContext : DbContext
    {
        public StockMarketDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BuyOrder> BuyOrders { get; set; }

        public DbSet<SellOrder> SellOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");

            modelBuilder.Entity<SellOrder>().ToTable("SellOrders");

            //Seed to BuyOrders

            string buyOrdersJson = File.ReadAllText("buyOrders.json");
            List<BuyOrder>? buyOrders = System.Text.Json.JsonSerializer.Deserialize<List<BuyOrder>>(buyOrdersJson);

            foreach (var item in buyOrders)
            {
                modelBuilder.Entity<BuyOrder>().HasData(item);
            }

            //Seed to Persons

            string sellOrdersJson = File.ReadAllText("sellOrders.json");

            List<SellOrder> sellOrders = System.Text.Json.JsonSerializer.Deserialize<List<SellOrder>>(sellOrdersJson);

            foreach (var item in sellOrders)
            {
                modelBuilder.Entity<SellOrder>().HasData(item);
            }
        }
    }
}
