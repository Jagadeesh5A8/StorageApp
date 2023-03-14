
using StorageWebApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<BlobRepository>(BlobRepository => new BlobRepository("DefaultEndpointsProtocol=https;AccountName=webstorage8;AccountKey=iPhP3TWz7mRob8JBMNz+OEk7k2x8DGM5YQ+7prPqrpC68Y0hKGhn3CBIG9GvapwEhCsAlzOpOA8h+AStmJ4e1Q==;EndpointSuffix=core.windows.net", "jdcon"));
builder.Services.AddScoped<ITableStorageRepository, TableStorageRepository>();
builder.Services.AddScoped<IQueueRepository, QueueRepository>();
builder.Services.AddScoped<IFileShareRepository, FileShareRepository>();
var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();
