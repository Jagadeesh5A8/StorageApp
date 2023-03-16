
using StorageWebApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<BlobRepository>(BlobRepository => new BlobRepository("DefaultEndpointsProtocol=https;AccountName=jdstora8;AccountKey=zqGe8/qeOtr7CryMq4/sGJklZtZHZ0rPt4ez25ZVHLuBT4Jc72KnpCqjdu64RtxpRzFF3Yi89kEm+AStz0dokQ==;EndpointSuffix=core.windows.net", "cont1"));
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
