using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using SchoolManagement.Api.Extensions;
using SchoolManagement.Api.Middleware;
using SchoolManagement.Application;
using SchoolManagement.Identity;
using SchoolManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

// add service to container

builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
//builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson(jsonOptions =>
 {
     jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
 });

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// configure the http request pipeline
var app = builder.Build();
//if (env.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseSwaggerDocumention();


app.UseDefaultFiles();
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//                 Path.Combine(Directory.GetCurrentDirectory(), "Content")
//             ),
//    RequestPath = "/content"
//});
app.MapFallbackToController("Index", "Fallback");
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();



