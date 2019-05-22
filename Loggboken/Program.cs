using System;
using System.Collections.Generic;

namespace Loggboken
{
    class Program
    {
        //global Lista som ska innehålla Log objecktet/klassen
        static readonly List<Log> Logs = new List<Log>();
        //lobal Bool värde som kommer att användas för se om bubbelsort metoden har utfört bubbelsort 
        static bool _bubbelSorted = false;

        static void Main(string[] args)
        {
            //Börjar med att kalla på  MenuSwitch() metoden
            MenuSwitch();
            //väntar på inmatning innan programmet avlutas helt
            Console.ReadKey();
        }

        //startar programmet och loopas tills användaren bestämmer sig för att avluta programet
        private static void MenuSwitch()
        {
            //Denna bool variabel används för bestämma om programmet ska köras. När denna metod kallas så variabel initierad och har värdet true tilldelat.
            bool programIsRunning = true;

            //kör do-while loop så att kodblocket kör åtminstone en gång
            do
            {
                //anropar PrintMenuSelection() metoden som skriver ut menyn
                PrintMenuSelection();
                //Tar emot användare inmatning av menyval och spara den i en string variabel.
                string menuCase = Console.ReadLine();

                //Kollar villken case har samma sträng värde som den inmatade värdet för menyval
                switch (menuCase)
                {
                    case "1":                                            //Första casen skapar nytt inlägg
                        CreateNewLog();                                  //anropar på CreateNewLog() metoden som ska skapa en ny log
                        break;                                           //bryter ut från switch kodblocken 
                    case "2":                                            //Andra casen redigera inlägg
                        EditLog();                                       //anropar på EditLog() metoden som ska redigera en existerande logga i logg listan
                        break;                                           //bryter ut från switch kodblocken 
                    case "3":                                            //Tredje casen skriver ut alla loggar
                        PrintAllLogs();                                  //anropar på PrintAllLogs()  skriver ut alla loggar från log listan
                        break;                                           //bryter ut från switch kodblocken 
                    case "4":                                            //Fjärde casen söker efter logg
                        SearchForLog();                                  //anropar på SearchForLog() som söker efter titel på loggan
                        break;                                           //bryter ut från switch kodblocken 
                    case "5":                                            //Femte casen raderar alla loggar
                        ClearListOfAllLogs();                            //anropar på PrintAllLogs() som rensar logg listan på alla loggar
                        break;                                           //bryter ut från switch kodblocken 
                    case "6":                                            //Sjätte casen som ska utföra bubbul sort
                        BubbelSortLogsAfterTitleAlphabetically();        //anropar på BubbelSortLogsAfterTitleAlphabetically() som ska köra bubbel sort med att kolla på första bokstaven i logg titel och sortera i bokstavsordning
                        break;                                           //bryter ut från switch kodblocken 
                    case "7":                                            //Sjunde casen som binär sökning
                        BinarySearch();                                  //anropar på BinarySearch() som kör en binärsökning efter inmatade bokstaven. Sker bara om listan är sorterat
                        break;                                           //bryter ut från switch kodblocken 
                    case "8":                                            //Åttonde casen som avlutar programmet
                        programIsRunning = false;                        //bool variabel programIsRunning få tilldelat false värdet vilket leder till att while loopen avlutas.
                        break;                                           //bryter ut från switch kodblocken 
                    default:                                             //Default casen som anropar på PrintInvalidInputValueMessage som anger fel meddelande om den inmatade värdet för menyval inte existera
                        PrintInvalidInputValueMessage();                 //anropar på PrintInvalidInputValueMessage()
                        break;                                           //bryter ut från switch kodblocken 
                }
            } while (programIsRunning);
        }


        //skriver ut menyn
        static void PrintMenuSelection()
        {
            Console.WriteLine("\tVälkommen till Loggboken!");
            Console.WriteLine("\t[1] Skriv nytt inlägg i Loggboken");
            Console.WriteLine("\t[2] Redigera Logg");
            Console.WriteLine("\t[3] Skriv ut alla loggar");
            Console.WriteLine("\t[4] Sök inlägg i Loggboken");
            Console.WriteLine("\t[5] Radera alla loggar");
            Console.WriteLine("\t[6] Sortera Lista på bokstavsordning efter titel (Bubbel sort)");
            Console.WriteLine("\t[7] Binärsökning");  
            Console.WriteLine("\t[8] Avsluta");
            Console.Write("\tVälj: ");
        }

