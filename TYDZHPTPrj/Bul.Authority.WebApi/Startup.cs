using Bul.Authority.Application;
using Bul.Authority.DBConnection.AuthorityMySqlDbContext;
using Bul.Authority.DBConnection.ConnectionFactory;
using Bul.Authority.Service;
using Chloe.Infrastructure;
using Chloe.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            services.AddControllers().AddJsonOptions(option=> {
                option.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddScoped<Entity.SqUsers>();//为当前登录用户使用

            services.AddScoped<SqCdbService>();
            services.AddScoped<SqCdbApplication>();
            services.AddScoped<AuthorityDbContext>();

            services.AddScoped<IDbConnectionFactory, MySqlConnectionFactory>(serviceProvider =>
            {
                return new MySqlConnectionFactory(Configuration.GetConnectionString("ConnectionString"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bul.Authority.WebApi", Version = "v1" });
            });

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bul.Authority.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
