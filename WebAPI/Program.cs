using Application;
using Application.Validators.Posts;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Filters;
using Infrastructure.Services.Storage.AzureStorage;
using Infrastructure.Services.Storage.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;
using WebAPI.Configurations.Logging.ColumnWriters;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.Storage<AzureStorage>();

builder.Services.AddCors(options=>options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

Logger log = new LoggerConfiguration().WriteTo.File("logs/log.txt").WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"),"logs",needAutoCreateTable: true,columnOptions:new Dictionary<string, ColumnWriterBase>
{
    {"message", new RenderedMessageColumnWriter() },
    {"message_template",new MessageTemplateColumnWriter() },
    { "level" , new LevelColumnWriter() },
    { "time_stamp" , new TimestampColumnWriter() },
    { "exception" , new ExceptionColumnWriter() },
    {"log_event",new LogEventSerializedColumnWriter() },
    {"user_name",new UsernameColumnWriter() }
}).WriteTo.Seq(builder.Configuration["Seq:Url"]).Enrich.FromLogContext().MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});


builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(congifuration =>congifuration.RegisterValidatorsFromAssemblyContaining<CreatePostValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParamater) => expires !=null ? expires > DateTime.UtcNow : false,

        NameClaimType = ClaimTypes.Name
    };
} );


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context,next)=>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name",username);

    await next();
});

app.MapControllers();
app.MapHubs();

app.Run();
