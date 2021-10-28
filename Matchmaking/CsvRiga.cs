using System;
using System.Collections.Generic;
using System.Text;

namespace Matchmaking
{
    // Classe per riportare cosa scrivere su file
    class CsvRiga : List<string> 
    {
        public string LineaDiTesto { get; set; }

        internal Coda Coda
        {
            get => default;
            set
            {
            }
        }
    }
}
