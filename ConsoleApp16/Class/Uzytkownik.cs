using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16.Class
{
    public class Uzytkownik : Szyfrowanie
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Login { get { return Imie + "_" + Nazwisko; } set { Login = Imie + "_" + Nazwisko; } }
        public string Haslo { get { Random rnd = new Random(); return Imie.Substring(0,3) + Convert.ToString(rnd.Next(100000, 999999)); } set { Random rnd = new Random(); Haslo = Imie[0] + Imie[1] + Imie[2] + Convert.ToString(rnd.Next(100000, 999999)); } }

        public Uzytkownik(string imie, string nazwisko) {
            Imie = imie;
            Nazwisko = nazwisko;
        }

        public string UkryjLogin()
        {
            return ZaszyfrujGADERYPOLUKI(Login);
        }
        public string UkryjHaslo()
        {
            return ZaszyfrujGADERYPOLUKI(Haslo);
        }
        public string PokazLogin()
        {
            return OdszyfrujGADERYPOLUKI(Login);
        }
        public string PokazHaslo()
        {
            return OdszyfrujGADERYPOLUKI(Haslo);
        }


        public void ZmienLogin()
        {
            Console.WriteLine("\nPodaj stary login:");
            string staryLogin = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Podaj nowy login:");
            string nowyLogin = Convert.ToString(Console.ReadLine());
            if (Login == staryLogin)
                Login = nowyLogin;
        }
        public void ZmienHaslo()
        {
            Console.WriteLine("\nPodaj stare hasło:");
            string stareHaslo = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Podaj nowe hasło:");
            string noweHaslo = Convert.ToString(Console.ReadLine());
            if (Haslo == stareHaslo)
                Haslo = noweHaslo;
        }
    }
}
