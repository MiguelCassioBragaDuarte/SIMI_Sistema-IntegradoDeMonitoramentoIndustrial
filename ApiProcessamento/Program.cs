using ApiProcessamento.Data;
using ApiProcessamento.Services;
using Microsoft.EntityFrameworkCore;

// Cria o builder da aplicação (configuração inicial)
var builder = WebApplication.CreateBuilder(args);

// ============================
// CONFIGURAÇÃO DOS SERVIÇOS
// ============================

// Adiciona suporte a Controllers (API)
builder.Services.AddControllers();

// Configuração do Swagger (documentação e testes da API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do banco de dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Injeção de dependência
builder.Services.AddScoped<ISensorService, SensorService>();

// ============================
// BUILD DA APLICAÇÃO
// ============================

var app = builder.Build();

// ============================================================
// NOVO: CRIAÇÃO AUTOMÁTICA DO BANCO E TABELAS
// Isso resolve o erro "no such table: Medicoes"
// ============================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated(); // Cria o banco e as tabelas se não existirem
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao criar o banco de dados.");
    }
}

// ============================
// CONFIGURAÇÃO DO PIPELINE HTTP
// ============================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Executa a aplicação
app.Run();