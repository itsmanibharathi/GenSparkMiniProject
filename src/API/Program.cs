using API.Context;
using API.Models;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services;
using API.Services.Interfaces;
using API.Utility;
using log4net.Config;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Log4NetConfig
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));
            #endregion

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddLogging(l => l.AddLog4Net());

            builder.Services.AddEndpointsApiExplorer();
            
            #region Swagger
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "web server api", Version = "v1" });
                c.SchemaFilter<EnumSchemaFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            #endregion

            #region JWT Auth/Authorization

            builder.Services.AddAuthentication()
                .AddJwtBearer("CustomerScheme", options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:CustomerSecret"]))
                    };
                })
                .AddJwtBearer("RestaurantScheme", options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:RestaurantSecret"]))
                    };
                })
                .AddJwtBearer("EmployeeScheme", options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:EmployeeSecret"]))
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CustomerPolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add("CustomerScheme");
                    policy.RequireAuthenticatedUser();
                });
                options.AddPolicy("RestaurantPolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add("RestaurantScheme");
                    policy.RequireAuthenticatedUser();
                });
                options.AddPolicy("EmployeePolicy", policy =>
                {
                    policy.AuthenticationSchemes.Add("EmployeeScheme");
                    policy.RequireAuthenticatedUser();
                });
            });

            #endregion

            #region DBContext
            builder.Services.AddDbContext<DBGenSparkMinirojectContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, Customer>, CustomerRepository>();
            builder.Services.AddScoped<ICustomerAuthRepository, CustomerAuthRepository>();
            builder.Services.AddScoped<IRepository<int, Restaurant>, RestaurantRepository>();
            builder.Services.AddScoped<IRestaurantAuthRepository,  RestaurantAuthRepository>();
            builder.Services.AddScoped<IRepository<int,Employee>, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeAuthRepository, EmployeeAuthRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
            builder.Services.AddScoped<ITokenService<Customer>, CustomerTokenService>();
            builder.Services.AddScoped<ITokenService<Restaurant>, RestaurantTokenService>();
            builder.Services.AddScoped<ITokenService<Employee>, EmployeeTokenService>();
            builder.Services.AddScoped<ICustomerAuthService, CustomerAuthService>();
            builder.Services.AddScoped<IRestaurantAuthService, RestaurantAuthService>();
            builder.Services.AddScoped<IEmployeeAuthService, EmployeeAuthService>();
            #endregion


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
