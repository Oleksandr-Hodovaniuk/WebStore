using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Interfaces;
using WebStore.Services;
using WebStore.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add IProductService.
builder.Services.AddScoped<IProductService, ProductService>();

//Add ILogInService
builder.Services.AddScoped<ILogInService, LogInService>();

//Add IRegisterService.
builder.Services.AddScoped<IRegisterService, RegisterService>();

//Add ICartService.
builder.Services.AddScoped<ICartService, CartService>();

//Add IEmailService.
builder.Services.AddScoped<IEmailService, EmailService>();

//Add validator for RegisterViewModel.
builder.Services.AddScoped<RegisterViewModelValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add connection string.
builder.Services.AddDbContext<AppDbContext>(options =>
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

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
