using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace MenuOnWeek.Data;

internal sealed class DBContextOptionsFactory<T> where T : DbContext
{
    private readonly DataOptions dataOptions;

    public DBContextOptionsFactory(IOptions<DataOptions> options)
    {
        dataOptions = options.Value;
    }

    public DbContextOptionsBuilder SetupOptions(DbContextOptionsBuilder options)
    {
        options.UseSqlite(dataOptions.MenuOnWeek);
        return options;
    }
}
