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

        public static List<Item> SearchForItemByCat(string cat)
        {
            var cString = ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString;
            var client = new MongoClient(cString);
            var database = client.GetDatabase("ItemDB");
            var coll = database.GetCollection<Item>("StockDB");

            var filter = Builders<Item>.Filter.Eq("Cat", cat);

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

        public static void updateStock(string id, int qty)
        {
            var cString = ConfigurationManager.ConnectionStrings["DBconnection"].ConnectionString;
            var client = new MongoClient(cString);
            var database = client.GetDatabase("ItemDB");
            var coll = database.GetCollection<Item>("StockDB");
            var newQty = qty - 1;
            var filter = Builders<Item>.Filter.Eq("_id", id);
            var update = Builders<Item>.Update.Set("Qty", newQty);
            coll.UpdateOne(filter, update);
        }
    }
}
