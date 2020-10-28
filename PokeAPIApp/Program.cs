using PokeAPI;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PokeAPIApp;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PokeAPIApp
{
    class Program
    {

        static async Task Main(string[] args)
            
        {

            Start:
            string URL = "";
            Console.WriteLine("Please Enter Pokemon Name \n");
            string pokemonname = Console.ReadLine();




            #region Pokemon
            URL = "http://pokeapi.co/api/v2/pokemon/" + pokemonname;

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(URL))
            using (HttpContent content = response.Content)
            {
                Console.WriteLine("********************Pokemon********************");
                string result = await content.ReadAsStringAsync();

                if (result == "Not Found")
                {
                    Console.WriteLine("Sorry, Data Not Found \n");

                }

                else
                {
                    Console.BufferHeight = Int16.MaxValue - 1;

                    Console.WriteLine("\n");
                    Console.WriteLine(JsonHelper.FormatJson(result));
                }
            }
            #endregion

            //#region Types
            //URL = "http://pokeapi.co/api/v2/type/" + pokemonname;

            //using (HttpClient client = new HttpClient())
            //using (HttpResponseMessage response = await client.GetAsync(URL))
            //using (HttpContent content = response.Content)
            //{
            //    Console.WriteLine("********************Types********************");
            //    string result = await content.ReadAsStringAsync();

            //    if (result == "Not Found")
            //    {
            //        Console.WriteLine("Data Not Found \n");
            //        //   goto Start;
            //    }
            //    else
            //    {
            //        Console.BufferHeight = Int16.MaxValue - 1;

            //        Console.WriteLine("\n");
            //        Console.WriteLine(JsonHelper.FormatJson(result));
            //    }


            //}
            //#endregion





            Console.WriteLine("Press 'c' to Clear Screen and Start New \n");
            string optionentered = Console.ReadLine();
            if (optionentered == "C" || optionentered == "c")
            {
                Console.Clear();
                goto Start;

            }
           
            else
            {
                goto Start;

            }
        }


    }

    class JsonHelper
    {
        private const string INDENT_STRING = "    ";
        public static string FormatJson(string str)
        {
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        bool escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }
    }

    static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }
    }
}
