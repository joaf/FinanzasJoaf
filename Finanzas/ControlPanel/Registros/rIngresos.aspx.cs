using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas.ControPanel.Registros
{
    public partial class rIngresos : System.Web.UI.Page
    {
        
        public Ingresos ing = new Ingresos();

        protected void Page_Load(object sender, EventArgs e)
        {
            int IdIngreso = 0;
            if (!IsPostBack)
            {


                int.TryParse(Request.QueryString["IdIngreso"], out IdIngreso);

                if (ing.Buscar(IdIngreso))
                {
                    TbIdIngreso.Text = Convert.ToString(IdIngreso);
                    DdIdCuenta.SelectedIndex = ing.IdCuenta;
                    DdIdCategoria.SelectedIndex = ing.IdCategoria;
                    TbFecha.Text = ing.Fecha.ToString("yyyy-MM-dd");
                    TbValor.Text = ing.Valor.ToString();
                    TbDescripcion.Text = ing.Descripcion;

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
            if (TbIdIngreso.Text == string.Empty)
            {

                ObtenerDatos();


                if (ing.Insertar())
                {

                    Alert("Ingreso Se Guardo Corectamente");
                    ing.CuentaSuma(Convert.ToInt32(DdIdCuenta.SelectedValue), Convert.ToInt32(TbValor.Text));
                    Limpiar();
                }
                else
                {
                    Alert("Ingreso No Se Pudo Guardar Corectamente");
                }
            }

            else
            {

                ObtenerDatos();

                if (ing.Modificar(TbIdIngreso.Text))
                {

                    Alert("Ingreso Se Modificada Corectamente");

                }
                else
                {
                    Alert("Ingreso No Se  Modificar Corectamente");
                }
            }
        }
        public void ObtenerDatos()
        {
            ing.IdCuenta = Convert.ToInt32(DdIdCuenta.SelectedValue);
            ing.IdCategoria = Convert.ToInt32(DdIdCuenta.SelectedValue);
            ing.Fecha = Convert.ToDateTime(TbFecha.Text);
            ing.Valor = Convert.ToDouble(TbValor.Text);
            ing.Descripcion = TbDescripcion.Text;

        }
        public void Limpiar()
        {
            TbIdIngreso.Text = "";
            TbValor.Text = "";
            TbDescripcion.Text = "";
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TbIdIngreso.Text == string.Empty)
                {
                    Alert("El IdIngreso No Puede Estar Vacio Para Eliminar");
                }
                else
                {
                    if (ing.Eliminar(TbIdIngreso.Text))
                    {
                      
                        Alert("Ingreso Se Eliminada Corectamente");

                        Limpiar();
                    }
                    else
                    {
                        Alert("Ingreso No Se  Eliminar  Corectamente");
                    }
                }
            }
            catch (Exception a)
            {
                Alert("No Se Puede Eliminar Esta IdIngreso Porque Esta Haciendeo Referencia En Otro Tabla");

            }
            
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void BtnConsulta_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ControlPanel/Consultas/fIngresos.aspx");
        }
        public void Alert(string text)
        {

            if (!ClientScript.IsClientScriptBlockRegistered("script"))
                ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('" + text + "')", true);

        } 

    }
}
