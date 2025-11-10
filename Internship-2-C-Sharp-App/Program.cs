namespace Internship_2_C_Sharp_App;

class Program
{
    static void Main(string[] args)
    {
        var korisnik = new Dictionary<int, Tuple<string, string,
            DateTime, Dictionary<int, Tuple<DateTime,double, double, double, double>>>>();

        var korisnik1PopisPutovanja = new Dictionary<int, Tuple<DateTime, double, double, double, double>>()
        {
            {1,Tuple.Create(new DateTime(2025, 10, 10), 220.0, 30.0, 1.49, 44.7)},
            {2,Tuple.Create(new DateTime(2025, 11, 10), 235.0, 35.0, 1.49, 52.15)}
        };

        var korisnik2PopisPutovanja = new Dictionary<int, Tuple<DateTime, double, double, double, double>>()
        {
            {3,Tuple.Create(new DateTime(2023, 03, 15), 220.0, 30.0, 1.49, 44.7)},
            {4,Tuple.Create(new DateTime(2023, 04, 16), 235.0, 35.0, 1.49, 52.15)},
            {5,Tuple.Create(new DateTime(2023, 02, 17), 50.0, 10.0, 1.20, 12.0)}
        };

        var korisnik3PopisPutovanja = new Dictionary<int, Tuple<DateTime, double, double, double, double>>()
        {
            { 6, Tuple.Create(new DateTime(2024, 03, 15), 100.0, 40.0, 1.20, 48.0) },
            { 7, Tuple.Create(new DateTime(2024, 12, 12), 120.0, 45.0, 1.20, 54.0) },
        };
        korisnik[1] = Tuple.Create("Dorian", "Leci",new DateTime(2004,03,28), korisnik1PopisPutovanja);
        korisnik[2] = Tuple.Create("Vesna", "Leci", new DateTime(1971, 01, 23), korisnik2PopisPutovanja);
        korisnik[3]=Tuple.Create("Damir","Leci",new DateTime(1969,05,18), korisnik3PopisPutovanja);
        
        GlavniIzbornik();
    }

    static void GlavniIzbornik()
    {
        while (true)
        {
            Console.WriteLine("1 - Korisnici\n2 - Putovanja\n0 - Izlaz iz aplikacije\n");
            if (int.TryParse(Console.ReadLine(), out int unosGlavniIzbornik ))
            {
                switch (unosGlavniIzbornik)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.izlaz iz aplikacije\n");
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine("Uspješan odabir menija za korisnike.\n");
                        KorisniciIzbornik();
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir menija za putovanja.\n");
                        PutovanjeIzbornik();
                        break;
                    default:
                        Console.WriteLine("Pogrešan unos broja.Pokušaj ponovno\n");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Pogrešan format unosa->mora biti integer\n");
            }
        }        
    }
    static void KorisniciIzbornik()
    {
        while (true)
        {
            Console.WriteLine("1 - Unos novog korisnika\n");
            Console.WriteLine("2 - Brisanje korisnika\n");
            Console.WriteLine("3 - Uređivanje korisnika\n");
            Console.WriteLine("4 - Pregled svih korisnika\n");
            Console.WriteLine("0 - Povratak na glavni izbornik\n");
            if (int.TryParse(Console.ReadLine(), out int unosKorisniciIzbornik))
            {
                switch (unosKorisniciIzbornik)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na glavni izbornik\n");
                        GlavniIzbornik();
                        return;
                    case 1:
                        Console.WriteLine("Uspješan odabir.Unos novog korisnika\n");
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir.Brisanje korisnika\n");
                        break;
                    case 3:
                        Console.WriteLine("Uspješan odabir.Uređivanje korisnika\n");
                        break;
                    case 4:
                        Console.WriteLine("Uspješan odabir.Pregled svih korisnika\n");
                        break;
                    default:
                        Console.WriteLine("Pogrešan odabir.Ponovno unesi\n");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Pogrešan format unosa->mora biti integer\n");
            }            
        }
    }

    static void PutovanjeIzbornik()
    {
        while (true)
        {
            Console.WriteLine("1 - Unos novog putovanja\n");
            Console.WriteLine("2 - Brisanje putovanja\n");
            Console.WriteLine("3 - Uređivanje postojećeg putovanja\n");
            Console.WriteLine("4 - Pregled svih putovanja\n");
            Console.WriteLine("5 - Izvještaji i analize\n");
            Console.WriteLine("0 - Povratak na glavni izbornik\n");
            if (int.TryParse(Console.ReadLine(), out int unosKorisniciIzbornik))
            {
                switch (unosKorisniciIzbornik)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na glavni izbornik\n");
                        GlavniIzbornik();
                        return;
                    case 1:
                        Console.WriteLine("Uspješan odabir.Unos novog putovanja\n");
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir.Brisanje putovanja\n");
                        break;
                    case 3:
                        Console.WriteLine("Uspješan odabir.Uređivanje postojećeg putovanja\n");
                        break;
                    case 4:
                        Console.WriteLine("Uspješan odabir.Pregled svih putovanja\n");
                        break;
                    case 5:
                        Console.WriteLine("Uspješan odabir.Izvještaji i analize\n");
                        break;
                    default:
                        Console.WriteLine("Pogrešan odabir.Ponovno unesi\n");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Pogrešan format unosa->mora biti integer\n");
            }   
        }        
    }


}
