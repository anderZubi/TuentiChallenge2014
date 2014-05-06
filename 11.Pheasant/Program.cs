using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;

namespace _11.Pheasant
{
    class ProgramC
    {
        static void Main(string[] args)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            CreateAllCombinations(3, alphabet, "");

            Dictionary<int, string> timestamps = new Dictionary<int, string>();

            string timestampsPath = @"D:\ander\SkyDrive\Proiektuak\Tuenti\201404.TuentiChallenge4\11.Pheasant\last_times";

            foreach (var dir in Directory.GetDirectories(timestampsPath))
            {
                foreach (var file in Directory.GetFiles(dir))
                {
                    int timestamp = int.Parse(File.ReadLines(file).First());

                    int index = file.LastIndexOf('\\') + 1;
                    string user = file.Substring(index, file.LastIndexOf('.') - index);

                    timestamps.Add(timestamp, user);
                }
            }

            string input = Console.ReadLine();

            List<List<string>> results = new List<List<string>>();

            do
            {
                string[] line = input.Split(new char[] { ';', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                int numEvents = int.Parse(line[0]);

                Dictionary<string, string> friends = new Dictionary<string, string>();
                List<Event> eventList = new List<Event>();

                for (int i = 1; i < line.Length; i = i + 2)
                {
                    friends.Add(line[i], line[i + 1]);
                }

                foreach (var f in friends)
                {
                    int timestamp = int.Parse(System.IO.File.ReadAllText(String.Format(@"..\last_times\{0}\{1}.timestamp", f.Key.Substring(f.Key.Length - 2), f.Key)));

                    string path = String.Format(@"..\encrypted\{0}\{1}.feed", f.Key.Substring(f.Key.Length - 2), f.Key);

                    byte[] encryptedFile = System.IO.File.ReadAllBytes(path);

                    string decryptedFile = "";

                    foreach (var comb in combinations)
                    {
                        string decrypted = AES_Decrypt(encryptedFile, f.Value + comb);

                        if (decrypted.StartsWith(f.Key))
                        {
                            decryptedFile = decrypted;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(decryptedFile))
                    {

                        string[] events = decryptedFile.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < events.Length - 1; i++)
                        {
                            string[] ev = events[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            eventList.Add(new Event { User = ev[0], Timestamp = int.Parse(ev[1]), Id = ev[2] });
                        }
                    }
                }

                results.Add(eventList.OrderByDescending(x => x.Timestamp).Select(x => x.Id).Take(numEvents).ToList());

                input = Console.ReadLine();

            } while (!String.IsNullOrEmpty(input));

            foreach (var r in results)
            {
                Console.WriteLine(String.Join(" ", r));
            }
        }

        private class Event
        {
            public string User { get; set; }
            public string Id { get; set; }
            public int Timestamp { get; set; }
        }

        static List<string> combinations = new List<string>();

        private static void CreateAllCombinations(int charNum, string alphabet, string current)
        {
            if (current.Length == charNum)
            {
                combinations.Add(current);
            }
            else
            {
                foreach (var c in alphabet)
                {
                    CreateAllCombinations(charNum, alphabet, current + c.ToString());
                }
            }
        }

        // Code to decrypt AES
        public static string AES_Decrypt(byte[] input, string pass)
        {
            System.Security.Cryptography.RijndaelManaged AES = new System.Security.Cryptography.RijndaelManaged();
            System.Security.Cryptography.MD5CryptoServiceProvider Hash_AES = new System.Security.Cryptography.MD5CryptoServiceProvider();
            string decrypted = "";
            try
            {
                AES.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(pass);
                AES.Padding = PaddingMode.None;
                AES.Mode = System.Security.Cryptography.CipherMode.ECB;
                System.Security.Cryptography.ICryptoTransform DESDecrypter = AES.CreateDecryptor();
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(input, 0, input.Length));
                return decrypted;
            }
            catch (Exception ex)
            {
                return ex + "Error";
            }
        }

    }
}
