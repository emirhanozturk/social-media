using Application;
using Application.Validators.Posts;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Filters;
using Infrastructure.Services.Storage.AzureStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.Storage<AzureStorage>();

builder.Services.AddCors(options=>options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));

Logger log = new LoggerConfiguration().WriteTo.File("logs/log.txt").WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"),"logs",needAutoCreateTable: true,columnOptions:new Dictionary<string, ColumnWriterBase>
{
    {"message", new RenderedMessageColumnWriter() },
    {"message_template",new MessageTemplateColumnWriter() },
    { "level" , new LevelColumnWriter() },
    { "time_stamp" , new TimestampColumnWriter() },
    { "exception" , new ExceptionColumnWriter() },
    {"log_event",new LogEventSerializedColumnWriter() }
})
    .CreateLogger();

builder.Host.UseSerilog(log);


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
        LifetimeValidator = (notBefore, expires, securityToken, validationParamater) => expires !=null ? expires > DateTime.UtcNow : false
    };
} );


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
