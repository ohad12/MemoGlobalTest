using Newtonsoft.Json;
using MemoGlobalTest.Data.Entities.MetadataConfiguration;
using MemoGlobalTest.Interface;
using MemoGlobalTest.Services.Reqres;

namespace MemoGlobalTest
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // To prevent browsers from making http requests from non allowed origins - CORS is the answer.
            // If browsers receive a response header "allow-origin", "list of origins" then they will throw an error "blocked by CORS" 
            // if the html/react app doesn't reside on a domain in that list.
            // But, for API calls made via server side code / postman etc. there is no mechanism that will prevent the request from completing the request.
            // To restrict requests from other domains (not browsers), we need to add
            // another middleware that checks context.Request.Host.Host and validate from a list of allow Origins and if the domain is not allow throw an exception ("Origin is not allowed").
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins(
                            "https://localhost",
                            "https://www.google.com",
                            "https://www.memoglobal.com"
                        );
                    });
            });

            services.AddLogging();

            services
                .AddControllers()
                .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen();

            services.AddDbContext<Entities>();
            services.AddScoped<IHttpClientService, ReqresClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("AllowSpecificOrigins");
            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });

        }
    }
}
