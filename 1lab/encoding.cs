using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Encoder
{
    class Program
    {
        static void Main(string[] args)
        {
            string address = @"D:\CS\Texts\";

            string[] nameOfFile = new string[] {"fet.txt", "heart.txt", "zelen.txt" };

            char[] alph = new char[33] {'а', 'б', 'в', 'г', 'ґ', 'д', 'е', 'є', 'ж', 'з', 'и',
        'і', 'ї', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с',
        'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ю', 'я' };

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            for (int f = 0; f < nameOfFile.Length; f++)
            {
                Console.WriteLine("{0} encoded with the use of Base64\n", nameOfFile[f]);

                string text = System.IO.File.ReadAllText(address + nameOfFile[f], Encoding.UTF8);
                byte[] arr = Encoding.UTF8.GetBytes(text);

                MyBase64 encoder = new MyBase64(arr);

                Console.WriteLine(encoder.Encode());
                Console.WriteLine();
                Console.ReadKey();
            }
        }

        public class MyBase64
        {
            byte[] source;
            int length1, length2;
            int block;
            int padding;
            public MyBase64(byte[] arr)
            {
                source = arr;
                length1 = arr.Length;
                if ((length1 % 3) == 0)
                {
                    padding = 0;
                    block = length1 / 3;
                }
                else
                {
                    padding = 3 - (length1 % 3);
                    block = (length1 + padding) / 3;
                }
                length2 = length1 + padding;
            }

            public char[] Encode()
            {
                byte[] source2;
                source2 = new byte[length2];
                for (int x = 0; x < length2; x++)
                {
                    if (x < length1)
                    {
                        source2[x] = source[x];
                    }
                    else
                    {
                        source2[x] = 0;
                    }
                }

                byte byte1, byte2, byte3;
                byte temp0, temp1, temp2, temp3, temp4;
                byte[] stack = new byte[block * 4];
                char[] result = new char[block * 4];
                for (int x = 0; x < block; x++)
                {
                    byte1 = source2[x * 3];
                    byte2 = source2[x * 3 + 1];
                    byte3 = source2[x * 3 + 2];

                    temp1 = (byte)((byte1 & 252) >> 2);
                    temp0 = (byte)((byte1 & 3) << 4);
                    temp2 = (byte)((byte2 & 240) >> 4);
                    temp2 += temp0;
                    temp0 = (byte)((byte2 & 15) << 2);
                    temp3 = (byte)((byte3 & 192) >> 6);
                    temp3 += temp0;
                    temp4 = (byte)(byte3 & 63);

                    stack[x * 4] = temp1;
                    stack[x * 4 + 1] = temp2;
                    stack[x * 4 + 2] = temp3;
                    stack[x * 4 + 3] = temp4;
                }
                for (int x = 0; x < block * 4; x++)
                {
                    result[x] = toResult(stack[x]);
                }
                switch (padding)
                {
                    case 0: break;
                    case 1: result[block * 4 - 1] = '='; break;
                    case 2:
                        result[block * 4 - 1] = '=';
                        result[block * 4 - 2] = '=';
                        break;
                    default: break;
                }
                return result;
            }

            private char toResult(byte b)
            {
                char[] s = new char[64]
                    {  'A','B','C','D','E','F','G','H','I','J','K','L','M',
            'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m',
            'n','o','p','q','r','s','t','u','v','w','x','y','z',
            '0','1','2','3','4','5','6','7','8','9','+','/'};
                if ((b >= 0) && (b <= 63))
                {
                    return s[(int)b];
                }
                else
                {
                    return ' ';
                }
            }
        }
    }
}
