using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TodoApi.Application.User;

namespace TodoApi.Infastructure.AuthorizationRequirement;

public class EmailCheckerHandler:AuthorizationHandler<EmailChecker>
{


    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailChecker requirement)
    {
        if (requirement.Email == "hello12@gmail.com")
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        context.Fail();
        return Task.CompletedTask;
    }
}