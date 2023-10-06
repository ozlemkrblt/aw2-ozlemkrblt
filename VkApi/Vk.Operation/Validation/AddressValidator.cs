using FluentValidation;
using Vk.Data.CustomRepository;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateAddressValidator : AbstractValidator<AddressRequest>
{

    public CreateAddressValidator()
    {
        RuleFor(x => x.AddressLine1).NotEmpty().MaximumLength(50).WithMessage("Adress Line 1 is required.Maximum 50 characters length.");
        RuleFor(x => x.AddressLine2).MaximumLength(50).WithMessage("Maximum 50 characters length.");
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.County).NotEmpty().WithMessage("County is required.");
        RuleFor(x => x.PostalCode).Length(5).WithMessage("Postal Code should be 5 digits long.");
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Adress must be binded to a customer. Please provide a customer id.");

    }
}