using Microsoft.EntityFrameworkCore;
using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Application.UseCases.Transaction;
using PaymentServiceProvider.Consumer;
using PaymentServiceProvider.Domain.Interfaces;
using PaymentServiceProvider.Infrastructure.Contexts;
using PaymentServiceProvider.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IUpdateStatusPaymentUseCase, UpdateStatusPaymentUseCase>();
builder.Services.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();
builder.Services.AddScoped<IGetPaymentByOrderIdUseCase, GetPaymentByOrderIdUseCase>();

builder.Services.AddDbContext<PaymentContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using var scope = app.Services.CreateScope();
var dataContext = scope.ServiceProvider.GetRequiredService<PaymentContext>();
dataContext.Database.Migrate();
var serviceCreateTransaction = scope.ServiceProvider.GetService<ICreateTransactionUseCase>();
if (serviceCreateTransaction != null)
{
    Thread threadConsumer = new(() => RabbitConsumer.Consume(serviceCreateTransaction));
    threadConsumer.Start();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
