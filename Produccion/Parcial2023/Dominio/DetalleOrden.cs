using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Produccion.Domino;

namespace Parcial2023.Dominio
{
    public class DetalleOrden
    {
        public int Id { get; set; }

        public Componente Componente { get; set; }

        public int Cantidad { get; set; }

        public DetalleOrden()
        { 
           Id = 0;
            Componente = null;
            Cantidad = 0;
        }

        public DetalleOrden(int id, Componente componente, int cantidad)
        { 
          Id=id;
            Componente=componente;
            Cantidad=cantidad;
        }

    }
}
