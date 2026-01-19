
using Application;
using Data;
using MenuOnWeek.Contracts;
using MenuOnWeek.Web.ExeptionHandlers;

namespace Web;


/// <inheritdoc/>
public class Program
{
    /// <inheritdoc/>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddApplication();
        builder.Services.AddData();

        builder.Services.AddLogging();

        builder.Services.AddExceptionHandler<LoggerExeptionHandler>();
        builder.Services.AddExceptionHandler<ProblemDetailsExeptionHandler>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            var xmlName = $"{typeof(Program).Assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlName));
            xmlName = $"{typeof(ApiResource).Assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlName));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.UseExceptionHandler(_ => { });

        app.MapControllers();

        app.Run();
    }
}
