using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using EntidadesCompartidas;

public partial class MasterEmpleado : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        Empleado empleado = (Empleado)Session["USER"];

        if (empleado != null)
        {
            if (!(empleado is Empleado))
            {
                Session["Mensaje"] = "¡ERROR! No tiene Autorización para acceder a esta página.";

                Response.Redirect("~/Default.aspx");
            }
            lbltodo.Text = ((Empleado)Session["USER"]).Nombre;
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void lnksalir_Click(object sender, EventArgs e)
    {
        Session["USER"] = null;
        Response.Redirect("Default.aspx");
    }
    
}
