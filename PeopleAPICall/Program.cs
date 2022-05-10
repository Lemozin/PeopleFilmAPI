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
using PeopleAPICall.Structures;

namespace PeopleAPICall
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest request = HttpWebRequest.Create("https://swapi.dev/api/people/");
            WebResponse response = request.GetResponse();
            FilmStruct filmStruct = new FilmStruct();
            List<FilmStruct> filmStructList = new List<FilmStruct>();
            List<FilmStruct> filmStructListUnique = new List<FilmStruct>();
            Dictionary<string, List<string>> personFilm = new Dictionary<string, List<string>>();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            string Person_JSON = reader.ReadToEnd();

            Person person = Newtonsoft.Json.JsonConvert.DeserializeObject<Person>(Person_JSON);

            foreach (var result in person.results)
            {
                foreach (var resultPerson in result.films)
                {                  
                    filmStruct.FilmName = resultPerson;
                    filmStructList.Add(filmStruct);
                }
                personFilm.Add(result.name, result.films);
            }

            filmStructListUnique = filmStructList.Distinct().ToList();

            foreach (var flmResult in filmStructListUnique)
            {
                request = HttpWebRequest.Create(flmResult.FilmName);
                response = request.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                Person_JSON = reader.ReadToEnd();
                Film film = Newtonsoft.Json.JsonConvert.DeserializeObject<Film>(Person_JSON);
                Console.WriteLine("===================MOVIE TITLE====================");
                Console.WriteLine("{0}", film.title);
                Console.WriteLine("===================MOVIE TITLE====================");
                foreach (KeyValuePair<string, List<string>> entry in personFilm)
                {
                    foreach (var dicValue in entry.Value)
                    {
                        if (flmResult.FilmName == dicValue)
                        {
                            var names = string.Join(", ", entry.Key);

                            Console.WriteLine(names);
                        }
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
