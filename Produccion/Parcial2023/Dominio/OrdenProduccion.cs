using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Parcial2023.Dominio;

namespace Produccion.Domino
{
    public class OrdenProduccion
    {
        //completar...
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string Modelo { get; set; }

        public int Cantidad { get; set; }

        public string Estado { get; set; }

        public List<DetalleOrden> ListaDetalles { get; set; }



        public OrdenProduccion()
        { 
         Numero = 0;
            Fecha = DateTime.Today;
            Modelo = String.Empty;
            Cantidad = 0;
            Estado = String.Empty;
            ListaDetalles = new List<DetalleOrden>();
        
        
        }

        public OrdenProduccion(int nro, DateTime fecha, string modelo, int cantidad, string estado, List<DetalleOrden> lista)
        { 
          Numero = nro;
            Fecha = fecha;
            Modelo = modelo;
            Cantidad = cantidad;
            Estado = estado;
            ListaDetalles = lista;
            
        
        }

        public void AgregarDetalle(DetalleOrden det)
        {
            ListaDetalles.Add(det);
        
        }

        public void QuitarDetalle(int nroDetalle)
        {
            ListaDetalles.RemoveAt(nroDetalle);
        
        }
    }
}
