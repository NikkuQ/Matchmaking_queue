using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Matchmaking
{
    class Rango
    {
        Random rng = new Random();
        public int lega;

        internal Program Program
        {
            get => default;
            set
            {
            }
        }

        // Funzione per la creazione di ranghi randomici
        public int Randomico()
        {
            lega = rng.Next(0, 10);
            return lega;
        }

        // Funzione per la creazione di ranghi da console
        public int Scrittura()
        {
            bool conversione;

            do
            {
                conversione = Int32.TryParse(Console.ReadLine(), out lega);
                
                if (conversione == false || lega > 9)
                    Console.WriteLine("\nInput non accettabile!\n");

            } 
            while (!conversione || lega > 9);

            return lega;
        }



    }
}
