using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Parcial2023.Datos;
using Parcial2023.Dominio;
using Parcial2023.Servicios;

using Produccion.Domino;

//COMPLETAR --> Curso:      Legajo:         Apellido y Nombre:

//CadenaDeConexion: "Data Source=172.16.10.196;Initial Catalog=Produccion;User ID=alumno1w1;Password=alumno1w1"

namespace Produccion.Presentacion
{
    public partial class FrmAlta : Form

    {
        //private OrdenDAO mavi = new OrdenDAO();
        private OrdenDAO DAO;
        private OrdenProduccion nuevaOrden;
        int auxDetalle;


        public FrmAlta()
        {
            InitializeComponent();
            DAO = new OrdenDAO();
            nuevaOrden = new OrdenProduccion();
            auxDetalle = 1;

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void FrmAlta_Load(object sender, EventArgs e)
        {
            CargarCombo();
            dtpFecha.Value = DateTime.Today;

            //txtFecha.Text = DateTime.Today.ToShortDateString();
            //txtCliente.Text = "Consumidor Final";
            //txtDescuento.Text = "0";
            //txtCantidad.Text = "1";
            //lblPresupuestoNro.Text = lblPresupuestoNro.Text + " " + servicio.TraerProximoPresupuesto().ToString();

            //CargarProductos();


        }

        private void CargarCombo()
        {
            List<Componente> LCombo = DAO.ObtenerComponentes();
            cboComponente.DataSource = LCombo;
            cboComponente.DropDownStyle = ComboBoxStyle.DropDownList;

            //cboMateriales.DataSource = dao.ObtenerMateriales();
            //cboMateriales.ValueMember = "codigo";
            //cboMateriales.DisplayMember = "nombre";

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Componente ComponenteCBO  = (Componente)cboComponente.SelectedItem;

            //if (existeEnGrilla(ComponenteCBO.NombreCompo))
            //{
            //    MessageBox.Show("Ese componente ya esta cargado");
            //    return;
            //}
            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                if (fila.Cells["colComponente"].Value.ToString().Equals(cboComponente.Text))
                {

                    MessageBox.Show("Este componente ya está presupuestado.", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }



            if (string.IsNullOrEmpty(txtDT.Text))
            
            {
                MessageBox.Show("Debe ingresar un modelo");
                return ;
            }

            if (nudCantidad.Value < 1)
            {
                MessageBox.Show("Debe ingresar una cantidad de componentes");
                return;
            }


         


            //if (string.IsNullOrEmpty(txtCantidad.Text) || !int.TryParse(txtCantidad.Text, out _))
            //{
            //    MessageBox.Show("Debe ingresar una cantidad válida...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}





            int cantidad = int.Parse(nudCantidad.Value.ToString());
            
            DetalleOrden det = new DetalleOrden(auxDetalle, ComponenteCBO, cantidad);
           
            int total = Convert.ToInt32(nudCantidad.Value * numericUpDown1.Value);
            
            nuevaOrden.AgregarDetalle(det);
            
            dgvDetalles.Rows.Add(new object[] {det.Componente, det.Cantidad, total,"quitar" });
            
            auxDetalle++;

            /*  int cantidad = int.Parse(nudCantidad.Value.ToString());
           
            DetalleOrden det = new DetalleOrden(compo,cantidad);
            
            ObjecOrdenPro.AgregarDetalle(det);
            
            dgvDetalles.Rows.Add(new object[] { compo.NombreComponente,  cantidad,cantidad, "Quitar" });
             */
        }

        private bool existeEnGrilla(string nombreCompo)
        {
            foreach (DataGridViewRow fila in dgvDetalles.Rows)
            {
                // Leo  if (fila.Cells["colComponente"].Value.Equals(text))
                // Cecilia if (fila.Cells["colComponente"].Value.ToString().Equals(nombre))
                if (fila.Cells["colComponente"].Value.ToString().Equals(nombreCompo))
                {
                    return true;
                }
               
            
            }
            return false;

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dtpFecha.Value < DateTime.Today)
            {
                MessageBox.Show("No puede ingresar fecha inferior a hoy");
                return;

            }
            if (dgvDetalles.Rows.Count < 2)
            {
                MessageBox.Show("Debe ingresar como minimo dos componentes");
                return ;
            }

            //if (dgvDetalles.Rows.Count == 0)
            //{
            //    MessageBox.Show("Debe ingresar al menos un componente...", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}



            nuevaOrden.Modelo = txtDT.Text;
            nuevaOrden.Fecha = dtpFecha.Value;
            nuevaOrden.Cantidad = Convert.ToInt32(nudCantidad.Value);
            nuevaOrden.Estado = "Creada";

            //nuevo.Fecha = Convert.ToDateTime(txtFecha.Text);
            //nuevo.Cliente = txtCliente.Text;
            //nuevo.Descuento = Convert.ToDouble(txtDescuento.Text);

            if (DAO.CrearOrden(nuevaOrden))
            {
                MessageBox.Show("¡La orden se cargo exitosamente!");
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No pudo confirmarse la orden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            //ejemplos de como limpiar
            //dgvDetalles.Rows.Clear();
            //nuevo.detalleOrden.Clear();
            //txtResp.Text = string.Empty;
            //nudCantidad.Text = "1";
            //cboMateriales.SelectedIndex = 0;
            //txtCliente.Text = "Consumidor Final";
            //txtDescuento.Text = "0";
            //txtCantidad.Text = "1";


        }

        private void LimpiarCampos()
        {
            dtpFecha.Value = DateTime.Today;
            txtDT.Text = "";
            nudCantidad.Value = 1;
            cboComponente.SelectedIndex = -1;
            numericUpDown1.Value = 1;
            dgvDetalles.Rows.Clear();


        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 3)
            {
                auxDetalle--;
                int index = dgvDetalles.CurrentRow.Index;
               
                nuevaOrden.QuitarDetalle(index);

                dgvDetalles.Rows.RemoveAt(index);
            
            
            }


        }


        //private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgvDetalles.CurrentCell.ColumnIndex == 4) //boton Quitar de la grilla
        //    {
        //        nuevo.QuitarDetalle(dgvDetalles.CurrentRow.Index);
        //        dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);
        //        CalcularTotales();
        //    }
        //}


        //private void CalcularTotales()
        //{
        //    txtSubTotal.Text = nuevo.CalcularTotal().ToString();
        //    if (!string.IsNullOrEmpty(txtDescuento.Text) && int.TryParse(txtDescuento.Text, out _))
        //    {
        //        double desc = nuevo.CalcularTotal() * Convert.ToDouble(txtDescuento.Text) / 100;
        //        txtTotal.Text = (nuevo.CalcularTotal() - desc).ToString();
        //    }
        //}




    }



}

