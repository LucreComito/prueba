using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produccion.Domino
{
    public class Componente
    {
        //completar...
        public int Codigo { get; set; } 
        public string NombreCompo { get; set; }

        public Componente()
        { 
          Codigo = 0;
            NombreCompo = "";

        }

        public Componente(int codigo, string nombreCompo)
        {
            Codigo = codigo;
            NombreCompo = nombreCompo;
        }

        public override string ToString()
        {
            return NombreCompo;
        }

    }
}
