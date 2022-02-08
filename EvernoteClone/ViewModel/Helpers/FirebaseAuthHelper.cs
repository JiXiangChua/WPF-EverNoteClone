using EvernoteClone.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvernoteClone.ViewModel.Helpers
{
    public class FirebaseAuthHelper
    {
        private static string api_key = API_keys.google_firebase_api_key;

        public static async Task<bool> Register(User user)
        {
            using(HttpClient client = new HttpClient())
            {
                //1. body object to send over to the firebase REST service
                var body = new //creating a new anonymous object and specify its properties
                {
                    email = user.Username,
                    password = user.Password,
                    returnSecureToken = true //firebase requires yu to pass this property and is set to always true
                };

                //2. However, we have to send over the C# object as a JSON object
                //Therefore, we need to serialise c# objects into JSON
                string bodyJson = JsonConvert.SerializeObject(body);
                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                //3. Make the request and the request will return our response
                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={api_key}", data); //must be a Post request to the REST API
                //first params is the url to make the post request
                //2nd params is the data (in JSON string content) send to the API service

                if (response.IsSuccessStatusCode)
                {
                    //Response successful
                    //If it was successful, you should have the ID that the user was created
                    //This ID will allow you to save new notebooks.

                    string resultJson = await response.Content.ReadAsStringAsync();

                    //Convert the Json into C# objects
                    var result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);

                    //Set the id created to the user ID
                    App.UserId = result.localId;

                    return true;
                }
                else
                {
                    //Not successful
                    string errorJson = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<Error>(errorJson);
                    MessageBox.Show(error.error.message);
                    
                    return false;
                }
            }
        }

        /* Below C# objects are classes taken from the JsonUtil website where we can convert
         * JSON objects into C# objects with the respective properties created for us.
         */
        public class FirebaseResult
        {
            public string kind { get; set; }
            public string idToken { get; set; }
            public string email { get; set; }
            public string refreshToken { get; set; }
            public string expiresIn { get; set; }
            public string localId { get; set; }
        }

        public class ErrorDetails
        {
            public int code { get; set; }
            public string message { get; set; }
        }

        public class Error
        {
            public ErrorDetails error { get; set; }
        }
    }
}
