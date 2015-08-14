using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
namespace Finanzas.ControlPanel.Consultas
{
    public partial class fCobrosCxC : System.Web.UI.Page
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
                GridViewVista.DataSource = CobroCxC.ListaF(TbDesde.Text, TbHasta.Text);
                GridViewVista.DataBind();
            }
            else
            {
                GridViewVista.DataSource = CobroCxC.Lista("*", "CobroCxC Where " + Tipo() + " like'" + TbBuscar.Text + "%'");
                GridViewVista.DataBind();
            }
           

            
        }
        public string Tipo()
        {
            string tipo = "";
            if (TipoDropDownList.SelectedIndex == 0)
            {

                tipo = "IdCobro";

            }
            else
                if (TipoDropDownList.SelectedIndex == 1)
                {
                    tipo = "IdCuenta";
                }
                else
                    if (TipoDropDownList.SelectedIndex == 2)
                    {
                        tipo = "Fecha";

                    }
                    else
                        if (TipoDropDownList.SelectedIndex == 3)
                        {
                            tipo = "Valor";
                        }
            return tipo;
        }

        protected void TipoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
             GridViewVista.DataSource =null;
             GridViewVista.DataBind();
        }
    }
}