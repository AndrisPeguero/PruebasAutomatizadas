

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ReservaBook.Core.Aplication.Dtos.User;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Settings;
using ReservaBook.Infraestructure.Indentity.Contexts;
using ReservaBook.Infraestructure.Indentity.Entities;
using ReservaBook.Infraestructure.Indentity.Seeds;
using ReservaBook.Infraestructure.Indentity.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ReservaBook.Infraestructure.Indentity
{
    public static class ServicesRegistration
    {

     
        public static void AddIdentityLayerIOCForWebApi(this IServiceCollection service, IConfiguration config)
        {

            #region context configuration
            GenerateConfiguration(service, config);
            #endregion


            #region configuration IOC
            service.AddScoped<IAccountServiceForWebApi, AccountServiceForWebApi>();
            service.Configure<JwtSettings>(config.GetSection("JwtSettings"));
            #endregion



            #region Idenitity configuration

            //configuraciones generales
            service.Configure<IdentityOptions>(opt =>
            {
                //configuracion de la contrseña
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
              



                
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;



                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
                
            });



            //Configuracion para gestion de usuarios
            service.AddIdentityCore<AppUser>()
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);




            service.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(12); //tiempo de duracion del token
               
            });



            service.AddAuthentication(opt =>
            {

                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;  

            }).AddCookie(IdentityConstants.ApplicationScheme,opt =>
            {
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = false;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Secretkey"] ?? ""))
                };

                opt.Events = new JwtBearerEvents()
                {

                    OnAuthenticationFailed = c =>
                    {

                        c.NoResult();   //El token tiene un formato invalido
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.Message.ToString());
                    },
                    OnChallenge = oc =>
                    {

                        oc.HandleResponse();

                        oc.Response.StatusCode = 401; //El usuario no esta authenticado
                        oc.Response.ContentType = "application/plain";
                        var result =  JsonConvert.SerializeObject(new JwtResponseDto() { HasError = true, Error = "You are not authorize"});
                        return oc.Response.WriteAsync(result);
                    },
                    OnForbidden = of =>
                    {

                        of.Response.StatusCode = 403; //El usuario no tiene permisos
                        of.Response.ContentType = "application/plain";
                        var result = JsonConvert.SerializeObject(new JwtResponseDto() { HasError = true, Error = "You are not authorize to access this resource"});
                        return of.Response.WriteAsync(result);

                    }
                };
                    
                    
            });


            #endregion

        }



        #region privated methods
        private static void GenerateConfiguration(IServiceCollection Service, IConfiguration config)
        {

            if (config.GetValue<bool>("useInMemoryDatabase"))
            {
                Service.AddDbContext<IdentityContext>(opt => opt.UseInMemoryDatabase("ReservaMemoryDb"));

            }
            else
            {


                var connectionStrings = config.GetConnectionString("IdentityConnection");
                Service.AddDbContext<IdentityContext>(


                    (ServiceProvider, opt) =>
                    {

                        opt.EnableSensitiveDataLogging();
                        opt.UseSqlServer(connectionStrings,
                         m => m.MigrationsAssembly(typeof(IdentityContext)
                       .Assembly.FullName));

                    },
                    contextLifetime: ServiceLifetime.Scoped,
                    optionsLifetime: ServiceLifetime.Scoped

                );


            }

        }
        #endregion




        #region identity Seed
        public static async Task RunIdentitySeed(this IServiceProvider Service)
        {

            using var scope = Service.CreateScope();
            
            var serviceProvider = scope.ServiceProvider;

            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


            await DefaultRoles.seedAsync(roleManager);
            await DefaultPropietarioUser.SeedAsync(userManager);
            await DefaultAdminUser.SeedAsync(userManager);
            await DefaultClienteUser.SeedAsync(userManager);

        }
        #endregion



    }
}
