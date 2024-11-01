WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container. 

builder.Services.AddControllers();
 
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
