using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.StatusOfShip.Validators
{
    public class UpdateStatusOfShipDtoValidator: AbstractValidator<StatusOfShipDto>
    {
        public UpdateStatusOfShipDtoValidator() {
            Include(new IStatusOfShipDtoValidator());
            RuleFor(b => b.StatusOfShipId).NotNull().WithMessage("{PropertyName} must be present");
        }

        private void Include(IStatusOfShipDtoValidator statusOfShipDtoValidator)
        {
            throw new NotImplementedException();
        }
    }
}
