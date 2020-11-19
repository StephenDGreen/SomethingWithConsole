using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Something.Application;
using Something.Domain;
using Something.Persistence;
using Something.Security;
using System.Text;
using JwtConstants = Something.Security.JwtConstants;

namespace Something.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string DevSomething3AllowSpecificOrigins = "_devSomething3AllowSpecificOrigins";
        readonly string DevSomething3AllowAllOrigins = "_devSomething3AllowAllOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: DevSomething3AllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200/")
                                      .AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                                  });
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: DevSomething3AllowAllOrigins,
                                  builder =>
                                  {
                                      builder
                                      .AllowAnyOrigin()
                                      .AllowAnyMethod().AllowAnyHeader();
                                  });
            });
            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config =>
                {
                    var secretBytes = Encoding.UTF8.GetBytes(JwtConstants.Secret);
                    var key = new SymmetricSecurityKey(secretBytes);
                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = JwtConstants.Issuer,
                        ValidAudience = JwtConstants.Audience,
                        IssuerSigningKey = key
                    };
                });
            services.AddAuthorization();
            services.AddDbContext<AppDbContext>(
                options => options.UseInMemoryDatabase(nameof(Something.API))
                );
            services.AddSingleton<ISomethingFactory, SomethingFactory>();
            services.AddScoped<ISomethingCreateInteractor, SomethingCreateInteractor>();
            services.AddScoped<ISomethingReadInteractor, SomethingReadInteractor>();
            services.AddScoped<ISomethingPersistence, SomethingPersistence>();
            services.AddSingleton<ISomethingElseFactory, SomethingElseFactory>();
            services.AddScoped<ISomethingElseCreateInteractor, SomethingElseCreateInteractor>();
            services.AddScoped<ISomethingElseReadInteractor, SomethingElseReadInteractor>();
            services.AddScoped<ISomethingElseUpdateInteractor, SomethingElseUpdateInteractor>();
            services.AddScoped<ISomethingElseDeleteInteractor, SomethingElseDeleteInteractor>();
            services.AddScoped<ISomethingElsePersistence, SomethingElsePersistence>();
            services.AddSingleton<ISomethingUserManager, SomethingUserManager>();
            services.AddSingleton(Log.Logger);
            services.AddControllersWithViews();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(2, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseCors(DevSomething3AllowAllOrigins);
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
