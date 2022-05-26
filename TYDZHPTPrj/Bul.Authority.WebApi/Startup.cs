using Bul.Authority.Application;
using Bul.Authority.DBConnection.AuthorityMySqlDbContext;
using Bul.Authority.DBConnection.ConnectionFactory;
using Bul.Authority.Service;
using Bul.System.Common;
using Bul.System.Extension.NetCore;
using Bul.System.Result;
using Chloe;
using Chloe.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Bul.Authority.WebApi
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
            //services.AddControllers().AddJsonOptions(option =>
            //{
            //    option.JsonSerializerOptions.PropertyNamingPolicy = null;
            //});

            services.AddControllers(mvcOption =>
            {
                mvcOption.Filters.Add<BulActionFilter>();

            }).AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                option.SerializerSettings.ContractResolver = null;
            });

            services.AddScoped<Entity.SqUsers>();//为当前登录用户使用

            services.AddScoped<SqCdbService>();
            services.AddScoped<SqCdbApplication>();

            services.AddScoped<SqUsersService>();
            services.AddScoped<SqUserApplication>();

            services.AddScoped<SqUserRoleService>();
            services.AddScoped<SqUserRoleApplication>();

            services.AddScoped<SqCzgnApplication>();
            services.AddScoped<SqCzgnService>();

            services.AddScoped<SqRoleServices>();
            services.AddScoped<SqRoleApplication>();

            services.AddScoped<IDbContext, AuthorityDbContext>();

            services.AddTransient<JwtBearer>();

            services.AddScoped<IDbConnectionFactory, MySqlConnectionFactory>(serviceProvider =>
            {
                return new MySqlConnectionFactory(Configuration.GetConnectionString("ConnectionString"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bul.Authority.WebApi", Version = "v1" });
            });

            services.AddBulAuthentication(Configuration);

            services.AddHttpContextAccessor();

            services.AddTurninCors(Configuration);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bul.Authority.WebApi v1"));
            }

            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var ex = error.Error;

                        BulLogger.Error(ex);
                        
                        await context.Response.WriteAsJsonAsync(BulResult.FailNonData(-1000, "出现错误"));
                    }
                });
            });

            app.AppServiceExtention();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
