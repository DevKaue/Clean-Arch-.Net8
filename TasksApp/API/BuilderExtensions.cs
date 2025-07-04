﻿using Application.UserCQ.Commands;
using Application.UserCQ.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;
using Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Mappings;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Abstractions;
using Services.AuthService;
using Infra.Repository.IRepositories;
using Infra.Repository.Repositories;
using Infra.Repository.UnitOfWork;

namespace API
{
    public static class BuilderExtensions
    {
        public static void AddSwaggerDocs(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Tasks App",
                    Description = "Um aplicativo de tarefas e escrito em ASP.NET Core V8",
                    Contact = new OpenApiContact
                    {
                        Name = "Kaue Sabino",
                        Url = new Uri("hhtps://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Página de Licença",
                        Url = new Uri("hhtps://example.com/license")
                    }
                });
                var xmlFilename= $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public static void AddJwtAuth(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var configuration = builder.Configuration;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!)),
                };
            })
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromDays(2);
            });
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CreateUserCommand).Assembly));

        }
        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            builder.Services.AddDbContext<TasksDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddValidations(this WebApplicationBuilder builder)
        {
            builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
            builder.Services.AddFluentValidationAutoValidation();
        }

        public static void AddMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(ProfileMappings).Assembly);
        }

        public static void AddInjections(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthService,AuthService>();
        }

        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
