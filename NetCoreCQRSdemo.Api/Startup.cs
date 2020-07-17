using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreCQRSdemo.Api.ProjectConfigurations;
using NetCoreCQRSdemo.Persistence.Context;
using NetCoreCqrsESdemo.BusinessLogic.Base;
using NetCoreCqrsESdemo.BusinessLogic.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Reflection;

namespace NetCoreCQRSdemo.Api
{
    public class Startup
    {
        private readonly string _dataSource;
        private readonly string _appMaps;
        private Assembly _assemblyBL;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            string path = GlobalVariables.DatasourcePrefix + environment.WebRootPath;

            _appMaps = Path.Combine(environment.WebRootPath, GlobalVariables.MappingFile);
            _dataSource = Path.Combine(path, GlobalVariables.DatabaseName);
            _assemblyBL = Assembly.Load(GlobalVariables.NMSP_BusinessLogic);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(o => {
                o.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            //services.AddHostedService<AppConfigurationService>();
            services.AddHostedService(provider => new AppConfigurationService(_appMaps));
            services.AddDbContext<ApplicationDbContext>(cfg => cfg.UseSqlite(_dataSource));
            
            services.AddSingleton(provider => new CommandService(_appMaps));
            services.AddTransient<CommandExecutionService>();
            
            services.AddMediatR(typeof(BaseHandler<,>).GetTypeInfo().Assembly);

            services.AddCors(options =>
                options.AddDefaultPolicy(builder =>
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
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
