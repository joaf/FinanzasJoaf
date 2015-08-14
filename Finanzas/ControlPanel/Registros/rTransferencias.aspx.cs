using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas.ControPanel.Registros
{
    public partial class rTransferencias : System.Web.UI.Page
    {
        public Transferencias tran = new Transferencias();

        protected void Page_Load(object sender, EventArgs e)
        {
            int IdTransferencia = 0;
            if (!IsPostBack)
            {


                int.TryParse(Request.QueryString["IdTransferencia"], out IdTransferencia);

                if (tran.Buscar(IdTransferencia))
                {
                    TbIdTransferencia.Text = IdTransferencia.ToString();
                    DdIdCuentaOrigen.SelectedIndex = tran.IdCuentaOrigen;
                    DdIdCuentaDestion.SelectedIndex = tran.IdCuentaDestino;
                    TbFecha.Text = tran.Fecha.ToString("yyyy-MM-dd");
                    TbValor.Text = tran.Valor.ToString();
                    TbDescripcion.Text = tran.Descripcion;

                    BtnGuardar.Text = "Actualizar";
                }
            }
            DdIdCuentaDestion.DataSource = Cuentas.Lista("IdCuenta,Descripcion", "Cuentas");
            DdIdCuentaDestion.DataTextField = "Descripcion";
            DdIdCuentaDestion.DataValueField = "IdCuenta";
            DdIdCuentaDestion.DataBind();

            DdIdCuentaOrigen.DataSource = Cuentas.Lista("IdCuenta,Descripcion", "Cuentas");
            DdIdCuentaOrigen.DataTextField = "Descripcion";
            DdIdCuentaOrigen.DataValueField = "IdCuenta";
            DdIdCuentaOrigen.DataBind();
        }
        
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (TbIdTransferencia.Text == string.Empty)
            {

                ObtenerDatos();
                if (tran.Insertar())
                {

                    Alert("Transferencia Se Guardo Corectamente");
                    tran.TransferenciaDestino(Convert.ToInt32(DdIdCuentaDestion.SelectedValue), Convert.ToInt32(TbValor.Text));

                    tran.TransferenciaOrigen(Convert.ToInt32(DdIdCuentaOrigen.SelectedValue), Convert.ToInt32(TbValor.Text));
                    Limpiar();
                }
                else
                {
                    Alert("Transferencia No Se Pudo Guardar Corectamente");
                }
            }

            else
            {
                ObtenerDatos();

                if (tran.Modificar(TbIdTransferencia.Text))
                {
                    Alert("Transferencia Se Modificada Corectamente");
                }
                else
                {
                    Alert("Transferencia No Se  Modificar Corectamente");
                }
            }
        }
        public void ObtenerDatos()
        {
            tran.Fecha = Convert.ToDateTime(TbFecha.Text);
            tran.IdCuentaOrigen = Convert.ToInt32(DdIdCuentaOrigen.SelectedValue);
            tran.IdCuentaDestino = Convert.ToInt32(DdIdCuentaDestion.SelectedValue);
            tran.Descripcion = TbDescripcion.Text;
            tran.Valor = Convert.ToInt32(TbValor.Text);
        }
        public void Limpiar()
        {
            TbIdTransferencia.Text = "";
            TbValor.Text = "";
            TbDescripcion.Text = "";
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TbIdTransferencia.Text == string.Empty)
                {
                    Alert("El IdTransferencia No Puede Estar Vacio Para Eliminar");
                }
                else
                {
                    if (tran.Eliminar(TbIdTransferencia.Text))
                    {
                        Alert("Transferencia Se Eliminada Corectamente");
                        Limpiar();
                    }
                    else
                    {                    
                        Alert("Transferencia No Se  Eliminar  Corectamente");
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
            Response.Redirect("/ControlPanel/Consultas/fTransferencias.aspx");
        }
        public void Alert(string text)
        {
            if (!ClientScript.IsClientScriptBlockRegistered("script"))
                ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('" + text + "')", true);
        }
    }
}