using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebAppT.Models;

namespace WebAppT
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public readonly string DefaultCorsPolicy = "_defaultCorsPolicy";// value is not important inside ""
        public string[] AllowOrigins = { "http://localhost:4200" };
        public string[] AllowMethods = { "GET" , "POST" , "PUT" , "DELETE" };



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            services.AddDbContext<EdDbContext>(options => {
                options.UseLazyLoadingProxies();// this was added along with { and }   has to be bevore sql server piece
                options.UseSqlServer(Configuration.GetConnectionString("EdDb"));
            });
            services.AddCors(option =>
                 option.AddPolicy(DefaultCorsPolicy , x =>

                   x.WithOrigins(AllowOrigins).WithMethods(AllowMethods).AllowAnyHeader()
                 )
            );
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app , IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(DefaultCorsPolicy);// must be added for CORS to work and we added it 

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();//we added this as well to create variable scope
            scope.ServiceProvider.GetService<EdDbContext>().Database.Migrate();// the statement plus the one above will cause any migrations to happen that would not have already been done both were added

        }
    }
}
