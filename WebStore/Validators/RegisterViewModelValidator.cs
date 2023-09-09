using FluentValidation;
using WebStore.Models.VeiwModels;

namespace WebStore.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        //Add validation rules for RegisterViewModel.
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Length(6, 30)
                .Matches(@"^\w+$")
                .WithMessage("Only letters, digits, and underscore character are allowed.");

            RuleFor(u => u.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Length(16, 40)
                .Matches(@"^\w+@gmail\.com$")
                .WithMessage("Must consist of @gmail.com at the end, Only letters, digits, and underscore are allowed.");

            RuleFor(u => u.Password)
               .Cascade(CascadeMode.Stop)
               .NotNull().NotEmpty()
               .Length(6, 30)
               .Matches(@"^\w+$")
               .WithMessage("Only letters, digits, and underscore are allowed.");

            RuleFor(u => u.ConfirmPassword)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .Equal(u => u.Password)
                .WithMessage("Passwords do not match.");
        }
    }
}
