using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matchmaking
{
    class Coda
    {
        private List<Giocatore> codaAttesa = new List<Giocatore>();
        private int numPartita = 0;
        private List<string> listaPartite = new List<string>();

        public List<Giocatore> CodaAttesa
        {
            get { return codaAttesa; }
            set { codaAttesa = value; }
        }

        internal Program Program
        {
            get => default;
            set
            {
            }
        }

        // Funzione per popolare la coda di attesa
        public void PopolaCoda()
        {
            foreach(Giocatore g in Program.giocatore.Giocatori)
            {
                codaAttesa.Add(new Giocatore(g.Nome, g.Rango));
            }

            // Ordino i giocatori in base al Rango
            codaAttesa = codaAttesa.OrderBy(x => x.Rango).ToList();
        }
        
        // Funzione per creare le partite
        public int CreaPartita(int p)
        {
            List<Giocatore> partita = new List<Giocatore>();
            int chiave;
            int contatore = 0;
            int pos = 0;
            int numNecessario = 0;

            // Controllo che l'indice della codaAttesa stia ancora puntando ad un elemento
            if(p < codaAttesa.Count)
                chiave = codaAttesa[p].Rango;
            else    // Se non punta più a nulla significa che ho girato tutta la lista e che quindi non posso creare altre partite
            {
                Console.WriteLine("Non è possibilie creare ulteriori partite.");
                return numNecessario = 2;
            }
            // Ricerco 10 giocatori per la mia partita
            while (contatore != 10 && numNecessario == 0)
            {

                if (contatore != 10 && pos == codaAttesa.Count)
                {
                    // Se entro qui significa che ho girato tutta la lista ma non ho trovato abbastanza giocatori con ranghi simili
                    numNecessario = 1;
                    Console.WriteLine("Non si ha il numero necessario di giocatori dello stesso rango per creare una partita utilizzando questa chiave\n\n");
                    
                    // Libero la partita e rimetto i giocatori in coda, riordinandola
                    for(int i = 0; i < partita.Count; i++)
                        codaAttesa.Add(partita[i]);

                    partita.Clear();
                    codaAttesa = codaAttesa.OrderBy(x => x.Rango).ToList();
                    return numNecessario;
                }
                else
                {
                    if (chiave != 0 && chiave != 9)
                    {
                        // Controllo che la differenza massima di rango sia massimo di 2
                        if (chiave - 1 <= codaAttesa[pos].Rango && codaAttesa[pos].Rango <= chiave + 1)
                        {
                            partita.Add(codaAttesa[pos]);
                            codaAttesa.Remove(codaAttesa[pos]);
                            contatore++;
                        }
                        else
                            // Se entro qui vuol dire che il giocatore che sto guardando non ha il rango necessario per partecipare,
                            // quindi aggiorno l'indice
                            pos++;
                    }
                    else
                    {
                        // Gestisco diversamente lo 0 e il 9 in quanto, rispettivamente, sotto e sopra non ho valori
                        if (chiave == 0)
                        {
                            if (codaAttesa[pos].Rango <= chiave + 1)
                            {
                                partita.Add(codaAttesa[pos]);
                                codaAttesa.Remove(codaAttesa[pos]);
                                contatore++;
                            }
                            else
                                pos++;
                        }
                        else
                        {
                            if (chiave - 1 <= codaAttesa[pos].Rango)
                            {
                                partita.Add(codaAttesa[pos]);
                                codaAttesa.Remove(codaAttesa[pos]);
                                contatore++;
                            }
                            else
                                pos++;
                        }
                    }
                }             
            }

            // Stampo a console la partita creata
            for(int i = 0; i < partita.Count; i++)
            {
                Console.WriteLine(partita[i].Nome + ", Rango: " + partita[i].Rango);
            }
            Console.WriteLine("\n\n\n\n");

            // Aumento il numero di partite
            numPartita++;

            // "listaPartite" servirà per stampare su file csv, qui aggiungo il numero della partita corrente
            listaPartite.Add("Partita " + numPartita);

            // Qui invece aggiungo ii giocatori della partita stessa
            for (int i = 0; i < partita.Count; i++)
                listaPartite.Add(partita[i].Nome + ", Rango " + partita[i].Rango);
            
            return numNecessario;
        }

        // Funzione per aggiungere gli eventuali giocatori ancora in coda
        public void RiportaCoda()
        {
            listaPartite.Add("Giocatori rimasti in coda di attesa:");
            for (int i = 0; i < codaAttesa.Count; i++)
                listaPartite.Add(codaAttesa[i].Nome + ", Rango " + codaAttesa[i].Rango);

            Console.WriteLine("\nGiocatori rimasti in coda di attesa:");
            for (int i = 0; i < codaAttesa.Count; i++)
                Console.WriteLine(codaAttesa[i].Nome + ", Rango " + codaAttesa[i].Rango);
        }

        // Funzione che mi controlla se in coda è ancora presente qualcuno
        public bool ControllaCoda()
        {
            bool giocatoriInCoda = false;

            if (codaAttesa.Count > 0)
                giocatoriInCoda = true;

            return giocatoriInCoda;
        }

        // Funzione per scrivere le partite su file csv
        public void ScriviPartite()
        {
            Scrittura(listaPartite);
        }

        // Divide il file in righe che verranno stampate una a una da "ScriviRiga"
        public void Scrittura(List<string> partita)
        {
            using CsvScrittura scrittore = new CsvScrittura("TestScrittura.csv");

            for (int i = 0; i < partita.Count; i++)
            {
                CsvRiga riga = new CsvRiga { partita[i] };

                scrittore.ScriviRiga(riga);
            }
        }
    }
}
