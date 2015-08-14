using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Finanzas
{
    public partial class rCategorias : System.Web.UI.Page
    {
        public Categorias cat = new Categorias();

        public string a = ""; // variable globa para los catch
       

        protected void Page_Load(object sender, EventArgs e)
        {
            int IdCategoria = 0;
            if (!IsPostBack)
            {


                int.TryParse(Request.QueryString["IdCategoria"], out IdCategoria);

                if (cat.Buscar(IdCategoria))
                {


                    TbIdCategoria.Text = (IdCategoria).ToString();
                    TbDescripcion.Text = cat.Descripcion;
                    TbTipo.Text = cat.Tipo.ToString();
                    BtnGuardar.Text = "Actualizar";
                   
                    

                }
            }
        }


     


        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

            if (TbIdCategoria.Text == string.Empty)
            {



                cat.Descripcion = TbDescripcion.Text;
                cat.Tipo = Convert.ToInt32(TbTipo.Text);

                if (cat.Insertar())
                {
                    
                    Alert("Categoria Se Guardo Corectamente");

                    Limpiar(); 
                }
                else
                {
                    Alert("Categoria No Se Pudo Guardar Corectamente ");
                }
            }

            else
            {

                cat.Descripcion = TbDescripcion.Text;
                cat.Tipo = Convert.ToInt32(TbTipo.Text);

                if (cat.Modificar(TbIdCategoria.Text))
                {
                    
                    Alert("Categoria Se Modificada Corectamente");
                    Limpiar(); 
                }
                else
                {
                    
                    Alert(" Categoria No Se  Modificar Corectamente");
                }
            }

        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar(); 
        }
        public void Limpiar()
        {
            TbDescripcion.Text = "";
            TbIdCategoria.Text = "";
            TbTipo.Text = "";
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
           
            if (TbIdCategoria.Text == string.Empty)
            {

              
                Alert("El IdCategoria No Puede Estar Vacio Para Eliminar");
                
            }
            else
            {
                try
                {
                    if (cat.Eliminar(TbIdCategoria.Text))
                    {
                        
                        Alert("Cateroria Se Eliminada Corectamente");

                        Limpiar();
                    }
                    else
                    {
                       
                        Alert("No Se  Eliminar Categoria Corectamente");
                    }
                }
                catch (Exception a)
                {
                    
                    Alert("No Se Puede Eliminar Esta IdCategoria Porque Esta Haciendeo Referencia En Otro Tabla");
                }
               
            }
        }

        protected void BtnConsulta_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ControlPanel/Consultas/fCategorias.aspx");
        }
        public void Alert(string text)
        {

            if (!ClientScript.IsClientScriptBlockRegistered("script"))
                ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('" + text + "')", true);

        }
    }
}