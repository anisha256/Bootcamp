using Bootcamp.Application.Category.Dto;
using Bootcamp.Application.Category.Service;
using Bootcamp.Application.Common.Exceptions;
using Bootcamp.Application.Common.Interfaces;
using Bootcamp.Application.Common.Interfaces.Repository;
using Bootcamp.Application.Common.Models;
using Bootcamp.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Application.UnitTests
{
    public class CategoryServiceUnitTest
    {
        private readonly CategoryService _sut;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private readonly Mock<IGenericRepository<Domain.Entities.Category>> _categoryRepositoryMock = new Mock<IGenericRepository<Domain.Entities.Category>>();

        public CategoryServiceUnitTest()
        {

            _sut = new CategoryService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnCategoryDto_WhenCategoryExists()
        {
            // Arrange
            var categoryID = Guid.NewGuid();
            var categoryName = "Test";
            var categoryDto = new CategoryDto
            {
                Id = categoryID,
                Name = categoryName,
                CreatedOn = DateTime.Now,
                DeleteFlag = false
            };

            _categoryRepositoryMock
                .Setup(x => x.GetByIdAsync(categoryID))
                .ReturnsAsync(new Domain.Entities.Category
                {
                    Id = categoryID,
                    Name = categoryName,
                    CreatedOn = DateTime.Now,
                    DeleteFlag = false
                });

            _unitOfWorkMock
                .Setup(uow => uow.GenericRepository<Domain.Entities.Category>())
                .Returns(_categoryRepositoryMock.Object);

            // Act
            var response = await _sut.GetCategoryById(categoryID);

            // Assert
            response.Should().NotBeNull();
            response.Id.Should().Be(categoryID);
            response.Name.Should().Be(categoryName);
            response.DeleteFlag.Should().BeFalse();

            _categoryRepositoryMock.Verify(repo => repo.GetByIdAsync(categoryID), Times.Once());

        }

        [Fact]
        public async Task GetCategoryById_ShouldThrowNotFoundException_WhenCategoryDoesNotExist()
        {
            // Arrange
            var categoryID = Guid.NewGuid();

            _unitOfWorkMock
                .Setup(uow => uow.GenericRepository<Domain.Entities.Category>())
                .Returns(_categoryRepositoryMock.Object);

            _categoryRepositoryMock
                .Setup(repo => repo.GetByIdAsync(categoryID))
                .ReturnsAsync((Domain.Entities.Category)null);
            // Act
            var exception = await Record.ExceptionAsync(() => _sut.GetCategoryById(categoryID));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Category not found!!");
        }

        [Fact]
        public async Task CreateCategory_WithValidRequest_ShouldReturnSuccessResponse()
        {
            // Arrange
            var categoryName = "TestCategory";
            var validRequest = new CategoryRequestDto
            {
                Name = categoryName
            };

            _unitOfWorkMock.Setup(uow => uow.GenericRepository<Domain.Entities.Category>())
           .Returns(_categoryRepositoryMock.Object);


            _categoryRepositoryMock.Setup(repo => repo.InsertAsync(It.IsAny<Domain.Entities.Category>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _sut.CreateCategory(validRequest);

            // Assert
            response.Success.Should().BeTrue();
            response.Message.Should().Be("Category created successfully");

            _categoryRepositoryMock.Verify(repo => repo.InsertAsync(It.Is<Domain.Entities.Category>(c => c.Name == categoryName)), Times.Once);

            _unitOfWorkMock.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

       /* [Fact]
        public async Task CreateCategory_ShouldReturnCategoryAlreadyExistsResponse_WhenCategoryNameExists()
        {
            // Arrange
             var categoryName = "ExistingCategory";
            var request = new CategoryRequestDto { Name = categoryName };
            var cancellationToken = new CancellationToken();
            var output = new Domain.Entities.Category()
            {
                Name = categoryName
            };


            // Set up the repository mock to return an existing category with the same name
            _categoryRepositoryMock
                .Setup(repo => repo.GetAllAsync().Result.Any(x => x.Name.ToUpper() == categoryName.ToUpper()))
                .Returns(true);
               

            _unitOfWorkMock
                .Setup(uow => uow.GenericRepository<Domain.Entities.Category>())
                .Returns(_categoryRepositoryMock.Object);


            // Act
            var response = await _sut.CreateCategory(request);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Category name already exists!!", response.Message);
            _categoryRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once());
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(cancellationToken), Times.Never());
        }
*/
    }
}