        //skapar ny inlägg i loggboken
        static void CreateNewLog()
        {
            //Rensar konsolen på allt text.
            Console.Clear();

            //ber användaren att mata in rubrik
            Console.Write("\tRubrik: ");
            //sparar inmatade rubrik värdet i sträng variabel title.
            var title = Console.ReadLine();

            //ber användaren att mata in meddelande
            Console.Write("\tMeddelande: ");
            //sparar inmatade meddelande värdet i sträng variabel text.
            var text = Console.ReadLine();

            //Kollar att både titel och text variabel inte innehåller tomma strängar.
            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(text))
            {
                //skapar ny log och lägger till den i Logs listan. Om 
                Logs.Add(new Log
                {
                    Title = title,          //logans title egenskap ska få tilldelat värde av sträng variabel title
                    Text = text,            //logans text egenskap ska få tilldelat värde av sträng variabel text
                    Date = DateTime.Now     ////logans date egenskap ska få tilldelat värde av den nuvaradande datum och tide av när instruktions körningen skedde
                });

                //Meddelar att loggar är sparat med nuvaradande datum och tide av när instruktions körningen skedde
            Console.WriteLine($"\tLogg sparat {DateTime.Now}\n");
            }
            //om både titel och text variabel innehåller tomma strängar eller är null så körs denna else blocken.
            else
            {
                //Meddelar användaren att titel och/eller meddelnade får inte vara tomma
                Console.WriteLine("\tTitle och/eller meddelande får inte vara tomma!\n");
            }
        }

        static void EditLog()
        {
            //Rensar konsolen på allt text.
            Console.Clear();
            //skriver ut alla loggar eftersom  denna metod även skriver ut index på loggan som kommer att behövas då användaren ska välja vilken logg de vill redigera
            PrintAllLogs();

            //kollar att det finn loggar i listan annars så körs ingenting
            if (Logs.Count > 0)
            {
                //Ber användaren att mata in loggnummer
                Console.Write("\tVilken log vill du redigera? ange log nummer: ");
                //sparar inmatningen i ens sträng, har sker ingen konvertering utan de kommer att ske på nästa kommande stukturelement
                string input = Console.ReadLine();

                //här kollar vi om den inmatade värdet kan konverteras till en int. Om det går så tilldelas värdet i en ny deklarerad int variabel som heter logIndex
                if (!int.TryParse(input, out int logIndex))
                {
                    //Om konverting inte går så skriv ut denna fel meddelandet
                    Console.WriteLine("\tDu kan bara skriva ett tal med siffror!");
                }
                //kollar att LogIndex inte är större eller lika med summa av loggar i listan eftersom vi vill undvika ArgumetOutOfBounds när vi ska använda LogIndex
                //för att hämta ut loggan från listan exception som kommer att krasha programmet
                else if (logIndex >= Logs.Count)
                {
                    //Om log index är större eller lika med summa av loggar i listan anger vi felmeddelande 
                    Console.WriteLine("\tAnge ett log nummer som finns i listan!");
                }
                //annars om de två föregående vilkor inte går igenom så betyder det att allt ting stämmer och nu körs denna else block
                else
                {
                    //Skriver ut loggan som har index värdet av den inmatade värdet.
                    Console.Write($"\t{logIndex}:");
                    Console.WriteLine("\tRubrik: " + Logs[logIndex].Title);
                    Console.WriteLine("\t\tMeddelande: " + Logs[logIndex].Text);
                    Console.WriteLine("\t\tDatum: " + Logs[logIndex].Date);

                    //ber användaren att skriva in en ny titel
                    Console.Write("\tRubrik: ");
                    var title = Console.ReadLine();

                    //ber användaren att skriva in en ny meddelande
                    Console.Write("\tMeddelande: ");
                    var text = Console.ReadLine();

                    Logs[logIndex].Title = title;           //logans title egenskap ska få nya tilldelat värde av sträng variabel title
                    Logs[logIndex].Text = text;             //logans text egenskap ska få nya tilldelat värde av sträng variabel text
                    Logs[logIndex].Date = DateTime.Now;     ////logans date egenskap ska få tilldelat värde av den nya nuvaradande datum och tiden av när instruktions körningen skedde

                    Console.WriteLine("\tÄndringar har sparats\n");
                }
            }          
        }

