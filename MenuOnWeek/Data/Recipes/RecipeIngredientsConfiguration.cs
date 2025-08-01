using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using MenuOnWeek.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenuOnWeek.Data.Recipes;

public sealed class RecipeIngredientsConfiguration : IEntityTypeConfiguration<RecipeIngredients>
{
    public void Configure(EntityTypeBuilder<RecipeIngredients> builder)
    {
        builder.ToTable("RecipeIngredients");

        builder.HasKey(k => new { k.RecipeId, k.IngredientId })
            .HasName("pk_dbo.RecipeIngredients");

        builder.HasOne(x => x.Recipe)
            .WithMany(x => x.RecipeIngredients)
            .HasForeignKey(x => x.RecipeId);
    }
}
