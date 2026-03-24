using Microsoft.AspNetCore.Authorization;

namespace TodoApi.Infastructure.AuthorizationRequirement;

public class AgeCheckerHandler:AuthorizationHandler<AgeChecker>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeChecker requirement)
    {
        if (requirement.Age >= 20)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        context.Fail();
        return Task.CompletedTask;
    }
}