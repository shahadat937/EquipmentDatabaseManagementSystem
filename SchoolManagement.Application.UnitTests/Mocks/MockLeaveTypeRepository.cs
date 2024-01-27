using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.UnitTests.Mocks
{
    public static class MockLeaveTypeRepository
    {
        //public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
        //{
        //    var leaveTypes = new List<LeaveType>
        //    {
        //        new LeaveType
        //        {
        //            LeaveTypeId = 1,
        //            DefaultDays = 10,
        //            Name = "Test Vacation"
        //        },
        //        new LeaveType
        //        {
        //            LeaveTypeId = 2,
        //            DefaultDays = 15,
        //            Name = "Test Sick"
        //        },
        //        new LeaveType
        //        {
        //            LeaveTypeId = 3,
        //            DefaultDays = 15,
        //            Name = "Test Maternity"
        //        }
        //    };

        //    var mockRepo = new Mock<ILeaveTypeRepository>();

        //    mockRepo.Setup(r => r.GetAll()).ReturnsAsync(leaveTypes);

        //    mockRepo.Setup(r => r.Add(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
        //    {
        //        leaveTypes.Add(leaveType);
        //        return leaveType;
        //    });

        //    return mockRepo;

        //}
    }
}
