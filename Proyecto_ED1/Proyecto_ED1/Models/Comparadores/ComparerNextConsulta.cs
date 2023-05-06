using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_ED1.Models.Comparadores
{
    public class ComparerNextConsulta : IComparer<Pacientes>
    {
        public int Compare(Pacientes x, Pacientes y)
        {
            return x.next_consult.CompareTo(y.next_consult);
        }
    }
}

