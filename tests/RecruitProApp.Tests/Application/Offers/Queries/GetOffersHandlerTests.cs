using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using RecruitProApp.Application.Common.Interfaces;
using RecruitProApp.Application.Offers.DTOs;
using RecruitProApp.Application.Offers.Queries;
using RecruitProApp.Domain.Entities.Offers;

namespace RecruitProApp.Tests.Application.Offers.Queries
{
    public class GetOffersHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly IOfferRepository _offerRepositoryMock;
        private readonly ILogger<GetOffersQueryHandler> _loggerMock;
        private readonly GetOffersQueryHandler _handler;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;

        public GetOffersHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            _offerRepositoryMock = _fixture.Freeze<IOfferRepository>();
            _loggerMock = _fixture.Freeze<ILogger<GetOffersQueryHandler>>();
            _handler = new GetOffersQueryHandler(_offerRepositoryMock, _loggerMock);
        }

        [Fact]
        public async Task Handle_ShouldReturnOfferDtos_WhenOffersExists()
        {
            // Arrange
            var offers = _fixture.CreateMany<Offer>(3).ToList();

            _offerRepositoryMock
                .GetAllAsync(_cancellationToken)
                .Returns(Task.FromResult(offers));

            var query = new GetOffersQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(offers.Count);
            result.Should().AllBeOfType<OfferDto>();
            result.Select(x => x.Title).Should().BeEquivalentTo(offers.Select(x => x.Title));
        }
    }
}
