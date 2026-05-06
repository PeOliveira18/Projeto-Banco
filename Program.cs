using BancoDigital.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =============================================
// SERVIÇOS
// =============================================

// Banco de dados Oracle com EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle")));

// Controllers da API
builder.Services.AddControllers();

// Swagger (interface visual para testar a API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =============================================
// PIPELINE
// =============================================

var app = builder.Build();

// Swagger só em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
