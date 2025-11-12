using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;

namespace Internship_2_C_Sharp_App;

using User = Dictionary<int, Tuple<string, string,
    DateOnly, Dictionary<int, Tuple<DateOnly, double, double, double, double>>>>;

using Trip=Dictionary<int, Tuple<DateOnly, double, double, double, double>>;

using UserDictValue=Tuple<string, string,
    DateOnly, Dictionary<int, Tuple<DateOnly, double, double, double, double>>>;

using TripValue=Tuple<DateOnly,double,double,double,double>;
class Program
{
    public static List<Tuple<int,TripValue>> GlobalTripList = new List<Tuple<int,TripValue>>();
    static void Main(string[] args)
    {

        var user = DataSeed();
        MainMenu(user);
    }

    static User DataSeed()
    {
        var user = new User();
        var user1TripList = new Trip()
        {
            { 1, Tuple.Create(new DateOnly(2025, 10, 10), 220.0, 30.0, 1.49, 44.7) },
            { 2, Tuple.Create(new DateOnly(2025, 11, 10), 235.0, 35.0, 1.49, 52.15) }
        };
        
        var user2TripList = new Trip()
        {
            { 3, Tuple.Create(new DateOnly(2023, 03, 15), 220.0, 30.0, 1.49, 44.7) },
            { 4, Tuple.Create(new DateOnly(2023, 04, 16), 235.0, 35.0, 1.49, 52.15) },
            { 5, Tuple.Create(new DateOnly(2023, 02, 17), 50.0, 10.0, 1.20, 12.0) }
        };

        var user3TripList = new Trip()
        {
            { 6, Tuple.Create(new DateOnly(2024, 03, 15), 100.0, 40.0, 1.20, 48.0) },
            { 7, Tuple.Create(new DateOnly(2024, 12, 12), 120.0, 45.0, 1.20, 54.0) },
        };
        user[1] = Tuple.Create("Dorian", "Leci", new DateOnly(2004, 03, 28), user1TripList);
        user[2] = Tuple.Create("Vesna", "Leci", new DateOnly(1971, 01, 23), user2TripList);
        user[3] = Tuple.Create("Damir", "Leci", new DateOnly(1969, 05, 18), user3TripList);
        
        AddToTripList(user1TripList);
        AddToTripList(user2TripList);
        AddToTripList(user3TripList);

        return user;
    }

