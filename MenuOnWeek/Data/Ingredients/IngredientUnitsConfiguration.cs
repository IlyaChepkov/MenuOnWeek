using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenuOnWeek.Data.Ingredients;

public class IngredientUnitsConfiguration : IEntityTypeConfiguration<IngredientUnits>
{
    public void Configure(EntityTypeBuilder<IngredientUnits> builder)
    {
        builder.ToTable("IngredientUnits");

        builder.HasKey(k => new { k.IngredientId, k.UnitId })
            .HasName("pk_dbo.IngredientUnits");

        builder.HasOne(x => x.Ingredient)
            .WithMany(x => x.IngredientUnits)
            .HasForeignKey(x => x.IngredientId);
    }
}
