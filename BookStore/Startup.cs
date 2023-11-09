using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepoLayer.Interface;
using RepoLayer.Models;
using RepoLayer.Service;
using System;
using System.IO;
using System.Reflection;
using System.Text;
//using RepoLayer.Context;

namespace BookStore
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
            //Added Database Configuration
            services.AddDbContext<BookStoreDBContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:BookStoreDB"]));

            //// Configuring JWT(JSON Web Token) authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            //Swagger Implementation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Book Store Application",
                    Version = "v1",
                    Description = "Book Store BackEnd.",
                    Contact = new OpenApiContact
                    {
                        Name = "Ritvik Sharma",
                        Email = "sharma.ritvik56@gmail.com",
                        Url = new Uri("https://github.com/Ritvik5"),
                    },
                });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };
                c.AddSecurityDefinition(securitySchema.Reference.Id, securitySchema);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement { {
                    securitySchema, Array.Empty<string>()}});

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();
            services.AddScoped<IAdminUserBusiness,AdminUserBusiness>();
            services.AddScoped<IAdminUserRepo, AdminUserRepo>();
            services.AddScoped<IBookBusiness, BookBusiness>();
            services.AddScoped<IBookRepo, BookRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Enable middleware to serve generated Swagger as a JSON Endpoint
            app.UseSwagger();

            //Enable middleware to serve swagger-ui(HTML,JS,CSS)
            //Specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/Swagger/v1/swagger.json", "BookStore API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
