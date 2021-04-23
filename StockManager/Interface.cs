using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;
using MongoDB.Bson;

namespace StockManager
{
    class Interface
    {
        public void WelcomeScreen() // Initial welcome screen / main menu
        {
            Console.Clear();
            AnsiConsole.Render(
            new FigletText("Acme Computing")
            .Centered()
            .Color(Color.Red));

            Console.WriteLine("\n");
            

            var rule = new Rule("[white]Stock Management System[/]");
            rule.Style = Style.Parse("red dim");
            rule.Alignment = Justify.Center;
            AnsiConsole.Render(rule);

            Console.WriteLine("The Cutting Edge Of Retail\n");
            Console.WriteLine("\u00A91986 All Rights Reserved\n");

            Console.WriteLine("\n");

            var select = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Please choose an option:")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoice("New Order")
            .AddChoice("Show Items By Category")
            .AddChoice("Search For Item By Id")
            .AddChoice("Manager's Menu")
            .AddChoice("Exit"));

            switch (select) // Menu logic
            {
                case "New Order":
                    NewOrder();
                    break;
                case "Show Items By Category":
                    Console.WriteLine("Enter Category: ");
                    var cat = Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Firing up database, herding hamsters onto their wheels, please wait....");
                    var result1 = db.SearchForItemByCat(cat);
                    Console.Clear();

                    var table = new Table();

                    // Add some columns
                    table.AddColumn("ID");
                    table.AddColumn("Qty");
                    table.AddColumn("Detail");
                    table.AddColumn("Price");

                    foreach (Item item in result1)
                    {
                        table.AddRow(item._id.ToString(), item.Qty.ToString(), item.Detail.ToString(), item.Price.ToString());
                    }

                    // Render the table to the console
                    AnsiConsole.Render(table);
                    Console.WriteLine("Press any key to return to main menu");
                    Console.ReadKey();
                    Console.Clear();
                    WelcomeScreen();
                    break;
                case "Search For Item By Id":
                    Console.WriteLine("Enter Item ID: ");
                    var id = Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Firing up database, herding hamsters onto their wheels, please wait....");
                    var result2 = db.SearchForItemById(id);
                    Console.Clear();

                    var table2 = new Table();

                    // Add some columns
                    table2.AddColumn("ID");
                    table2.AddColumn("Qty");
                    table2.AddColumn("Detail");
                    table2.AddColumn("Price");

                    foreach (Item item in result2)
                    {
                        table2.AddRow(item._id.ToString(), item.Qty.ToString(), item.Detail.ToString(), item.Price.ToString());
                    }

                    // Render the table to the console
                    AnsiConsole.Render(table2);
                    Console.WriteLine("Press any key to return to main menu");
                    Console.ReadKey();
                    Console.Clear();
                    WelcomeScreen();
                    break;
                case "Manager's Menu":
                    ManagersMenu();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        //------------------- Interface Methods --------------------------------------------------

        public void NewOrder() // Method to be run when a new order is created via the menu
        {
            var newOrder = new Order(); // new order object created from Order class
            var cat = "";
            do
            {                
                Console.Clear();
                newOrder.ShowOrder();
                cat = newOrder.ShowCategories(); // Show categories menu and store selection in cat variable
                newOrder.SelectItems(cat); // Show items from the selected category. Output from above line passed as argument. Will loop until order is finished

            } while (cat != "X"); // Menu will loop until user quits of finalises the order
        }

        //----------------- Managers Menu Method ------------------------------------------------

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
            .AddChoice("Show Items By Category")
            .AddChoice("Search For Item By Id")
            .AddChoice("Exit To Main Menu"));

            switch (select)
            {
                case "Add Item To Stock":
                    var choice = "";
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Enter ID: ");
                        var Id = Console.ReadLine();
                        Console.WriteLine("Enter Category: ");
                        var Cat = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Quantity: ");
                        var qty = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Price: ");
                        var price = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Description: ");
                        var detail = Console.ReadLine();
                        Console.WriteLine("Enter Sale Category: ");
                        var sale = int.Parse(Console.ReadLine());
                        var newItem = new Item(Id, Cat, qty, price, detail, sale);
                        db.AddItem(newItem);
                        Console.WriteLine("Item added to stock, add another item - 'Y' or 'N'?");
                        choice = Console.ReadLine();
                    } while (choice.ToUpper() == "Y");
                    ManagersMenu();
                    break;
                case "Show Items By Category":
                    Console.WriteLine("Enter Search: ");
                    var cat = Console.ReadLine();
                    var result = db.SearchForItemByCat(cat);
                    foreach (Item item in result)
                    {
                        Console.WriteLine($"ID: {item._id} Qty: {item.Qty} {item.Detail} ${item.Price}");
                    }
                    break;
                case "Search For Item By Id":
                    Console.WriteLine("Enter Item ID: ");
                    var id = Console.ReadLine();
                    var output = db.SearchForItemById(id);
                    foreach (Item item in output)
                    {
                        Console.WriteLine($"ID: {item._id} Qty: {item.Qty} {item.Detail} ${item.Price}");
                    }
                    break;
                case "View All Stock Items":
                    Console.Clear();
                    Console.WriteLine("Firing up database, herding hamsters onto their wheels, please wait....");
                    var allresults = db.ShowAllItems();

                    var table = new Table();

                    // Add some columns
                    table.AddColumn("ID");
                    table.AddColumn("Qty");
                    table.AddColumn("Detail");
                    table.AddColumn("Price");

                    foreach (Item item in allresults)
                    {
                        table.AddRow(item._id.ToString(), item.Qty.ToString(), item.Detail.ToString(), item.Price.ToString());
                    }

                    // Render the table to the console
                    AnsiConsole.Render(table);
                    Console.WriteLine("Press any key to return to menu");
                    Console.ReadKey();
                    ManagersMenu();
                    break;
                default:
                    WelcomeScreen();
                    break;
            }
        }

        
    }
}