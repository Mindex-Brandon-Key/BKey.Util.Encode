
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

namespace BKey.Util.Encode.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddControllers();
        builder.Services.AddEncodings();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "encoding-ui", "browser")),
            RequestPath = ""
        });

        // Serve assets like images from wwwroot/assets
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "assets")),
            RequestPath = "/assets"
        });

        app.UseRouting();

        //app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.UseEndpoints(endpoints =>
        {
            foreach (var endpoint in endpoints.DataSources.SelectMany(ds => ds.Endpoints))
            {
                var routeEndpoint = endpoint as RouteEndpoint;
                if (routeEndpoint != null)
                {
                    Console.WriteLine($"Endpoint: {routeEndpoint.RoutePattern.RawText}");
                }
            }
        });

        var appTask = app.RunAsync();

        if (app.Environment.IsProduction())
        {
            // Open the default browser to the application's URL
            string url = "http://localhost:5000/index.html"; // Replace with the appropriate URL
            await WaitForServerToBeAvailableAsync(url);
            OpenBrowser(url);
        }

        await appTask;
    }

    private static async Task WaitForServerToBeAvailableAsync(string url)
    {
        using var client = new HttpClient();
        int retries = 10;
        int delayMilliseconds = 500;

        for (int i = 0; i < retries; i++)
        {
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return; // Server is ready
                }
            }
            catch
            {
                // Ignore exceptions (server is not ready yet)
            }

            await Task.Delay(delayMilliseconds); // Wait before retrying
        }

        // Optionally handle the case where the server didn't start in time
        Console.WriteLine("Warning: Server did not start in the expected time.");
    }

    private static void OpenBrowser(string url)
    {
        try
        {
            // Open URL in default browser
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            // Log or handle exception if needed
            Console.WriteLine($"Could not open browser: {ex.Message}");
        }
    }
}