    static void AddToTripList(Trip userTripList)
    {
        foreach (var trip in userTripList)
        {
            GlobalTripList.Add(Tuple.Create(trip.Key,trip.Value));       
        }      
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
                        ModifyUser(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
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

    static void NewUserInput(User userDict)
    {
        int id = IdInput(userDict);
        var addedInfo = AddUserInfo(userDict, id);
        userDict[id] = addedInfo;
        Console.WriteLine("\nUspješno dodan novi korisnik.");
    }

    static Tuple<string, string, DateOnly, Trip> AddUserInfo(User userDict, int id)
    {
        string name = StringInput("ime");
        string surname = StringInput("prezime");
        DateOnly birthDate = BirthDateInput();
        return Tuple.Create(name, surname, birthDate, new Trip());
    }

    static int IdInput(User userDict)
    {
        while (true)
        {
            Console.WriteLine("Unesi jedinstveni id korisnika");
            if (int.TryParse(Console.ReadLine(), out int inputId) && inputId >= 0 && !userDict.ContainsKey(inputId))
                return inputId;

            if (inputId < 0)
                Console.WriteLine("\nId ne smije biti negativan broj.");
            else if (userDict.ContainsKey(inputId))
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
            Console.WriteLine("Unesi {0} korisnika", nameSurnameOutput);
            string? inputNameSurname = Console.ReadLine();
            if (!string.IsNullOrEmpty(inputNameSurname) &&
                RemoveWhiteSpace(inputNameSurname).All(stringChar => char.IsLetter(stringChar)))
            {
                inputNameSurname = inputNameSurname.Trim();
                string[] inputArr = inputNameSurname.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int i = 0;
                foreach (var nameSurname in inputArr)
                {
                    inputArr[i] = char.ToUpper(nameSurname[0]) + nameSurname.Substring(1).ToLower();
                    if (i != (inputArr.Length - 1))
                        inputArr[i] += " ";
                    i++;
                }

                return string.Concat(inputArr);
            }

            Console.WriteLine("\nPogrešan unos {0}na .Ne smije biti prazno ili sadržavati brojeve/specijalne znakove",
                nameSurnameOutput);
        }
    }

    static string RemoveWhiteSpace(string target)
    {
        return string.Concat(target.Where(c => !char.IsWhiteSpace(c)));
    }

    static DateOnly BirthDateInput()
    {
        while (true)
        {
            Console.WriteLine("Unesi datum rođenja (YYYY-MM-DD)");
            var bdateChecked = DateCheck();
            if (bdateChecked != DateOnly.MaxValue)
                return bdateChecked;

            Console.WriteLine("\nPogrešan unos datuma rođenja.");
        }
    }

    static DateOnly DateCheck()
    {
        if (DateOnly.TryParse(Console.ReadLine(), out DateOnly inputBirthDate) &&
            inputBirthDate.ToDateTime(new TimeOnly()).Date <= DateTime.Now.Date)
            return inputBirthDate;
        return DateOnly.MaxValue;
    }

    static void UserDelete(User userDict)
    {
        while (true)
        {
            Console.WriteLine("1 - Brisanje korisnika po id-u");
            Console.WriteLine("2 - Brisanje korisnika po imenu i prezimenu");
            Console.WriteLine("0 - Povratak na korisnicki izbornik");

            if (int.TryParse(Console.ReadLine(), out int inputNumber))
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
                        UserDeleteByNameSurname(userDict);
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
        int inputId = InputValidUserId(userDict, "obrisati");

        if (ConfirmationMessage("obrisati"))
        {
            userDict.Remove(inputId);
            Console.WriteLine("Uspješno brisanje korisnika.\n");
        }

    }

    static int InputValidUserId(User userDict, string messageType)
    {
        while (true)
        {
            if (messageType == "putovanje")
                Console.WriteLine("Unesi id korisnika kojem želiš dodati {0}", messageType);
            else Console.WriteLine("Unesi id korisnika kojeg želiš {0}", messageType);
            if (int.TryParse(Console.ReadLine(), out int inputId) && userDict.ContainsKey(inputId))
            {
                return inputId;
            }
            else Console.WriteLine("\nId korisnika je u krivom formatu ili nije pronađen.");
        }

    }

    static void UserDeleteByNameSurname(User userDict)
    {
        var foundUsersIdList = new List<int>();
        string inputFormatted;
        while (true)
        {
            Console.WriteLine("Unesi ime i prezima(u obliku Ime Prezime) korisnika kojeg želiš obrisati.");
            string? inputNameSurname = Console.ReadLine();

            if (!string.IsNullOrEmpty(inputNameSurname) &&
                RemoveWhiteSpace(inputNameSurname).All(stringChar => char.IsLetter(stringChar)))
            {
                inputNameSurname = inputNameSurname.Trim();
                var inputArr = inputNameSurname.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int i = 0;
                foreach (var nameSurname in inputArr)
                {
                    inputArr[i] = char.ToUpper(nameSurname[0]) + nameSurname.Substring(1).ToLower();
                    if (i != (inputArr.Length - 1))
                        inputArr[i] += " ";
                    i++;
                }

                inputFormatted = string.Concat(inputArr);
                var userDictList = userDict.ToList();
                var foundUsers = userDictList.FindAll(kvPair =>
                    (kvPair.Value.Item1 + " " + kvPair.Value.Item2) == inputFormatted);

                foreach (var user in foundUsers)
                    foundUsersIdList.Add(user.Key);

                break;
            }
            else
                Console.WriteLine(
                    "\nPogrešan unos imena i prezimena.Ne smije biti prazno ili sadržavati brojeve/specijalne znakove.\n");
        }

        if (foundUsersIdList.Count > 0)
        {
            var userSelection = DeleteManyUsers(userDict, foundUsersIdList, inputFormatted);
            UserDelConfirm(userDict, userSelection);
        }
        else
        {
            Console.WriteLine("Ne postoji user s tim imenom i prezimenom.\n");
        }

    }

    static void UserDelConfirm(User userDict, string[] userSelection)
    {
        if (ConfirmationMessage("obrisati"))
        {
            foreach (var id in userSelection)
            {
                if (int.TryParse(id, out int parsedId))
                    userDict.Remove(parsedId);
            }


            Console.WriteLine("Uspješno brisanje korisnika.\n");
        }
    }

    static bool ConfirmationMessage(string messageType)
    {
        Console.WriteLine(
            "\nŽeliš li zaista {0} korisnika -- y/n. Ako je unos krajnjeg odabira neispravan ili je odabir 'n' operacija se obustavlja.\n",
            messageType);
        if (char.TryParse(Console.ReadLine(), out char inputChar) && inputChar == 'y')
            return true;
        else if (inputChar == 'n')
        {
            Console.WriteLine(
                "\nOperacija obustavljena.Povratak na prethodni izbornik nakon pritiska bilo koje tipke.\n");
            return false;
        }
        else
        {
            Console.WriteLine(
                "\nUnos neispravan.Operacija se obustavljena.Povratak na prethodni izbornik nakon pritiska bilo koje tipke.\n");
            return false;
        }
    }

    static string[] DeleteManyUsers(User userDict, List<int> foundUsersIdList, string inputFormatted)
    {
        string[] inputParts;
        foundUsersIdList.Sort();
        while (true)
        {
            Console.WriteLine("\nPostoji {0} korisnik(a) s istim imenom i prezimenom:{1}\n", foundUsersIdList.Count,
                inputFormatted);
            IdListOutput(foundUsersIdList);
            Console.WriteLine("Unesi sve korisnike koje želiš obrisati u formatu (id1 id2 id3..)\n");

            string? inputIdList = Console.ReadLine();
            if (!string.IsNullOrEmpty(inputIdList))
            {
                inputIdList = inputIdList.Trim();
                inputParts = inputIdList.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int flag = 0;
                foreach (var inputId in inputParts)
                {
                    flag = 0;

                    if (int.TryParse(inputId, out int id) && (foundUsersIdList.BinarySearch(id) >= 0))
                        flag = 1;
                }

                if (flag == 1)
                    break;
                else Console.WriteLine("\nNeki od unesenih id-ova nije pronađen u listi dostupnih.");
            }

            else Console.WriteLine("\nId lista korisnika je u krivom formatu.\n");
        }

        return inputParts;
    }

    static void IdListOutput(List<int> foundUsersIdList)
    {
        Console.WriteLine("Lista dostupnih id-eva za obrisati");
        Console.Write("[ ");
        foreach (var id in foundUsersIdList)
        {
            Console.Write(id + " ");
        }

        Console.Write("]\n");
    }

    static void ModifyUser(User userDict)
    {
        int inputId = InputValidUserId(userDict, "izmijeniti");
        var modifiedInfo = AddUserInfo(userDict, inputId);
        if (ConfirmationMessage("izmijeniti podatke"))
        {
            userDict[inputId] = modifiedInfo;
            Console.WriteLine("Uspješna izmjena podataka korisnika.\n");
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
            if (int.TryParse(Console.ReadLine(), out int inputNumber))
            {
                switch (inputNumber)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na korisnicki izbornik\n");
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

    static void AlphabetSortedOutput(User userDict)
    {
        Console.WriteLine("\nIspis korisnika sortranih abecedno po prezimenu\n");
        var dictSorted = userDict.OrderBy(kvPar => kvPar.Value.Item2).ThenBy(kvPar => kvPar.Value.Item1).ToList();
        foreach (var kvPair in dictSorted)
        {
            FormatedOutput(kvPair);
        }
    }

    static void YearTresholdOutput(User userDict)
    {
        Console.WriteLine("\nIspis korisnika starijih od 20 godina\n");
        foreach (var user in userDict)
        {

            var birthDate = user.Value.Item3;
            var birthDateTime = birthDate.ToDateTime(new TimeOnly()).Date;
            var newestDate = DateTime.Today;
            int age = newestDate.Year - birthDate.Year;

            if (birthDateTime > newestDate.AddYears(-age))
                age--;

            if (age > 20)
                FormatedOutput(user);
        }
    }

    static void TripTrehsoldOutput(User userDict)
    {
        Console.WriteLine("\nIspis korisnika koji imaju bar 2 putovanja\n");
        foreach (var user in userDict)
        {
            if (user.Value.Item4.Count >= 2)
                FormatedOutput(user);
        }
    }

    static void FormatedOutput(KeyValuePair<int, UserDictValue> dictElement)
    {
        Console.WriteLine("{0} - {1} - {2} - {3}\n", dictElement.Key, dictElement.Value.Item1, dictElement.Value.Item2,
            dictElement.Value.Item3);
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
                        NewTripInput(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir.Brisanje putovanja.");
                        break;
                    case 3:
                        Console.WriteLine("Uspješan odabir.Uređivanje postojećeg putovanja");
                        break;
                    case 4:
                        Console.WriteLine("Uspješan odabir.Pregled svih putovanja");
                        TripOutput();
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
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

    static void NewTripInput(User userDict)
    {
        int userId = InputValidUserId(userDict, "putovanje");
        Console.WriteLine("Unos za korisnika na id-u: {0} imena {1}",userId,userDict[userId].Item1+" - "+userDict[userId].Item2);
        int tripId = TripIdInput();
        var tripInfo = AddTripInfo();
        
        userDict[userId].Item4[tripId] = tripInfo;
        var tripNew = Tuple.Create(tripId, tripInfo);
        GlobalTripList.Add(tripNew);
        Console.WriteLine("\nUspješno dodano novo putovanje.");
        TripOutput();
    }

    static int TripIdInput()
    {
        while (true)
        {
            Console.WriteLine("Unesi jedinstveni id putovanja");
            if (int.TryParse(Console.ReadLine(), out int inputId) && inputId >= 0 &&
                !GlobalTripList.Any(trip => trip.Item1 == inputId))
                return inputId;

            if (inputId < 0)
                Console.WriteLine("\nId ne smije biti negativan broj.");
            else if (GlobalTripList.Any(trip => trip.Item1 == inputId))
                Console.WriteLine("\nId mora biti jedinstven.");

            else Console.WriteLine("\nPogrešan tip podatka-> id mora biti cijeli broj");

        }
    }

    static Tuple<DateOnly, double, double, double, double> AddTripInfo()
    {

        DateOnly tripDate = TripDateInput();
        double distance = TripDoubleInput(0);
        double petrolUsed = TripDoubleInput(1);
        double petrolPerLitre = TripDoubleInput(2);
        double netSpend = petrolUsed * petrolPerLitre;
        return Tuple.Create(tripDate, distance, petrolUsed,petrolPerLitre,netSpend);
    }

    static DateOnly TripDateInput()
    {
        while (true)
        {
            Console.WriteLine("Unesi datum putovanja (YYYY-MM-DD)");
            var dateChecked = DateCheck();
            if (dateChecked != DateOnly.MaxValue)
                return dateChecked;

            Console.WriteLine("\nPogrešan unos datuma rođenja.");
        }
    }

    static double TripDoubleInput(int inputCase)
    {
        while (true)
        {
            switch (inputCase)
            {
                case 0:
                    Console.WriteLine("Unesi prijeđenu kilometražu u decimalnom ili cjelobrjonom obliku,npr.243 ili 243.5");
                    break;
                case 1:
                    Console.WriteLine("Unesi potrošeno gorivo u decimalnom ili cjelobrojnom obliku,npr. 23 ili 23.5");
                    break;
                case 2:
                    Console.WriteLine("Unesi cijenu goriva po litri u decimalnom ili cjelobrojnom obliku,npr 1 ili 1.49");
                    break;
                default:
                    break;
            }

            if (double.TryParse(Console.ReadLine(), out double inputDist) && inputDist >= 0)
                return inputDist;
            else Console.WriteLine("\nPogrešan unos kilometraže\n");
        }
    }

    static void TripOutput()
    {
        foreach (var trip in GlobalTripList)
        {
            FormatedTripOutput(trip);

        }
    }

    static void FormatedTripOutput(Tuple<int,TripValue> trip)
    {
        Console.WriteLine("\n---------\n");
        Console.WriteLine("Putovanje #{0}",trip.Item1);
        Console.WriteLine("Datum: {0}-{1}-{2}",trip.Item2.Item1.Year,trip.Item2.Item1.Month,trip.Item2.Item1.Day);
        Console.WriteLine("Kilometri: {0} L",trip.Item2.Item3);
        Console.WriteLine("Cijena po litri: {0} EUR",trip.Item2.Item4);
        Console.WriteLine("Ukupno: {0} EUR",trip.Item2.Item5);
        Console.WriteLine("\n---------\n");
    }
}



