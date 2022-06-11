using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dasafio_Api
{
    internal class Moeda
    {
        public Motd Motd { get; set; }
        public bool Success { get; set; }
        public bool Historical { get; set; }
        public string Base { get; set; }
        public DateTimeOffset Date { get; set; }
        public Dictionary<string, double> Rates { get; set; }
    }
}
