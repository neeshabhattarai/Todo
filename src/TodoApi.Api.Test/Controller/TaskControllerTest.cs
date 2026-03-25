using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TodoApi.Api.Test.Controller;

public class TaskControllerTest:IClassFixture<WebApplicationFactory<Program>>
{
    public HttpClient client;
    public TaskControllerTest(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async void GetAllTask_returns_AllTask_WithSuccess()
    {
        var taskController = await client.GetAsync("/api/Task/GetTask");
        taskController.EnsureSuccessStatusCode();
        Assert.Equal(taskController.StatusCode,HttpStatusCode.OK);
    }
}