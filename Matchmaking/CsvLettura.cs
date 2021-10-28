using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Matchmaking
{
    class CsvLettura : StreamReader
    {
        public CsvLettura(Stream stream) : base(stream)
        {
        }

        public CsvLettura(string filename) : base(filename)
        {
        }

        internal Program Program
        {
            get => default;
            set
            {
            }
        }

        // Funzione per il controllo del numero di ranghi presenti su file
        public int ContaRanghi(CsvLettura lettura)
        {
            int righe = 0;
            bool conversione;
            string testoInLettura;

            while ((testoInLettura = lettura.ReadLine()) != null)
            {
                // Controllo che la riga sia effettivamente un valore intero, in caso aumento il contatore "righe"
                conversione = Int32.TryParse(testoInLettura, out int valore);
                if (conversione && valore <= 9 && valore >= 0)
                    righe++;
            }

            lettura.Close();
            return righe;
        }
        
        // Funzione per l'assegnamento dei ranghi ai giocatori leggendo il file csv
        public List<int> LetturaRango(CsvLettura lettura)
        {
            List<int> ranghi = new List<int>();
            bool conversione;
            string testoInLettura;

            while ((testoInLettura = lettura.ReadLine()) != null)
            {
                conversione = Int32.TryParse(testoInLettura, out int valore);

                if (conversione && (0 <= valore && valore <= 9 ))
                {
                    ranghi.Add(valore);
                }
            }
            return ranghi;
        }    
    }
}