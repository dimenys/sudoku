using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SudokuCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Feladvany> feladvanyok = BeolvasFeladvanyok("feladvanyok.txt");
            if (feladvanyok.Count == 0)
            {
                Console.WriteLine("Nincsenek feladványok a fájlban.");
                return;
            }

            Console.WriteLine($"3. Feladat: Beolvasva: {feladvanyok.Count} feladvány");

            int meret;
            do
            {
                Console.Write("4. Feladat: Kérem a feladvány méretét [4..9]:");
                meret = int.Parse(Console.ReadLine());
            } while (meret < 4 || meret > 9);

            int meretAlapjan = feladvanyok.Count(f => f.Meret == meret);
            if (meretAlapjan == 0)
            {
                Console.WriteLine($"Nincsenek {meret}-es méretű feladványok a forrásállományban.");
            }
            else
            {
                Console.WriteLine($"{meret}x{meret} méretű feladványból {meretAlapjan} darab van tárolva");
                List<Feladvany> meretSzerintiFeladvanyok = feladvanyok.Where(f => f.Meret == meret).ToList();
                Random random = new Random();
                int randomIndex = random.Next(0, meretSzerintiFeladvanyok.Count);
                Feladvany randomFeladvany = meretSzerintiFeladvanyok[randomIndex];

                Console.WriteLine("5. Feladat: A kiválasztott feladvány:");
                

                double kitoltottseg = (double)randomFeladvany.Kezdo.Count(c => c != '0') / randomFeladvany.Kezdo.Length * 100;
                Console.WriteLine($"6. Feladat: A feladvány kitöltöttsége: {Math.Round(kitoltottseg)}%");
                Console.WriteLine($"7. Feladat: A feladvány kirajzolva: ");
                randomFeladvany.Kirajzol();
                

                KiirFeladvanyFajlba($"sudoku{meret}.txt", randomFeladvany.Kezdo);
            }

            Console.ReadKey();
        }

        static List<Feladvany> BeolvasFeladvanyok(string fajlnev)
        {
            List<Feladvany> feladvanyok = new List<Feladvany>();
            try
            {
                string[] sorok = File.ReadAllLines(fajlnev);
                foreach (string sor in sorok)
                {
                    if (!string.IsNullOrEmpty(sor))
                    {
                        Feladvany feladvany = new Feladvany(sor);
                        feladvanyok.Add(feladvany);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Hiba a fájl olvasása során: {e.Message}");
            }
            return feladvanyok;
        }

        static void KiirFeladvanyFajlba(string fajlnev, string feladvany)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fajlnev))
                {
                    writer.WriteLine(feladvany);
                }
                Console.WriteLine($"8.Feladat: {fajlnev} állomány {feladvany.Count()} darab feladvánnyal létrehozva");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Hiba a fájl írása során: {e.Message}");
            }
            Console.ReadKey();
        }
    }
}
