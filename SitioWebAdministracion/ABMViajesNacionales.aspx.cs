using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EntidadesCompartidas;
using Logica;

public partial class ABMViajesNacionales : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                estadoInicial();
            }
            catch (Exception ex)
            {
                lblerror.Text = ex.Message;
            }
        }
    }
    private void estadoInicial()
    {
        txtNumeroViaje.Enabled = true;
        txtNumeroViaje.Text = "";
        txtCantParadas.Enabled = false;
        txtCantParadas.Text = "";
        txtCantAsientos.Enabled = false;
        txtCantAsientos.Text = "";
        
        btnagregar.Enabled = false;
        btneliminar.Enabled = false;
        btnbuscar.Enabled = true;
        btnmodificar.Enabled = false;

        txtNumeroViaje.Focus();
        Session["NACIONAL"] = null;

        InterfaceLogicaCompania interfaceCompania = FabricaLogica.GetLogicaCompania();
        List<Compania> companias = interfaceCompania.ListaCompanias();
        Session["COMPANIAS"] = companias;
        ddlCompanias.DataSource = Session["COMPANIAS"];
        ddlCompanias.DataBind();
        ddlCompanias.Enabled = false;

        InterfaceLogicaTerminal interfaceTerminal = FabricaLogica.GetLogicaTerminal();
        List<Terminal> terminales = interfaceTerminal.ListaTerminales();
        List<Terminal> terminalNac = new List<Terminal>();
        foreach(Terminal term in terminales)
        {
            if (term.Pais.Equals("URUGUAY"))
            {
                terminalNac.Add(term);
            }
        }
        Session["TERMINALES"] = terminalNac;
        ddlTerminales.DataSource = Session["TERMINALES"];
        ddlTerminales.DataBind();
        ddlTerminales.Enabled = false;
        cpFechaPartida.Enabled = false;
        cpFechaArribo.Enabled = false;
        if (terminalNac.Count < 1) 
        {
            txtNumeroViaje.Enabled = false;
            btnbuscar.Enabled = false;
            throw new Exception("No existen destinos Nacionales.");
        }
        if (companias.Count < 1)
        {
            txtNumeroViaje.Enabled = false;
            btnbuscar.Enabled = false;
            throw new Exception("No existen Compañías.");
        }
    }
    protected void btnagregar_Click(object sender, EventArgs e)
    {

        try
        {
            InterfaceLogicaViajes ILViaje = FabricaLogica.GetLogicaViaje();
            DateTime fechaPartida = cpFechaPartida.FechaSeleccionada;
            DateTime fechaArribo = cpFechaArribo.FechaSeleccionada;
            int cantAsientos, paradas;
            Compania compania = ((List<Compania>)Session["COMPANIAS"])[ddlCompanias.SelectedIndex];

            Terminal terminal = ((List<Terminal>)Session["TERMINALES"])[ddlTerminales.SelectedIndex];
           
            if(int.TryParse(txtCantAsientos.Text.Trim(), out cantAsientos))
            {
                if (int.TryParse(txtCantParadas.Text.Trim(), out paradas))
                {
                    Nacional viajeNacional = new Nacional(paradas, 1, cantAsientos, fechaPartida, fechaArribo, compania, terminal, (Empleado)Session["USER"]);
                    ILViaje.AltaViaje(viajeNacional);

                    lblerror.Text = "Se dió de alta el viaje Nacional con el número " + viajeNacional.NumeroViaje;
                    estadoInicial();
                    txtNumeroViaje.Text = viajeNacional.NumeroViaje.ToString();
                }
                else
                {
                    txtCantParadas.Focus();
                    throw new Exception("Debe especificar el número de Paradas del viaje.");
                }
            }
            else
            {
                txtCantAsientos.Focus();
                throw new Exception("Debe especificar el número de Asientos del viaje.");
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }
    protected void btnmodificar_Click(object sender, EventArgs e)
    {
        try
        {
            InterfaceLogicaViajes ILViaje = FabricaLogica.GetLogicaViaje();
            DateTime fechaPartida = cpFechaPartida.FechaSeleccionada;
            DateTime fechaArribo = cpFechaArribo.FechaSeleccionada;
            int cantAsientos, paradas;           
       
            if (int.TryParse(txtCantAsientos.Text.Trim(), out cantAsientos))
            {
                if (int.TryParse(txtCantParadas.Text.Trim(), out paradas))
                {
                    Nacional viajeNacional = (Nacional)Session["NACIONAL"];
                    viajeNacional.Partida = fechaPartida;
                    viajeNacional.Arribo = fechaArribo;
                    viajeNacional.Asientos = cantAsientos;
                    viajeNacional.Paradas = paradas;
                    viajeNacional.Empleado = (Empleado)Session["USER"];
                    if (!ddlCompanias.SelectedItem.Text.Equals(viajeNacional.Compania.Nombre))
                    {
                        viajeNacional.Compania = ((List<Compania>)Session["COMPANIAS"])[ddlCompanias.SelectedIndex];
                    }
                    if (!ddlTerminales.SelectedItem.Text.Equals(viajeNacional.Terminal.ToString()))
                    {
                        viajeNacional.Terminal = ((List<Terminal>)Session["TERMINALES"])[ddlTerminales.SelectedIndex];
                    }
                    
                    ILViaje.ModificarViaje(viajeNacional);

                    lblerror.Text = "Se modificó el viaje Nacional con el número " + viajeNacional.NumeroViaje;
                    estadoInicial();
                }
                else
                {
                    txtCantParadas.Focus();
                    throw new Exception("Debe especificar el número de Paradas del viaje.");
                }
            }
            else
            {
                txtCantAsientos.Focus();
                throw new Exception("Debe especificar el número de Asientos del viaje.");
            }
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
            InterfaceLogicaViajes ILViaje = FabricaLogica.GetLogicaViaje();
            ILViaje.BajaViaje((Viaje)Session["NACIONAL"]);
            lblerror.Text = "Se dió de baja el viaje Nacional.";
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
    }
    protected void btnbuscar_Click(object sender, EventArgs e)
    {
        int numeroViaje;
        try
        {
            if (int.TryParse(txtNumeroViaje.Text.Trim(), out numeroViaje))
            {
                InterfaceLogicaViajes ILViaje = FabricaLogica.GetLogicaViaje();
                Viaje viajeNacional = ILViaje.BuscarViaje(numeroViaje);


                if (viajeNacional != null)
                {
                    if (viajeNacional is Nacional)
                    {
                        Session["NACIONAL"] = viajeNacional;
                        txtCantAsientos.Text = viajeNacional.Asientos.ToString();
                        txtCantParadas.Text = ((Nacional)viajeNacional).Paradas.ToString();

                        txtNumeroViaje.Enabled = false;
                        txtCantAsientos.Focus();
                        txtCantAsientos.Enabled = true;
                        txtCantParadas.Enabled = true;
                        cpFechaArribo.FechaSeleccionada = viajeNacional.Arribo;
                        cpFechaPartida.FechaSeleccionada = viajeNacional.Partida;
                        List<Compania> companias = ((List<Compania>)Session["COMPANIAS"]);
                        int indice = 0;
                        bool encuentro = false;
                        while (indice < companias.Count)
                        {
                            if (companias[indice].Nombre == viajeNacional.Compania.Nombre)
                            {
                                ddlCompanias.SelectedIndex = indice;
                                indice = companias.Count;
                                encuentro = true;
                            }
                            else
                            {
                                indice++;
                            }
                        }
                        lblerror.Text = "Búsqueda exitosa.</br>";
                        if (!encuentro)
                        {
                            ddlCompanias.Items.Add(viajeNacional.Compania.Nombre);
                            ddlCompanias.SelectedIndex=ddlCompanias.Items.Count-1;
                            ddlCompanias.DataBind();
                            lblerror.Text += "La Compañía de este viaje no está activa, si la modifica ya no podrá volver a seleccionarla.</br>";
                        }
                        List<Terminal> terminales = ((List<Terminal>)Session["TERMINALES"]);
                        indice = 0;
                        encuentro = false;
                        while (indice < terminales.Count)
                        {
                            if (terminales[indice].Codigo == viajeNacional.Terminal.Codigo)
                            {
                                ddlTerminales.SelectedIndex = indice;
                                indice = terminales.Count;
                                encuentro = true;
                                //lblerror.Text += "La Compañía de este viaje no está activa, si la modifica ya no podrá volver a seleccionarla.</br>";
                            }
                            else
                            {
                                indice++;
                            }
                        }
                        if (!encuentro)
                        {
                            ddlTerminales.Items.Add(viajeNacional.Terminal.ToString());
                            ddlTerminales.SelectedIndex = ddlTerminales.Items.Count - 1;
                            ddlTerminales.DataBind();
                            lblerror.Text += "La Terminal de este viaje no está activa, si la modifica ya no podrá volver a seleccionarla.";
                        }
                        btnbuscar.Enabled = false;
                        btnagregar.Enabled = false;
                        btneliminar.Enabled = true;
                        btnmodificar.Enabled = true;                     
                    }
                    else
                    {
                        throw new Exception("El viaje encontrado no es Nacional, busque otro o dirígase a la página de mantenimiento de viajes Internacionales.");
                    }

                }
                else
                {
                    txtNumeroViaje.Enabled = false;

                    txtCantAsientos.Focus();

                    btnbuscar.Enabled = false;
                    btnagregar.Enabled = true;
                    btneliminar.Enabled = false;
                    btnmodificar.Enabled = false;
                    lblerror.Text = "No se encontró el viaje con el número " + txtNumeroViaje.Text + ", para realizar nueva búsqueda presione 'Limpiar Formulario', de lo contrario agregue un nuevo Viaje.";
                    txtNumeroViaje.Text = "";
                }
                txtCantAsientos.Enabled = true;
                txtCantParadas.Enabled = true;
                ddlCompanias.Enabled = true;
                ddlTerminales.Enabled = true;
                cpFechaPartida.Enabled = true;
                cpFechaArribo.Enabled = true;
            }
            else
            {
                throw new Exception("Debe especificar el número de viaje.");
            }
        }
        catch (Exception ex)
        {
            lblerror.Text = ex.Message;
        }
    }
    
}