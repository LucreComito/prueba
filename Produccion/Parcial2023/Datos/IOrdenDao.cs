using Produccion.Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produccion.Datos
{
    public interface IOrdenDao
    {
        List<Componente> ObtenerComponentes();
        bool CrearOrden(OrdenProduccion equipo);


        // int ObtenerNroNuevaOrdenRetiro();

        // Presupuesto ObtenerPresupuestoPorNro(int numero);
    }
}
