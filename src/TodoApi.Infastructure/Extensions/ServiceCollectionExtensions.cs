using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Infastructure.Persistency;
using Swashbuckle.AspNetCore;
using Microsoft.AspNetCore.Identity;
using TodoApi.Domain.Entities;
using TodoApi.Infastructure.AuthorizationRequirement;
using TodoApi.Infastructure.Service;

namespace TodoApi.Infastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfastructure(this IServiceCollection collection, IConfigurationManager configuration)
    {
        collection.AddDbContext<TodoApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        collection.AddIdentityApiEndpoints<User>().AddClaimsPrincipalFactory<UserCalims>().AddEntityFrameworkStores<TodoApplicationDbContext>();
        collection.AddEndpointsApiExplorer();
        collection.AddScoped<IAuthorizationHandler, AgeCheckerHandler>();
        collection.AddScoped<IAuthorizationHandler,EmailCheckerHandler>();
        collection.AddAuthorizationBuilder()
            .AddPolicy("CheckAge", builder => builder.AddRequirements(new AgeChecker(22)))
            .AddPolicy("CheckEmail", builder => builder.AddRequirements(new EmailChecker("hello12@gmail.com")));

    }
}

