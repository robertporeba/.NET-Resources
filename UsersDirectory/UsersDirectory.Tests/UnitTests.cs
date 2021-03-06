using FluentAssertions;
using Moq;
using System;
using UsersDirectory.App.Abstract;
using UsersDirectory.App.Concrete;
using UsersDirectory.App.Managers;
using UsersDirectory.Domain.Entity;
using Xunit;

namespace UsersDirectory.Tests
{
    public class UnitTests
    {
        [Fact]
        public void GetItemByIdUnitTest()
        {
            //Arrange
            User user = new User(1, "Name", "Surname", "City");
            var mock = new Mock<IService<User>>();
            mock.Setup(u => u.GetUserById(1)).Returns(user);

            var manager = new UserManager(mock.Object);

            //Act
            var returnedUser = manager.GetUserById(user.Id);

            //Assert
            returnedUser.Should().BeOfType(typeof(User));
            returnedUser.Should().NotBeNull();
            returnedUser.Should().BeSameAs(user);
        }
    }
}
