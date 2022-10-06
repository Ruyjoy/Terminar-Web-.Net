using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;
using Logica;

public partial class ABMCompanias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            txtnombre.Text = "";
            btnagregar.Enabled = false;
            btneliminar.Enabled = false;
            btnmodificar.Enabled = false;
        }
        lblerror.Text = "";
    }

    protected void btnagregar_Click(object sender, EventArgs e)
    {
        string nombre = "";
        string Direccion = "";
        string telefono = "";

        if (txtnombre.Text.Trim().Length != 0)
        {
            nombre = txtnombre.Text;
        }
        else
        {
            mostrarMensajeError("Ingrese Nombre.<br>");
            return;
        }
        if (txtdireccion.Text.Trim().Length != 0)
        {
            Direccion = txtdireccion.Text;
        }
        else
        {
            mostrarMensajeError("Ingrese Dirección.<br>");
            return;
        }
        if (txttelefono.Text.Trim().Length != 0)
        {
            telefono = txttelefono.Text;
        }
        else
        {
            mostrarMensajeError("Ingrese Teléfono.<br>");
            return;
        }
        Compania unaCompania  = null;
        try
        {
            unaCompania = new Compania(nombre, Direccion, telefono);
            FabricaLogica.GetLogicaCompania().AltaCompania(unaCompania);
            lblerror.Text = "La Compañía fue agregado con éxito.";
            LimpiarFormulario();
        }
        catch (ExcepcionPersonalizada ex)
        {
            mostrarMensajeError(ex.Message);
            return;
        }
        catch
        {
            mostrarMensajeError("Ocurrió un problema al agregar La Compañía.");
            return;
        }
    }

    protected void btnmodificar_Click(object sender, EventArgs e)
    {
        string nombre = "";
        string Direccion = "";
        string telefono = "";

        if (txtdireccion.Text.Trim().Length != 0)
        {
            Direccion = txtdireccion.Text;
        }
        else
        {
            mostrarMensajeError("Ingrese Dirección.<br>");
            return;
        }
        if (txttelefono.Text.Trim().Length != 0)
        {
            telefono = txttelefono.Text;
        }
        else
        {
            mostrarMensajeError("Ingrese Teléfono.<br>");
            return;
        }
        Compania unaCompania = null;
        try
        {
            nombre = txtnombre.Text;
            unaCompania = new Compania(nombre, Direccion, telefono);
            FabricaLogica.GetLogicaCompania().ModificarCompania(unaCompania);
            lblerror.Text = "La Compañía fue Modificada con éxito.";
            LimpiarFormulario();
        }
        catch (ExcepcionPersonalizada ex)
        {
            mostrarMensajeError(ex.Message);
            return;
        }
        catch
        {
            mostrarMensajeError("Ocurrió un problema al Modificar la Compañía.");
            return;
        }

    }

    protected void btneliminar_Click(object sender, EventArgs e)
    {
        try
        {
            Compania compania = ((Compania)Session["Compania"]);

            FabricaLogica.GetLogicaCompania().BajaCompania(compania);
            LimpiarFormulario();
            lblerror.Text = "La Compañía fue eliminado con éxito.";
            
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {
        txtnombre.Enabled = true;
        txttelefono.Enabled = true;
        txtdireccion.Enabled = true;

        txtdireccion.Text = "";
        txtnombre.Text = "";
        txttelefono.Text = "";

        btnbuscar.Enabled = true;
        txttelefono.Enabled = true;
        btnmodificar.Enabled = false;
        btneliminar.Enabled = false;
        btnagregar.Enabled = false;
    }

    protected void btnbuscar_Click(object sender, EventArgs e)
    {
        string Nombre="";

        if (txtnombre.Text.Trim().Length != 0)
        {
            Nombre= txtnombre.Text;
        }
        else
        {
            mostrarMensajeError("Ingrese Nombre de la Compañía.<br>");
            return;
        }
        Compania unaCompania = null;
        try
        {
            unaCompania = FabricaLogica.GetLogicaCompania().BuscarCompania(Nombre);
        }
        catch (ExcepcionPersonalizada ex)
        {
            mostrarMensajeError(ex.Message);
            return;
        }
        catch
        {
            mostrarMensajeError("Ocurrió un problema al buscar la Compañía.");
            return;
        }
        if (unaCompania == null)
        {
            lblerror.Text = "No se encontró Compañía, puede agregarla si lo desea.";
            txtnombre.Enabled = true;
            txttelefono.Enabled = true;
            btnagregar.Enabled = true;
            btnbuscar.Enabled = false;
        }
        else
        {    
            txtnombre.Text = unaCompania.Nombre;
            txtdireccion.Text = unaCompania.Direccion;
            txttelefono.Text = unaCompania.Telefono;
            btnmodificar.Enabled = true;
            txtnombre.Enabled = false;
            btnbuscar.Enabled = false;
            btnagregar.Enabled = false;
            btneliminar.Enabled = true;
            Session["Compania"] = unaCompania;

            lblerror.Text = "Se ha encontrado Compañía.";
        }
        txtnombre.Enabled = false;
    }

    protected void LimpiarFormulario()
    {
        txtnombre.Text = "";
        txtdireccion.Text = "";
        txttelefono.Text = "";
        txtnombre.Enabled = true;
        btnbuscar.Enabled = true;
        btnagregar.Enabled = false;
        btnmodificar.Enabled = false;
        btneliminar.Enabled = false;
    }

    protected void mostrarMensajeError(string mensajeError)
    {
        lblerror.ForeColor = System.Drawing.Color.Red;
        lblerror.Text = "¡ERROR! " + mensajeError;
    }

}