using System.Threading.Tasks;
using FluentAssertions;
using Travel.Application.Common.Exceptions;
using Travel.Application.TourLists.Commands.CreateTourList;
using Travel.Application.TourPackages.Commands.CreateTourPackage;
using Travel.Application.TourPackages.Commands.UpdateTourPackage;
using Travel.Application.TourPackages.Commands.UpdateTourPackageDetail;
using Travel.Domain.Entities;
using Xunit;

namespace Application.IntegrationTests.TourPackages.Commands
{
    using static DatabaseFixture;
    
    [Collection("DatabaseCollection")]
    public class UpdateTourPackageDetailTests
    {
        public UpdateTourPackageDetailTests()
        {
            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public void ShouldRequireValidTourPackageId()
        {
            var command = new UpdateTourPackageCommand
            {
                Id = 88,
                Name = "Free Walking Tour"
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task ShouldUpdateTourPackage()
        {
            var listId = await SendAsync(new CreateTourListCommand
            {
                City = "Zagreb",
                Country = "Croatia",
                About = "Lorem Ipsum"
            });

            var packageId = await SendAsync(new CreateTourPackageCommand
            {
                ListId = listId,
                Name = "Free Walking Tour Zagreb",
                Duration = 2,
                Price = 10,
                InstantConfirmation = true,
                MapLocation = "Lorem Ipsum",
                WhatToExpect = "Lorem Ipsum",
                Currency = Travel.Domain.Enums.Currency.JPY
            });

            var command = new UpdateTourPackageDetailCommand
            {
                Id = packageId,
                ListId = listId,
                Duration = 3,
                Price = 20,
                InstantConfirmation = false,
                MapLocation = "Lorem Ipsum",
                WhatToExpect = "Lorem Ipsum",
                Currency = Travel.Domain.Enums.Currency.USD
            };

            await SendAsync(command);

            var item = await FindAsync<TourPackage>(packageId);

            item.ListId.Should().Be(command.ListId);
            item.Duration.Should().Be(command.Duration);
            item.Price.Should().Be(command.Price);
            item.InstantConfirmation.Should().Be(command.InstantConfirmation);
            item.MapLocation.Should().Be(command.MapLocation);
            item.WhatToExpect.Should().Be(command.WhatToExpect);
        }
    }
}