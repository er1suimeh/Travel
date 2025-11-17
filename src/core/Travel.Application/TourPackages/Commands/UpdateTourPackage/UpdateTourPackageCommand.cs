using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Travel.Application.Common.Exceptions;
using Travel.Application.Common.Interfaces;
using Travel.Domain.Entities;

namespace Travel.Application.TourPackages.Commands.UpdateTourPackage
{
  public partial class UpdateTourPackageCommand : IRequest
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string WhatToExpect { get; set; }
    public string MapLocation { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; }
    public bool InstantConfirmation { get; set; }
    public int Currency { get; set; }
  }

  public class UpdateTourPackageCommandHandler : IRequestHandler<UpdateTourPackageCommand>
  {
    private readonly IApplicationDbContext _context;

    public UpdateTourPackageCommandHandler(IApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Unit> Handle(UpdateTourPackageCommand request, CancellationToken cancellationToken)
    {
      var entity = await _context.TourPackages.FindAsync(request.Id);
      if (entity == null)
      {
        throw new NotFoundException(nameof(TourPackage), request.Id);
      }
      entity.Name = request.Name;
      entity.WhatToExpect = request.WhatToExpect;
      entity.MapLocation = request.MapLocation;
      entity.Price = (float)request.Price;
      entity.Duration = request.Duration;
      entity.InstantConfirmation = request.InstantConfirmation;
      entity.Currency = (Domain.Enums.Currency)request.Currency;

      await _context.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }
  }
}