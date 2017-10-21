using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using LoveKicher.Llsif.Live.Extras;
using System.IO;
using attribute = LoveKicher.Llsif.Attribute;
namespace Malody2SIF
{
    class Program
    {
        internal class config
        {
            internal string fileName;
            internal attribute attribute = attribute.All;
            internal bool isRandom = false;
            internal bool tryCastSwing = false;
        }

        static void Main(string[] args)
        {
            var cfg = new config();
            if (args.Length > 0)
            {
                ParseArgs(args.ToList(),cfg);
            }
            else
            {
                Console.Write("Input the malody map file name(end with .mc): ");
                cfg.fileName = Console.ReadLine();

                Console.Write("Input the attribute of map:\n"+
                    "[Enter] = Random note attribute\n"+
                    "[1] = All smile\n"+
                    "[2] = All pure\n"+
                    "[3] = All cool\n");
                string s;
                if ((s = Console.ReadLine()) != "")
                {
                    switch (s)
                    {
                        case "1":
                            cfg.attribute = attribute.Smile;
                            break;
                        case "2":
                            cfg.attribute = attribute.Pure;
                            break;
                        case "3":
                            cfg.attribute = attribute.Cool;
                            break;
                        default:
                            break;
                    }
                }

            }




            var outputFile = Path.ChangeExtension(cfg.fileName, ".json");

            try
            {
                var malodyJson = File.ReadAllText(cfg.fileName);
                var c = new MapConverter(malodyJson);
                c.MapAttribute = cfg.attribute;
                var map = c.Convert();

                File.WriteAllText(outputFile, JsonConvert.SerializeObject(map));
                Console.Write($"Convertion finished successfully!Total available note count is {map.Count}.");
            }
            catch (Exception ex)
            {
                Console.Write($"Convertion failed!\n{ex.Message}");
            }



        }


        static void  ParseArgs(List<string> args,config cfg)
        {
            //=========parse filename
            var fileName = args[0];
            if (File.Exists(fileName))
                cfg.fileName = fileName;
            else
                throw new ArgumentException(
                    $"'{fileName}' cannot be found or is invalid.");
            //=========parse attribute
            if (args.Contains("-a"))
            {
                var type = args[args.IndexOf("-a") + 1];
                switch (type)
                {
                    case "1":
                        cfg.attribute = attribute.Smile;
                        break;
                    case "2":
                        cfg.attribute = attribute.Pure;
                        break;
                    case "3":
                        cfg.attribute = attribute.Cool;
                        break;
                    default:
                        break;
                }
            }


        }
    }
}
