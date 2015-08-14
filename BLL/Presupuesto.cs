using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;

namespace BLL
{
    public class Presupuesto
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int IdPresupuesto { get; set; }
        public List<PresupuestoDetalles> presupuestoDetalle { get; set; }

        public Presupuesto(DateTime Fecha,string Descripcion)
        {
            this.Fecha = Fecha;
            this.Descripcion = Descripcion;
            this.presupuestoDetalle = new List<PresupuestoDetalles>();
           
        }
        public Presupuesto()
        {
             Fecha = DateTime.Now;
             Descripcion = "";
             IdPresupuesto = 0;
             presupuestoDetalle = new List<PresupuestoDetalles>();
        }
        public Conexion ConexiondB = new Conexion();


        public bool Insertar()
        {
            String comando;
            comando = "Insert Into Presupuestos(Fecha,Descripcion)Values('" + this.Fecha.ToString("MM/dd/yyyy") + "','" + this.Descripcion + "')";
            foreach (PresupuestoDetalles ped in presupuestoDetalle)
            {
                comando += "Insert Into PresupuestoDetalles(IdPresupuesto,IdCategoria,Valor)Values(@@IDENTITY,'" + ped.IdCategoria + "','" + ped.Valor + "')";


           }
            return ConexiondB.EjecutarDB(comando);
           
        }
        public void AgregarDetalle( int IdCategoria, double Valor)
        {

            this.presupuestoDetalle.Add(new PresupuestoDetalles( IdCategoria, Valor));
        }

        public bool Modificar()
        {
            String Comando;

            Comando = "Update Presupuesto set Fecha ='" + this.Fecha.ToString("MM/dd/yyyy") + "',Descripcion='" + this.Descripcion + "'where IdPresupuesto='" + this.IdPresupuesto + "' ";

            ConexiondB.EjecutarDB("Delete Presupuesto where IdPresupuesto=" + this.IdPresupuesto);

            foreach (PresupuestoDetalles pre in presupuestoDetalle)
            {
                Comando += "Insert Into PresupuestoDetalles(IdPresupuesto,IdCategoria,Valor)Values('" + this.IdPresupuesto + "','" + pre.IdCategoria
                    + "','" + pre.Valor + "')";
            }

            return ConexiondB.EjecutarDB(Comando);
        }

        public bool Eliminar(string Id)
        {
            return ConexiondB.EjecutarDB("Delete from Presupuesto where IdPresupuesto=" + Id);
        }

        public bool Buscar(int Id)
        {
            bool mesj = false;
            DataTable dt = new DataTable();
            dt = ConexiondB.BuscarDb("Select *from Presupuestos where IdPresupuesto=" + Id);
            if (dt.Rows.Count > 0)
            {
                IdPresupuesto = (int)dt.Rows[0]["IdPresupuesto"];
                Fecha = (DateTime)dt.Rows[0]["Fecha"];
                Descripcion = dt.Rows[0]["Descripcion"].ToString();

                mesj = true;


            }
            return mesj;
        }
        
        public static DataTable Lista(String Campos, String FiltroWhere)
        {
            Conexion c = new Conexion();
            return c.BuscarDb("Select " + Campos + " from " + FiltroWhere);
        }
        public static DataTable ListaF(string fecha1, string fecha2)
        {
            Conexion ConexionDB = new Conexion();
            return ConexionDB.BuscarDb("select * from Presupuestos where Fecha   <='" + fecha1 + "' and Fecha >='" + fecha2 + "' ");
        }
    }
}
