using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreCQRSdemo.Api.ProjectConfigurations;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using NetCoreCqrsESdemo.BusinessLogic.Tests;
using System.IO;
using System.Reflection;

namespace NetCoreCQRSdemo.Api
{
    public class Startup
    {
        private readonly string _dataSource;
        private Assembly _assemblyBL;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            string path = GlobalVariables.DatasourcePrefix + environment.WebRootPath;
            
            _dataSource = Path.Combine(path, GlobalVariables.DatabaseName);
            _assemblyBL = Assembly.Load(GlobalVariables.NMSP_BusinessLogic);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(cfg => cfg.UseSqlite(_dataSource));
            services.AddSingleton<CommandService>();
            services.AddMediatR(_assemblyBL);

            services.AddCors(x =>
                x.AddDefaultPolicy(b =>
                    b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
