using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using LoveKicher.Llsif.Live.Extras;
using System.IO;

namespace Malody2SIF
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName;
            if (args.Length > 0)
            {
                fileName = args[0];
            }
            else
            {
                Console.Write("Input the malody map file name(end with .mc): ");
                fileName = Console.ReadLine();
            }
            var outputFile = Path.ChangeExtension(fileName, ".json");

            try
            {
                var malodyJson = File.ReadAllText(fileName);
                var c = new MapConverter(malodyJson);
                var map = c.Convert();

                File.WriteAllText(outputFile, JsonConvert.SerializeObject(map));
                Console.Write($"Convertion finished successfully!Total available note count is {map.Count}.");
            }
            catch (Exception ex)
            {
                Console.Write($"Convertion failed!\n{ex.Message}");
            }



        }
    }
}
