using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SchoolManagementDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("SchoolManagementConnectionString")));


            services.AddScoped(typeof(ISchoolManagementRepository<>), typeof(SchoolManagementRepository<>));
          //  services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            //services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            //services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

            return services;
        }
    }
}
