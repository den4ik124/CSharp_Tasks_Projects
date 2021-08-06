using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLE_BWT
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //string input = "aDDDDDDDAAAAAAABBBCCCX";
            //string input = "aBcDeFgHiJkLmNoP";
            //string input = "$кокакоала";
            string input = "TATGATGGTCATATCGAGAGCTTGGCCGGACCTGCAGCAGAACCGCCTCGCATTTGATGACGTTGCAGTTATACCTGGAAGCGGCCTAATACAGCACAATGCTCGCCGCAGCTTATTAAAAAGATTGGTCCAAGGCGGCAAAGTCTTAAAAGACAAGACACAGGTGTACATGAGGTAGCTCACTGACTTAGGCCTGATGTCGAAAATAACAATAGTCTAATGGTTCTGTGACGCCACTAATATTTAGAAGTCTGTCAGTCGACATGAATGCCTTGTCTACATGCGGATTTTCTACCGCGAACCTTTGTACAACGGGCCTTGACTGCCAACGGGAGAACGCCCGGGATGAGATCAAGTCCTCCCAGCGAGCAAAAATAGCAAAACATTGGGGCTACCATCAGATCCGAGTTGAATTCATAATAAAAGTCCTTCCTCTAGAGGCGAGTCGAATGTGAGGCAACTCATAACGTGCGAGCGAGTCTGCTACAAGAGGTGACATTACCTGCTAAAGTAGCAGTATTCGCACAGCCGGGGGACTGGCATTTACCATCTAACCCCCCTACGTAGCGCTAGTAGGACGACCAGAGGTAAAGATAGCACGTGCACTAATACACGTACACAAGATCCTGATTAATGTTCGAAAAAATAGTACCTGTCTTTCTGGCGGGCCTTGCGTCTATTAATTCTAATTACGTTAAGTAAGCGAAGCGTGTTCGGGCGCGGTTTCTTTTTGGTAGCGGGCATTTTATATGGGGGAGTTCTCACTTAGCAGGGATCTAGCAGGCGTGTTGCGTGGATATCCGCACTGGTCAAGGCGGTTGGCAGAAACATCATTAGGGGTCGCACCTTCTTATACAACAAGAGGGTCATTTCTCAAAGGCCCTAAGGATCACGGGGAATTGGCTGTCACCGTCGGTCACAATCTGGGTTCTAGCGGC";

            Console.WriteLine($"initial string: (input LENGTH {input.Length})\n");
            PrintString(input);

            var BWT = BWT_test(input);

            string result = RLE(BWT);

            //input = RLE_BackTransform(result);

            Console.WriteLine($"BWT шифрование: (BWT LENGTH: {BWT.Length})\n");
            PrintString(BWT);
            Console.WriteLine($"RLE сжатие: (RLE LENGTH: {result.Length})\n");
            PrintString(result);

            Console.WriteLine($"Коеф-т сжатия: {(double)input.Length / (double)result.Length}");
        }

        private static void PrintString(string input)
        {
            var chars = input.ToArray();
            foreach (var item in chars)
            {
                if (item == '$')
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(item);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine();
        }

        private static string RLE(string input)
        {
            char symbol = default;
            int counter = 0;
            string result = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (i == 0)
                {
                    symbol = input[i];
                    counter++;
                }
                else if (input[i] == input[i - 1])
                {
                    counter++;
                    symbol = input[i];
                    if (i == input.Length - 1)
                    {
                        result += $"{symbol}{counter}";
                    }
                }
                else
                {
                    result += $"{symbol}{counter}";
                    symbol = input[i];
                    counter = 1;
                    if (i == input.Length - 1)
                    {
                        counter = 1;
                        result += $"{symbol}{counter}";
                    }
                }
            }
            return result;
        }

        private static string RLE_BackTransform(string input)
        {
            string result = "";
            for (int i = 0; i < input.Length; i += 2)
                result += new string(input[i], int.Parse(input[i + 1].ToString()));
            return result;
        }

        private static string BWT_test(string input)
        {
            input += '$';
            List<string> result = new List<string>(input.Length) { input };
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < input.Length; i++)
            {
                input = $"{input[input.Length - 1]}{input.Substring(0, input.Length - 1)}";
                result.Add(input);
            }

            result.Sort();
            string res = "";
            foreach (var item in result)
                res += item[item.Length - 1];

            return res;
        }
    }
}