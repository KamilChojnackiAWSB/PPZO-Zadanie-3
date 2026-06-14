using System;
using System.Collections.Generic;

abstract class Pojazd // Klasa abstrakcyjna
{
    public string NumerRejestracyjny { get; private set; }

    public string Marka { get; private set; }

    public bool CzyZaparkowany { get; protected set; }

    public Pojazd(string numer, string marka)
    {
        NumerRejestracyjny = numer.ToUpper(); // Nr rej z dużej
        Marka = marka;
        CzyZaparkowany = false;
    }

    public virtual void Zaparkuj()
    {
        CzyZaparkowany = true;
    }

    public virtual void Odjedz()
    {
        CzyZaparkowany = false;
    }

    public abstract string TypPojazdu(); // Rózna metoda dla róznej klasy
}


class Samochod : Pojazd // Klasa pochodna, dziedziczenie
{
    public Samochod(string nr, string marka) : base(nr, marka) { }

    public override string TypPojazdu()
    {
        return " Samochód";
    }
}

class Motocykl : Pojazd 
{
    public Motocykl(string nr, string marka) : base(nr, marka) { }

    public override string TypPojazdu()
    {
        return " Motocykl";
    }
}

class Parking
{
    // słownik przechowujący strefy parkingu
    private Dictionary<string, List<Pojazd>> miejsca;
    private int limit = 20;

    public Parking()
    {
        miejsca = new Dictionary<string, List<Pojazd>>()
        {
            { "P1-W", new List<Pojazd>() },
            { "P1-Z", new List<Pojazd>() },
            { "P2-W", new List<Pojazd>() },
            { "P2-Z", new List<Pojazd>() }
        };

        DodajStartowePojazdy();
    }

    private void DodajStartowePojazdy()
    {
        miejsca["P1-W"].Add(new Samochod("KR1D34", "BMW"));
        miejsca["P1-W"].Add(new Motocykl("WADA78", "Yamaha"));
        miejsca["P1-Z"].Add(new Samochod("GD1D11", "Audi"));
        miejsca["P2-W"].Add(new Motocykl("PO2DF2", "Honda"));
        miejsca["P2-Z"].Add(new Samochod("SL3GP3", "Toyota"));

        // ustawienie wszystkich jako zaparkowanych
        foreach (var strefa in miejsca)
        {
            foreach (var p in strefa.Value)
            {
                p.Zaparkuj();
            }
        }
    }

    public int WolneMiejsca(string strefa)
    {
        return limit - miejsca[strefa].Count;
    }


    public void DodajPojazd(Pojazd p, string miejsce)
    {
        foreach (var strefa in miejsca)
        {
            if (strefa.Value.Exists(x => x.NumerRejestracyjny == p.NumerRejestracyjny))
            {
                Console.WriteLine(" Pojazd o tym numerze rej już istnieje! Dodawanie przerwane");

                Console.WriteLine("\n Wciśnij dowolny klawisz aby kontynuować ...");
                Console.ReadKey();
                return;
            }
        }

        if (miejsca[miejsce].Count >= limit)
        {
            Console.WriteLine(" Brak wolnych miejsc!");

            Console.WriteLine("\n Wciśnij dowolny klawisz aby kontynuować ...");
            Console.ReadKey();
            return;
        }

        p.Zaparkuj();
        miejsca[miejsce].Add(p);

        Console.WriteLine("\n Zaparkowano: " + p.NumerRejestracyjny + " na " + miejsce);

        Console.WriteLine("\n Wciśnij dowolny klawisz aby kontynuować ...");
        Console.ReadKey();
    }

    public void UsunPojazd(string numer)
    {
        numer = numer.ToUpper();

        foreach (var strefa in miejsca)
        {
            var pojazd = strefa.Value.Find(p => p.NumerRejestracyjny == numer);

            if (pojazd != null)
            {
                pojazd.Odjedz();
                strefa.Value.Remove(pojazd);

                Console.WriteLine(" Usunięto pojazd.");

                Console.WriteLine("\n Wciśnij dowolny klawisz aby kontynuować ...");
                Console.ReadKey();
                return;
            }
        }

        Console.WriteLine(" Nie znaleziono pojazdu.");
        Console.WriteLine("\n Wciśnij dowolny klawisz aby kontynuować ...");
        Console.ReadKey();
    }

