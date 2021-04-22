using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

namespace StockManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Interface newInterface = new Interface();
            newInterface.WelcomeScreen();
        }
    }
}
