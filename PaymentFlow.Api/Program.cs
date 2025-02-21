using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using PaymentFlow.Application.Services;
using PaymentFlow.Domain.Repositories;
using PaymentFlow.Domain.Services;
using PaymentFlow.Infrastructure;
using PaymentFlow.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados Oracle
builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDb")));

// Configuração do OAuth 2.0
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Jwt:Authority"];
        options.Audience = builder.Configuration["Jwt:Audience"];
    });

// Dependências
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
