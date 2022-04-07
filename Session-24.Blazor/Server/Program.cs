using BlackCoffeeshop.EF.Configuration;
using BlackCoffeeshop.EF.Context;
using BlackCoffeeshop.Model;
using Microsoft.AspNetCore.ResponseCompression;
using Session_24.Services.Handlers;
using Session_24.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddScoped<IEntityRepo<ProductCategory>, ProductCategoryRepo>();
builder.Services.AddScoped<IEntityRepo<MonthlyLedger>, MonthlyLedgerRepo>();
builder.Services.AddScoped<MonthlyLedgerHandler>();
builder.Services.AddScoped<IEntityRepo<Employee>, EmployeeRepo>();
builder.Services.AddScoped<IEntityRepo<Transaction>, TransactionRepo>();
builder.Services.AddScoped<IEntityRepo<TransactionLine>, TransactionLineRepo>();
builder.Services.AddScoped<IEntityRepo<Customer>, CustomerRepo>();
builder.Services.AddScoped<IEntityRepo<Product>, ProductRepo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
