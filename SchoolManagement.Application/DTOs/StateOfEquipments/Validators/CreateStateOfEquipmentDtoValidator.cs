using FluentValidation;

namespace SchoolManagement.Application.DTOs.StateOfEquipments.Validators
{
    public class CreateStateOfEquipmentDtoValidator : AbstractValidator<CreateStateOfEquipmentDto>
    {
        public CreateStateOfEquipmentDtoValidator()
        {
            Include(new IStateOfEquipmentDtoValidator());
        }
    }
}
 