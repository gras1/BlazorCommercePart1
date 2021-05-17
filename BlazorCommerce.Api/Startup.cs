using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BlazorCommerce.Data;

namespace BlazorCommerce.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlazorCommerce.Api", Version = "v1" });
            });
            services.Configure<BlazorCommerce.Data.DatabaseOptions>(Configuration.GetSection("DatabaseOptions"));
            services.AddScoped<BlazorCommerce.Data.IRepository<BlazorCommerce.Shared.CategoryDto>, CategoryRepository>();
            services.AddScoped<BlazorCommerce.Data.IRepository<BlazorCommerce.Shared.CategoryMinDto>, CategoryMinRepository>();
            services.AddScoped<BlazorCommerce.Data.IRepository<BlazorCommerce.Shared.ProductMinDto>, ProductMinRepository>();
            services.AddScoped<BlazorCommerce.Data.IRepository<BlazorCommerce.Shared.ProductDto>, ProductRepository>();
            services.AddScoped<BlazorCommerce.Data.IRepository<BlazorCommerce.Shared.CartMinDto>, CartMinRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "Open",
                    builder => builder.AllowAnyOrigin().AllowAnyHeader());
            }); 
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlazorCommerce.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("Open");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
