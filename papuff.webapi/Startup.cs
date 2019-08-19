using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using papuff.bootstrap;
using papuff.webapi.Startups.Kernel;

namespace papuff.webapi {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            // # DI
            Bootstrap.Configure(services, Configuration.GetConnectionString("DefaultConnection"));

            services.AddDistributedMemoryCache();
            services.AddCustomMvc();
            services.AddJWTService(Configuration);
            services.AddCustomSwagger();
            services.AddSignalR();
            services.AddLocalizations();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseApiUnitOfWork();
            app.UserDevExceptionIfDebug(env);
            app.UseStaticFiles();
            app.MiddleException();
            app.UserCustomCors();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSwaggerDocs();
            app.UserNotifyHub();
            app.UseCookiePolicy();
            app.UseMvc();
        }
    }
}
