using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using PeopleAPICall.Models;
using System.Net;
using System.IO;

namespace PeopleAPICall
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest request = HttpWebRequest.Create("https://swapi.dev/api/people/");
            WebResponse response = request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            string Person_JSON = reader.ReadToEnd();

            Person person = Newtonsoft.Json.JsonConvert.DeserializeObject<Person>(Person_JSON);

            foreach (var result in person.results)
            {
                Console.WriteLine("{0}", result.name);

                foreach (var resultPerson in result.films)
                {
                    Console.WriteLine("{0}", resultPerson);
                }
            }
            Console.ReadKey();
        }
    }
}
