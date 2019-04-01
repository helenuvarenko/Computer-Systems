using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisionShiftLeft
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose first number:");
            string num1 = Console.ReadLine();

            Console.WriteLine("Choose second number:");
            string num2 = Console.ReadLine();
            Console.WriteLine();

            Division(num1, num2);

            Console.ReadKey();
        }

        static string AddZerosToString(string val)
        {
            int count = 64 - val.Length;
            string head = "";
            for (int i = 0; i < count; ++i)
                head += "0";
            return head + val;
        }

        public static void Division(string num1, string num2)
        {
            Int64 divider, refresherAndQuotient;
            try
            {
                divider = Int32.Parse(num1);
                refresherAndQuotient = Int32.Parse(num2);
            }
            catch
            {
                Console.WriteLine("Chosen numbers are not int32 type of data.");
                return;
            }
            divider <<= 32;

            bool setRemLSBToOne = false;
            for (int i = 0; i <= 32; ++i)
            {
                Console.WriteLine("Move №" + (i + 1) + ":");

                Console.Write("Divider is ");
                if (divider <= refresherAndQuotient)
                {
                    refresherAndQuotient -= divider;
                    setRemLSBToOne = true;
                    Console.Write("less");
                }
                else
                    Console.Write("more ");

                Console.WriteLine("than Refresher.");
                
                refresherAndQuotient <<= 1;

                if (setRemLSBToOne)
                {
                    setRemLSBToOne = false;
                    refresherAndQuotient |= 1; 
                    Console.WriteLine("Set refresher lsb to 1");
                }
                Console.WriteLine();

                Console.WriteLine("Divider:                     " + AddZerosToString(Convert.ToString(divider, 2)) +
                    "\nRefresher and quotient:      " + AddZerosToString(Convert.ToString(refresherAndQuotient, 2)) + "\n");
            }
            long quotient = refresherAndQuotient & ((long)Math.Pow(2, 33) - 1);
            long remainder = refresherAndQuotient >> 33;
            Console.WriteLine("Quotient:        " + AddZerosToString(Convert.ToString(quotient, 2)) +
                " ( " + quotient + " )\n");

            Console.WriteLine("Refresher:       " + AddZerosToString(Convert.ToString(remainder, 2)) +
               " ( " + remainder + " )");
        }       
    }
}
