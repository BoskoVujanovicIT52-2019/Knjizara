using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();


builder.Services.AddControllers().AddJsonOptions(x =>
                       x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<IIzdavacRepository, IzdavacRepository>();
builder.Services.AddScoped<IKnjigaRepository,KnjigaRepository>();
builder.Services.AddScoped<IKorisnikRepository, KorisnikRepository>();
builder.Services.AddScoped<IKorpaRepository, KorpaRepository>();
builder.Services.AddScoped<INacinPlacanja, NacinPlacanjaRepository>();
builder.Services.AddScoped<IPisacRepository, PisacRepository>();
builder.Services.AddScoped<IPovezRepository, PovezRepository>();
builder.Services.AddScoped<IRacunRepository, RacunRepository>();
builder.Services.AddScoped<IStavkaKorpe, StavkaKorpeRepository>();
builder.Services.AddScoped<ITipKorisnika, TipKorisnikaRepository>();
builder.Services.AddScoped<IZanrRepository, ZanrRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
