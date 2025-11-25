using Travel.Application.Common.Exceptions;
using Travel.Application.TourLists.Commands.CreateTourList;
using Travel.Application.TourPackages.Commands.CreateTourPackage;
using Travel.Application.TourPackages.Commands.UpdateTourPackage;
using Travel.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.TourPackages.Commands
{
    using static DatabaseFixture;

    [Collection("DatabaseCollection")]
    public class UpdateTourPackageTests
    {
        public UpdateTourPackageTests()
        {
            ResetState().GetAwaiter().GetResult();
        }

        [Fact]
        public void ShouldRequireValidTourPackageId()
        {
            var command = new UpdateTourPackageCommand
            {
                Id = 4,
                Name = "Free Walking Tour"
            };

            FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task ShouldUpdateTourPackage()
        {
            var listId = await SendAsync(new CreateTourListCommand
            {
                City = "Rabat",
                Country = "Morocco",
                About = "Lorem Ipsum"
            });

            var packageId = await SendAsync(new CreateTourPackageCommand
            {
                ListId = listId,
                Name = "Free Walking Tour Rabat",
                Duration = 2,
                Price = 10,
                InstantConfirmation = true,
                MapLocation = "Lorem Ipsum",
                WhatToExpect = "Lorem Ipsum",
                Currency = Travel.Domain.Enums.Currency.EUR
            });

            var command = new UpdateTourPackageCommand
            {
                Id = packageId,
                Name = "Night Free Walking Tour Rabat",
                // REQUIRED FOR YOUR HANDLER
                WhatToExpect = "Updated Expectations",
                MapLocation = "Updated Map",
                Price = 20,
                Duration = 3,
                InstantConfirmation = false,
                Currency = (int)Travel.Domain.Enums.Currency.USD
            };

            await SendAsync(command);

            var item = await FindAsync<TourPackage>(packageId);

            item.Name.Should().Be(command.Name);
            item.WhatToExpect.Should().Be(command.WhatToExpect);
            item.MapLocation.Should().Be(command.MapLocation);
            item.Price.Should().Be((float)command.Price);
            item.Duration.Should().Be(command.Duration);
            item.InstantConfirmation.Should().Be(command.InstantConfirmation);
        }

        [Fact]
        public async Task ShouldRequireUniqueName()
        {
            var listId = await SendAsync(new CreateTourListCommand
            {
                City = "Bogota",
                Country = "Colombia",
                About = "Lorem Ipsum"
            });

            var package1Id = await SendAsync(new CreateTourPackageCommand
            {
                ListId = listId,
                Name = "Bike Tour in Bogota",
                Duration = 2,
                Price = 10,
                InstantConfirmation = true,
                MapLocation = "A",
                WhatToExpect = "B",
                Currency = Travel.Domain.Enums.Currency.USD
            });

            var package2Id = await SendAsync(new CreateTourPackageCommand
            {
                ListId = listId,
                Name = "Salt Cathedral Tour",
                Duration = 2,
                Price = 10,
                InstantConfirmation = true,
                MapLocation = "A",
                WhatToExpect = "B",
                Currency = Travel.Domain.Enums.Currency.USD
            });

            var command = new UpdateTourPackageCommand
            {
                Id = package2Id,
                Name = "Bike Tour in Bogota",
                WhatToExpect = "Lorem Ipsum",      
                MapLocation = "Location",
                Price = 20,
                Duration = 2,
                InstantConfirmation = true,
                Currency = (int)Travel.Domain.Enums.Currency.USD
            };

            var ex = await FluentActions.Invoking(() => SendAsync(command))
                .Should()
                .ThrowAsync<ValidationException>();

            ex.Which.Errors.Should().ContainKey("Name");
            ex.Which.Errors["Name"].Should().Contain("The specified name already exists.");
        }
    }
}