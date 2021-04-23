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
            .AddChoice("Category 1")
            .AddChoice("Category 2")
            .AddChoice("Category 3")
            .AddChoice("Category 4")
            .AddChoice("Category 5")
            .AddChoice("Finalise Order")
            .AddChoice("Exit"));
            var catChoice = "";
            switch (select)
            {
                case "Category 1": // Case should use single quotes
                    catChoice = "1";
                    break;
                case "Category 2":
                    catChoice = "2";
                    break;
                case "Category 3":
                    catChoice = "3";
                    break;
                case "Category 4":
                    catChoice = "4";
                    break;
                case "Category 5":
                    catChoice = "5";
                    break;
                case "Finalise Order":
                    this.FinaliseOrder();
                    Console.WriteLine("Order closed");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Exit");
                    break;
            }
            return catChoice;
        }

        public void SelectItems(string cat)
        {
            var choice1 = "";
            do
            {
                Console.Clear();
                var result1 = db.SearchForItemByCat(cat);
                foreach (Item item in result1)
                {
                    Console.WriteLine($"ID: {item._id} Cat: {item.Cat} Qty: {item.Qty} {item.Detail} ${item.Price}");
                }
                Console.WriteLine("Enter Id and press ENTER to add item it order");
                var orderId1 = Console.ReadLine();
                this.AddToOrder(orderId1);
                Console.WriteLine("Add another item from this category?");
                choice1 = Console.ReadLine();
            }
            while (choice1.ToUpper() == "Y");
            ShowCategories();
        }

        public void ShowOrder()
        {
            decimal total = 0.00M;
            foreach (Item item in this.GetList())
            {
                Console.WriteLine($"{item.Detail}\t\t\t£{item.Price}");
                total += item.Price;
            }
            Console.WriteLine($"Order Total: \t\t\t{total}");
        }

        public void FinaliseOrder()
        {
            foreach(Item item in this.GetList())
            {
                db.updateStock(item._id, item.Qty);
            }
        }
    }
}
