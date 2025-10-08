using GesticApi.Data;
using GesticApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar la serialización JSON para ignorar ciclos en las referencias.
// Esto evita excepciones del tipo "A possible object cycle was detected"
// al serializar entidades con relaciones bidireccionales, como Service
// -> Category -> Services【76584833554123†L93-L101】. Se utiliza la clase
// ReferenceHandler.IgnoreCycles disponible en System.Text.Json.
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Configurar el contexto de base de datos utilizando la cadena de conexión
// definida en appsettings.json. Npgsql es el proveedor para PostgreSQL.
builder.Services.AddDbContext<GesticDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios de Swagger para documentar y probar la API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar la UI de Swagger en desarrollo.
app.UseSwagger();
app.UseSwaggerUI();

// ------------------- ENDPOINTS PARA CATEGORÍAS -------------------
app.MapGet("/api/categories", async (GesticDbContext db) =>
    await db.Categories.ToListAsync());

app.MapGet("/api/categories/{id:int}", async (int id, GesticDbContext db) =>
{
    var category = await db.Categories.FindAsync(id);
    return category is not null ? Results.Ok(category) : Results.NotFound();
});

app.MapPost("/api/categories", async (Category category, GesticDbContext db) =>
{
    db.Categories.Add(category);
    await db.SaveChangesAsync();
    return Results.Created($"/api/categories/{category.Id}", category);
});

app.MapPut("/api/categories/{id:int}", async (int id, Category input, GesticDbContext db) =>
{
    var c = await db.Categories.FindAsync(id);
    if (c is null) return Results.NotFound();
    c.Name = input.Name;
    c.Description = input.Description;
    await db.SaveChangesAsync();
    return Results.Ok(c);
});

app.MapDelete("/api/categories/{id:int}", async (int id, GesticDbContext db) =>
{
    var c = await db.Categories.FindAsync(id);
    if (c is null) return Results.NotFound();
    db.Categories.Remove(c);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// ------------------- ENDPOINTS PARA SERVICIOS -------------------
// Listar todos los servicios con sus categorías.
app.MapGet("/api/services", async (GesticDbContext db) =>
    await db.Services.Include(s => s.Category)
                     .Include(s => s.CreatedByUser)
                     .ToListAsync());

app.MapGet("/api/services/{id:int}", async (int id, GesticDbContext db) =>
{
    var service = await db.Services
        .Include(s => s.Category)
        .Include(s => s.CreatedByUser)
        .FirstOrDefaultAsync(s => s.Id == id);
    return service is not null ? Results.Ok(service) : Results.NotFound();
});

app.MapPost("/api/services", async (Service service, GesticDbContext db) =>
{
    // Por defecto la fecha de creación se asigna en el constructor de la
    // entidad. Si no se especifica estado se usará ACTIVO.
    db.Services.Add(service);
    await db.SaveChangesAsync();
    return Results.Created($"/api/services/{service.Id}", service);
});

app.MapPut("/api/services/{id:int}", async (int id, Service input, GesticDbContext db) =>
{
    var s = await db.Services.FindAsync(id);
    if (s is null) return Results.NotFound();
    s.Name = input.Name;
    s.Description = input.Description;
    s.CategoryId = input.CategoryId;
    s.Sla = input.Sla;
    s.Status = input.Status;
    s.CreatedBy = input.CreatedBy;
    await db.SaveChangesAsync();
    return Results.Ok(s);
});

app.MapDelete("/api/services/{id:int}", async (int id, GesticDbContext db) =>
{
    var s = await db.Services.FindAsync(id);
    if (s is null) return Results.NotFound();
    db.Services.Remove(s);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// ------------------- ENDPOINTS PARA SOLICITUDES -------------------
app.MapGet("/api/requests", async (GesticDbContext db) =>
    await db.Requests.Include(r => r.User)
                     .Include(r => r.Service)
                     .ToListAsync());

app.MapGet("/api/requests/{id:int}", async (int id, GesticDbContext db) =>
{
    var req = await db.Requests
        .Include(r => r.User)
        .Include(r => r.Service)
        .FirstOrDefaultAsync(r => r.Id == id);
    return req is not null ? Results.Ok(req) : Results.NotFound();
});

app.MapPost("/api/requests", async (Request req, GesticDbContext db) =>
{
    // Se asignará la fecha actual si no viene especificada.
    db.Requests.Add(req);
    await db.SaveChangesAsync();
    return Results.Created($"/api/requests/{req.Id}", req);
});

app.MapPut("/api/requests/{id:int}", async (int id, Request input, GesticDbContext db) =>
{
    var req = await db.Requests.FindAsync(id);
    if (req is null) return Results.NotFound();
    req.UserId = input.UserId;
    req.ServiceId = input.ServiceId;
    req.RequestDate = input.RequestDate;
    req.Status = input.Status;
    req.Details = input.Details;
    await db.SaveChangesAsync();
    return Results.Ok(req);
});

app.MapDelete("/api/requests/{id:int}", async (int id, GesticDbContext db) =>
{
    var req = await db.Requests.FindAsync(id);
    if (req is null) return Results.NotFound();
    db.Requests.Remove(req);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();