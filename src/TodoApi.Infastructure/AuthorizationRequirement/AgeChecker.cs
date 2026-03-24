using Microsoft.AspNetCore.Authorization;

namespace TodoApi.Infastructure.AuthorizationRequirement;

public class AgeChecker(int age):IAuthorizationRequirement
{
    public int Age { get; } = age;
}