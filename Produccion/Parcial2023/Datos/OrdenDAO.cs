using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Parcial2023.Dominio;

using Produccion.Datos;
using Produccion.Domino;

namespace Parcial2023.Datos
{
    internal class OrdenDAO : IOrdenDao
    {
        //public int ordenNro = 0;
        public bool CrearOrden(OrdenProduccion equipo)
        {
            bool aux = true;

            SqlConnection conexion = DBHelper.GetInstancia().GetConnection();
            SqlTransaction t = null;
            try
            {
                conexion.Open();
                t = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand("SP_INSERTAR_ORDEN", conexion, t);
                comando.CommandType = CommandType.StoredProcedure;

                SqlParameter p = new SqlParameter("@prox_orden", SqlDbType.Int);

                p.Direction = System.Data.ParameterDirection.Output;
                comando.Parameters.Add(p);

                comando.Parameters.AddWithValue("@fecha", equipo.Fecha);
                comando.Parameters.AddWithValue("@modelo", equipo.Modelo);
                comando.Parameters.AddWithValue("@estado", equipo.Estado);
                comando.Parameters.AddWithValue("@cantidad", equipo.Cantidad);
                comando.ExecuteNonQuery();

                int nroOrden = (int)p.Value;

                foreach (DetalleOrden det in equipo.ListaDetalles)
                {
                    SqlCommand comando2 = new SqlCommand("SP_INSERTAR_DETALLE", conexion, t);
                    comando2.CommandType = CommandType.StoredProcedure;

                    comando2.Parameters.AddWithValue("@nro_orden", nroOrden);
                    comando2.Parameters.AddWithValue("@Id", det.Id);
                    comando2.Parameters.AddWithValue("@Componente", det.Componente.Codigo);
                    comando2.Parameters.AddWithValue("@Cantidad", det.Cantidad);

                    comando2.ExecuteNonQuery();

                }
                t.Commit();


             }
            catch (Exception ex)
            {
                if (t != null)
                {
                    aux = false;
                    t.Rollback();

                }


            }
             finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                { 
                  conexion.Close();
                
                }
        }

            return aux;
        }


        






        /////////////////////////////////////////////////////////
        public List<Componente> ObtenerComponentes()
        {
            DataTable tabla = DBHelper.GetInstancia().Consultar("SP_CONSULTAR_COMPONENTES");
            List<Componente> LComponentes = new List<Componente>();
            ////mapear un registro de la tabla de BD a un objeto del modelo de dominio

            foreach (DataRow fila in tabla.Rows)
            {
                int cod = int.Parse(fila["cod_componente"].ToString());//como esta en bd
                string nombre = fila["nom_componente"].ToString();

                //int stock = Convert.ToInt32(fila["stock"]);

                Componente Objcomponente = new Componente(cod, nombre);
                LComponentes.Add(Objcomponente);
            
            }
            return LComponentes;


            //public int ObtenerNroNuevaOrdenRetiro()
            //{
            //    return ordenNro;
            //}



            //public int ObtenerProximoPresupuesto()
            //{
            //    return HelperDao.ObtenerInstancia().ConsultarEscalar("SP_PROXIMO_ID", "@next");
            //}

        }
    }
}