        //Implementerade bbueblsorting från när man jobbade med det på övning 7
        static void BubbelSortLogsAfterTitleAlphabetically()
        {
            //Rensar konsolen på allt text
            Console.Clear();

            //kollar att loggan inte är tom
            if (Logs.Count > 0)
            {
                //Kör for loop som itererar så många gånger det finns loggar in listan - 1
                for (int i = 0; i < Logs.Count - 1; i++)
                {
                    //Kör for loop som itererar så många gånger det finns loggar in listan - 1 - i
                    for (int index = 0; index < Logs.Count - 1 - i; index++)
                    {
                        //Initierar int variabel tmp med värdet
                        //1 om loggan[med nuvarande indexen] titel ligger förre i bokstavs ordningen av loggans[nästa kommande index] titels 
                        //-1 om loggan[med nuvarande indexen] titel ligger efter i bokstavs ordningen av loggans[nästa kommande index] titels 
                        //0 om både loggan[med nuvarande indexen] titels och loggans[nästa kommande index] titels har samma värdet
                        int tmp = Logs[index].Title.CompareTo(Logs[index + 1].Title);
                        //om tmp är större en 0 så ska logan med nuvarande index flytta plats med loggan av den näst kommande indexen.
                        if (tmp > 0)
                        {
                            Log temp = Logs[index];
                            Logs[index] = Logs[index + 1];
                            Logs[index + 1] = temp;
                        }
                    }
                }
                //bool värdet _bubbel sort blir true om denna if sats körs.
                _bubbelSorted = true;

                //skriver ut alla loggar för att visa att de är nu i bokstavsordning efter titel.
                PrintAllLogs();
            }
            //anger fel meddelande om att listan är tom
            else
            {
                Console.WriteLine("\n\tKan EJ sortera eftersom att Loggboken är tom!");
            }
        }

        //Implementerade binär sökning från när man jobbade med det på övning 7
        static void BinarySearch()
        {
            //Rensar konsolen på allt text
            Console.Clear();
            //kollar att bool variabel _bubbelSorted är sant. Detta innebär då att listan har bubbelsorterat i bokstavordning efter titel.
            if (_bubbelSorted)
            {
                //ber användaren att mata in en bokstav som ska söka på efter titel
                Console.Write("\tVilken bokstav ska sökningen börja på? ");
                //Initierar en sträng variabel med den tilldelade värdet av användaren inmatning
                string key = Console.ReadLine();
                // Ser till att sökningen alltid är något, i det här fallet "a"
                if (key.Length <= 0) key = "a"; 
                
                //Initiera en int variabel med värdet 0
                int minValue = 0;
                //Initiera en int variabel med av den största index i log listan
                int maxValue = Logs.Count - 1;

                //loopen körs så länge minvärde är mindre än maxvärdet
                while (minValue <= maxValue)
                {
                    //får ut meddel värdet på min och max
                    int middle = (minValue + maxValue) / 2;

                    //Initierar int variabel med värdet
                    //1 om sök bokstaven ligger förre i bokstavs ordningen av loggans[som har index medel] titels första bokstav
                    //-1 om om sök bokstaven ligger efter i bokstavs ordningen av loggans[som har index medel] titels första bokstav
                    //0 om både sök bokstaven loggans[som har index medel] titels första bokstav har samma bokstav
                    int tmp = key.CompareTo(Logs[middle].Title[0].ToString());

                    //om tmp är större än noll ska min value vara lika med meddel + 1
                    if (tmp > 0)
                    {
                        minValue = middle + 1;
                    }
                    //om tmp är mindre än noll ska max value vara lika med meddel - 1
                    else if (tmp < 0)
                    {
                        maxValue = middle - 1;
                    }
                    //körs till slut om tmp är lika med 0, detta innebär sök träff
                    else
                    {
                        Console.WriteLine($"tBokstaven du sökt på finns på element {middle} i Log listan\n");
                        //Bryter från while loopen
                        break;
                    }
                }

                //den föregående while-loppen körs tills att min value blir större än max värdet eller om elseblocken körs. Om while-loopen körs tills att vilkoren blir falsk så innebär de
                //att else blocken körde aldring i while-loopen detta betyder då att det fanns ingen sökning som hade första bokstaven som den inmatade värdet
                if (minValue > maxValue)
                {
                    Console.WriteLine("\tSökningen lyckades inte!\n");
                }
            }
        }

