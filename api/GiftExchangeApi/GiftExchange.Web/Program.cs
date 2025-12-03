using GiftExchange.Web.DependencyInjection;
using GiftExchange.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebDependencyInjection();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "~/{controller}/{action}").WithRequestTimeout(TimeSpan.FromMinutes(3));

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();

app.Run();