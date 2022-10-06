using Identity.Domain;
using Identity.Domain.Context;
using Identity.Infrastructure.Configuration;
using Identity.Infrastructure.Filters;
using Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Identity
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
            services.AddControllers(opts => {
                opts.Filters.Add(typeof(CustomExceptionFilter));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurant", Version = "v1" });
            });

            // Adds IdentityServer
            services.AddIdentityServer(x =>
            {
                x.IssuerUri = "null";
                x.Authentication.CookieLifetime = TimeSpan.FromHours(2);
            })
            .AddDeveloperSigningCredential()
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddProfileService<ProfileService>()
            .AddInMemoryApiScopes(Config.ApiScopes())
            .AddInMemoryApiResources(Config.ApiResources())
            .AddInMemoryIdentityResources(Config.IdentityResources())
            .AddInMemoryClients(Config.GetClients());
            //.AddOperationalStore(options =>
            //{
            //    options.ConfigureDbContext = builder =>
            //     builder.UseSqlServer(Configuration["ConnectionStrings:OnlineStoreContext"],
            //     sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));

            //    options.EnableTokenCleanup = true;
            //    options.TokenCleanupInterval = 30;
            //});

            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddScoped<IIdentityContext, IdentityContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity v1"));

            app.UseRouting();

            app.UseAuthorization();
            app.UseIdentityServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
