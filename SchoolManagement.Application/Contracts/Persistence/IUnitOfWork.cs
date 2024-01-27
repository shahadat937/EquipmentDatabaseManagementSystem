using System;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        //ILeaveAllocationRepository LeaveAllocationRepository { get; }
        //ILeaveRequestRepository LeaveRequestRepository { get; }
        //ILeaveTypeRepository LeaveTypeRepository { get; }
        ISchoolManagementRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task Save();
    }
}
