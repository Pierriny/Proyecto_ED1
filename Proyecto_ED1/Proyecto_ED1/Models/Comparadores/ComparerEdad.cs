using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_ED1.Models.Comparadores
{
    public class ComparerEdad :IComparer<Pacientes>
    {
        public int Compare(Pacientes x, Pacientes y)
        {
            return x.age.CompareTo(y.age);
        }
    }
    
}
