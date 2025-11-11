using System.Globalization;

namespace Internship_2_C_Sharp_App;

using User = Dictionary<int, Tuple<string, string,
    DateOnly, Dictionary<int, Tuple<DateOnly, double, double, double, double>>>>;

using Trip=Dictionary<int, Tuple<DateOnly, double, double, double, double>>;

using UserDictValue=Tuple<string, string,
    DateOnly, Dictionary<int, Tuple<DateOnly, double, double, double, double>>>;
class Program
{
    static void Main(string[] args)
    {
        var user = new User();

        var user1TripList = new Trip()
        {
            {1,Tuple.Create(new DateOnly(2025, 10, 10), 220.0, 30.0, 1.49, 44.7)},
            {2,Tuple.Create(new DateOnly(2025, 11, 10), 235.0, 35.0, 1.49, 52.15)}
        };

        var user2TripList = new Trip()
        {
            {3,Tuple.Create(new DateOnly(2023, 03, 15), 220.0, 30.0, 1.49, 44.7)},
            {4,Tuple.Create(new DateOnly(2023, 04, 16), 235.0, 35.0, 1.49, 52.15)},
            {5,Tuple.Create(new DateOnly(2023, 02, 17), 50.0, 10.0, 1.20, 12.0)}
        };

        var user3TripList = new Trip()
        {
            { 6, Tuple.Create(new DateOnly(2024, 03, 15), 100.0, 40.0, 1.20, 48.0) },
            { 7, Tuple.Create(new DateOnly(2024, 12, 12), 120.0, 45.0, 1.20, 54.0) },
        };
        user[1] = Tuple.Create("Dorian", "Leci",new DateOnly(2004,03,28), user1TripList);
        user[2] = Tuple.Create("Vesna", "Leci", new DateOnly(1971, 01, 23), user2TripList);
        user[3]=Tuple.Create("Damir","Leci",new DateOnly(1969,05,18), user3TripList);
        
        MainMenu(user);
    }

