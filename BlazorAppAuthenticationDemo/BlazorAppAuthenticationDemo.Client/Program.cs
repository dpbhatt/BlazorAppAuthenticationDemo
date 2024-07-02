using BlazorAppAuthenticationDemo.Client;
using BlazorAppAuthenticationDemo.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped<TemplateService>();

//builder.Services.AddScoped(http => new HttpClient
//{
//    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
//});

//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddHttpContextAccessor();

//builder.Services.AddScoped<IdentityCookieHandler>();
builder.Services.AddHttpClient("serverApi", options =>
{
    options.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    //options.DefaultRequestHeaders.Add("Accept", "application/json");

});//.AddHttpMessageHandler<IdentityCookieHandler>();

await builder.Build().RunAsync();