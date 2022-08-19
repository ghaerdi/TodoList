using System.Text.Json.Serialization;
using todolist.Repositories;
using todolist.Services;
using todolist.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.Filters.Add<ErrorFilter>()).AddControllersAsServices().AddJsonOptions(option =>
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Database
builder.Services.AddDbContext<TodoListDataContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DATABASE")));
#endregion

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .SetIsOriginAllowed(origin => true);
    });
});
#endregion

#region Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

#region Repository
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<TaskRepository>();
#endregion

#region Service
builder.Services.AddTransient<UserService>();
#endregion

#region Filters
builder.Services.AddScoped<CheckAuthFilter>();
builder.Services.AddScoped<ErrorFilter>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseCookiePolicy(new());

app.UseAuthorization();

app.MapControllers();

string port = Environment.GetEnvironmentVariable("PORT") ?? "3000";
string url = $"http://localhost:{port}";

app.Run(url);
