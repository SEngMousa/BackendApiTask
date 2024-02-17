using Application.Mapper;
using Application.Services;
using AutoMapper;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<DatabaseContext>();
builder.Services.AddScoped<IDriverRepository,DriverRepository>();
builder.Services.AddScoped<IDriverService,DriverService>();
var mapperProfile = new MapperConfiguration(item => item.AddProfile(new MapperProfile()));
IMapper mappers = mapperProfile.CreateMapper();
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();  
});
builder.Services.AddSingleton(mappers);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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