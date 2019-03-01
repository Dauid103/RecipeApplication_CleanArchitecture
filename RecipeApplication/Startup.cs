using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Infrastructure.Data.Repos;
using ApplicationCore.Services;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RecipeApplication
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<RecipeUser, IdentityRole>()
              .AddEntityFrameworkStores<RecipeContext>();

            services.AddAuthentication()
                .AddCookie(options => 
                {
                    options.LoginPath = ("/Account/Login");
                });
            
            services.AddDbContext<RecipeContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("RecipeConnection"));
            });


            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IShopListRepository, ShopListRepository>();
            services.AddTransient<RecipeSeeder>();

            services.AddScoped<IListItemService, ListItemService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseNodeModules(env);

            app.UseAuthentication();


            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                  "{controller}/{action}/{id?}",
                  new { controller = "Recipe", Action = "Index" });
            });



        }
    }
}
