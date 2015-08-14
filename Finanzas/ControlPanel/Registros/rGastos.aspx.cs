using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas.ControPanel.Registros
{
    public partial class rGastos : System.Web.UI.Page
    {
   
        public Gastos gas = new Gastos();
        protected void Page_Load(object sender, EventArgs e)
        {
            int IdGasto=0;
            if (!IsPostBack)
            {


                int.TryParse(Request.QueryString["IdGasto"], out IdGasto);

                if (gas.Buscar(IdGasto))
                {
                    TbIdGasto.Text = Convert.ToString(IdGasto);
                    DdIdCuenta.SelectedIndex = gas.IdCuenta;
                    DdIdCategoria.SelectedIndex = gas.IdCategoria;
                    TbFecha.Text = gas.Fecha.ToString("yyyy-MM-dd");
                    TbValor.Text = gas.Valor.ToString();
                    TbDescripcion.Text = gas.Descripcion;

                    BtnGuardar.Text = "Actualizar";



                }
                DdIdCategoria.DataSource = Categorias.Lista("IdCategoria,Descripcion", "Categorias");
                DdIdCategoria.DataTextField = "Descripcion";
                DdIdCategoria.DataValueField = "IdCategoria";
                DdIdCategoria.DataBind();

                DdIdCuenta.DataSource = Cuentas.Lista("IdCuenta,Descripcion", "Cuentas");
                DdIdCuenta.DataTextField = "Descripcion";
                DdIdCuenta.DataValueField = "IdCuenta";
                DdIdCuenta.DataBind();

            }
           
            

        }
       
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (TbIdGasto.Text == string.Empty)
            {

                ObtenerDatos();


                if (gas.Insertar())
                {

                    Alert("Gasto Se Guardo Corectamente");
                  
                    gas.CuentaResta(Convert.ToInt32(DdIdCuenta.SelectedValue), Convert.ToInt32(TbValor.Text));
                    Limpiar(); 
                }
                else
                {                 
                    Alert("Gasto No Se Pudo Guardar Corectamente");
                }
            }

            else
            {

                ObtenerDatos();

                if (gas.Modificar(TbIdGasto.Text))
                {
                    Alert("Gasto Se Modificada Corectamente");
                }
                else
                {                 
                    Alert("Cuenta No Se  Modificar Corectamente");
                }
            }
        }
        public void ObtenerDatos()
        {
            gas.IdCuenta = Convert.ToInt32(DdIdCuenta.SelectedValue);
            gas.IdCategoria = Convert.ToInt32(DdIdCategoria.SelectedValue);
            gas.Fecha = Convert.ToDateTime(TbFecha.Text);
            gas.Valor = Convert.ToDouble(TbValor.Text);
            gas.Descripcion = TbDescripcion.Text;

        }
        public void Limpiar()
        {
            TbIdGasto.Text = "";
            TbDescripcion.Text = "";
            TbValor.Text = "";
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TbIdGasto.Text == string.Empty)
                {


                    Alert("El IdGasto No Puede Estar Vacio Para Eliminar");

                }
                else
                {
                    if (gas.Eliminar(TbIdGasto.Text))
                    {

                        Alert("Gasto Se Eliminada Corectamente");

                        Limpiar();
                    }
                    else
                    {

                        Alert("Gasto No Se  Eliminar  Corectamente");
                    }
                }
            }
            catch (Exception a)
            {

                Alert("No Se Puede Eliminar Esta IdGasto Porque Esta Haciendeo Referencia En Otro Tabla");
            }
           
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void BtnConsulta_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ControlPanel/Consultas/fGastos.aspx");
        }
        public void Alert(string text)
        {

            if (!ClientScript.IsClientScriptBlockRegistered("script"))
                ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('" + text + "')", true);

        }       

    }
}