using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenuOnWeek.Data.Menu;

public sealed class Me1nuRecipesConfiguration : IEntityTypeConfiguration<MenuRecipes>
{
    public void Configure(EntityTypeBuilder<MenuRecipes> builder)
    {
        builder.ToTable("MenuRecipes");

        builder.HasOne(x => x.Menu)
            .WithMany(x => x.MenuRecipes)
            .HasForeignKey(x => x.MenuId);
    }
}
