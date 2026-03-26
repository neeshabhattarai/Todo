using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TodoApi.Application.User;

public  interface IUserContext
{
    public  CurrentUser? GetCurrentUser();
}

public class UserContext(IHttpContextAccessor httpAccessorContext):IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpAccessorContext.HttpContext.User;
        if (user == null)
        {
           throw new Exception("User Not Found"); 
        }
var Id= user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var Email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var UserName=user.FindFirst(c=>c.Type==ClaimTypes.Name)!.Value;
        return new CurrentUser(Id,Email,UserName,"ADMIN");
    }
}