using System.Diagnostics;
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
            Console.WriteLine("----------------------");
            Console.WriteLine("1 - Unos novog korisnika\n");
            Console.WriteLine("2 - Brisanje korisnika\n");
            Console.WriteLine("3 - Uređivanje korisnika\n");
            Console.WriteLine("4 - Pregled svih korisnika\n");
            Console.WriteLine("0 - Povratak na glavni izbornik");
            Console.WriteLine("----------------------\n");
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
                        if (UserExists(userDict))
                        {
                            Console.WriteLine("Uspješan odabir.Brisanje korisnika.\n");
                            UserDelete(userDict);                           
                        }
                        else Console.WriteLine("Ne postoji niti jedan korisnik kojeg možeš obrisati.\n");
                        
                        WaitingForUser();
                        break;
                    case 3:
                        if (UserExists(userDict))
                        {
                            Console.WriteLine("Uspješan odabir.Uređivanje korisnika.\n");
                            ModifyUser(userDict);                            
                        }
                        else Console.WriteLine("Ne postoji niti jedan korisnik kojeg možeš izmijeniti.\n");
                        
                        WaitingForUser();
                        break;
                    case 4:
                        if (UserExists(userDict))
                        {
                            Console.WriteLine("Uspješan odabir.Pregled svih korisnika.\n");
                            FormatedOutputType(userDict);                            
                        }
                        else Console.WriteLine("Ne postoji niti jedan korisnik.\n");
                        
                        WaitingForUser();
                        
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

    static bool UserExists(User userDict)
    {
        return userDict.Count != 0;
    }

    static void WaitingForUser()
    {
        Console.WriteLine("...Čeka se any key od korisnika...");
        Console.ReadKey(true);        
    }
    static void NewUserInput(User userDict)
    {
        int id = IdInput(userDict);
        var addedInfo = AddUserInfo(userDict, id,true);
        userDict[id] = addedInfo;
        Console.WriteLine("\nUspješno dodan novi korisnik.");
    }

    static Tuple<string, string, DateOnly, Trip> AddUserInfo(User userDict, int id,bool newItem)
    {
        string name, surname;
        DateOnly birthDate;
        
        if (AskUser("ime",newItem))
            name = StringInput("ime");
        else name = userDict[id].Item1;
        
        if (AskUser("prezime",newItem))
            surname = StringInput("prezime");
        else surname = userDict[id].Item2;
        
        if(AskUser("datum rođenja",newItem))
            birthDate = BirthDateInput();
        else  birthDate = userDict[id].Item3;
        
        
        return (newItem) ? Tuple.Create(name, surname, birthDate, new Trip()) : Tuple.Create(name,surname,birthDate,userDict[id].Item4);
    }

    static bool AskUser(string message,bool newItem)
    {
        if (newItem)
            return true;
        
        Console.WriteLine("\nŽeliš li unijeti {0}. y/n",message);
        if (char.TryParse(Console.ReadLine()?.Trim().ToLower(), out char inputChar) && inputChar == 'y')
            return true;
        else if (inputChar == 'n')
        {
            Console.WriteLine("Odustao si od promjene ovog podatka.");
            return false;
        }
        else
        {
            Console.WriteLine("\nUnos neispravan.Operacija se obustavlja.");
            return false;
        }
        
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
        if (DateOnly.TryParse(Console.ReadLine(), out DateOnly inputDate) &&
            inputDate.ToDateTime(new TimeOnly()).Date <= DateTime.Now.Date)
            return inputDate;
        return DateOnly.MaxValue;
    }

    static void UserDelete(User userDict)
    {
        while (true)
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("1 - Brisanje korisnika po id-u\n");
            Console.WriteLine("2 - Brisanje korisnika po imenu i prezimenu\n");
            Console.WriteLine("0 - Povratak na korisnicki izbornik");
            Console.WriteLine("----------------------\n");
            if (!UserExists(userDict))
            {
                Console.WriteLine("Ne postoji niti jedan korisnik kojeg možeš obrisati.\n");
                return;
            }
            if (int.TryParse(Console.ReadLine(), out int inputNumber))
            {
                switch (inputNumber)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na korisnicki izbornik.\n");
                        UserMenu(userDict);
                        return;
                    case 1:
                        Console.WriteLine("Uspješan odabir.Brisanje korisnika po id-u.\n");
                        UserDeleteById(userDict);                           
                        
                        WaitingForUser();
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir.Brisanje korisnika po imenu i prezimenu.\n");
                        UserDeleteByNameSurname(userDict);                           
                            
                        WaitingForUser();
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
            else if(messageType=="obrisati") Console.WriteLine("Unesi id korisnika kojeg želiš {0}", messageType);
            else if(messageType=="izvještaj") Console.WriteLine("Unesi id korisnika za kojeg želiš {0} ", messageType);
            else if(messageType=="izmijeniti") Console.WriteLine("Unesi id korisnika kojeg želiš {0} ", messageType);
            
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
        if (ConfirmationMessage("obrisati korisnika"))
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
            "\nŽeliš li zaista {0}  -- y/n. Ako je unos krajnjeg odabira neispravan ili je odabir 'n' operacija se obustavlja.\n",
            messageType);
        if (char.TryParse(Console.ReadLine()?.Trim().ToLower(), out char inputChar) && inputChar == 'y')
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
        var modifiedInfo = AddUserInfo(userDict, inputId,false);
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
            dictElement.Value.Item3.ToString("yyyy-MM-dd"));
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
                        DeleteTrip(userDict);
                        break;
                    case 3:
                        Console.WriteLine("Uspješan odabir.Uređivanje postojećeg putovanja.");
                        ModifyTrip(userDict);
                        break;
                    case 4:
                        Console.WriteLine("Uspješan odabir.Pregled svih putovanja.");
                        TripOutputSelection(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 5:
                        Console.WriteLine("Uspješan odabir.Izvještaji i analize");
                        ReportAnalysisSelection(userDict);
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
        if (userDict.Count == 0)
        {
            Console.WriteLine("Ne postoji više niti jedan korisnik za kojega možeš unijeti novo putovanje.");
            Console.WriteLine("...Čeka se any key od korisnika...");
            Console.Read();
            return;
        }
        int userId = InputValidUserId(userDict, "putovanje");
        Console.WriteLine("Unos za korisnika na id-u: {0} imena {1}",userId,userDict[userId].Item1+" - "+userDict[userId].Item2);
        int tripId = TripIdInput();
        var tripInfo = AddTripInfo(tripId,userId,true,userDict);
        
        userDict[userId].Item4[tripId]= tripInfo;
        var tripNew = Tuple.Create(tripId, tripInfo);
        GlobalTripList.Add(tripNew);
        Console.WriteLine("\nUspješno dodano novo putovanje.");
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

    static TripValue AddTripInfo(int tripId,int userId,bool newItem,User userDict)
    {
        DateOnly tripDate;
        double distance, petrolUsed, petrolPerLitre, netSpend;
        Tuple<int,TripValue>? selectedTrip = null;
        if (!newItem)
        { 
            selectedTrip = GlobalTripList.FirstOrDefault(trip => trip.Item1 == tripId);
        }
        if (AskUser("datum putovanja", newItem))
            tripDate = TripDateInput(userDict,userId);
        else tripDate = selectedTrip!.Item2.Item1;

        if (AskUser("kilometražu", newItem))
                distance = TripDoubleInput(0);
        else distance = selectedTrip!.Item2.Item2;

        if(AskUser("gorivo",newItem))
                petrolUsed = TripDoubleInput(1);
        else petrolUsed = selectedTrip!.Item2.Item3;
        
        if(AskUser("cijenu goriva po litri",newItem))
                petrolPerLitre = TripDoubleInput(2);
        else petrolPerLitre = selectedTrip!.Item2.Item4;
            
        netSpend = petrolUsed * petrolPerLitre;
            
        return new TripValue(tripDate, distance, petrolUsed,petrolPerLitre,netSpend);            

    }

    static DateOnly TripDateInput(User userDict,int userId)
    {
        while (true)
        {
            Console.WriteLine("Unesi datum putovanja (YYYY-MM-DD)");
            var dateChecked = DateCheck();
            if (dateChecked != DateOnly.MaxValue && dateChecked>=userDict[userId].Item3)
                return dateChecked;

            else if(dateChecked!=DateOnly.MaxValue)
                Console.WriteLine("\nPogrešan unos datuma putovanja.Datum ne može biti noviji od današnjeg.");
            else if(dateChecked>=userDict[userId].Item3)
                Console.WriteLine("\nPogrešan unos datuma putovanja.Datum ne može biti stariji od datuma rođenja korisnika ( {0} )",
                    userDict[userId].Item3.ToString("yyyy-MM-dd"));               
        }
    }

    static double TripDoubleInput(int inputCase)
    {
        while (true)
        {
            switch (inputCase)
            {
                case 0:
                    Console.WriteLine("Unesi prijeđenu kilometražu u decimalnom ili cjelobrojonom obliku,npr.243 ili 243.5");
                    break;
                case 1:
                    Console.WriteLine("Unesi potrošeno gorivo u decimalnom ili cjelobrojnom obliku,npr. 23 ili 23.5");
                    break;
                case 2:
                    Console.WriteLine("Unesi cijenu goriva po litri u decimalnom ili cjelobrojnom obliku,npr 1 ili 1.49");
                    break;
                case 3:
                    Console.WriteLine("Unesi ukupni trošak nekog putovanja u EUR.");
                    break;
                default:
                    break;
            }

            if (double.TryParse(Console.ReadLine(), out double input) && input >= 0)
                return input;
            else Console.WriteLine("\nPogrešan unos.\n");
        }
    }

    static void ModifyTrip(User userDict)
    {
        int inputId = InputValidTripId("izmijeniti");
        var user = userDict.FirstOrDefault(user => user.Value.Item4.ContainsKey(inputId));
        var userId = user.Key;
        var modifiedInfo = AddTripInfo(inputId,userId,false,userDict);
        if (ConfirmationMessage("izmijeniti podatke"))
        {
            var selectedTripId = GlobalTripList.FindIndex(trip => trip.Item1 == inputId);
            GlobalTripList[selectedTripId]= Tuple.Create(inputId,modifiedInfo);
            userDict[userId].Item4[inputId]= modifiedInfo;
            Console.WriteLine("Uspješna izmjena podataka za putovanje.\n");
        }      
    }

    static int InputValidTripId(string message)
    {
            while (true)
            {
                    Console.WriteLine("Unesi id putovanja kojeg želiš {0}", message);
                
                if (int.TryParse(Console.ReadLine(), out int inputId) && GlobalTripList.Any(trip => trip.Item1 == inputId))
                {
                    return inputId;
                }
                else Console.WriteLine("\nId putovanja je u krivom formatu ili nije pronađen.");
            }       
    }
    static void TripOutputSelection(User userDict)
    {
          while (true)
        {
            Console.WriteLine("1 - Ispis putovanja po redu dodavanja\n");
            Console.WriteLine("2 - Ispis putovanja sortiranih po trošku uzlazno\n");
            Console.WriteLine("3 - Ispis putovanja sortranih po trošku silazno\n");
            Console.WriteLine("4 - Ispis putovanja sortranih po kilometraži uzlazno\n");
            Console.WriteLine("5 - Ispis putovanja sortranih po kilometraži silazno\n");
            Console.WriteLine("6 - Ispis putovanja sortranih po datumu uzlazno\n");
            Console.WriteLine("7 - Ispis putovanja sortranih po datumu silazno\n");
            Console.WriteLine("0 - Povratak na izbornik putovanja\n");
            if (int.TryParse(Console.ReadLine(), out int outputSel))
            {
                switch (outputSel)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na izbornik putovanja.");
                        TripMenu(userDict);
                        return;
                    case 1:
                        Console.WriteLine("Uspješan odabir.Ispis putovanja po redu dodavanja.");
                        TripOutputByOrder();
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir.Ispis putovanja sortiranih po trošku uzlazno.");
                        TripOutputSpend(0);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 3:
                        Console.WriteLine("Uspješan odabir.Ispis putovanja sortiranih po trošku silazno.");
                        TripOutputSpend(1);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 4:
                        Console.WriteLine("Uspješan odabir.Ispis putovanja sortranih po kilometraži uzlazno.");
                        TripOutputDist(0);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 5:
                        Console.WriteLine("Uspješan odabir.Ispis putovanja sortranih po kilometraži silazno.");
                        TripOutputDist(1);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 6:
                        Console.WriteLine("Uspješan odabir.Ispis putovanja sortranih po datumu uzlazno.");
                        TripOutputDate(0);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 7:
                        Console.WriteLine("Uspješan odabir.Ispis putovanja sortranih po datumu silazno.");
                        TripOutputDate(1);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
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
    static void TripOutputByOrder()
    {
        foreach (var trip in GlobalTripList)
            FormattedTripOutput(trip);
            
    }

    static void FormattedTripOutput(Tuple<int,TripValue> trip)
    {
        Console.WriteLine("\n---------");
        Console.WriteLine("Putovanje #{0}",trip.Item1);
        Console.WriteLine("Datum: {0}",trip.Item2.Item1.ToString("yyyy-MM-dd"));
        Console.WriteLine("Kilometri: {0}",trip.Item2.Item2);
        Console.WriteLine("Gorivo: {0} L",trip.Item2.Item3);
        Console.WriteLine("Cijena po litri: {0} EUR",trip.Item2.Item4);
        Console.WriteLine("Ukupno: {0:F2} EUR",trip.Item2.Item5);
        Console.WriteLine("---------\n");
    }
    static void TripOutputSpend(int sortDir)
    {
        if (sortDir == 0)
        {
            var listSorted = GlobalTripList.OrderBy(trip => trip.Item2.Item5);
            foreach (var trip in listSorted)
                FormattedTripOutput(trip);
        }
        else if (sortDir == 1)
        {
            var listSorted = GlobalTripList.OrderByDescending(trip => trip.Item2.Item5);
            foreach (var trip in listSorted)
                FormattedTripOutput(trip);            
        }
    }
    static void TripOutputDist(int sortDir)
    {
        if (sortDir == 0)
        {
            var listSorted = GlobalTripList.OrderBy(trip => trip.Item2.Item2);
            foreach (var trip in listSorted)
                FormattedTripOutput(trip);
        }
        else if (sortDir == 1)
        {
            var listSorted = GlobalTripList.OrderByDescending(trip => trip.Item2.Item2);
            foreach (var trip in listSorted)
                FormattedTripOutput(trip);            
        }
    }
    static void TripOutputDate(int sortDir)
    {
        if (sortDir == 0)
        {
            var listSorted = GlobalTripList.OrderBy(trip => trip.Item2.Item1);
            foreach (var trip in listSorted)
                FormattedTripOutput(trip);
        }
        else if (sortDir == 1)
        {
            var listSorted = GlobalTripList.OrderByDescending(trip => trip.Item2.Item1);
            foreach (var trip in listSorted)
                FormattedTripOutput(trip);            
        }
    }

    static void ReportAnalysisSelection(User userDict)
    {
        var inputId = InputValidUserId(userDict, "izvještaj");
        while (true)
        {
            Console.WriteLine("1 - Ukupna potrošnja goriva\n");
            Console.WriteLine("2 - Ukupni troškovi goriva\n");
            Console.WriteLine("3 - Prosječna potrošnja goriva u L/100km\n");
            Console.WriteLine("4 - Putovanje s najvećom potrošnjom goriva\n");
            Console.WriteLine("5 - Pregled putovanja po određenom datumu\n");
            Console.WriteLine("0 - Povratak na izbornik putovanja\n");
            if (int.TryParse(Console.ReadLine(), out int outputSel))
            {
                switch (outputSel)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na izbornik putovanja.");
                        TripMenu(userDict);
                        return;
                    case 1:
                        Console.WriteLine("Uspješan odabir.Izvještaj za ukupnu potršnju goriva.");
                        double sum1=TotalPetrolUsed(userDict[inputId].Item4);
                        Console.WriteLine("Ukupna potrošnja goriva: {0:F2} L\n",sum1);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir.Izvještaj za ukupne troškove goriva.");
                        double sum2=TotalSpend(userDict[inputId].Item4);
                        Console.WriteLine("Ukupni troškovi goriva: {0:F2} EUR\n",sum2);  
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 3:
                        Console.WriteLine("Uspješan odabir.Izvještaj za prosječnu potrošnja goriva u L/100km.");
                        double avg=AvgSpend(userDict[inputId].Item4);
                        Console.WriteLine("Prosječna potrošnja goriva: {0:F2} L/100km",avg);     
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 4:
                        Console.WriteLine("Uspješan odabir.Izvještaj za putovanje s najvećom potrošnjom goriva.");
                        MaxPetrolUsed(userDict[inputId].Item4);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 5:
                        Console.WriteLine("Uspješan odabir.Pregled putovanja po određenom datumu.");
                        if (userDict[inputId].Item4.Count > 0)
                        {
                            var inputDate=TripDateSearch(userDict[inputId].Item4);
                            TripDateReport(userDict[inputId].Item4, inputDate);                            
                        }
                        else Console.WriteLine("Popis putovanja ovog korisnika je prazan.");
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
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

    static double TotalPetrolUsed(Trip userTripDict)
    {
        var sum = 0.0;
        foreach (var trip in userTripDict)
            sum += trip.Value.Item3;
        return sum;
    }

    static double TotalSpend(Trip userTripDict)
    {
        var sum = 0.0;
        foreach (var trip in userTripDict)
            sum += trip.Value.Item5;
        return sum;
    }


    static double AvgSpend(Trip userTripDict)
    {
        double totPetrol = TotalPetrolUsed(userTripDict);
        double totDist= TotalDist(userTripDict);
        double avg=(totPetrol/totDist)*100;
        return avg;
    }

    static double TotalDist(Trip userTripDict)
    {
        var sum = 0.0;
        foreach (var trip in userTripDict)
            sum += trip.Value.Item2;
        return sum;
    }

    static void MaxPetrolUsed(Trip userTripDict)
    {

        try
        {
            var max = userTripDict.Max(trip => trip.Value.Item3);
            var filtrated = userTripDict.Where(trip => trip.Value.Item3 >= max);

            foreach (var trip in filtrated)
                FormattedTripOutput(Tuple.Create(trip.Key, trip.Value));
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Putovanje s najvećom potrošnjom goriva ne postoji jer je lista putovanja prazna.");
        }
    }

    static void TripDateReport(Trip userTripDict, DateOnly inputDate)
    {
        var filtrated=userTripDict.Where(trip => trip.Value.Item1 == inputDate);
        foreach (var trip in filtrated)
            FormattedTripOutput(Tuple.Create(trip.Key,trip.Value));
    }
    static DateOnly TripDateSearch(Trip userTripDict)
    {
        while (true)
        {
            Console.WriteLine("Unesi datum putovanja (YYYY-MM-DD)");
            Console.WriteLine("Ispis svih datuma\n");
            Console.Write("[ ");
            foreach (var trip in userTripDict.DistinctBy(trip=>trip.Value.Item1))
            {
                Console.Write("{0} - {1} - {2},",trip.Value.Item1.Year,trip.Value.Item1.Month,trip.Value.Item1.Day);
            }
            Console.Write("]\n");
            var dateChecked = DateCheck();
            if (dateChecked != DateOnly.MaxValue && userTripDict.Any(trip => trip.Value.Item1 == dateChecked))
                return dateChecked;

            else if(dateChecked==DateOnly.MaxValue)
                Console.WriteLine("\nPogrešan unos datuma putovanja.");
            else Console.WriteLine("\nNepostojeći datum.");
        }
    }

    static void DeleteTrip(User userDict)
    {
        while (true)
        {
            if (GlobalTripList.Count == 0)
            {
                Console.WriteLine("Ne postoji više niti jedno putovanje koje možeš obrisati.");
                Console.WriteLine("...Čeka se any key od korisnika...");
                Console.Read();
                return;
            }
            Console.WriteLine("1 - Brisanje putovanja po id-u\n");
            Console.WriteLine("2 - Brisanje svih putovanja skupljih od unesenog iznosa\n");
            Console.WriteLine("3 - Brisanje svih putovanja jefitinijih od unesenog iznosa\n");
            Console.WriteLine("0 - Povratak na izbornik putovanja\n");
            if (int.TryParse(Console.ReadLine(), out int outputSel))
            {
                switch (outputSel)
                {
                    case 0:
                        Console.WriteLine("Uspješan odabir.Povratak na izbornik putovanja.");
                        TripMenu(userDict);
                        return;
                    case 1:
                        Console.WriteLine("Uspješan odabir.Brisanje putovanja po id-u.");
                        DeleteTripById(userDict);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 2:
                        Console.WriteLine("Uspješan odabir.Brisanje svih putovanja skupljih od unesenog iznosa.");
                        DeleteTripByTotSpend(userDict,0);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
                        break;
                    case 3:
                        Console.WriteLine("Uspješan odabir.Brisanje svih putovanja jeftinijih od unesenog iznosa.");
                        DeleteTripByTotSpend(userDict,1);
                        Console.WriteLine("...Čeka se any key od korisnika...");
                        Console.Read();
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

    static void DeleteTripById(User userDict)
    {
        
        int inputId = InputValidTripId("obrisati");              
        if (ConfirmationMessage("obrisati putovanje"))
        {
            var user = userDict.FirstOrDefault(user => user.Value.Item4.ContainsKey(inputId));
            var userId = user.Key;
            var selectedTripId = GlobalTripList.FindIndex(trip => trip.Item1 == inputId);
            GlobalTripList.RemoveAt(selectedTripId);
            userDict[userId].Item4.Remove(inputId);
            Console.WriteLine("Uspješno brisanje putovanja.\n");
        }      
    }

    static void DeleteTripByTotSpend(User userDict,int spendDirection)
    {
        double inputSpend = TripDoubleInput(3);
        int removed = 0;
        if (ConfirmationMessage("obrisati putovanja skuplja od unesenog iznosa"))
        {
            if (spendDirection == 0)
            {
                var delTripIdEnum = GlobalTripList.Where(trip=>trip.Item2.Item5>inputSpend).Select(trip=>trip.Item1);
                var delTripIdList=delTripIdEnum.ToList();
                removed=GlobalTripList.RemoveAll(trip=>trip.Item2.Item5>inputSpend);             
                RemoveFromDict(userDict,delTripIdList);
            }
            
            else if (spendDirection == 1)
            {
                var delTripIdEnum = GlobalTripList.Where(trip=>trip.Item2.Item5<inputSpend).Select(trip=>trip.Item1);
                var delTripIdList=delTripIdEnum.ToList();  
                removed=GlobalTripList.RemoveAll(trip=>trip.Item2.Item5<inputSpend);  
                RemoveFromDict(userDict,delTripIdList);
            }

            Console.WriteLine("Uspješno brisanje {0} putovanja.\n",removed);
        }        
    }

    static void RemoveFromDict(User userDict,List<int>idList)
    {
        foreach (var userId in userDict.Keys)
        foreach (var trip in userDict[userId].Item4)
            if (idList.Contains(trip.Key))
                userDict[userId].Item4.Remove(trip.Key);       
    }
}

    


