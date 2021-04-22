using FruitAndVegi.Models;
using Microsoft.EntityFrameworkCore;

namespace FruitAndVeg.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
       public DbSet<FruitsAndVegetable> FruitsAndVegetable {get;set;}
    }
}