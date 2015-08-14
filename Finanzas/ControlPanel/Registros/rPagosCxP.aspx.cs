using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas.ControlPanel.Registros
{
    public partial class rPagosCxP : System.Web.UI.Page
    {

       public  PagosCxP pag = new PagosCxP();
       public  PagoDetalle Pd = new PagoDetalle();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            int IdP = 0; /// IdPago Detalle Sacado del QueryString
            int.TryParse(Request.QueryString["IdPago"], out IdP);          
            if (!IsPostBack)
            {
                if (pag.Buscar(IdP))
                {
                   
                    TbIdPago.Text = pag.IdCobro.ToString();
                    DdIdCuenta.SelectedIndex = pag.IdCuenta;
                    TbFecha.Text = pag.Fecha.ToString("yyyy-MM-dd");
                    TbValor.Text = pag.Valor.ToString();
                    Buscar(IdP);
                    BtnGuardar.Text = "Actulizar";
                   
                }
               
                DdIdCuenta.DataSource = Cuentas.Lista("IdCuenta,Descripcion ", "Cuentas");
                DdIdCuenta.DataTextField = "Descripcion";
                DdIdCuenta.DataValueField = "IdCuenta";
                DdIdCuenta.DataBind();

                DdIdCxP.DataSource = CxP.Lista("Descripcion,IdCxP ", "CxP");
                DdIdCxP.DataTextField = "Descripcion";
                DdIdCxP.DataValueField = "IdCxP";
                DdIdCxP.DataBind();

            }
        }
        public void Buscar(int ID)
        {
            VistaGridView.DataSource = PagoDetalle.Lista("*", "PagoDetalle where IdPago ="+ID);
            VistaGridView.DataBind();
        }   
        public void OtenerDatos()
        {
            pag.IdCuenta = Convert.ToInt32(DdIdCuenta.SelectedValue);
            pag.Fecha = Convert.ToDateTime(TbFecha.Text);
            pag.Valor = Convert.ToDouble(TbValor.Text);
        }
        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (Session["PagoDetalle"] != null)
            {
                pag = (PagosCxP)Session["PagoDetalle"];
            }
            pag.AgregarDetalle(Convert.ToInt32(DdIdCxP.SelectedValue), Convert.ToDouble(TbValorDetalle.Text));
            VistaGridView.DataSource = pag.pagoDetalle;
            VistaGridView.DataBind();

            Session["PagoDetalle"] = pag;
            LimpiarDetelle();
        }
        public void LimpiarDetelle()
        {
            TbIdPagoDetalle.Text = "";
            TbValorDetalle.Text = "";
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (TbIdPago.Text == string.Empty)
            {


                if (Session["PagoDetalle"] != null)
                {
                    pag = (PagosCxP)Session["PagoDetalle"];
                }
                OtenerDatos();

                if (pag.Insertar())
                {

                    Alert("PagoCxP Se Guardo Corectamente");

                    Limpiar();
                }
                else
                {
                    Alert("PagoCxP No Se Pudo Guardar Corectamente");
                }
            }
            else
            {

                OtenerDatos();
                Limpiar();
                if (pag.Modificar())
                {
                    Alert("PagoCxP Se Modificada Corectamente");
                }
                else
                {
                    Alert("PagoCxP No Se  Modificar Corectamente");
                }
            }
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (TbIdPago.Text == string.Empty)
            {

                Alert("El PagoCxP No Puede Estar Vacio Para Eliminar");

            }
            else
            {
                try
                {
                    if (pag.Eliminar(TbIdPago.Text))
                    {
                        Alert("Se Elimino  PagoCxP Corectamente");

                        Limpiar();
                    }
                    else
                    {
                        Alert("No Se  Elimino PagoCxP Corectamente");
                    }
                }
                catch (Exception a)
                {                 
                    Alert("Este IdPago No Se Puede Eliminar Se Esta Haciendo Referencia En Otra Tabla");
                }
            }

        }
        public void Limpiar()
        {
            TbIdPago.Text = "";
            TbIdPagoDetalle.Text = "";
            TbValorDetalle.Text = "";
            TbValor.Text = "";
            VistaGridView.DataSource = null;
            VistaGridView.DataBind();
        }

        protected void BtnConsulta_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ControlPanel/Consultas/fPagosCxP.aspx");
        }
        public void Alert(string text)
        {

            if (!ClientScript.IsClientScriptBlockRegistered("script"))
                ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('" + text + "')", true);

        }
    }
}