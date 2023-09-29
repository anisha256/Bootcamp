using Bootcamp.Application.Common.Interfaces;
using Bootcamp.Application.Item.Dto;
using Bootcamp.Application.Item.ItemServices;
using Moq;
using FluentAssertions;
using Bootcamp.Application.Common.Interfaces.Repository;

namespace Bootcamp.Application.UnitTests
{

    public class ItemServiceUnitTest
    {
      
        [Fact]
        public async Task AddItem_WithValidRequest_ShouldReturnSuccessResponse()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var genericRepositoryMock = new Mock<IGenericRepository<Domain.Entities.Item>>();

            // Configure the generic repository mock behavior.
            genericRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Domain.Entities.Item>().AsQueryable());

            // Simulate the insertion of an item with a generated itemId.
            genericRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Domain.Entities.Item>()))
                .Returns<Domain.Entities.Item>(item =>
                {
                    return Task.FromResult(item.Id);
                });

            unitOfWorkMock.Setup(uow => uow.GenericRepository<Domain.Entities.Item>())
                .Returns(genericRepositoryMock.Object);

            var itemService = new ItemService(unitOfWorkMock.Object);

            List<Guid> categoriesOfItem = new List<Guid>();

            categoriesOfItem.Add(Guid.Parse("4BDAE5A5-BAE1-41F1-A4C0-D57F9B72D3E0"));

            var validRequest = new ItemRequestDto
            {
                Name = "Table",
                Description = null,
                Price = 200,
                Quantity = 10,
                ImageUrl = null,
                ThresholdQuantity = 5,
                IsAvailable = true,
                Categories = categoriesOfItem
            };

            // Act
            var response = await itemService.AddItem(validRequest);

            // Assert
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Item is created successfully!!");
            response.Data.Should().BeNull();
        }


    }
}