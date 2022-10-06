using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;
using Logica;
using System.Text.RegularExpressions;

public partial class Terminales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            estadoInicial();
        }
    }

    private void estadoInicial()
    {
        txtCodigo.Enabled = true;
        txtCodigo.Text = "";
        txtFacilidades.Enabled = false;
        txtFacilidades.Text = "";
        txtnombre.Enabled = false;
        txtnombre.Text = "";
        LbFacilidades.Items.Clear();
        ddlPaises.Enabled = false;
        btnagregar.Enabled = false;
        btneliminar.Enabled = false;
        btnbuscar.Enabled = true;
        btnmodificar.Enabled = false;
        ddlPaises.Enabled = false;
        txtCodigo.Focus();
        BtnAgregarFacilidad.Enabled = false;
        btnBorrar.Enabled = false;
        Session["TERMINAL"] = null;
    }

    protected void btnagregar_Click(object sender, EventArgs e)
    {
        string codigoTerminal = txtCodigo.Text.Trim();
        try
        {
            validoCodigo(codigoTerminal);
            string nombreCiudad = txtnombre.Text.Trim();
            string pais = ddlPaises.SelectedItem.Value;
            Terminal terminal = new Terminal(codigoTerminal, nombreCiudad, pais);
            foreach (ListItem item in LbFacilidades.Items)
            {
                terminal.AgregarFacilidad(new Facilidad(item.Text));
            }
            FabricaLogica.GetLogicaTerminal().AltaTerminal(terminal);
            estadoInicial();
            lblerror.Text = "Alta exitosa";          
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }

    private void validoCodigo(string codigoTerminal)
    {
        bool resultado = Regex.IsMatch(codigoTerminal, @"^[a-zA-Z]+$");
        if (resultado)
        {
            if (codigoTerminal.Length != 3)
            {
                throw new Exception("El código de la terminal deben ser tres letras.");
            }
        }
        else
        {
            throw new Exception("Debe ingresar solo letras.");
        }
    }

    protected void btnmodificar_Click(object sender, EventArgs e)
    {
        string codigoTerminal = txtCodigo.Text.Trim();
        try
        {
            validoCodigo(codigoTerminal);
            string nombreCiudad = txtnombre.Text.Trim();
            string pais = ddlPaises.SelectedItem.Value;
            Terminal terminal = (Terminal)Session["TERMINAL"];
            terminal.NombreCiudad = nombreCiudad;
            terminal.Pais = pais;
            terminal.Facilidades.Clear();
            foreach (ListItem item in LbFacilidades.Items)
            {
                terminal.AgregarFacilidad(new Facilidad(item.Text));
            }
            FabricaLogica.GetLogicaTerminal().ModificarTerminal(terminal);
            estadoInicial();
            lblerror.Text = "Modificación exitosa.";
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }

    protected void btneliminar_Click(object sender, EventArgs e)
    {
        
        try
        {           
            Terminal terminal = ((Terminal)Session["TERMINAL"]);
            
            FabricaLogica.GetLogicaTerminal().BajaTerminal(terminal);
            lblerror.Text = "Baja exitosa.";
            estadoInicial();
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {
        estadoInicial();
        lblerror.Text = "";
    }

    protected void btnbuscar_Click(object sender, EventArgs e)
    {
        string codigoTerminal = txtCodigo.Text.Trim();
        try
        {
            validoCodigo(codigoTerminal);
            Terminal terminal = FabricaLogica.GetLogicaTerminal().BuscarTerminal(codigoTerminal);
            if (terminal != null)
            {
                txtnombre.Text = terminal.NombreCiudad;
                ddlPaises.SelectedValue = terminal.Pais;
                foreach (Facilidad item in terminal.Facilidades)
                {
                    LbFacilidades.Items.Add(item.Descripcion);
                }
                btnagregar.Enabled = false;
                btneliminar.Enabled = true;
                btnbuscar.Enabled = false;
                btnmodificar.Enabled = true;
                txtFacilidades.Focus();
                lblerror.Text = "Búsqueda exitosa.";
                Session["TERMINAL"] = terminal;
            }
            else
            {
                btnagregar.Enabled = true;
                btneliminar.Enabled = false;
                btnbuscar.Enabled = false;
                btnmodificar.Enabled = false;
                txtnombre.Focus();
                lblerror.Text = "La búsqueda no obtuvo resultados, pude dar de alta la Terminal.";
            }
            txtCodigo.Enabled = false;
            txtFacilidades.Enabled = true;
            txtnombre.Enabled = true;
            ddlPaises.Enabled = true;
            BtnAgregarFacilidad.Enabled = true;
            btnBorrar.Enabled = true;
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }

    protected void BtnAgregarFacilidad_Click(object sender, EventArgs e)
    {
        try
        {
            string facilidad = txtFacilidades.Text.Trim();
            txtFacilidades.Focus();
            if (facilidad.Length > 0)
            {
                LbFacilidades.Items.Add(facilidad);
                txtFacilidades.Text = "";             
            }
            else
            {
                throw new Exception("Debe especificar la Facilidad.");
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }
   
    protected void btnBorrar_Click(object sender, EventArgs e)
    {      
        LbFacilidades.Items.Remove(LbFacilidades.SelectedValue);
    }
}