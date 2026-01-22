using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolTermTracker.Services
{
    public class DataService
    {
        
        private static SQLiteAsyncConnection db;

        public DataService()
        {
            if (db != null)
            {
                return;
            }

            // Get an absolute path to the database file
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "StudentData.db");

            db = new SQLiteAsyncConnection(databasePath);
        }

        public SQLiteAsyncConnection GetConnection()
        {
            Console.WriteLine("Connected!");
            return db;
        }
    }
}
