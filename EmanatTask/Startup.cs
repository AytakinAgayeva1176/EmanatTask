using EmanatTask.Configuration;
using EmanatTask.Helpers;
using EmanatTask.Interfaces;
using EmanatTask.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmanatTask
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
          
            var omdbApiConfig = Configuration.GetSection("omdbapi").Get<OmdbApiConfiguration>();

            services.AddSingleton(omdbApiConfig);

            var emailConfig = Configuration.GetSection("EmailSettings").Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);

            

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<HttpClientHelper>();
            services.AddScoped<EmailHelper>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
