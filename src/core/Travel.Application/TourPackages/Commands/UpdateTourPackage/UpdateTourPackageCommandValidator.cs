using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Travel.Application.Common.Interfaces;

namespace Travel.Application.TourPackages.Commands.UpdateTourPackage
{
  public class UpdateTourPackageCommandValidator : AbstractValidator<UpdateTourPackageCommand>
  {
    private readonly IApplicationDbContext _context;

    public UpdateTourPackageCommandValidator(IApplicationDbContext context)
    {
      _context = context;

      RuleFor(v => v.Name)
        .NotEmpty().WithMessage("Name is required.")
        .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
        .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
    }

    public Task<bool> BeUniqueName(UpdateTourPackageCommand command, string name, CancellationToken cancellationToken)
    {
      return _context.TourPackages
        .AnyAsync(x => x.Name == name && x.Id != command.Id, cancellationToken)
        .ContinueWith(t => !t.Result, cancellationToken);
    }
  }
}