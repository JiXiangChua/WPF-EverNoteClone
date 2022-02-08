using EvernoteClone.Model;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteClone.ViewModel.Helpers
{
    public class DatabaseHelper
    {

        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3"); //specify where to save and what is the name of the file saved.
        private static string dbPath = API_keys.google_firebase_realTimeDataBase_path;

        //Method for inserting any type of object into each particular table
        public static async Task<bool> Insert<T>(T item) //since we dont know the type of object, we can use T as a placeholder for generics declarations
        {
            //bool result = false; //result of insert if is successful

            ////Insert
            //using(SQLiteConnection conn = new SQLiteConnection(dbFile))
            //{
            //    //While the SQLite connection is open

            //    conn.CreateTable<T>(); //Creating the data of type T placeholder for any type. Therefore any type of table can be created.
            //    int numberOfRows = conn.Insert(item); // returns an integer with a number of rows added to the table
            //    if (numberOfRows > 0) // means if more than one row inserted into the table,
            //        result = true;

            //    //Close the SQLite connection when it leaves this block of code
            //}
            //return result;

            /*Firebase Method*/
            string jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //Set up a http client to perform request
            using (var client = new HttpClient())
            {
                //Use Post async because this is a insert method!! Unlike read method which uses the getasync
                var result = await client.PostAsync($"{dbPath}{item.GetType().Name.ToLower()}.json", content);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            }
        }

        public static bool Update<T>(T item) 
        {
            bool result = false; 

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                //While the SQLite connection is open

                conn.CreateTable<T>(); 
                int numberOfRows = conn.Update(item); 
                if (numberOfRows > 0) 
                    result = true;

                //Close the SQLite connection when it leaves this block of code
            }

            return result;
        }

        public static bool Delete<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                //While the SQLite connection is open

                conn.CreateTable<T>();
                int numberOfRows = conn.Delete(item);
                if (numberOfRows > 0)
                    result = true;

                //Close the SQLite connection when it leaves this block of code
            }

            return result;
        }

        //Read from database
        public static async Task<List<T>> Read<T>() where T : IHasId //Output is a list containing the same data type as the caller who call this method
        {
            //The Table method cannot infer that T is going to be a non abstract type with a public parameter less constructor because T can be anything.
            //So we need to establish that T is going to have at least a parameter less constructor

            //List<T> items;

            //using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            //{
            //    //While the SQLite connection is open

            //    conn.CreateTable<T>();
            //    //Call the table method which it returns a query and we are able to call the ToList method
            //    items = conn.Table<T>().ToList();

            //    //Close the SQLite connection when it leaves this block of code
            //}

            //return items;

            /*Firebase Realtime database method:*/

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{dbPath}{typeof(T).Name.ToLower()}.json");
                var jsonResult = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    var objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonResult);

                    List<T> list = new List<T>();
                    foreach (var o in objects)
                    {
                        o.Value.Id = o.Key;
                        list.Add(o.Value);
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
