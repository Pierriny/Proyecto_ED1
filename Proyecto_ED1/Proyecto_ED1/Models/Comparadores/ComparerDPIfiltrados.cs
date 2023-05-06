using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Text;


namespace Proyecto_ED1.Models.Comparadores
{
    class ComparerDPIfiltrados : IComparer<Pacientes>
    {
        public int Compare(Pacientes x, Pacientes y)
        {
            return x.next_consult.CompareTo(y.next_consult);
        }
    }
}
