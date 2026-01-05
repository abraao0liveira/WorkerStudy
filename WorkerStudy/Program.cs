using WorkerStudy.Services;
using WorkerStudy.Workers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<OrderProcessingWorkerV3>(); // Aplicação excutada em segundo plano

builder.Services.AddScoped<IEmailService, EmailService>(); // Serviço de envio de email

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
