using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Todos.Controllers;
using Xunit;

namespace Todos.Tests;

public class SwaggerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public SwaggerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ReturnsSwaggerDocument()
    {
        var client = _factory.CreateClient();
        
        var response = await client.GetAsync("/swagger/v1/swagger.json");
        var content = await response.Content.ReadAsStringAsync();
     
        var routes = typeof(Routes.Todos).GetFields().Select(r => r.GetValue(r).ToString());
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().ContainAll(routes);
    }
}
