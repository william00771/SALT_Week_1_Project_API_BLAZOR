using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Salt.Stars.Web;
using Salt.Stars.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IGreetingApiClient, GreetingApiClient>();
builder.Services.AddScoped<IHeroApiClient, HeroApiClient>();

await builder.Build().RunAsync();
