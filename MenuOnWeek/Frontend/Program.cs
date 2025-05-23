using Microsoft.Extensions.DependencyInjection;

namespace MenuOnWeek.Frontend;

internal static class Program
{
    internal static IServiceProvider ServiceProvider;

    static Program()
    {
        ServiceProvider = Startup.Load(new ServiceCollection());
    }

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        System.Windows.Forms.Application.Run(new Form1());
    }
}
