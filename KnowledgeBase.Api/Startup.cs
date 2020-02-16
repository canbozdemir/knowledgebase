using System;
using System.Text;
using KnowledgeBase.Core.Data;
using KnowledgeBase.Core.Infrastructure.Repositories;
using KnowledgeBase.Core.Infrastructure.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using KnowledgeBase.Api.Infrastructure.Repositories;
using KnowledgeBase.Api.Infrastructure.Services;
using AutoMapper;
using KnowledgeBase.Core.Infrastructure.Handlers;
using KnowledgeBase.Api.Infrastructure.Commands;
using KnowledgeBase.Core.Entitties;
using KnowledgeBase.Api.Infrastructure.Handlers;
using KnowledgeBase.Api.Utils;
using KnowledgeBase.Core.Infrastructure.Context;
using Microsoft.AspNetCore.Http;

namespace KnowledgeBase.Api
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
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            string connection = @"Server=CNBZDMR\SQLEXPRESS;Database=KnowledgeBase;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddDbContext<KnowledgeBaseDbContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IInformationRepository, InformationRepository>();
            services.AddScoped<IInformationService, InformationService>();
            services.AddScoped<IViewCommandExecutor, ViewCommandExecutor>();
            services.AddScoped<IHttpRequest, Utils.HttpRequest>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            RegisterDecorators(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseMvc();
        }

        private void RegisterDecorators(IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<CreateInformationCommand, Information>>(x =>
            new TransactedCommandHandlerDecorator<CreateInformationCommand, Information>(
                new CreateInformationCommandHandler(x.GetService<IInformationRepository>(), x.GetService<IHttpRequest>()), x.GetService<KnowledgeBaseDbContext>()));

            services.AddTransient<ICommandHandler<DeleteInformationCommand, Information>>(x =>
            new TransactedCommandHandlerDecorator<DeleteInformationCommand, Information>(
                new DeleteInformationCommandHandler(x.GetService<IInformationRepository>(), x.GetService<IHttpRequest>()), x.GetService<KnowledgeBaseDbContext>()));
        }
    }
}
