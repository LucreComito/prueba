using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Parcial2023.Datos
{
    public class DBHelper
    {
        private static DBHelper instancia = null;
        private SqlConnection conexion;
        private DBHelper()
        {
            conexion = new SqlConnection(@"Data Source=DESKTOP-BLN6TUB;Initial Catalog=Parcial1_PrograII;Integrated Security=True");
            //conexion = new SqlConnection(@"Data Source=172.16.10.196;Initial Catalog=Produccion;User ID=alumno1w1;Password=alumno1w1");
        }
        
        public static DBHelper GetInstancia()
        {
            if (instancia == null)
                instancia = new DBHelper();
            return instancia;
        }

        //parte agregada:
        public SqlConnection GetConnection()
        {
            return conexion;
        }
        //////////////-----------------------------
        public DataTable Consultar(string nombreSP)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = nombreSP;
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;   
        }

        public int ConsultarEscalar(string nombreSP, string paramSalida)
        {
            conexion.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = nombreSP;
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = paramSalida;
            parametro.SqlDbType = SqlDbType.Int;
            parametro.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(parametro);
            cmd.ExecuteNonQuery();

            conexion.Close();
            return (int)parametro.Value;
        }


    }
}
