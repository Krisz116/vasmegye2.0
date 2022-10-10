using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace vasmegye2._0
{
    class Program
    {
        static List<SzemelyiSzam> szemelyszamok = new List<SzemelyiSzam>();
        static void Main(string[] args)
        {
            Console.WriteLine("2.feladt Adatok beolvasása, tárolása");
            adatokBeolvasasa("vas.txt");

            Console.WriteLine("\n 4.feladat: Ellenörzés");
            feladat4();
            Console.WriteLine($"\n 5.feladat: Vas megyében a vizsgált évek alatt {szemelyszamok.Count} csecsemő született");
            Console.WriteLine($"\n 6.feladat: Fiúk száma: {szemelyszamok.FindAll(a => a.szam[0] == '1' || a.szam[0] == '3').Count}");
            Console.WriteLine($"\n 7 feladat: Vizsgált időszak {szemelyszamok.Min(a => a.evSzam())} - {szemelyszamok.Max(a => a.evSzam())}");
            if (szokoEvbenSzuletett())
            {
                Console.WriteLine("(8.feladat: Szökőnapon született baba");
            }
            else
            {
                Console.WriteLine("(8.feladat: Szökőnapon nem született baba");
            }
            feladat9();


            Console.WriteLine("\n Program vége");

            Console.ReadKey();
        }

        private static void feladat9()
        {
            Console.WriteLine("9.feladat: Statisztika");
            var statisztika = szemelyszamok.GroupBy(a => a.evSzam()).Select(b => new { ev = b.Key, fo = b.Count()});
            foreach (var item in statisztika)
            {
                Console.WriteLine($"\t{item.ev} - {item.fo} fő");
            }
        }

        private static bool szokoEvbenSzuletett()
        {
            var szokoEvi = szemelyszamok.Find(a => a.evSzam() % 4 == 0 && a.szam.Substring(4,4).Equals("0224"));
            return szokoEvi != null;
        }

        private static void feladat4()
        {
            List<SzemelyiSzam> hibasSzamok = szemelyszamok.FindAll(a => !CdvEll(a.szam) );
            foreach (SzemelyiSzam item in hibasSzamok)
            {
                Console.WriteLine($"Hibás a {item.szam} személyi azonosíto!");
                szemelyszamok.Remove(item);
            }
        }

        public static bool CdvEll(string szam) 
        {
            string szamNumeric = new string(szam.Where(a => char.IsDigit(a)).ToArray());
            if (szamNumeric.Length != 11)
            {
                return false;
            }
            double szum = 0;
            for (int i = 0; i < szamNumeric.Length -1; i++)
            {
                szum += char.GetNumericValue(szamNumeric[i]) *(10 -i);

            }
            return char.GetNumericValue(szamNumeric[10] )== szum % 11;
        }

        private static void adatokBeolvasasa(string adatFile)
        {
            if (!File.Exists(adatFile))
            {
                Console.WriteLine("A forrás adatok hiányoznak!");
                Console.ReadLine();
                Environment.Exit(0);
            }
            using (StreamReader sr = new StreamReader(adatFile) )
            {
                while (!sr.EndOfStream)
                {
                    szemelyszamok.Add(new SzemelyiSzam(sr.ReadLine()));
                }
                
            }
        }
    }
}
