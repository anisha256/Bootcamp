using Bootcamp.Application.Common.Interfaces;
using Bootcamp.Application.Item.Dto;
using Bootcamp.Application.Item.ItemServices;
using Moq;
using FluentAssertions;

namespace Bootcamp.Application.UnitTests
{

    public class UnitTest1
    {
        [Fact]
        public async Task AddItem_WithValidInput_ShouldCreateItem()
        {
            //Arrange 
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var itemService = new ItemService(unitOfWorkMock.Object);
            List<Guid> categoriesOfItem = new List<Guid>();

            categoriesOfItem.Add(Guid.Parse("4BDAE5A5-BAE1-41F1-A4C0-D57F9B72D3E0"));

            var request = new ItemRequestDto { Name = "Table", 
                Description = null,
                Price = 200, 
                Quantity = 0, 
                ImageUrl = null, 
                ThresholdQuantity = 5, 
                IsAvailable = false, 
                Categories = categoriesOfItem };
            // Act
            var result = await itemService.AddItem(request);
            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Item is created successfully!!");
        }
    }
}