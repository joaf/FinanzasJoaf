using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;


namespace Finanzas.ControPanel.Registros
{
    public partial class rUsuarios : System.Web.UI.Page
    {
       public Usuarios usus = new Usuarios();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
     
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (TbIdUsuario.Text == string.Empty)
            {

                OtenerDatos();


                if (usus.Insertar())
                {
                    Alert("Usuario Se Guardo Corectamente");
                    Limpiar();
                }
                else
                {
                    Alert("Usuario No Se Pudo Guardar Corectamente");
                }
            }

            else
            {

                OtenerDatos();

                if (usus.Modificar(TbIdUsuario.Text))
                {

                    Alert("Usuario Se Modificada Corectamente");

                }
                else
                {
                    Alert("Usuario No Se  Modificar Corectamente");
                }
            }
        }
        public void OtenerDatos()
        {
            usus.Usuario = TbUsuario.Text;
            usus.Clave = TbContraceña.Text;
            usus.Email = TbEmail.Text;
        }
        public void Limpiar()
        {
            TbIdUsuario.Text = "";
            TbUsuario.Text = "";
            TbContraceña.Text = "";
            TbEmail.Text = "";

        }
        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TbIdUsuario.Text == string.Empty)
                {
                    Alert("El IdUsuario No Puede Estar Vacio Para Eliminar");
                }
                else
                {
                    if (usus.Eliminar(TbIdUsuario.Text))
                    {
                        Alert("Usuario Se Eliminada Corectamente");
                        Limpiar();
                    }
                    else
                    {
                        Alert("Usuario No Se  Eliminar  Corectamente");
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
        public void Alert(string text)
        {
            if (!ClientScript.IsClientScriptBlockRegistered("script"))
                ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('" + text + "')", true);
        }
    }
}
