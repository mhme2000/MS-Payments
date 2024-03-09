using Microsoft.EntityFrameworkCore;
using PaymentServiceProvider.Application.Interfaces.Payable;
using PaymentServiceProvider.Application.Interfaces.Transaction;
using PaymentServiceProvider.Application.UseCases.Payable;
using PaymentServiceProvider.Application.UseCases.Transaction;
using PaymentServiceProvider.Domain.Interfaces;
using PaymentServiceProvider.Infrastructure.Contexts;
using PaymentServiceProvider.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IPayableRepository, PayableRepository>();
builder.Services.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();
builder.Services.AddScoped<IGetListTransactionsUseCase, GetListTransactionsUseCase>();
builder.Services.AddScoped<IGetBalancesUseCase, GetBalancesUseCase>();
builder.Services.AddScoped<IProcessPaymentsByDateUseCase, ProcessPaymentsByDateUseCase>();

builder.Services.AddDbContext<TransactionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<TransactionContext>();
    dataContext.Database.Migrate();
}
app.UseSwagger();
app.UseSwaggerUI(); 
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
