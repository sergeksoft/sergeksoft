using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on port 3000
builder.WebHost.ConfigureKestrel(options => {
    options.ListenAnyIP(3000);
});

var app = builder.Build();

// Middleware to intercept requests and return an HTML page based on the requested domain name
app.Use(async (context, next) =>
{
    // Extract the domain name from the request host header
    var domain = context.Request.Host.Host;
    
    // Generate an HTML response based on the domain
    var htmlContent = $"<html><body><h1>Requested Domain: {domain}</h1></body></html>";

    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(htmlContent);
});

app.Run();