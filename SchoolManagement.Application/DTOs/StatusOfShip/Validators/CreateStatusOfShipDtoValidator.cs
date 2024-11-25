using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.StatusOfShip.Validators
{
    public class CreateStatusOfShipDtoValidator: AbstractValidator<CreateStatusOfShipDto>
    {
        public CreateStatusOfShipDtoValidator() {

            Include(new IStatusOfShipDtoValidator());
        }

        //private void Include(IStatusOfShipDtoValidator statusOfShipDtoValidator)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
