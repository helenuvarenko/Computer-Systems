using System;
using System.Collections.Generic;

namespace MultiplyShiftRight
{
    class Program
    {
        static void Main(string[] args)
        {
            Binary obj = new Binary();

            Console.WriteLine("Multiplication shift right");
            Console.WriteLine();

            Console.WriteLine("Enter first number:");
            int num1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter second number:");
            int num2 = int.Parse(Console.ReadLine());

            var result = obj.Multiply(obj.ConvertToBinary(num1), obj.ConvertToBinary(num2));

            string help = obj.Output(result);
            Console.WriteLine($"Help code:        {help}");

            Console.ReadKey();
        }
    }

    public class Binary
    {
        public string Output(List<int> value)
        {
            string number = "";
            foreach (var bit in value)
            {
                number += bit.ToString();
            }
            return number;
        }

        public List<int> ConvertToBinary(int value)
        {
            List<int> binaryColl = new List<int>(8) { 0, 0, 0, 0, 0, 0, 0, 0 };
            int counter = 0;
            if (value >= 0) binaryColl[0] = 0;
            else binaryColl[0] = 1;

            value = Math.Abs(value);
            do
            {
                if (counter == 8 - 1) break;

                binaryColl[binaryColl.Count - 1 - counter] = value % 2;
                value = value / 2;
                counter++;

            } while (value != 0);

            return binaryColl;
        }

        public List<int> ConvertToHelpCode(int value)
        {
            List<int> binaryColl = ConvertToBinary(value);
            if (binaryColl[0] == 0) return binaryColl;
            var helpCodeColl = new List<int>(15);


            var invertColl = new List<int>(8) { 1, 0, 0, 0, 0, 0, 0, 0 };
            for (int index = 1; index < binaryColl.Count; index++)
            {
                invertColl[index] = (binaryColl[index] == 0 ? 1 : 0);
            }
            helpCodeColl = Add(first: invertColl, second: ConvertToBinary(1));
            return helpCodeColl;

        }
        public List<int> ConvertToNegative(List<int> binaryColl)
        {

            var helpCodeColl = new List<int>(binaryColl.Count);

           
            var invertColl = new List<int>(binaryColl.Count) { 1, 0, 0, 0, 0, 0, 0, 0 };
            for (int index = 1; index < binaryColl.Count; index++)
            {
                invertColl[index] = (binaryColl[index] == 0 ? 1 : 0);
            }
            helpCodeColl = Add(first: invertColl, second: ConvertToBinary(1));
            return helpCodeColl;

        }
        public List<int> Add(List<int> first, List<int> second)
        {
            var result = new List<int>(8) { 0, 0, 0, 0, 0, 0, 0, 0 };

            int temporary = 0;
            for (int index = first.Count - 1; index >= 1; index--)
            {
                int sum = first[index] + second[index] + temporary;
                if (sum <= 1)
                {
                    result[index] = sum;
                    temporary = 0;
                }
                else if (sum == 2)
                {
                    result[index] = 0;
                    temporary = 1;
                }
                else if (sum == 3)
                {
                    result[index] = 1;
                    temporary = 1;
                }
                if (temporary == 1)
                {
                    var a = 1;
                }
            }

            return result;
        }
        public List<int> AddWithoutSign(List<int> first, List<int> second)
        {
            var result = new List<int>(16) { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            int temporary = 0;
            for (int index = first.Count - 1; index >= 0; index--)
            {
                int sum = first[index] + second[index] + temporary;
                if (sum <= 1)
                {
                    result[index] = sum;
                    temporary = 0;
                }
                else if (sum == 2)
                {
                    result[index] = 0;
                    temporary = 1;
                }
                else if (sum == 3)
                {
                    result[index] = 1;
                    temporary = 1;
                }
                if (temporary == 1)
                {
                    var a = 1;
                }
            }


            return result;
        }

        public List<int> WriteToRegister(List<int> source, List<int> register)
        {
            int temporary = 0;
            for (int index = source.Count - 1; index >= 1; index--)
            {
                int sum = source[index] + register[index] + temporary;
                if (sum <= 1)
                {
                    register[index] = sum;
                    temporary = 0;
                }
                else if (sum == 2)
                {
                    register[index] = 0;
                    temporary = 1;
                }
                else if (sum == 3)
                {
                    register[index] = 1;
                    temporary = 1;
                }

            }

            register[0] = temporary;
            return register;
        }
        
        public List<int> Multiply(List<int> num1, List<int> num2)
        {
            
            Console.WriteLine($"First number binary form:       {Output(num1)}");
            Console.WriteLine($"Second number binary form:      {Output(num2)}");

            int sign = Convert.ToInt16(Convert.ToBoolean(num1[0]) ^ Convert.ToBoolean(num2[0]));

            var register = new List<int>(15) { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 1; i < num2.Count; i++)
            {

                if (num2[num2.Count - 1] == 1)
                {
                    register = WriteToRegister(num1, register);

                    Console.WriteLine($"Add to register:\n    {Output(register)}");

                }


                num2 = Shift(num2);


                register = Shift(register);
                Console.WriteLine($"Shift register:\n    {Output(register)}");
                Console.WriteLine($"First register:\n    {Output(num1)}");

                Console.WriteLine($"Shift multiplication:\n    { Output(num2)}");


            }
           

            int res = ConvertRegisrtyToInt(register);
            res = sign == 0 ? res * 1 : res * -1;

           
            Console.WriteLine($"Register:         {Output(register)}");
            Console.WriteLine($"Int value:        {res}");

            register[0] = sign;
            return register;
        }

        public List<int> Shift(List<int> source)
        {
           
            int prev = source[0];
            int next = source[0];
            source[0] = 0;
            for (int i = 1; i < source.Count; i++)
            {

                next = source[i];
                source[i] = prev;
                prev = next;
            }
            return source;
        }
       
        public int ConvertToInt(List<int> helpCodeColl)
        {
            
            if (helpCodeColl[0] != 0)
            {
                helpCodeColl = Add(first: helpCodeColl, second: ConvertToBinary(-1));
                for (int index = 1; index < helpCodeColl.Count; index++)
                {
                    helpCodeColl[index] = (helpCodeColl[index] == 0 ? 1 : 0);
                }
            }

            int value = 0;
            for (int i = helpCodeColl.Count - 1; i >= 1; i--)
            {
                value += helpCodeColl[i] * (int)Math.Pow(2, helpCodeColl.Count - 1 - i);
            }
            return value;

        }
        public int ConvertRegisrtyToInt(List<int> helpCodeColl)
        {
           
            if (helpCodeColl[0] != 0)
            {
                helpCodeColl = WriteToRegister(ConvertToBinary(-1), helpCodeColl);
                for (int index = 1; index < helpCodeColl.Count; index++)
                {
                    helpCodeColl[index] = (helpCodeColl[index] == 0 ? 1 : 0);
                }
            }

            int value = 0;
            for (int i = helpCodeColl.Count - 1; i >= 1; i--)
            {
                value += helpCodeColl[i] * (int)Math.Pow(2, helpCodeColl.Count - 1 - i);
            }
            return value;

        }
        
    }

}