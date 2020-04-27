using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2015MajEmeltLatinTancok
{
    class Latin
    {
        public static List<Latin> Adat = new List<Latin>();

        public int Sorszam;
        public string Tanc;
        public string Lany;
        public string Fiu;

        public static string megadottTanc;

        public Latin(int sorszam, string tanc, string lany, string fiu)
        {
            this.Sorszam = sorszam;
            this.Tanc = tanc;
            this.Lany = lany;
            this.Fiu = fiu;
        }

        public static void ElsoFeladat(string fajl)
        {
            int sor = 0;

            using (StreamReader olvas = new StreamReader(fajl))
            {
                while (!olvas.EndOfStream)
                {
                    sor++;

                    string tanc = olvas.ReadLine();
                    string lany = olvas.ReadLine();
                    string fiu = olvas.ReadLine();

                    Latin latin = new Latin(sor, tanc, lany, fiu);

                    Adat.Add(latin);
                }
            }
        }

        public static void MasodikFeladat() => Console.WriteLine($"2. feladat: \nAz elsőként bemutatott tánc: {Adat.Select(x => x.Tanc).First()}\nAz utolsóként bemutatott tánc: {Adat.Select(x => x.Tanc).Last()}");

        public static void HarmadikFeladat() => Console.WriteLine($"\n3. feladat: \n{Adat.Where(x => x.Tanc == "samba").Count()} pár mutatta be a sambát");

        public static void NegyedikFeladat()
        {
            Console.WriteLine("\n4. feladat:");

            var vilmaTanc = Adat.Where(x => x.Lany == "Vilma").Select(x => x.Tanc).ToList();

            for (int i = 0; i < vilmaTanc.Count; i++)
            {
                Console.WriteLine(vilmaTanc[i]);
            }
        }

        public static void OtodikFeladat()
        {
            try
            {
                Console.Write("\n5. feladat: \nKérem a tánc nevét: ");
                megadottTanc = Console.ReadLine();

                var vilmaParja = Adat.Where(x => x.Tanc == megadottTanc).Where(x => x.Lany == "Vilma").Select(x => x.Fiu).First();

                Console.WriteLine($"A {megadottTanc} bemutatóján Vilma párja {vilmaParja} volt.");
            }
            catch (Exception) { Console.WriteLine($"Vilma nem táncolt {megadottTanc}-t."); }
        }

        public static void HatodikFeladat(string fajlNeve)
        {
            Console.WriteLine("\n6. feladat: \nFiú és lány táncosok kiíratva.");

            string rendF = "";
            string rendL = "";

            using (FileStream fajl = new FileStream(fajlNeve, FileMode.Create))
            using (StreamWriter ki = new StreamWriter(fajl, Encoding.UTF8))
            {
                var fiuk = Adat.Select(x => x.Fiu).Distinct().ToList();
                var lanyok = Adat.Select(x => x.Lany).Distinct().ToList();

                for (int i = 0; i < fiuk.Count; i++)
                {
                    rendF += String.Concat(", ", fiuk[i]);
                }

                for (int i = 0; i < lanyok.Count; i++)
                {
                    rendL += String.Concat(", ", lanyok[i]);
                }

                ki.WriteLine($"Lányok: {rendL.Substring(2, rendL.Length - 2)}\nFiúk: {rendF.Substring(2, rendF.Length - 2)}");
                ki.Flush();
            }
        }

        public static void HetedikFeladat()
        {
            Console.WriteLine("\n7. feladat:");

            var rendezesFiu = new SortedDictionary<string, int>();
            var rendezesLany = new SortedDictionary<string, int>();

            for (int i = 0; i < Adat.Count; i++)
            {
                if (rendezesFiu.ContainsKey(Adat[i].Fiu))
                    rendezesFiu[Adat[i].Fiu]++;
                else
                    rendezesFiu[Adat[i].Fiu] = 1;
            }

            for (int i = 0; i < Adat.Count; i++)
            {
                if (rendezesLany.ContainsKey(Adat[i].Lany))
                    rendezesLany[Adat[i].Lany]++;
                else
                    rendezesLany[Adat[i].Lany] = 1;
            }

            var legtobbFiu = rendezesFiu.OrderByDescending(x => x.Value).Select(x => new { Nev = x.Key, Hanyszor = x.Value}).First();
            var legtobbLany = rendezesLany.OrderByDescending(x => x.Value).Select(x => new { Nev = x.Key, Hanyszor = x.Value }).First();

            Console.WriteLine($"A fiúk közül a legtöbbször {legtobbFiu.Nev} szerepelt({legtobbFiu.Hanyszor}x).\nA lányok közül a legtöbbször {legtobbLany.Nev} szerepelt({legtobbLany.Hanyszor}x).");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Feladatok();

            Console.ReadKey();
        }

        private static void Feladatok()
        {
            Latin.ElsoFeladat(@"tancrend.txt");
            Latin.MasodikFeladat();
            Latin.HarmadikFeladat();
            Latin.NegyedikFeladat();
            Latin.OtodikFeladat();
            Latin.HatodikFeladat(@"szereplok.txt");
            Latin.HetedikFeladat();
        }
    }
}
