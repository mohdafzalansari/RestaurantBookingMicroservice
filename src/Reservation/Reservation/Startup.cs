using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Reservation.authPolicy.handler;
using Reservation.authPolicy.requirement;
using Reservation.CommonUtility.Constants;
using Reservation.Domain.Context;
using Reservation.Infrastructure.Common.Helpers;
using Reservation.Infrastructure.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace Reservation
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
            services.AddControllers(opts =>
            {
                opts.Filters.Add(typeof(CustomExceptionFilter));
            });
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Reservation", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            services.AddScoped<IReservationContext, ReservationContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICustomClaimHelpers, CustomClaimHelpers>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AppConstants.UserPolicy, policy =>
                {
                    policy.Requirements.Add(new UserRolePermission());
                });
                options.AddPolicy(AppConstants.CustomerPolicy, policy =>
                {
                    policy.Requirements.Add(new CustomerRolePermission());
                });
                options.AddPolicy(AppConstants.AdminPolicy, policy =>
                {
                    policy.Requirements.Add(new AdminRolePermission());
                });
            });

            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            ConfigureAuthService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reservation v1"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            //  prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration.GetValue<string>("IdentitySection:url");
                options.Audience = Configuration.GetValue<string>("IdentitySection:scope");
                options.RequireHttpsMetadata = false;
            });
        }
    }
}
