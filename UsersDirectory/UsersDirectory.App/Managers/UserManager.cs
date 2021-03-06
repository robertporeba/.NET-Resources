﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UsersDirectory.App.Abstract;
using UsersDirectory.App.Concrete;
using UsersDirectory.Domain.Entity;

namespace UsersDirectory.App.Managers
{
    public class UserManager
    {
        private IService<User> _userService;
        public UserManager(IService<User> userService)
        {
            _userService = userService;
        }

        public User GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            return user;
        }

        public int AddUserManager()
        {
            Console.WriteLine("Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Surname:");
            string surName = Console.ReadLine();
            Console.WriteLine("City:");
            string city = Console.ReadLine();
            Console.Clear();
            var lastId = _userService.GetLastId();
            User user = new User(lastId+1, name, surName, city);
            _userService.AddUser(user);

            return user.Id;
        }

        public int RemoveUserManager()
        {
            Console.WriteLine("Enter user id you want to remove:");
            var id = Console.ReadLine();
            int userId;
            Int32.TryParse(id, out userId);
            Console.Clear();

            var user = _userService.GetUserById(userId);
            _userService.RemoveUser(user);

            return userId;
        }

        public int GetUserDetailsManager()
        {
            Console.WriteLine("Enter user id you want to show:");
            var id = Console.ReadLine();
            int userId;
            Int32.TryParse(id, out userId);
            Console.Clear();

            var user = _userService.GetUserById(userId);
            _userService.GetUserDetails(user);

            return userId;
        }

        public string GetUserByCityManager()
        {
            Console.WriteLine("Enter city for users you want to show:");
            var cityName = Console.ReadLine();
            Console.Clear();

            var usersToShow = _userService.Users.Where(p => p.City == cityName);
            _userService.GetUserByCity(usersToShow);

            return cityName;
        }

        public void GetAllUsersManager()
        {
            _userService.GetAllUsers();
        }

        public void UpdateUserManager()
        {
            Console.WriteLine("Enter user id to edit:");
            var id = Console.ReadLine();
            int userId;
            Int32.TryParse(id, out userId);
            Console.WriteLine("New name:");
            string name = Console.ReadLine();
            Console.WriteLine("New surname:");
            string surName = Console.ReadLine();
            Console.WriteLine("New city:");
            string city = Console.ReadLine();
            Console.Clear();
            User user = new User(userId, name, surName, city);

            _userService.UpdateUser(user);
        }
    }
}
