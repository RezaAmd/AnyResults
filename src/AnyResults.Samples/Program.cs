
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TestDb"));

builder.Services.AddMvc();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.TodoList.Add(new TodoEntity { Title = "Prepare .net project." });
    db.TodoList.Add(new TodoEntity { Title = "Develop infrastructure & database." });
    db.TodoList.Add(new TodoEntity { Title = "Setup Redis cache." });
    db.TodoList.Add(new TodoEntity { Title = "Prepare docker compose." });
    db.TodoList.Add(new TodoEntity { Title = "Refactor commands logic." });
    db.TodoList.Add(new TodoEntity { Title = "Complete todo list." });
    db.TodoList.Add(new TodoEntity { Title = "Setup MinIO in infrastructure." });
    db.TodoList.Add(new TodoEntity { Title = "Develop file storage services." });
    db.TodoList.Add(new TodoEntity { Title = "Install AnyResults package." });
    db.TodoList.Add(new TodoEntity { Title = "Use Result model in all command.", });
    db.SaveChanges();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.RunAsync();