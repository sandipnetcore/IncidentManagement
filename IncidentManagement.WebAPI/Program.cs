using IncidentManagement.BusinessLogic.Audit;
using IncidentManagement.BusinessLogic.Category;
using IncidentManagement.BusinessLogic.Configuration;
using IncidentManagement.BusinessLogic.Incident;
using IncidentManagement.BusinessLogic.User;
using IncidentManagement.DataModel.AuditLogs;
using IncidentManagement.DataModel.User;
using IncidentManagement.EntityFrameWork.DBOperations;
using IncidentManagement.WebAPI;
using IncidentManagement.WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var frontEndOrigin = "FrontEndApps";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: frontEndOrigin,
        policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
});

builder.Services.Configure<JWTConfigurationSettings>(builder.Configuration.GetSection("JWTConfiguration"));

builder.Services.Configure<ConnectionStringConfiguration>(builder.Configuration.GetSection("DBConnectionConfigurations"));

builder.Services.AddDbContext<DataContext>(conn =>
{
    var connectionString = builder.Configuration.GetSection("DBConnectionConfigurations:IMConnString").Value;
    conn.UseSqlServer(connectionString, mig =>
    {
        mig.MigrationsAssembly("IncidentManagement.WebAPI");
    });
});

builder.Services.AddScoped<IAuditRepository, AuditRepository>(info =>
{
    var dataContext = info.GetRequiredService<DataContext>();
    return new AuditRepository(dataContext);
});



builder.Services.AddScoped<IUserRepository, UserRepository>(info =>
{
    var dataContext = info.GetRequiredService<DataContext>();
    var jwtConfig = info.GetRequiredService<IOptions<JWTConfigurationSettings>>();
    return new UserRepository(dataContext, jwtConfig);
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(cat =>
{
    var dataContext = cat.GetRequiredService<DataContext>();
    return new CategoryRepository(dataContext);
});

builder.Services.AddScoped<IIncidentRepository, IncidentRepository>(inc =>
{
    var dataContext = inc.GetRequiredService<DataContext>();
    return new IncidentRepository(dataContext);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch(Exception ex)
    {
        var repository = context.RequestServices.GetRequiredService<IAuditRepository>();

        var user = context.Items["User"] as UserModel;

        user = user?.GetEmptyUserModel();

        var auditModel = new AuditLogsModel
        {
            Action = $"{context.Request.Method} request to {context.Request.Path}",
            Description = ex.Message,
            CreatedOn = DateTime.UtcNow,
            UserId = user.UserId, 
            UserName = user.UserName,
            AuditType = "Error"
        };
    }
});


app.UseCors(frontEndOrigin);


app.UseAuthenticationMiddleware(); // Custom authentication middleware

app.UseAuthorization();

app.MapControllers();

app.Run();
