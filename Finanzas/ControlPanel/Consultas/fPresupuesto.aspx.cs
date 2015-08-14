using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas.ControlPanel.Consultas
{
    public partial class fPresupuesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Tipo() == "Fecha")
            {
                TbDesde.Visible = true;
                TbHasta.Visible = true;
                Lde.Visible = true;
            }
            else
            {
                TbDesde.Visible = false;
                TbHasta.Visible = false;
                Lde.Visible = false;
            }
        }


        protected void BtnConsulta_Click(object sender, EventArgs e)
        {
           
            if (Tipo() == "Fecha")
            {
                GridViewVista.DataSource = PagosCxP.ListaF(TbDesde.Text, TbHasta.Text);
                GridViewVista.DataBind();
            }
            else
            {
                GridViewVista.DataSource = PagosCxP.Lista("*", "Presupuestos Where " + Tipo() + " like'" + TbBuscar.Text + "%'");
                GridViewVista.DataBind();

            }
        }
        public string Tipo()
        {
            string tipo = "";
            switch (TipoDropDownList.SelectedIndex){
            
                case 0:
                          tipo = "IdPresupuesto";
                    break;
                case 1:
                          tipo = "Fecha";
                      break;
                case 2:
                         tipo = "Descripcion";
                     break;
                }        
                return tipo;
            
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            GridViewVista.DataSource = null;
            GridViewVista.DataBind();
        }
    }
}