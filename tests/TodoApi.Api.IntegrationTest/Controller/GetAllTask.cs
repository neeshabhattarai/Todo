using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApi.Api.IntegrationTest.Service;
using Xunit;
namespace TodoApi.Api.IntegrationTest;

public class GetAllTask:IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly CustomWebApplicationFactory<Program> _factory;
  private readonly HttpClient _httpClient;

  public GetAllTask(CustomWebApplicationFactory<Program> factory)
  {
    _factory = factory;
    _httpClient=factory.CreateClient();
  }
  [Fact]
  public async Task GetAllTask_ShouldReturnTasks()
  {
   var result=await _httpClient.GetAsync("api/Task/GetTask?pageNumber=1&pageSize=10&orderBy=Id&sortDirection=asc");
   result.EnsureSuccessStatusCode();
   Assert.Equal(result.StatusCode,HttpStatusCode.OK);
  }
}