using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Proyecto_ED1.Models.Comparadores
{
    class ComparerDPI : IComparer<Pacientes>
    {
        public int Compare(Pacientes x, Pacientes y)
        {
            return x.ID.CompareTo(y.ID);
        }

    }
}
