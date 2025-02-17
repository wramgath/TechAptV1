// Copyright © 2025 Always Active Technologies PTY Ltd

using Microsoft.EntityFrameworkCore;
using Serilog;
using TechAptV1.Client.Components;
using TechAptV1.Client.Data;
using TechAptV1.Client.Interfaces;
using TechAptV1.Client.Services;

namespace TechAptV1.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.Title = "Tech Apt V1";

                var builder = WebApplication.CreateBuilder(args);

                var connectionString = builder.Configuration.GetConnectionString("Default");

                builder.Services.AddSerilog(lc => lc
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
                    .ReadFrom.Configuration(builder.Configuration));

                // Add services to the container.
                builder.Services.AddRazorComponents().AddInteractiveServerComponents();

                builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

                builder.Services.AddScoped<DataService>();
                builder.Services.AddScoped<ThreadingService>();
                builder.Services.AddScoped<IOddNumberService,OddNumberService>();
                builder.Services.AddScoped<IEvenNumberService,EvenNumberService>();
                builder.Services.AddScoped<IPrimeNumberService, PrimeNumberService>();
                builder.Services.AddScoped<IXmlService, XmlService>();
                builder.Services.AddScoped<IBinaryService, BinaryService>();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Error");
                }

                app.UseStaticFiles();
                app.UseAntiforgery();

                app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

                app.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Fatal exception in Program");
                Console.WriteLine(exception);
            }
        }
    }
}
