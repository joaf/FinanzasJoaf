using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas.ControlPanel.Consultas
{
    public partial class fTransferencias : System.Web.UI.Page
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
                GridViewVista.DataSource = Transferencias.ListaF(TbDesde.Text, TbHasta.Text);
                GridViewVista.DataBind();
            }
            else
            {
                GridViewVista.DataSource = Transferencias.Lista("*", "Transferencias Where " + Tipo() + " like'" + TbBuscar.Text + "%'");
                GridViewVista.DataBind();
            }
           
        }
        public string Tipo()
        {
            string tipo = "";
            switch (TipoDropDownList.SelectedIndex)
            {

                case 0:
                        tipo = "IdTransferencia";
                    break;
                case 1:
                         tipo = "Fecha";
                    break;
                case 2:
                         tipo = "IdCuentaOrigen";
                    break;
                case 3:
                         tipo = "IdCuentaDestino";
                    break;
                case 4:
                         tipo = "Descripcion";
                    break;
                case 5:
                          tipo = "Valor";
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