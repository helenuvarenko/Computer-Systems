using System;
using System.Linq;
using System.IO;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = @"D:\CS\Texts\";
            string[] nameOfFile = new string[] { "fet.txt", "heart.txt", "zelen.txt" };
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            char[] alph = new char[33] {'а', 'б', 'в', 'г', 'ґ', 'д', 'е', 'є', 'ж', 'з', 'и', 'і', 'ї', 'й',
                    'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с',
                    'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ю', 'я' };

            for (int f = 0; f < nameOfFile.Length; f++)
            {
                try
                {
                    double[] numberOfOccurence = new double[alph.Length];
                    double frequency = 0;
                    double entropy = 0;
                    int amountOfLetters = 0;

                    string addressOfFile = address + nameOfFile[f];
                    FileInfo file = new FileInfo(addressOfFile);
                    Console.WriteLine("File to analize: " + file.Name);
                    using (StreamReader sr = new StreamReader(addressOfFile))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            for (int i = 0; i < alph.Length; i++)
                            {
                                var count = line.Count(x => x == alph[i]);
                                numberOfOccurence[i] += count;
                            }
                            amountOfLetters += line.Count(Char.IsLetter);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Total summ of symbols in file: " + amountOfLetters);
                    Console.WriteLine();
                    for (int i = 0; i < alph.Length; i++)
                    {
                        frequency = numberOfOccurence[i] / amountOfLetters;
                        Console.WriteLine("Relative frequency of appearing letter \"" + alph[i] + "\" in the text = " + frequency +
                            " ; Concrete letter present in the text: " + numberOfOccurence[i] + " times.");
                        if (frequency != 0)
                        {
                            entropy += -(frequency * Math.Log(frequency, 2));
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Average entropy of the unequipotent alphabet in the text: " + entropy);
                    Console.WriteLine();
                    Console.WriteLine("Amount of information in the text: " + (entropy * amountOfLetters) / 8);
                    if ((entropy * amountOfLetters) / 8 > amountOfLetters)
                    {
                        Console.WriteLine("");
                    }
                    else if ((entropy * amountOfLetters) / 8 < amountOfLetters)
                    {
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("Amount of information in the text is the same as file size: ");
                    }
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
