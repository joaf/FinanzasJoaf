using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas.ControPanel.Registros
{
    public partial class rCxC : System.Web.UI.Page
    {
      
        public CxC cc = new CxC();
        public Cuentas c = new Cuentas();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            int IdCxC = 0;
            if (!IsPostBack)
            {
                DdIdCuenta.DataSource = Cuentas.Lista("IdCuenta,Descripcion", "Cuentas");
                DdIdCuenta.DataTextField = "Descripcion";
                DdIdCuenta.DataValueField = "IdCuenta";
                DdIdCuenta.DataBind();


                int.TryParse(Request.QueryString["IdCxC"], out IdCxC);

                if (cc.Buscar(IdCxC))
                {
                    TbIdCxC.Text = cc.IdCxC.ToString();
                    DdIdCuenta.SelectedIndex = cc.IdCuenta;
                    TbFecha.Text = cc.Fecha.ToString("yyyy-MM-dd");
                    TbValor.Text = cc.Valor.ToString();
                    TbBalance.Text = cc.Balance.ToString();
                    TbDescripcion.Text = cc.Descripcion;
                  
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
            if (TbIdCxC.Text == string.Empty)
            {

                ObtenerDatos();


                if (cc.Insertar())
                {

                    Alert("Cuenta Por Cobrar Se Guardo Corectamente");
                    Limpiar(); 


                }
                else
                {
                    Alert("No Cuenta Por Cobrar Se Pudo Guardar Corectamente ");
                }
            }

            else
            {

                ObtenerDatos();

                if (cc.Modificar(TbIdCxC.Text))
                {

                    Alert("Cuenta Por Cobrar Se Modificada Corectamente");

                }
                else
                {
                    Alert("Cuenta Por Cobar No Se  Modificar Corectamente");
                }
            }
        }
        public void ObtenerDatos()
        {
            cc.IdCuenta =Convert.ToInt32(DdIdCuenta.SelectedValue);
            cc.Fecha = Convert.ToDateTime(TbFecha.Text);
            cc.Valor = Convert.ToDouble(TbValor.Text);
            cc.Balance = Convert.ToDouble(TbBalance.Text);
            cc.Descripcion = TbDescripcion.Text;
        }
        public void Limpiar(){

            TbIdCxC.Text="";
            TbValor.Text = "";
            TbDescripcion.Text = "";
            TbBalance.Text = "";
            
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TbIdCxC.Text == string.Empty)
                {

                   
                    Alert("El IdCxC No Puede Estar Vacio Para Eliminar");

                }
                else
                {
                    if (cc.Eliminar(TbIdCxC.Text))
                    {
                       
                        Alert("Cuenta Por Cobrar Se Eliminada Corectamente");

                        Limpiar();
                    }
                    else
                    {
                        Alert("Cuenta Por Cobar No Se  Eliminar Cuenta Corectamente");
                    }
                }
            }
            catch (Exception)
            {
              Alert("No Se Puede Eliminar Esta Cuenta Porque Esta Haciendeo Referencia En Otro Tabla");
            }
           
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void BtnConsulta_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ControlPanel/Consultas/fCxC.aspx");
        }

        protected void DdIdCuenta_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DdIdCuenta.SelectedValue != "")
            {
                if (c.Buscar(DdIdCuenta.SelectedValue) == true)
                    TbBalance.Text = c.Balance.ToString();

            }
        }
        public void Alert(string text)
        {

            if (!ClientScript.IsClientScriptBlockRegistered("script"))
                ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('" + text + "')", true);

        }

        
    }
}