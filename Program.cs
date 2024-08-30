using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using simpleBankingAppAPI.DataAccess;
using simpleBankingAppAPI.DataAccess.Interface;
using simpleBankingAppAPI.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//adding connectionString
builder.Services.AddDbContext<ApplicationContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection"));
} );

/////
//Add Dependcy Injection
builder.Services.AddScoped<ISimpleBanking,SimpleBankingRepository>();


//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
