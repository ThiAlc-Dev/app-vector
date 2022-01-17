using CrosCutting.ConfigureContext;
using CrossCutting.Mappers;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Service.Services;
using System;

namespace application
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
            //configuration context and respositories
            ConfigureContext.ConfigureDbContext(services);

            //autoMapper
            var configureMapper = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AddToProfile());
            });

            var mapper = configureMapper.CreateMapper();
            services.AddSingleton(mapper);

            //service
            services.AddTransient<IUserService, UsersService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddHttpClient();
            services.AddControllers();

            //swegger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "application", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "application v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (Environment.GetEnvironmentVariable("MIGRATION").ToLower().Equals("apply"))
            {
                using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    ConfigureContext.ConfigureMgrationDatabase(service);
                }
            }
        }
    }
}
