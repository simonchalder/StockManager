using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace StockManager
{
    class Order
    {
        List<Item> OrderList = new List<Item>();

        public void AddToOrder(string item)
        {
            var result = db.SearchForItemById(item)[0];
            OrderList.Add(result);
        }

        public List<Item> GetList()
        {
            return OrderList;
        }

        public string ShowCategories()
        {
            var select = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Please choose an option:")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoice("Cables & Connectors")
            .AddChoice("Components")
            .AddChoice("Peripherals")
            .AddChoice("Displays")
            .AddChoice("Networking")
            .AddChoice("Desktop & Laptop Systems")
            .AddChoice("Finalise Order")
            .AddChoice("Exit"));
            var catChoice = "";
            switch (select)
            {
                case "Cables & Connectors": // Case should use single quotes
                    catChoice = "1";
                    break;
                case "Components":
                    catChoice = "2";
                    break;
                case "Peripherals":
                    catChoice = "3";
                    break;
                case "Displays":
                    catChoice = "4";
                    break;
                case "Networking":
                    catChoice = "5";
                    break;
                case "Desktop & Laptop Systems":
                    catChoice = "6";
                    break;
                case "Finalise Order":
                    this.FinaliseOrder(this.ShowOrder());
                    Console.WriteLine("Order closed, press any key to return to main menu");
                    catChoice = "X";
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Order cancelled, press any key to return to main menu");
                    Console.ReadKey();
                    catChoice = "X";
                    break;
            }
            return catChoice;
        }

        public void SelectItems(string cat)
        {
            if(cat == "X")
            {
                var newInterface = new Interface();
                newInterface.WelcomeScreen();
            }
            else
            {
                var choice1 = "";
                do
                {
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

                    Console.WriteLine("Enter Id and press ENTER to add item it order");
                    var orderId1 = Console.ReadLine();
                    this.AddToOrder(orderId1);
                    Console.WriteLine("Add another item from this category?");
                    choice1 = Console.ReadLine();
                }
                while (choice1.ToUpper() == "Y");
            }
            
        }

        public decimal ShowOrder()
        {
            Console.Clear();
            Console.WriteLine("*** Current Order ***\n");
            decimal total = 0.00M;
            var table = new Table();

            // Add some columns
            table.AddColumn("Detail");
            table.AddColumn("Price");

            foreach (Item item in this.GetList())
            {
                table.AddRow(item.Detail.ToString(), item.Price.ToString());
                total += item.Price;
            }

            // Render the table to the console
            AnsiConsole.Render(table);
            Console.WriteLine($"\nOrder Total: \t£{total}\n");
            return total;
        }

        public void FinaliseOrder(decimal total)
        {
            Console.WriteLine($"Order Total: \t\t${total}");
            Console.WriteLine("Enter payment amount:");
            var payment = decimal.Parse(Console.ReadLine());
            Console.WriteLine($"Customer Payment: \t\t${payment}");
            if (payment >= total)
            {
                Console.WriteLine($"Payment accepted, change due: {total - payment}");
                Console.WriteLine("Press any key to finalise the order");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Insufficient payment, ${payment - total} required. A manager has been called");
                Console.WriteLine("Press any key when payment has been made");
                Console.ReadKey();
            }
            foreach (Item item in this.GetList())
            {
                db.updateStock(item._id, item.Qty);
            }
            Console.WriteLine("Order finalised, press any key to return to main menu");
            Console.ReadKey();
            var newInterface = new Interface();
            newInterface.WelcomeScreen();
        }
    }
}
