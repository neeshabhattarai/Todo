using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TodoApi.Infastructure.Persistency;

namespace TodoApi.Api.IntegrationTest.Service;

public class CustomWebApplicationFactory<T>:WebApplicationFactory<T> where T:class
{
 
    protected void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(x=>x.ServiceType==typeof(TodoApplicationDbContext));
            if(descriptor != null)
                services.Remove(descriptor);
            services.AddDbContext<TodoApplicationDbContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=BlogApplicationDbTest");
            });
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<TodoApplicationDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        });
    }
}