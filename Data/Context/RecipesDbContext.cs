using Data.Entities;
using System.Data.Entity;

namespace Data.Context
{
  public  class RecipesDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
