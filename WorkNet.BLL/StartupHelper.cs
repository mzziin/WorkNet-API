﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkNet.BLL.Services;
using WorkNet.BLL.Services.IServices;
using WorkNet.DAL.Data;
using WorkNet.DAL.Repositories;
using WorkNet.DAL.Repositories.IRepositories;

namespace WorkNet.BLL
{
    public static class StartupHelper
    {
        public static void RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WorkNetDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IEmployerRepository, EmployerRepository>();
        }
    }
}