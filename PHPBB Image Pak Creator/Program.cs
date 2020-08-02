using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PHPBB_Image_Pak_Creator
{
    class Program
    {
        private static string[] _acceptedFileTypes = { "png", "gif", "jpg", "jpeg" };

        static void Main(string[] args)
        {
            try
            {
                Console.Write("Location: ");
                string location = Console.ReadLine();

                if (!Directory.Exists(location))
                {
                    ConsoleColor col = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"'{location}' does not exist");
                    Console.ForegroundColor = col;
                    return;
                }

                string[] files = Directory.GetFiles(location);

                List<string> lines = new List<string>();
                string pakLocation = $"{location}/rogue_dawn.pak";

                foreach (string file in files)
                {
                    bool accepted = false;
                    string extension = "";

                    foreach (string ext in _acceptedFileTypes)
                    {
                        if (file.EndsWith(ext))
                        {
                            accepted = true;
                            extension = $".{ext}";
                        }
                    }

                    if (!accepted)
                    {
                        continue;
                    }

                    Image img = Image.FromFile(file);
                    FileInfo info = new FileInfo(file);

                    string[] emotionWords = info.Name.Replace(extension, "").Split('-');

                    string emotion = default;

                    foreach (string emotionWord in emotionWords)
                    {
                        emotion += $" {emotionWord.Capitalise()}";
                    }

                    emotion = emotion.Trim();

                    string line = $"'{info.Name}', '{img.Width}', '{img.Height}', '1', '{emotion}', ':{info.Name.Replace(extension, "")}:',";

                    lines.Add(line);
                }

                File.WriteAllLines(pakLocation, lines);

                Console.WriteLine($"Finished writing {lines.Count} lines to the pak file");
            }
            catch(Exception e)
            {
                ConsoleColor col = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{e.Message}");

                if (e.InnerException != null)
                {
                    Console.WriteLine($"{e.InnerException.Message}");
                }

                Console.ForegroundColor = col;
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
