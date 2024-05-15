using MongoDB.Bson.Serialization;
using TransitX.API.Data;
using TransitX.API.Data.Interfaces;
using TransitX.API.Service;
using TransitX.Common.Models;
using TransitX.Common.Service;

BsonSerializer.RegisterSerializer(typeof(Coordinate), new CoordinateSerializer());

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "TransitX", Version = "v1.0" });
});
builder.Services.AddSingleton<IMongoDbRepository, MongoDbRepository>();
builder.Services.AddScoped(typeof(IService<>), typeof(MongoService<>));

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
