using System.Reflection;
using MenuOnWeek.Domain.Ingredients;
using MenuOnWeek.Domain.Menus;
using MenuOnWeek.Domain.Recipes;
using MenuOnWeek.Domain.Units;
using Microsoft.EntityFrameworkCore;
using File = MenuOnWeek.Domain.Files.File;

namespace Data;

internal sealed class DataContext : DbContext
{
    public DbSet<Menu> Menus => Set<Menu>();

    public DbSet<MenuRecipes> MenuRecipes => Set<MenuRecipes>();

    public DbSet<Recipe> Recipes => Set<Recipe>();

    public DbSet<RecipeIngredients> RecipeIngredients => Set<RecipeIngredients>();

    public DbSet<Ingredient> Ingredients => Set<Ingredient>();

    public DbSet<IngredientUnits> IngredientUnits => Set<IngredientUnits>();

    public DbSet<Unit> Units => Set<Unit>();

    public DbSet<File> Files => Set<File>();

     /*

    public DataContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=helloapp.db");
    }
    
    */

    public DataContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