        //Skriver ut all loggar
        static void PrintAllLogs()
        {
            //Rensar konsolen på allt text.
            Console.Clear();
            //skriver ut alla loggar och antalen
            Console.WriteLine($"\tAlla loggar '{Logs.Count}': \n");

            //Kör en for-loop så jag kan även få indexen för att skriva ut den med logans information. Detta sätt vet man vilken index loggan har i listan.
            for (var index = 0; index < Logs.Count; index++)
            {
                //skriver ut index för loggan i samma rad som rubriken fast nästa utmatning dvs rubriken kommer skrivas med tab ifrån index
                Console.Write($"\t{index}:");
                //skriver ut index för Rubriken för loggan        
                Console.WriteLine("\tRubrik: " + Logs[index].Title);
                //skriver ut index för texten för loggan
                Console.WriteLine("\t\tMeddelande: " + Logs[index].Text);
                //skriver ut index för datumet för loggan
                Console.WriteLine("\t\tDatum: " + Logs[index].Date);
                //skriver ut denna för att seperera alla loggar från varandra.
                Console.WriteLine("\t-----------------------------------------------------------\n");
            }
        }

        static void SearchForLog()
        {
            //Rensar konsolen på allt text.
            Console.Clear();

            //Ber användaren att skriva in sök ord som ska användas för att söka efter titel.
            Console.Write("\tSök log med titel: ");
            //sparar in den inmatade sökorden från användarern i sträng variabel
            string searchWord = Console.ReadLine();
            //Denna bool variabel är till för att kolla om det har skett någon sökträff. varje gång dena metod kallas så är searchHit variabel intierad med false från början
            bool searchHit = false;

            //Kör en for-loop så jag kan även få indexen för att skriva ut den med logans information. Detta sätt vet man vilken index loggan har i listan.
            for (int index = 0; index < Logs.Count; index++)
            {
                //kör ToLower so att användaren inte beöver tänka på case sensitivity när de söker
                if (Logs[index].Title.ToLower() == searchWord.ToLower())
                {
                    //Om denna kod block kör så får bool variabeln searchHit true som värdet efter som att man har fått en sökträff
                    searchHit = true;
                    //skriver ut index för loggan i samma rad som rubriken fast nästa utmatning dvs rubriken kommer skrivas med tab ifrån index
                    Console.Write($"\t{index}:");
                    //skriver ut index för Rubriken för loggan        
                    Console.WriteLine("\tRubrik: " + Logs[index].Text);
                    //skriver ut index för texten för loggan
                    Console.WriteLine("\t\tMeddelande: " + Logs[index].Text);
                    //skriver ut index för datumet för loggan
                    Console.WriteLine("\t\tDatum: " + Logs[index].Date);
                    //skriver ut denna för att seperera alla loggar från varandra.
                    Console.WriteLine("\t-----------------------------------------------------------\n");
                }
            }

            //om sökningen inte ger någon resultat ska denna meddelande skrivas ut
            if (!searchHit)
            {
                //skriver ut meddelande om denna innerliggande kodblock körs
                Console.WriteLine($"\tFinner inget inlägg med Titel sökning '{searchWord}' i loggboken!\n");
            }
        }

        //Raderar alla inlägg från loggboken
        static void ClearListOfAllLogs()
        {
            //Rensar konsolen på allt text.
            Console.Clear();
            //Raderar alla loggar från Logs listan
            Logs.Clear();
            //skriver ut en meddelande som säger att radering har lyckats
            Console.WriteLine("\tLoggboken har rensats på alla inlägg\n");
        }

        //skriver ut denna fel meddelande om något annat värden än 1,2,3,4,5,6,7,8 är valt
        static void PrintInvalidInputValueMessage()
        {
            //Rensar konsolen på allt text.
            Console.Clear();
            //Ber användaren om att mata in en värdet mellan 1-8
            Console.WriteLine("\tMata in en siffra (1-8).\n");
        }
    }

    //Skapar en logg class med egenskaper som loggen ska ha
    class Log
    {
        //egenskap för rubrik
        public string Title { get; set; }
        //egensap för meddelande
        public string Text { get; set; }
        //egenskap för datum
        public DateTime Date { get; set; }
    }
}
