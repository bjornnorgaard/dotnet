using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
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

        var response = await client.GetAsync("/swagger");
    
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
