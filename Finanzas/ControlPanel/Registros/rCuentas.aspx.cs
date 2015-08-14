using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas
{
    public partial class rCuentas : System.Web.UI.Page
    {
        
        Cuentas c = new Cuentas();
     
        protected void Page_Load(object sender, EventArgs e)
        {
            int IdCuenta=0;
            if (!IsPostBack)
            {


                int.TryParse(Request.QueryString["IdCuenta"], out IdCuenta);

                if (c.Buscar(IdCuenta.ToString()))
                {

                    TbIdCuenta.Text= c.IdCuenta.ToString();
                    TbDescripcion.Text = c.Descripcion;
                    TbBalence.Text= c.Balance.ToString();
                    BtnGuardar.Text = "Actualizar";
                   
                    

                }
            }
        
        }
       
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (TbIdCuenta.Text == string.Empty)
            {

                OtenerDatos();


                if (c.Insertar())
                {
                  
                    Alert("Cuenta Se Guardo Corectamente");

                    Limpiar(); 
                 
                }
                else
                {

                    Alert("Cuenta No Se Pudo Guardar Corectamente");
                }
            }

            else
            {

                OtenerDatos();

                if (c.Modificar(TbIdCuenta.Text))
                {
                    
                    Alert("Cuenta Se Modificada Corectamente");

                }
                else
                {
                    
                    Alert("Cuenta No Se  Modificar Corectamente");
                }
            }
        }
       

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TbIdCuenta.Text == string.Empty)
                {

                   
                    Alert("El IdCuenta No Puede Estar Vacio Para Eliminar");

                }
                else
                {
                    if (c.Eliminar(TbIdCuenta.Text))
                    {
                      
                        Alert("No  Se Eliminada Cuenta Corectamente");

                        Limpiar();
                    }
                    else
                    {
                       
                        Alert(" No Se  Eliminar  Cuenta Corectamente");
                    }
                }
            }
            catch (Exception)
            {
                
                Alert("No Se Puede Eliminar Esta IdCuenta Porque Esta Haciendeo Referencia En Otro Tabla");
            }
           
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        public void OtenerDatos()
        {
            c.Descripcion = TbDescripcion.Text;
            c.Balance = Convert.ToDouble(TbBalence.Text);
        }
        public void Limpiar()
        {
            TbIdCuenta.Text = "";
            TbDescripcion.Text = "";
            TbBalence.Text = "";
        }

        protected void BtnConsulta_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ControlPanel/Consultas/fCuentas.aspx");
        }
        public void Alert(string text)
        {

            if (!ClientScript.IsClientScriptBlockRegistered("script"))
                ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('" + text + "')", true);

        }
    }
}