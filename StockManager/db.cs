using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

namespace StockManager
{
    class db
    {
        public static void AddItem(Item item)
        {
            var cString = ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString;
            var client = new MongoClient(cString);
            var database = client.GetDatabase("ItemDB");
            var coll = database.GetCollection<Item>("StockDB");

            coll.InsertOne(item);

            Console.WriteLine("Item added");

        }

        public static List<Item> ShowAllItems()
        {
            var cString = ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString;
            var client = new MongoClient(cString);
            var database = client.GetDatabase("ItemDB");
            var coll = database.GetCollection<Item>("StockDB");

            var results = coll.Find(new BsonDocument()).ToList();

            return results;
        }

        public static List<Item> SearchForItemByName(string name)
        {
            var cString = ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString;
            var client = new MongoClient(cString);
            var database = client.GetDatabase("ItemDB");
            var coll = database.GetCollection<Item>("StockDB");

            var filter = Builders<Item>.Filter.Eq("LastName", name);

            var results = coll.Find(filter).ToList();

            return results;
        }

        public static List<Item> SearchForItemById(string id)
        {
            var cString = ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString;
            var client = new MongoClient(cString);
            var database = client.GetDatabase("ItemDB");
            var coll = database.GetCollection<Item>("StockDB");

            var filter = Builders<Item>.Filter.Eq("_id", id);

            var results = coll.Find(filter).ToList();

            return results;
        }
    }
}
