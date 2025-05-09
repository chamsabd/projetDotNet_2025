using Microsoft.EntityFrameworkCore;

using projet.Data;
using projet.extensions;
using projet.Services.Interfaces;
using projet.Services.Implementations;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*builder.Environment.IsDevelopment()
   ? builder.Configuration.GetConnectionString("LocalConnection")
   :*/

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IRoleService,RoleService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddControllers();
/*.AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});*/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}



app.UseAuthorization();

app.MapControllers();

app.Run();
