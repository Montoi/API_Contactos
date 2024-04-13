using API_Contactos;
using API_Contactos.Data;
using API_Contactos.Repository.ContactRepository;
using API_Contactos.Repository.IRepository.IContactRepository;
using API_Contactos.Repository.IRepository.IPhoneNumbers;
using API_Contactos.Repository.PhoneNumbersRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AplicationDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IPhoneNumbersRepository, PhoneNumbersRepository>();
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
