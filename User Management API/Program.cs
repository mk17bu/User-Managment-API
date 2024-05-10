using Microsoft.EntityFrameworkCore;
using User_Management_API.DbContexts;
using User_Management_API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserManagementContext>(dbContextOptions =>
    dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:UserManagementDBConnectionString"]));

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
});

builder.Services.AddScoped<UserManagementRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();