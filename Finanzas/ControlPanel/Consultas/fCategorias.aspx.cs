using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas.ControlPanel.Consultas
{
    public partial class fCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void BtnConsulta_Click(object sender, EventArgs e)
        {
            GridViewVista.DataSource = Categorias.Lista("*", "Categorias Where " + Tipo() + " like'" + TbBuscar.Text + "%'");
            GridViewVista.DataBind();
        }
        public string Tipo()
        {
            string tipo = "";
            switch (TipoDropDownList.SelectedIndex)
            {
                case 0:
                    tipo = "Descripcion";
                    break;

                case 1:
                    tipo = "IdCategoria";
                break;
                case 2:
                       tipo = "Tipo";
                break;
            }
            return tipo;

        }

        protected void TipoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}