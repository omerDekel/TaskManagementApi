using BusinessLogic.Contracts;
using BusinessLogic.Services;
using DataAccessLayer.TaskDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManagement.DataAccessLayer.Mappings;
using TaskManagement.DataAccessLayer.Repositories;
using TaskManagementApi.Middleware;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);


// Configure DbContext
builder.Services.AddDbContext<TaskManagementContext>(opt =>
     opt.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagementContext")));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure Services
builder.Services.AddScoped<ITaskService, TaskService>();


// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddCors(p => p.AddPolicy(MyAllowSpecificOrigins, builder =>
{
    builder.WithOrigins("*", "http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);


app.UseAuthorization();

app.MapControllers();

app.Run();
