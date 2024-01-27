
using AutoMapper;
using SchoolManagement.Application.Constants;
using SchoolManagement.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections;

namespace SchoolManagement.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly SchoolManagementDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Hashtable _repositories;
        //private ILeaveAllocationRepository _leaveAllocationRepository;
        //private ILeaveTypeRepository _leaveTypeRepository;
        //private ILeaveRequestRepository _leaveRequestRepository;


        public UnitOfWork(SchoolManagementDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = httpContextAccessor;
        }
        public ISchoolManagementRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(SchoolManagementRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (ISchoolManagementRepository<TEntity>)_repositories[type];
        }
        //public ILeaveAllocationRepository LeaveAllocationRepository => 
        //    _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);
        //public ILeaveTypeRepository LeaveTypeRepository => 
        //    _leaveTypeRepository ??= new LeaveTypeRepository(_context);
        //public ILeaveRequestRepository LeaveRequestRepository => 
        //    _leaveRequestRepository ??= new LeaveRequestRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save() 
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;
          
            await _context.SaveChangesAsync(username);
        }
    }
}
