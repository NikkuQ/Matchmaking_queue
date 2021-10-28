using System;
using System.Collections.Generic;

namespace Matchmaking
{
    class Program
    {
        public static Giocatore giocatore = new Giocatore();
        static void Main(string[] args)
        {
            Coda coda = new Coda();
            Rango rango = new Rango();

            int numGiocatori;
            int partita = 0;
            int numNecessario;
            bool conversione;
            int numRanghi = 0;

            Console.WriteLine("|CODA MATCHMAKING|");

            // Controllo dell'inserimento di un valore adatto
            do
            {
                Console.WriteLine("Scegliere il numero di giocatori: ");
                conversione = Int32.TryParse(Console.ReadLine(), out numGiocatori);
                if (conversione == false)
                    Console.WriteLine("Input non accettabile!");
            }
            while (!conversione);

            // Aggiungo il numero di giocatori selezionato alla mia lista
            for (int i = 0; i < numGiocatori; i++)
            {
                giocatore.Giocatori.Add(new Giocatore("Giocatore_" + i));
            }
            numRanghi = giocatore.Giocatori.Count;

            Console.WriteLine("\nSelezionare opzione desiderata:");
            Console.WriteLine("[1]Ranghi casuali [2]Scrittura manuale [3]Importare da file");
            string opzione = Console.ReadLine();
            Console.WriteLine("\n\n");

            switch(opzione)
            {
                case "1":
                    for (int i = 0; i < giocatore.Giocatori.Count; i++)
                    {
                        giocatore.Giocatori[i].Rango = rango.Randomico();
                    }

                    break;

                case "2":
                    for (int i = 0; i < giocatore.Giocatori.Count; i++)
                    {
                        Console.WriteLine("\nInserire rango del Giocatore_" + i + " (da 0 a 9):");
                        giocatore.Giocatori[i].Rango = rango.Scrittura();
                    }
                    
                    break;

                case "3":
                    using (CsvLettura lettore = new CsvLettura("Ranghi.csv"))
                    {
                        List<int> lega = new List<int>();
                        numRanghi = lettore.ContaRanghi(lettore);

                        // Mi assicuro che il numero di ranghi su file sia uguale al numero dei giocatori selezionato
                        if (numGiocatori == numRanghi)
                        {
                            CsvLettura lettore1 = new CsvLettura("Ranghi.csv");
                            lega = lettore1.LetturaRango(lettore1);

                            for (int i = 0; i < lega.Count; i++)
                            {
                                giocatore.Giocatori[i].Rango = lega[i];
                            }
                        }
                        else
                        {
                            Console.WriteLine("Il numero di ranghi nel file e il numero dei giocatori non corrispondono, riprovare");
                            break;
                        }
                    }

                    break;

                default:
                    Console.WriteLine("Input non accettabile!");
                    break;
            }

            // Controllo che il numero dei ranghi corrisponda al numero dei giocatori
            if(numRanghi == numGiocatori)
            {
                coda.PopolaCoda();

                do
                {
                    numNecessario = coda.CreaPartita(partita);

                    // Se non sono riuscito a creare una partita aggiorno il puntatore
                    if (numNecessario == 1)
                        partita++;
                } while (coda.CodaAttesa.Count != 0 && (numNecessario == 0 || numNecessario == 1));

                // Controllo se sono rimasti giocatori in coda, in caso gli aggiungo al file csv
                if (coda.ControllaCoda())
                    coda.RiportaCoda();

                // Scrivo le partite in csv
                coda.ScriviPartite();
            }
        }
    }
}
