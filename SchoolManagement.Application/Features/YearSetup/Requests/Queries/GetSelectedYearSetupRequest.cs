﻿using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.YearSetups.Requests.Queries
{
    public class GetSelectedYearSetupRequest : IRequest<List<SelectedModel>>
    {
    }
} 
