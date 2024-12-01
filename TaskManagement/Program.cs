using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.Services;
using DataAccessLayer.TaskDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManagement.DataAccessLayer.Contracts;
using TaskManagement.DataAccessLayer.Mappings;
using TaskManagement.DataAccessLayer.Repositories;
using TaskManagementApi.Middleware;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

SetRepository();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure Services
builder.Services.AddScoped<ITaskService, TaskService>();

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
void SetRepository()
{
    var repositorySettings = builder.Configuration.GetSection("RepositorySettings");
    var repositoryType = repositorySettings["Type"];

    switch (repositoryType)
    {
        case "SQL":
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            var sqlConnectionString = repositorySettings.GetSection("SQL")["ConnectionString"];
            builder.Services.AddDbContext<TaskManagementContext>(options =>
                options.UseSqlServer(sqlConnectionString));
            builder.Services.AddScoped<ITaskRepository, SqlTaskRepository>();
            break;

        case "MongoDB":            
            builder.Services.AddAutoMapper(typeof(MongoMappingProfile));
            var mongoSettings = repositorySettings.GetSection("MongoDB");
            var mongoConnectionString = mongoSettings["ConnectionString"];
            var databaseName = mongoSettings["DatabaseName"];
            var collectionName = mongoSettings["CollectionName"];
            builder.Services.AddSingleton<ITaskRepository>(provider =>
                new MongoTaskRepository(mongoConnectionString, databaseName, collectionName, provider.GetRequiredService<IMapper>()));
            break;

        case "File":
            var filePath = repositorySettings.GetSection("File")["FilePath"];
            builder.Services.AddSingleton<ITaskRepository>(new FileTaskRepository(filePath));
            break;

        default:
            throw new InvalidOperationException($"Unsupported repository type: {repositoryType}");
    }
}