using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas
{
    public partial class rCobroCxC : System.Web.UI.Page
    {
        CobroCxC Cob = new CobroCxC();
        CobroDetalle Cd = new CobroDetalle();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            int IdC = 0;// IdCobro De QueyString;
            
            if (!IsPostBack)
            {
                

                int.TryParse(Request.QueryString["IdCobro"], out IdC);
                if (Cob.Buscar(IdC))
                {
                    TbIdCobro.Text = Cob.IdCobro.ToString();
                    DdIdCuenta.SelectedIndex = Cob.IdCuenta;
                    TbFecha.Text = Cob.Fecha.ToString("yyyy-MM-dd");
                    TbValor.Text = Cob.Valor.ToString();


                    BtnGuardar.Text = "Actulizar";
                    Buscar(IdC);

                } 
                 
               
               
               
            }
                DdIdCuenta.DataSource = Cuentas.Lista("IdCuenta,Descripcion ", "Cuentas");
                DdIdCuenta.DataTextField = "Descripcion";
                DdIdCuenta.DataValueField = "IdCuenta";
                DdIdCuenta.DataBind();

                DdIdCxC.DataSource = CxC.Lista("Descripcion,IdCxC ", "CxC");
                DdIdCxC.DataTextField = "Descripcion";
                DdIdCxC.DataValueField = "IdCxC";
                DdIdCxC.DataBind();


            
        }
        public void Buscar(int ID)
        {
            

            VistaGridView.DataSource = CobroDetalle.Lista("*", "CobroDetalle where IdCobro =" + ID);
            VistaGridView.DataBind();
            
        }

        protected void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (TbIdCobro.Text == string.Empty)
            {


                if (Session["CobroDetalle"] != null)
                {
                    Cob = (CobroCxC)Session["CobroDetalle"];
                }
                OtenerDatos();

                if (Cob.Insertar())
                {
                    
                    Alert("CobroCxC Se Guardo Corectamente");

                    Limpiar();
                }
                else
                {
                  
                    Alert("CobroCxC No Se Pudo Guardar Corectamente");
                }
            }
            else
            {

                OtenerDatos();
                Limpiar();
                if (Cob.Modificar())
                {
                    
                    Alert("CobroCxC Se Modificada Corectamente");

                }
                else
                {
                    Alert("CobroCxC No Se  Modificar Corectamente");
                }


            }

        }
        public void OtenerDatos()
        {
            


            Cob.IdCuenta = Convert.ToInt32(DdIdCuenta.SelectedValue);
            Cob.Fecha = Convert.ToDateTime(TbFecha.Text);
            Cob.Valor = Convert.ToDouble(TbValor.Text);
            
        }
        public void Limpiar()
        {
            TbIdCobro.Text = "";
            TbIdCobroDetalle.Text = "";
            TbValorDetalle.Text = "";
            TbValor.Text = "";
            VistaGridView.DataSource = null;
            VistaGridView.DataBind();
        }
        protected void BtnAgregar_Click(object sender, EventArgs e)
        {


            if (Session["CobroDetalle"] != null)
            {
                Cob = (CobroCxC)Session["CobroDetalle"];
            }

            Cob.AgregarDetalle(Convert.ToInt32(DdIdCxC.SelectedValue), Convert.ToDouble(TbValorDetalle.Text));

            VistaGridView.DataSource = Cob.cobroDetalle;
            VistaGridView.DataBind();

            Session["CobroDetalle"] = Cob;
           
            LimpiarDetalle();
        }
        public void LimpiarDetalle()
        {
            TbIdCobroDetalle.Text = "";
            TbValorDetalle.Text = "";
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (TbIdCobro.Text == string.Empty)
            {

               
                Alert("El IdCobro No Puede Estar Vacio Para Eliminar");

            }
            else
            {
                try
                {
                    if (Cob.Eliminar(TbIdCobro.Text))
                    {
                        
                        Alert(" Se Elimino  CobroCxC Corectamente");
                        Limpiar();
                    }
                    else
                    {
                        
                        Alert("No Se  Elimino CobroCxC Corectamente");
                    }
                }
                catch (Exception a)
                {
                    
                    Alert("Este IdCobroDetalle No Se Puede Eliminar Se Esta Haciendo Referencia En Otra Tabla");
                }

            }
        }

        protected void BtnConsulta_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ControlPanel/Consultas/fCobrosCxC.aspx");
        }
        public void Alert( string text) 
        {

            if (!ClientScript.IsClientScriptBlockRegistered("script"))
                ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('"+ text+"')", true);
            
        }

        protected void VistaGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdCd = 0;
            int.TryParse(Request.QueryString["IdCobroDetalle"], out IdCd);
            if (Cd.Buscar(IdCd))
            {
                DdIdCxC.SelectedIndex = Cd.IdCxC;
                TbValorDetalle.Text = Cd.Valor.ToString();

                BtnGuardar.Text = "Actulizar";
            }
        }
    }
}