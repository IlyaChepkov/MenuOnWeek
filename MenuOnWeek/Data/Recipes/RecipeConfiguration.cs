using MenuOnWeek.Domain.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = MenuOnWeek.Domain.Files.File;

namespace MenuOnWeek.Data.Recipes;

public sealed class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("Recipes");

        builder.HasKey(x => x.Id);

        builder.HasOne<File>().WithOne().HasForeignKey<Recipe>(foreignKeyExpression: x => x.FileId);
    }
}
