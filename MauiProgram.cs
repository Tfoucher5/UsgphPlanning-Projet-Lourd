using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using UsgphPlanning.Services;

namespace UsgphPlanning
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            var connectionString = "server=10.192.145.6;port=3306;database=usgph_planning;user=homestead;password=Not24get;";

            builder.Services.AddDbContextFactory<UsgphPlanningDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                       .LogTo(Console.WriteLine, LogLevel.Information);
            });

            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("RequireAuthenticated", policy =>
                    policy.RequireAuthenticatedUser());
            });

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            
            builder.Services.AddScoped<DatabaseService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public class UnhandledExceptionLogger
        {
            public UnhandledExceptionLogger()
            {
                AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                {
                    var exception = args.ExceptionObject as Exception;
                    System.Diagnostics.Debug.WriteLine($"ERREUR NON GÉRÉE : {exception?.Message}");
                    System.Diagnostics.Debug.WriteLine($"STACK TRACE : {exception?.StackTrace}");

                    // Journalisation potentielle dans un fichier
                    File.WriteAllText("error_log.txt",
                        $"Date: {DateTime.Now}\nMessage: {exception?.Message}\nStack Trace: {exception?.StackTrace}");
                };
            }
        }
    }
}
