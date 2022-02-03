using System;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.IO;

namespace Parser
{
    class Program
    {
        public class UniqueWord
        {
            public string Text;
            public int Count;
        }
        
        public static void Main(string[] args)
        {
            string filePath =  @"c:\errorlist.txt";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            char[] dividers = { ' ', ',', '.', '!', '?', ';', ':', '[', ']', '"', '(', ')', '\n', '\r', '\t', '<', '>', '\'' };

            Console.WriteLine("ENTER LINK HERE:");

            string htmlPage = Console.ReadLine();

            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(htmlPage);

                var htmlNodes = doc.DocumentNode.SelectNodes("//html");

                string[] allWords;

                List<string> nodeString = new List<string>();
                List<string> uniqueWords = new List<string>();                

                foreach (var node in htmlNodes)
                {
                    try
                    {
                        string result = node.InnerText.ToUpper();
                        nodeString.Add(result);                        

                        foreach (string n in nodeString)
                        {
                            allWords = n.Split(dividers);
                            uniqueWords = new List<string>(allWords);
                            var grouped = uniqueWords.GroupBy(x => x);
                            var groupedCount = grouped.Select(x => new { key = x.Key, count = x.Count() });


                            foreach (var word in groupedCount)
                            {
                                UniqueWord output = new UniqueWord();
                                output.Text = word.key;
                                output.Count = word.count;
                                Console.WriteLine(output.Text + "-" + output.Count);
                            }
                        }
                    }

                    catch
                    {
                        var time = DateTime.Now.ToString();
                        string error1 = "PROGRAM ERROR --- " + time;
                        Console.WriteLine(error1);
                        File.WriteAllText(filePath, error1);
                    }

                }
            }

            catch
            {
                var time = DateTime.Now.ToString();
                string error2 = "LINK ERROR --- " + time;
                Console.WriteLine(error2);
                File.WriteAllText(filePath, error2);

            }

        }
    }
}