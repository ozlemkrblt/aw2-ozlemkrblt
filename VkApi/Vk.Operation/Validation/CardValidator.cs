using FluentValidation;
using System.Security.Cryptography.X509Certificates;
using Vk.Data.CustomRepository;
using Vk.Schema;

namespace Vk.Operation.Validation;
public class CreateCardValidator : AbstractValidator<CardRequest>
{
    public CreateCardValidator()
    {
        RuleFor(x => x.Cvv).NotEmpty().WithMessage("Cvv is required.");
        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("Card number is required.")
            .Must(x => x.ToString().Length >= 16).WithMessage("Card number length min value is 16.");
        RuleFor(x => x.CardHolder).NotEmpty().MinimumLength(10).WithMessage("Cardholder is required.");
        RuleFor(x => x.AccountId).NotEmpty().WithMessage("Card must be connected to an account. Please provide an account Id");
        RuleFor(x => x.ExpiryDate).NotEmpty().WithMessage("Expiry date for card must be provided.");
    }
}

