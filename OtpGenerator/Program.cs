using OtpGenerator.Data;
using Microsoft.EntityFrameworkCore;
using OtpGenerator.Interfaces;
using OtpGenerator.Repository;
using Hangfire;
using OtpGenerator;
using System;
using OtpGenerator.Models;
using OtpGenerator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IOtpRepository, OtpRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHangfire(configuration => configuration
    .UseSqlServerStorage("Data Source=DESKTOP-3RN5R9Q;Initial Catalog=OtpCollection;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<EmailService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();

}
Console.WriteLine("before use routing");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseHangfireServer();

app.UseHangfireDashboard();


/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    RecurringJob.AddOrUpdate(() => new BackgroundTask().DeleteExpiredOtps(), Cron.Minutely); "*\/5 * * * *"
});
Console.WriteLine("after sss"); */



app.Run();

