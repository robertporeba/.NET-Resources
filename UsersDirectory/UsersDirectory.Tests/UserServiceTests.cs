﻿using FluentAssertions;
using Moq;
using System;
using System.Linq;
using UsersDirectory.App.Abstract;
using UsersDirectory.App.Concrete;
using UsersDirectory.App.Managers;
using UsersDirectory.Domain.Entity;
using Xunit;


namespace UsersDirectory.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void AddNewUserTest()
        {
            //Arrange
            User user = new User(33, "Name", "Surname", "City");
            IService<User> userService = new UserService();

            //Act
            userService.AddUser(user);
            userService.Users.Add(user);
            //Assert 
            userService.Users.FirstOrDefault(i => i.Id == user.Id).Should().NotBeNull();
        }
    }
}