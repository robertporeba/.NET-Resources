﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UsersDirectory.App.Concrete;
using UsersDirectory.App.Managers;
using UsersDirectory.Domain.Entity;

namespace UsersDirectory
{
    class Program
    {
        public static void Main(string[] args)
        {
            MenuActionService actionService = new MenuActionService();
            UserService userService = new UserService();
            UserManager userManager = new UserManager(userService);
            ListService listService = new ListService(userService);
            listService.ReadDataFromJsonFile();

            while (true)
            {
                Console.WriteLine("USERS DIRECTORY");
                Console.WriteLine("Choose option:");

                var mainMenu = actionService.GetMenuActionsByMenuName("MainMenu");
                for (int i = 0; i < mainMenu.Count; i++)
                {
                    Console.WriteLine(mainMenu[i].Id + " " + mainMenu[i].Name);
                }

                var opertion = Console.ReadKey();
                Console.Clear();

                switch (opertion.KeyChar)
                {
                    case '1':
                        var newUserId = userManager.AddUserManager();
                        listService.SaveDataToJsonFile();
                        break;
                    case '2':
                        var removeUserId = userManager.RemoveUserManager();
                        listService.SaveDataToJsonFile();
                        break;
                    case '3':
                        var detailsUserId = userManager.GetUserDetailsManager();
                        break;
                    case '4':
                        userManager.GetUserByCityManager();
                        break;
                    case '5':
                        userManager.GetAllUsersManager();
                        break;
                    case '6':
                        userManager.UpdateUserManager();
                        listService.SaveDataToJsonFile();
                        break;
                    case '7':
                        actionService.ExitProgram();
                        break;
                    default:
                        Console.WriteLine("Wrong option");
                        Console.WriteLine("\nPress any key to back to menu...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
