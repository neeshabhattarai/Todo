using Microsoft.AspNetCore.Authorization;

namespace TodoApi.Infastructure.AuthorizationRequirement;

public class EmailChecker(string email):IAuthorizationRequirement
{
 public string Email { get; } = email;
}