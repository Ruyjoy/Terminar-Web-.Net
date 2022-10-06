using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;
using Logica;

public partial class ABMEmpleado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtcontraseña.Text = "";
            txtnombre.Text = "";
            btnagregar.Enabled = false;
            btneliminar.Enabled = false;
            btnmodificar.Enabled = false;
            txtcontraseña.Enabled = false;
            txtRepetir.Enabled = false;
        }
        lblerror.Text = "";
    }

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {
        Limpiar();
    }

    protected void btnbuscar_Click(object sender, EventArgs e)
    {
        int cedula;
        Empleado unEmpleado = null;
        try
        {
            if (txtcedula.Text.Trim().Length >= 10)
            {
                mostrarMensajeError("Ingrese una Cedula correcta.<br>");
                return;
            }

            if (txtcedula.Text.Trim().Length != 0)
            {
                cedula = Convert.ToInt32(txtcedula.Text);
            }
            else
            {
                mostrarMensajeError("Ingrese una Cedula correcta.<br>");
                return;
            }
            
            unEmpleado = FabricaLogica.GetLogicaEmpleado().Buscar(cedula);
        }
        catch (FormatException)
        {
            mostrarMensajeError("La cedula no es válida.");

            return;
        }
        catch (ExcepcionPersonalizada ex)
        {
            mostrarMensajeError(ex.Message);
            return;
        }
        catch
        {
            mostrarMensajeError("Ocurrió un problema al Busacar el Empleado");
            return;
        }
        if (unEmpleado == null)
        {
            lblerror.Text = "No se encontró el empleado, pero puede agregarlo";
            txtcedula.Enabled = false;
            txtnombre.Enabled = true;
            txtcontraseña.Enabled = true;
            txtRepetir.Enabled = true;
            btnagregar.Enabled = true;
            btnbuscar.Enabled = false;
            btnagregar.Enabled = true;
        }
        else
        {
            if (unEmpleado.Cedula == ((Empleado)Session["USER"]).Cedula)
            {
                txtnombre.Text = unEmpleado.Nombre;
                txtcedula.Text = Convert.ToString(unEmpleado.Cedula);
                txtcedula.Enabled = false;
                btnmodificar.Enabled = true;
                txtnombre.Enabled = true;
                txtcontraseña.Enabled = true;
                txtRepetir.Enabled = true;
                btnbuscar.Enabled = false;
                btnagregar.Enabled = false;

                lblerror.Text = "Se ha encontrado su usuario.";
            }
            else
            {
                txtcedula.Text = Convert.ToString(unEmpleado.Cedula);
                txtnombre.Text = unEmpleado.Nombre;
                txtcedula.Enabled = false;
                btnmodificar.Enabled = true;
                btneliminar.Enabled = true;
                txtnombre.Enabled = true;
                txtcontraseña.Enabled = true;
                txtRepetir.Enabled = true;
                lblerror.Text = "El Empleado se a encontrado con exito.";

                Session["Empleado"] = unEmpleado;
            }
        }
    }

    protected void btnagregar_Click(object sender, EventArgs e)
    {
        int cedula = 0;
        string nombre = "";
        string contraseña = "";

        if (txtcedula.Text.Trim().Length != 0)
        {
            cedula = Convert.ToInt32(txtcedula.Text);
        }
        else
        {
            mostrarMensajeError("Ingrese una Cedula.<br>");
            return;
        }
        if (txtnombre.Text.Trim().Length != 0)
        {
            nombre = txtnombre.Text;
        }
        else
        {
            mostrarMensajeError("Ingrese el nombre.<br>");
            return;
        }
        if (txtcontraseña.Text.Trim().Length != 0)
        {
            contraseña = txtcontraseña.Text;
        }
        else
        {
            mostrarMensajeError("Ingrese la contraseña.<br>");
            return;
        }
        if (txtRepetir.Text.Trim().Length != 0)
        {
            if (txtRepetir.Text != contraseña)
            {
                mostrarMensajeError("Debe repetir correctamente la contraseña.");
                return;
            }
        }
        else
        {
            mostrarMensajeError("Debe repetir la contraseña.");
            return;
        }
        Empleado unEmpleado = null;
        try
        {
            unEmpleado = new Empleado(cedula, contraseña, nombre);
            FabricaLogica.GetLogicaEmpleado().Agregar(unEmpleado);
            lblerror.Text = "El empleado fue agregado con éxito";
            Limpiar();
        }
        catch (ExcepcionPersonalizada ex)
        {
            mostrarMensajeError(ex.Message);
            return;
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
        catch
        {
            mostrarMensajeError("Ocurrió un problema al agregar el empleado.");
            return;
        }
    }

    protected void btnmodificar_Click(object sender, EventArgs e)
    {
        int cedula;
        string contraseña = "";
        string nombre = "";

        if (txtcedula.Text.Trim().Length != 0)
        {
            cedula= Convert.ToInt32(txtcedula.Text);
        }
        else
        {
            mostrarMensajeError("Ingrese el nombre.<br>");
            return;
        }
        if (txtcontraseña.Text.Trim().Length != 0)
        {
            contraseña = txtcontraseña.Text;
        }
        else
        {
            mostrarMensajeError("Ingrese la contraseña.<br>");
            return;
        }
        if (txtRepetir.Text.Trim().Length != 0)
        {
            if (txtRepetir.Text != contraseña)
            {
                mostrarMensajeError("Debe repetir correctamente la contraseña.");
                return;
            }
        }
        else
        {
            mostrarMensajeError("Debe repetir la contraseña.");
            return;
        }
        if (txtnombre.Text.Trim().Length != 0)
        {
            nombre = txtnombre.Text;
        }
        else
        {
            mostrarMensajeError("Ingrese su Nombre.<br>");
            return;
        }
        Empleado unEmpleado = null;
        try
        {
            unEmpleado = new Empleado(cedula, contraseña,nombre);
            FabricaLogica.GetLogicaEmpleado().Modificar(unEmpleado);
            lblerror.Text = "El empleado fue modificado con éxito";

            Limpiar();
        }
        catch (ExcepcionPersonalizada ex)
        {
            mostrarMensajeError(ex.Message);
            return;
        }
        catch
        {
            mostrarMensajeError("Ocurrió un problema al modificar el empleado.");
            return;
        }
    }

    protected void btneliminar_Click(object sender, EventArgs e)
    {
        string nombre = "";
        Empleado emp = (Empleado)Session["USER"];

        if (txtnombre.Text.Trim().Length != 0)
        {
            nombre = txtnombre.Text;
        }
        else
        {
            mostrarMensajeError("Ingrese el nombre.<br>");
            return;
        }
        Empleado unEmpleado = null;
        try
        {
            unEmpleado = (Empleado)Session["Empleado"];
            if (unEmpleado.Cedula== emp.Cedula  )
            {
                lblerror.Text = "No se puede eliminar a sí mismo!";
            }
            else
            {
                FabricaLogica.GetLogicaEmpleado().Eliminar(unEmpleado);
                lblerror.Text = "El empleado fue eliminado con éxito";

                Limpiar();
            }
            
            unEmpleado = (Empleado)Session["Empleado"];
        }
        catch (ExcepcionPersonalizada ex)
        {
            mostrarMensajeError(ex.Message);
            return;
        }
        catch
        {
            mostrarMensajeError("Ocurrió un problema al eliminar el empleado.");
            return;
        }
    }

    protected void mostrarMensajeError(string mensajeError)
    {
        lblerror.ForeColor = System.Drawing.Color.Red;
        lblerror.Text = "¡ERROR! " + mensajeError;
    }  
    
    protected void Limpiar()
    {
        txtcedula.Text = "";
        txtcedula.Enabled = true;
        txtcontraseña.Text = "";
        txtRepetir.Text = "";
        txtnombre.Text = "";
        txtRepetir.Enabled = false;
        txtcedula.ReadOnly = false;
        txtcontraseña.Enabled = false;
        btnagregar.Enabled = false;
        btneliminar.Enabled = false;
        btnmodificar.Enabled = false;
        btnbuscar.Enabled = true;
    }
}