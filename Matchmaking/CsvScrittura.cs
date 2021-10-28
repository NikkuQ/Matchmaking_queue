using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Matchmaking
{
    class CsvScrittura : StreamWriter
    {
        // Costruttori
        public CsvScrittura(Stream stream) : base(stream)
        {

        }

        public CsvScrittura(string filename) : base(filename)
        {

        }

        internal CsvRiga CsvRiga
        {
            get => default;
            set
            {
            }
        }

        internal Coda Coda
        {
            get => default;
            set
            {
            }
        }

        // Scrive una sigola riga su un file CSV
        // "riga" sarà il parametro da scrivere sul file
        public void ScriviRiga(CsvRiga riga)
        {
            StringBuilder costruttoreDiStringhe = new StringBuilder();
            bool primaColonna = true;
            foreach (string valore in riga)
            {
                // Aggiungo un separatore in caso non sia il primo valore 
                if (!primaColonna)
                    costruttoreDiStringhe.Append(',');

                // Implementa un modo per gestire i valori che contengono la virgola o le virgolette alte
                // Racchiude le virgolette in altre virgolette e le divide
                if (valore.IndexOfAny(new char[] { '"', ',' }) != -1)
                    costruttoreDiStringhe.AppendFormat("\"{0}\"", valore.Replace("\"", "\"\""));
                else
                    costruttoreDiStringhe.Append(valore);
                primaColonna = false;
            }
            riga.LineaDiTesto = costruttoreDiStringhe.ToString();
            WriteLine(riga.LineaDiTesto);
        }
    }
}
