using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TodoApi.Domain.Entities;

namespace TodoApi.Infastructure.Service;

public class UserCalims:UserClaimsPrincipalFactory<User>
{
    private readonly UserManager<User> _userManager;
    private readonly IOptions<IdentityOptions> _optionsAccessor;

    public UserCalims(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
    {
        _userManager = userManager;
        _optionsAccessor = optionsAccessor;
    }

    public async override Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user);
        if (id == null)
        {
            throw new Exception("User not found");
        }
        id.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        return new ClaimsPrincipal(id);
        
    }
}