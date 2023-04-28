using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16.Class
{
    public class Student : Uzytkownik
    {
        public int Rocznik { get; set; }
        public Student(string imie, string nazwisko, int rocznik) : base(imie, nazwisko)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            Rocznik = rocznik;
        }
        public override string ToString()
        {
            return $"Login: {Login} \nHasło: {ZaszyfrujGADERYPOLUKI(Haslo)}";
        }
        public override bool Equals(Object obj)
        {
            if (!(obj is Student)) 
                return false; 
            return true;
        }
    }
}
