using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas.ControlPanel.Registros
{
    public partial class rPresupuesto : System.Web.UI.Page
    {
       public Presupuesto pre = new Presupuesto();
       public PresupuestoDetalles Pd=new PresupuestoDetalles();

        protected void Page_Load(object sender, EventArgs e)
        {
            int IdP = 0;
            int.TryParse(Request.QueryString["IdPresupuesto"], out IdP);

            if (!IsPostBack)
            {
                if (pre.Buscar(IdP))
                {
                    TbIdPresupuesto.Text = pre.IdPresupuesto.ToString();
                    TbFecha.Text = pre.Fecha.ToString("yyyy-MM-dd");
                    TbDescripcion.Text = pre.Descripcion;
                    Buscar(IdP);
                    BtnGuardar.Text = "Actulizar";
                }
            
                DdIdCategoria.DataSource = Categorias.Lista("Descripcion,IdCategoria ", "Categorias");
                DdIdCategoria.DataTextField = "Descripcion";
                DdIdCategoria.DataValueField = "IdCategoria";
                DdIdCategoria.DataBind();
            }
        }
        
        public void Buscar(int ID)
        {
            VistaGridView.DataSource = PresupuestoDetalles.Lista("*", "PresupuestoDetalles where IdPresupuesto =" + ID);
            VistaGridView.DataBind();
        }
        protected void BtnSiguiente_Click(object sender, EventArgs e)
        {
             if (TbIdPresupuesto.Text == string.Empty)
            {
                if (Session["CobroDetalle"] != null)
                {
                    pre = (Presupuesto)Session["PresupuestoDetalle"];
                }
                OtenerDatos();

                if (pre.Insertar())
                {
                     Alert("Presupuesto Se Guardo Corectamente");
                    
                     VistaGridView.DataSource = null;
                     VistaGridView.DataBind();
                     Limpiar();
                }
                else
                {
                    Alert("Presupuesto No Se Pudo Guardar Corectamente ");
                }
            }
            else
            {

                OtenerDatos();
                Limpiar();
                if (pre.Modificar())
                {
                    Alert("Presupuesto Se Modificada Corectamente");

                }
                else
                {
                    Alert("Presupuesto No Se  Modificar Corectamente");
                }           
            }
        }
        public void OtenerDatos()
        {
            pre.Fecha= Convert.ToDateTime(TbFecha.Text);
            pre.Descripcion=TbDescripcion.Text;
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (TbIdPresupuesto.Text == string.Empty)
            {
                Alert("El IdPresupuesto No Puede Estar Vacio Para Eliminar");
            }
            else
            {
                try
                {
                    if (pre.Eliminar(TbIdPresupuesto.Text))
                    {
                        Alert(" Se Elimino  Presupuesto Corectamente");
                        Limpiar();
                    }
                    else
                    {
                        Alert("No Se  Elimino Presupuesto Corectamente");
                    }
                }
                catch (Exception a)
                {
                    Alert("Este IdPresupuesto No Se Puede Eliminar Se Esta Haciendo Referencia En Otra Tabla");
                }

            }
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        public void LimpiarDetalle()
        {
            TbIdPresupuestoDetalle.Text = "";
            TbValor.Text = "";

        }
        public void Limpiar()
        {
            TbIdPresupuesto.Text = "";
            TbIdPresupuestoDetalle.Text = "";
            TbValor.Text = "";
            TbDescripcion.Text = "";
            VistaGridView.DataSource = null;
            VistaGridView.DataBind();
            
        }
        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (Session["PresupuestoDetalle"] != null)
            {
                pre = (Presupuesto)Session["PresupuestoDetalle"];
            }

            pre.AgregarDetalle(Convert.ToInt32(DdIdCategoria.SelectedValue), Convert.ToDouble(TbValor.Text));

            VistaGridView.DataSource = pre.presupuestoDetalle;
            VistaGridView.DataBind();

            Session["PresupuestoDetalle"] = pre;
            LimpiarDetalle();
        }

        protected void BtnConsulta_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ControlPanel/Consultas/fPresupuesto.aspx");
        }
        public void Alert(string text)
        {

            if (!ClientScript.IsClientScriptBlockRegistered("script"))
                ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('" + text + "')", true);

        }
    }
}