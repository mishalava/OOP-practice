using Microsoft.Extensions.DependencyInjection;
using WinFormsAppSport.Data;
using WinFormsAppSport.Services;
using Microsoft.EntityFrameworkCore;

namespace WinFormsAppSport
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());

            var services = new ServiceCollection();

            services.AddDbContext<WorkoutContext>(options =>
            options.UseSqlite("Data Source=workouts.db"));


            services.AddTransient<WorkoutService>();
            services.AddTransient<Form1>();

            using var serviceProvider = services.BuildServiceProvider();

            var form = serviceProvider.GetRequiredService<Form1>();

            Application.Run(form);
            //привет!
        }
    }
}