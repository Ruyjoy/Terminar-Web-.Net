using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;
using Logica;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        string mensaje = (string)Session["Mensaje"];

        if (mensaje != null)
        {
            if (mensaje.Contains("¡ERROR!"))
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }

            lblMensaje.Text = mensaje;

            Session.Remove("Mensaje");
        }

    }

    protected void mostrarMensajeError(string mensajeError)
    {
        lblMensaje.ForeColor = System.Drawing.Color.Red;
        lblMensaje.Text = "¡ERROR! " + mensajeError;
    }

    protected void btnir_Click(object sender, ImageClickEventArgs e)
    {
        if (String.IsNullOrWhiteSpace(txtCedula.Text))
        {
            lblMensaje.Text = "Debe ingresar una Cedula.";
            return;
        }

        if (String.IsNullOrWhiteSpace(txtContraseña.Text))
        {
            lblMensaje.Text = "Debe ingresar la contraseña.";
            return;
        }
        int Cedula;
        string contraseña = "";
        Empleado empleado=null;


        try
        {
            Cedula = Convert.ToInt32(txtCedula.Text);
            contraseña = txtContraseña.Text;
            empleado = FabricaLogica.GetLogicaEmpleado().Login(Cedula, contraseña);
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
        


        if (empleado != null && empleado.Pass == contraseña)
        {
            Session["USER"] = empleado;

            Response.Redirect("ABMEmpleado.aspx");
        }
        else
        {
            lblMensaje.Text = "¡ERROR! Nombre de usuario y/o contraseña incorrecto/a(s).";
        }
    }
}