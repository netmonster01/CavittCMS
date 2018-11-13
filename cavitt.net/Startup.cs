using cavitt.net.Converter;
using cavitt.net.Data;
using cavitt.net.Dtos;
using cavitt.net.Interface;
using cavitt.net.Interfaces;
using cavitt.net.Models;
using cavitt.net.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace cavitt.net
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var appSettingsSection = Configuration.GetSection("AppSettings");

            // configure DI for application services
            services.Configure<AppSettings>(appSettingsSection);

            // add repositories
            services.AddScoped<ILoggerRepository, LoggerRepository>();
            services.AddScoped<IRolesRepository, RoleRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            // add converters
            services.AddScoped<IConverter<Post, PostDto>, PostToPostDtoConverter>();
            services.AddScoped<IConverter<ApplicationUser, UserDto>, ApplicationUserToUserDtoConverter>();

            // set up database
            services.AddDbContext<ApplicationDbContext>(options =>
                            options.UseSqlite(Configuration.GetConnectionString("IdentityConnection")), ServiceLifetime.Transient);

            // add http context
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // add custom identity
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var securityKey = Encoding.UTF8.GetBytes(appSettings.Secret);
            var signingKey = new SymmetricSecurityKey(securityKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = appSettings.Audience,
                    ValidIssuer = appSettings.Issuer,
                    IssuerSigningKey = signingKey,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            // cors policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });

            // mvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // angular
            services.AddSpaStaticFiles(options =>
            {
                options.RootPath = Constants.App.ClientAppPath;
            });


            // swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cavitt.net", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();

            // add static files
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default",
                    template: "{controller}/{action=index}/id");
            });

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseSpa(spa => {

                spa.Options.SourcePath = Constants.App.ClientApp;
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }

            });
        }
    }
}
