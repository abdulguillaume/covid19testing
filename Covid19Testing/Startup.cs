using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19Testing.IRepos;
using Covid19Testing.Models;
using Covid19Testing.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Covid19Testing
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });

            //add on march 24, 2021
            services.AddAuthentication(HttpSysDefaults.AuthenticationScheme);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            

            //services.AddSession()

            services.AddSingleton<ISpecimenRepos , SpecimenRepos>();
            services.AddSingleton<ITestIndicatorRepos, TestIndicatorRepos>();//
            services.AddSingleton<IMethodRepos, MethodRepos>();

            services.AddSingleton<IBiodataRepos, BiodataRepos>();

            services.AddSingleton<ILabTestRepos, LabTestRepos>();

            //ILabTestRepos

            //services.AddDbContext<DbContext, Covid19TestingContext>();
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
