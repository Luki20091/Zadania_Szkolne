using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp16.Class
{
    public class Szyfrowanie
    {
        public static string ZaszyfrujGADERYPOLUKI(string text)
        {
            string szyfr = "GADERYPOLUKI";
            string f = "";
            text = text.ToUpper();
            for (int a = 0; a < text.Length; a++)
            {
                for (int b = 0; b < szyfr.Length; b++)
                {
                    if (text[a] == szyfr[b])
                    {
                        if (b % 2 == 0)
                        {
                            f += szyfr[b + 1];
                        }
                        else
                        {
                            f += szyfr[b - 1];
                        };
                    };
                };
                if (f.Length - a == 0)
                {
                    f += text[a];
                };
            };
            return f;
        }




        public string OdszyfrujGADERYPOLUKI(string text)
        {
            return ZaszyfrujGADERYPOLUKI(text);
        }
    }

}
