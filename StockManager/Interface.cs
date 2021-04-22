using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;
using MongoDB.Bson;

namespace StockManager
{
    class Interface
    {
        public void WelcomeScreen()
        {
            AnsiConsole.Render(
            new FigletText("Acme Stores")
            .LeftAligned()
            .Color(Color.Red));

            Console.WriteLine("Stock Manager System\n");
            Console.WriteLine("The Cutting Edge Of Retail\n");
            Console.WriteLine("\u00A91986 All Rights Reserved\n");

            var select = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Please choose an option:")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoice("New Order")
            .AddChoice("Search For Item By Name")
            .AddChoice("Search For Item By Id")
            .AddChoice("Manager's Menu")
            .AddChoice("Exit"));

            switch (select)
            {
                case "New Order":
                    NewOrder();
                    break;
                case "Search For Item By Name":
                    Console.WriteLine("Enter Search: ");
                    var name = Console.ReadLine();
                    db.SearchForItemByName(name);
                    break;
                case "Search For Item By Id":
                    Console.WriteLine("Enter Item ID: ");
                    var id = Console.ReadLine();
                    db.SearchForItemByName(id);
                    break;
                case "Manager's Menu":
                    ManagersMenu();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        public void NewOrder()
        {

        }

        public void ManagersMenu()
        {
            Console.Clear();
            var select = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Please choose an option:")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoice("Add Item To Stock")
            .AddChoice("View All Stock Items")
            .AddChoice("Search For Item By Name")
            .AddChoice("Search For Item By Id")
            .AddChoice("Manager's Menu")
            .AddChoice("Exit"));

            switch (select)
            {
                case "Add Item To Stock":
                    var choice = "";
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Enter ID: ");
                        var id = Console.ReadLine();
                        Console.WriteLine("Enter Category: ");
                        var cat = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Quantity: ");
                        var qty = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Price: ");
                        var price = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Description: ");
                        var detail = Console.ReadLine();
                        Console.WriteLine("Enter Sale Category: ");
                        var sale = int.Parse(Console.ReadLine());
                        var newItem = new Item(id, cat, qty, price, detail, sale);
                        db.AddItem(newItem);
                        Console.WriteLine("Item added to stock, add another item - 'Y' or 'N'?");
                        choice = Console.ReadLine();
                    } while (choice.ToUpper() == "Y");
                    ManagersMenu();
                    break;
                case "Search For Item By Name":
                    Console.WriteLine("Enter Search: ");
                    var name = Console.ReadLine();
                    db.SearchForItemByName(name);
                    break;
                case "Search For Item By Id":
                    Console.WriteLine("Enter Item ID: ");
                    var search = Console.ReadLine();
                    db.SearchForItemByName(search);
                    break;
                case "Manager's Menu":
                    ManagersMenu();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}