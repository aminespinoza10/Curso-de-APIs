using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OpenApi;
using ApiRestNet.Data;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

async Task<List<ApiRestNet.Models.Task>> GetTasks(DataContext context) => await context.Tasks.ToListAsync();
app.MapGet("/Tasks", async (DataContext context) => await GetTasks(context))
.WithName("GetTasks")
.WithOpenApi();


app.MapPost("Add/Tasks", async (DataContext context, ApiRestNet.Models.Task item) =>
{
    context.Tasks.Add(item);
    await context.SaveChangesAsync();
    return Results.Ok(item);
})
.WithName("AddTask")
.WithOpenApi();

app.MapPut("Update/Tasks", async (DataContext context, ApiRestNet.Models.Task item) =>
{
    context.Tasks.Update(item);
    await context.SaveChangesAsync();
    return Results.Ok(item);
})
.WithName("UpdateTask")
.WithOpenApi();

app.MapDelete("Delete/Tasks", async (DataContext context, [FromBody]ApiRestNet.Models.Task item) =>
{
    context.Tasks.Remove(item);
    await context.SaveChangesAsync();
    return Results.Ok(item);
})
.WithName("DeleteTask")
.WithOpenApi();

app.Run();