    static void MainMenu(User userDict)
    {
        while (true)
        {
            Console.WriteLine("1 - Korisnici\n2 - Putovanja\n0 - Izlaz iz aplikacije\n");
            if (int.TryParse(Console.ReadLine(), out int inputMainMenu))
            {
                switch (inputMainMenu)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.izlaz iz aplikacije\n");
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.WriteLine("Uspješan odabir menija za korisnike.\n");
                        UserMenu(userDict);
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir menija za putovanja.\n");
                        TripMenu(userDict);
                        break;
                    default:
                        Console.WriteLine("Unos nije među ponuđenima.Unesi ponovno");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPogrešan tip podatka->unesi cijeli broj.");
            }
        }        
    }
    static void UserMenu(User userDict)
    {
        while (true)
        {
            Console.WriteLine("1 - Unos novog korisnika\n");
            Console.WriteLine("2 - Brisanje korisnika\n");
            Console.WriteLine("3 - Uređivanje korisnika\n");
            Console.WriteLine("4 - Pregled svih korisnika\n");
            Console.WriteLine("0 - Povratak na glavni izbornik\n");
            if (int.TryParse(Console.ReadLine(), out int inputUserMenu))
            {
                switch (inputUserMenu)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na glavni izbornik\n");
                        MainMenu(userDict);
                        return;
                    case 1:
                        Console.WriteLine("Uspješan odabir.Unos novog korisnika\n");
                        NewUserInput(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir.Brisanje korisnika.\n");
                        UserDelete(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 3:
                        Console.WriteLine("Uspješan odabir.Uređivanje korisnika.\n");
                        break;
                    case 4:
                        Console.WriteLine("Uspješan odabir.Pregled svih korisnika.\n");
                        FormatedOutputType(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    default:
                        Console.WriteLine("Unos nije među ponuđenima.Unesi ponovno");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPogrešan tip podatka->unesi cijeli broj.");
            }            
        }
    }

    static void TripMenu(User userDict)
    {
        while (true)
        {
            Console.WriteLine("1 - Unos novog putovanja\n");
            Console.WriteLine("2 - Brisanje putovanja\n");
            Console.WriteLine("3 - Uređivanje postojećeg putovanja\n");
            Console.WriteLine("4 - Pregled svih putovanja\n");
            Console.WriteLine("5 - Izvještaji i analize\n");
            Console.WriteLine("0 - Povratak na glavni izbornik\n");
            if (int.TryParse(Console.ReadLine(), out int inputTripMenu))
            {
                switch (inputTripMenu)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na glavni izbornik.");
                        MainMenu(userDict);
                        return;
                    case 1:
                        Console.WriteLine("Uspješan odabir.Unos novog putovanja.");
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir.Brisanje putovanja.");
                        break;
                    case 3:
                        Console.WriteLine("Uspješan odabir.Uređivanje postojećeg putovanja");
                        break;
                    case 4:
                        Console.WriteLine("Uspješan odabir.Pregled svih putovanja");
                        break;
                    case 5:
                        Console.WriteLine("Uspješan odabir.Izvještaji i analize");
                        break;
                    default:
                        Console.WriteLine("\nUnos nije među ponuđenima.Unesi ponovno.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPogrešan tip podatka->unesi cijeli broj.");
            }   
        }        
    }

    static void NewUserInput(User userDict)
    {
        int id = IdInput(userDict);
        string name = StringInput("ime");
        string surname= StringInput("prezime");
        DateOnly birthDate = BirthDateInput();
        userDict[id] = Tuple.Create(name, surname,birthDate,new Trip());
        
    }

    static int IdInput(User userDict)
    {
        while (true)
        {
            Console.WriteLine("Unesi jedinstveni id korisnika");
            if (int.TryParse(Console.ReadLine(), out int inputId) && inputId>=0 && !userDict.ContainsKey(inputId))
                return inputId;
            
            if(inputId<0)
                Console.WriteLine("\nId ne smije biti negativan broj.");
            else if(userDict.ContainsKey(inputId))
                Console.WriteLine("\nId mora biti jedinstven.");
            else
            {
                Console.WriteLine("\nPogrešan tip podatka-> id mora biti cijeli broj");
            }
        }
    }

    static string StringInput(string nameSurnameOutput)
    {
        while (true)
        {
            Console.WriteLine("Unesi {0} korisnika",nameSurnameOutput);
            string? inputNameSurname = Console.ReadLine();
            if (!string.IsNullOrEmpty(inputNameSurname) && inputNameSurname.All(stringChar => char.IsLetter(stringChar)))
            {
                string inputNameSurnameUpper=char.ToUpper(inputNameSurname[0])+inputNameSurname.Substring(1).ToLower();
                return inputNameSurnameUpper;
            }

            Console.WriteLine("\nPogrešan unos {0}na .Ne smije biti prazno ili sadržavati brojeve/specijalne znakove",nameSurnameOutput);
        }       
    }

    static DateOnly BirthDateInput()
    {
        while (true)
        {
            Console.WriteLine("Unesi datum rođenja (YYYY-MM-DD)");
            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly inputBirthDate))
                return inputBirthDate;
            Console.WriteLine("\nPogrešan unos datuma rođenja.");
        }               
    }

    static void UserDelete(User userDict)
    {
        while (true)
        {
            Console.WriteLine("0 - Povratak na KorisnickiIzbornik");
            Console.WriteLine("1 - Brisanje korisnika po id-u");
            Console.WriteLine("2 - Brisanje korisnika po imenu i prezimenu"); 

            if (int.TryParse(Console.ReadLine(),out int inputNumber))
            {
                switch (inputNumber)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na korisnicki izbornik\n");
                        UserMenu(userDict);
                        return;
                    case 1:
                        UserDeleteById(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 2:
                        YearTresholdOutput(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    default:
                        Console.WriteLine("\nUnos nije među ponuđenima.Unesi ponovno.");
                        break;
                }
            }   
            else Console.WriteLine("\nPogrešan tip podatka->unesi cijeli broj.");
        }
    }

    static void UserDeleteById(User userDict)
    {
        while (true)
        {
            Console.WriteLine("Unesi id korisnika kojeg želiš obrisati");
            if (int.TryParse(Console.ReadLine(), out int inputId) && userDict.ContainsKey(inputId))
            {
                if(userDict.Remove(inputId))
                    Console.WriteLine("Uspješno brisanje korisnika.\n");
                return;
            }
            else Console.WriteLine("\nId korisnika je u krivom formatu ili nije pronađen.");
                

        }
    }
    static void AlphabetSortedOutput(User userDict)
    {
        var dictSorted=userDict.OrderBy(kvPar=>kvPar.Value.Item2);
        foreach (var kvPair in dictSorted)
        {
            FormatedOutput(kvPair);
        }
    }
    static void FormatedOutputType(User userDict)
    {
        while (true)
        {
            Console.WriteLine("0- povratak na KorisnickiIzbornik");
            Console.WriteLine("1- Ispis korisnika sortiranih abecedno po prezimenu");
            Console.WriteLine("2 - Ispis korisnika starijih od 20 godina"); 
            Console.WriteLine("3- Ispis korisnika koji imaju bar 2 putovanja");
            if (int.TryParse(Console.ReadLine(),out int inputNumber))
            {
                switch (inputNumber)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na korisnicki izbornik izbornik\n");
                        UserMenu(userDict);
                        return;
                    case 1:
                        AlphabetSortedOutput(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 2:
                        YearTresholdOutput(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 3:
                        TripTrehsoldOutput(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    default:
                        Console.WriteLine("\nUnos nije među ponuđenima.Unesi ponovno.");
                        break;
                }
            }   
            else Console.WriteLine("\nPogrešan tip podatka->unesi cijeli broj.");
        }
        
    }
    static void YearTresholdOutput(User userDict)
    {
        Console.WriteLine("Ispis korisnika starijih od 20 godina\n");
        foreach (var user in userDict)
        {
  
            var birthDate = user.Value.Item3;
            var birthDateTime = birthDate.ToDateTime(new TimeOnly()).Date;
            var newestDate = DateTime.Today;
            int age = newestDate.Year-birthDate.Year;
            
            if (birthDateTime> newestDate.AddYears(-age))
                age--;
            
            if (age > 20)
                FormatedOutput(user);         
        }
    }
    
    static void TripTrehsoldOutput(User userDict)
    {
        foreach (var user in userDict )
        {
            if (user.Value.Item4.Count() >=2)
                FormatedOutput(user);
        }   
    }

    static void FormatedOutput(KeyValuePair<int,UserDictValue> dictElement)
    {
        Console.WriteLine("{0} - {1} - {2} - {3}\n",dictElement.Key,dictElement.Value.Item1,dictElement.Value.Item2,dictElement.Value.Item3);           
    }
}