    public void PokazPojazdy()
    {
        Console.Clear();
        Console.WriteLine("\n--- Parking ---");

        foreach (var strefa in miejsca)
        {
            Console.WriteLine($" \n{strefa.Key} | Ilość: {strefa.Value.Count} / 20");

            if (strefa.Value.Count == 0)
            {
                Console.WriteLine(" Brak pojazdów.");
                continue;
            }

            int i = 1;

            foreach (var p in strefa.Value)
            {
                Console.WriteLine($" {i}. {p.NumerRejestracyjny} - {p.Marka} {p.TypPojazdu()}");
                i++;
            }
        }

        Console.WriteLine("\n Wciśnij dowolny klawisz aby kontynuować ...");
        Console.ReadKey();
    }
}

class Program
{
    static void Main()
    {
        Parking parking = new Parking();
        bool dziala = true;

        while (dziala)
        {
            Console.Clear();
            Console.WriteLine(" Kamil Chojnacki nr albumu 68261 \n");

            Console.WriteLine("\n --- MENU ---");
            Console.WriteLine(" 1. Zaparkuj pojazd");
            Console.WriteLine(" 2. Usuń pojazd");
            Console.WriteLine(" 3. Pokaż parking");
            Console.WriteLine(" 0. Zamknij program");

            Console.Write("--> ");
            string wybor = Console.ReadLine();

            switch (wybor)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine(" Typ pojazdu:");
                    Console.WriteLine(" 1. Samochód");
                    Console.WriteLine(" 2. Motocykl");

                    Console.Write("--> ");
                    string typ = Console.ReadLine();

                    Console.Write(" \n Numer rejestracyjny: ");
                    string nr = Console.ReadLine();

                    Console.Write(" Marka: ");
                    string marka = Console.ReadLine();

                    Console.WriteLine(" \n Wybierz miejsce:");
                    Console.WriteLine($" 1 - Pietro 1 - Wschód (wolne: {parking.WolneMiejsca("P1-W")})");
                    Console.WriteLine($" 2 - Pietro 1 - Zachód (wolne: {parking.WolneMiejsca("P1-Z")})");
                    Console.WriteLine($" 3 - Pietro 2 - Wschód (wolne: {parking.WolneMiejsca("P2-W")})");
                    Console.WriteLine($" 4 - Piętro 2 - Zachód (wolne: {parking.WolneMiejsca("P2-Z")})");

                    Console.Write("--> ");
                    string wyborMiejsca = Console.ReadLine();
                    string miejsce = "";

                    switch (wyborMiejsca)
                    {
                        case "1": miejsce = "P1-W"; break;
                        case "2": miejsce = "P1-Z"; break;
                        case "3": miejsce = "P2-W"; break;
                        case "4": miejsce = "P2-Z"; break;
                        default:
                            Console.WriteLine(" Błędny wybór!");
                            break;
                    }

                    if (miejsce == "")
                        break;
                    if (typ == "1")
                        parking.DodajPojazd(new Samochod(nr, marka), miejsce);
                    else if (typ == "2")
                        parking.DodajPojazd(new Motocykl(nr, marka), miejsce);
                    else
                        Console.WriteLine(" Błędny typ.");

                    break;

                case "2":
                    Console.Clear();
                    Console.Write(" Podaj numer rejestracyjny: ");
                    string nrUsun = Console.ReadLine();
                    parking.UsunPojazd(nrUsun);
                    break;

                case "3":
                    parking.PokazPojazdy();
                    break;

                case "0":
                    dziala = false;
                    break;

                default:
                    Console.WriteLine(" Błędna opcja.");
                    break;
            }
        }
    }
}