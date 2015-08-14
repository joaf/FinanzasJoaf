using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas.ControPanel.Registros
{
    public partial class rCxP : System.Web.UI.Page
    {
       
        public CxP cp = new CxP();
        public Cuentas c = new Cuentas();

        protected void Page_Load(object sender, EventArgs e)
        {
             int IdCxP = 0;
            if (!IsPostBack)
            {
                DdIdCuenta.DataSource = Cuentas.Lista("IdCuenta,Descripcion", "Cuentas");
                DdIdCuenta.DataTextField = "Descripcion";
                DdIdCuenta.DataValueField = "IdCuenta";
                DdIdCuenta.DataBind();

                int.TryParse(Request.QueryString["IdCxP"], out IdCxP);

                if (cp.Buscar(IdCxP))
                {
                    TbIdCxP.Text = cp.IdCxP.ToString();
                    DdIdCuenta.SelectedIndex = cp.IdCuenta;
                    TbFecha.Text = cp.Fecha.ToString("yyyy-MM-dd");
                    TbValor.Text = cp.Valor.ToString();
                    TbBalance.Text = cp.Balance.ToString();
                    TbDescripcion.Text = cp.Descripcion;

                    BtnGuardar.Text = "Actualizar";


                }

                if (DdIdCuenta.SelectedValue != "")
                {
                    if (c.Buscar(DdIdCuenta.SelectedValue) == true)
                        TbBalance.Text = c.Balance.ToString();

                }
            }
          
        }
        
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (TbIdCxP.Text == string.Empty)
            {

                ObtenerDatos();

                if (cp.Insertar())
                {
                    Alert("Cuenta Por Pagar Se Guardo Corectamente");

                    Limpiar();


                }
                else
                {
                    
                    Alert("No Cuenta Por Pagar Se Pudo Guardar Corectamente ");
                }
            }

            else
            {

                ObtenerDatos();
                if (cp.Modificar(TbIdCxP.Text))
                {
                  
                    Alert("Cuenta Por Pagar Se Modificada Corectamente");


                }
                else
                {
                    Alert("Cuenta Por Pagar No Se  Modificar Corectamente");
                }
            }
        }
        public void ObtenerDatos()
        {
            cp.IdCuenta = Convert.ToInt32(DdIdCuenta.SelectedValue);
            cp.Fecha = Convert.ToDateTime(TbFecha.Text);
            cp.Valor = Convert.ToDouble(TbValor.Text);
            cp.Balance = Convert.ToDouble(TbBalance.Text);
            cp.Descripcion = TbDescripcion.Text;
        }
        public void Limpiar()
        {

            TbIdCxP.Text = "";
            TbValor.Text = "";
            TbDescripcion.Text = "";
            TbBalance.Text = "";

        }
        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TbIdCxP.Text == string.Empty)
                {

                    Alert("El IdCxP No Puede Estar Vacio Para Eliminar");

                }
                else
                {
                    if (cp.Eliminar(TbIdCxP.Text))
                    {
                        
                        Alert("Cuenta Por Pagar NO Se Eliminada Corectamente");

                        Limpiar();
                    }
                    else
                    {
                        Alert("Cuenta Por Pagar No Se  Eliminar Cuenta Corectamente");
                    }
                }
            }
            catch (Exception a)
            {
              
                Alert("No Se Puede Eliminar Esta CxP Porque Esta Haciendeo Referencia En Otro Tabla");
            }
           
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void BtnConsultas_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ControlPanel/Consultas/fCxP.aspx");
        }

        
        public void Alert(string text)
        {

            if (!ClientScript.IsClientScriptBlockRegistered("script"))
                ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('" + text + "')", true);

        }

        protected void DdIdCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdIdCuenta.SelectedValue != "")
            {
                if (c.Buscar(DdIdCuenta.SelectedValue) == true)
                    TbBalance.Text = c.Balance.ToString();

            }
        }

        
    }
}