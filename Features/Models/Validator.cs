namespace MiniDevTo.Features.Models
{
    public class Validator:Validator<SignupRequest>
    {
        public Validator() { 
        RuleFor(x=>x.Email)
             .NotEmpty().WithMessage("your email is required!")
             .EmailAddress().WithMessage("the format of your email address is wrong!");

            RuleFor(x => x.Password)
           .NotEmpty().WithMessage("a password is required!")
           .MinimumLength(3).WithMessage("password is too short!")
           .MaximumLength(25).WithMessage("password is too long!");

        }
    }
}
