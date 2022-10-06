using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EntidadesCompartidas;
using Logica;

public partial class ABMViajesInternacionales : System.Web.UI.Page
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
        txtDocumentacion.Enabled = false;
        txtDocumentacion.Text = "";
        txtCantAsientos.Enabled = false;
        txtCantAsientos.Text = "";
        chkServicio.Enabled = false;

        btnagregar.Enabled = false;
        btneliminar.Enabled = false;
        btnbuscar.Enabled = true;
        btnmodificar.Enabled = false;

        txtNumeroViaje.Focus();
        Session["INTERNACIONAL"] = null;

        InterfaceLogicaCompania interfaceCompania = FabricaLogica.GetLogicaCompania();
        List<Compania> companias = interfaceCompania.ListaCompanias();
        Session["COMPANIAS"] = companias;
        ddlCompanias.DataSource = Session["COMPANIAS"];
        ddlCompanias.DataBind();
        ddlCompanias.Enabled = false;

        InterfaceLogicaTerminal interfaceTerminal = FabricaLogica.GetLogicaTerminal();
        List<Terminal> terminales = interfaceTerminal.ListaTerminales();
        List<Terminal> terminalInter = new List<Terminal>();
        foreach (Terminal term in terminales)
        {
            if (!term.Pais.Equals("URUGUAY"))
            {
                terminalInter.Add(term);
            }
        }
        Session["TERMINALES"] = terminalInter;
        ddlTerminales.DataSource = Session["TERMINALES"];
        ddlTerminales.DataBind();
        ddlTerminales.Enabled = false;
        cpFechaPartida.Enabled = false;
        cpFechaArribo.Enabled = false;
        if (terminalInter.Count < 1)
        {
            txtNumeroViaje.Enabled = false;
            btnbuscar.Enabled = false;
            throw new Exception("No existen destinos Internacionales.");
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
            bool servicioAbordo = chkServicio.Checked;
            string documentacion = txtDocumentacion.Text;
            int cantAsientos;
            Compania compania = ((List<Compania>)Session["COMPANIAS"])[ddlCompanias.SelectedIndex];

            Terminal terminal = ((List<Terminal>)Session["TERMINALES"])[ddlTerminales.SelectedIndex];

            if (int.TryParse(txtCantAsientos.Text.Trim(), out cantAsientos))
            {
                
                    Internacional viajeInternacional = new Internacional(documentacion, servicioAbordo, 1, cantAsientos, fechaPartida, fechaArribo, compania, terminal, (Empleado)Session["USER"]);
                    ILViaje.AltaViaje(viajeInternacional);

                    lblerror.Text = "Se dió de alta el viaje Internacional con el número " + viajeInternacional.NumeroViaje;
                    estadoInicial();
                    txtNumeroViaje.Text = viajeInternacional.NumeroViaje.ToString();
                
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
            bool servicioAbordo = chkServicio.Checked;
            string documentacion = txtDocumentacion.Text;
            int cantAsientos;
            
            if (int.TryParse(txtCantAsientos.Text.Trim(), out cantAsientos))
            {
                
                    Internacional viajeInternacional = (Internacional)Session["INTERNACIONAL"];
                    viajeInternacional.Partida = fechaPartida;
                    viajeInternacional.Arribo = fechaArribo;
                    viajeInternacional.Asientos = cantAsientos;
                    viajeInternacional.Servicios = servicioAbordo;
                    viajeInternacional.Documentacion = documentacion;
                    viajeInternacional.Empleado = (Empleado)Session["USER"];
                    if (!ddlCompanias.SelectedItem.Text.Equals(viajeInternacional.Compania.Nombre))
                    {
                        viajeInternacional.Compania = ((List<Compania>)Session["COMPANIAS"])[ddlCompanias.SelectedIndex];
                    }
                    if (!ddlTerminales.SelectedItem.Text.Equals(viajeInternacional.Terminal.ToString()))
                    {
                        viajeInternacional.Terminal = ((List<Terminal>)Session["TERMINALES"])[ddlTerminales.SelectedIndex];
                    }

                    ILViaje.ModificarViaje(viajeInternacional);

                    lblerror.Text = "Se modificó el viaje Internacional con el número " + viajeInternacional.NumeroViaje;
                    estadoInicial();
               
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
            ILViaje.BajaViaje((Viaje)Session["INTERNACIONAL"]);
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
                Viaje viajeInternacional = ILViaje.BuscarViaje(numeroViaje);


                if (viajeInternacional != null)
                {
                    if (viajeInternacional is Internacional)
                    {
                        Session["INTERNACIONAL"] = viajeInternacional;
                        txtCantAsientos.Text = viajeInternacional.Asientos.ToString();
                        txtDocumentacion.Text = ((Internacional)viajeInternacional).Documentacion;
                        chkServicio.Checked = ((Internacional)viajeInternacional).Servicios;
                        txtNumeroViaje.Enabled = false;
                        txtCantAsientos.Focus();
                        cpFechaArribo.FechaSeleccionada = viajeInternacional.Arribo;
                        cpFechaPartida.FechaSeleccionada = viajeInternacional.Partida;
                        List<Compania> companias = ((List<Compania>)Session["COMPANIAS"]);
                        int indice = 0;
                        bool encuentro = false;
                        while (indice < companias.Count)
                        {
                            if (companias[indice].Nombre == viajeInternacional.Compania.Nombre)
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
                            ddlCompanias.Items.Add(viajeInternacional.Compania.Nombre);
                            ddlCompanias.SelectedIndex = ddlCompanias.Items.Count - 1;
                            ddlCompanias.DataBind();
                            lblerror.Text += "La Compañía de este viaje no está activa, si la modifica ya no podrá volver a seleccionarla.</br>";
                        }
                        List<Terminal> terminales = ((List<Terminal>)Session["TERMINALES"]);
                        indice = 0;
                        encuentro = false;
                        while (indice < terminales.Count)
                        {
                            if (terminales[indice].Codigo == viajeInternacional.Terminal.Codigo)
                            {
                                ddlTerminales.SelectedIndex = indice;
                                indice = terminales.Count;
                                encuentro = true;
                            }
                            else
                            {
                                indice++;
                            }
                        }
                        if (!encuentro)
                        {
                            ddlTerminales.Items.Add(viajeInternacional.Terminal.ToString());
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
                        throw new Exception("El viaje encontrado no es Internacional, busque otro o dirígase a la página de mantenimiento de viajes Nacionales.");
                    }
                }
                else
                {
                    txtNumeroViaje.Enabled = false;
                    txtCantAsientos.Focus();

                    btnbuscar.Enabled = false;
                    btnagregar.Enabled = true;
                    btneliminar.Enabled = true;
                    btnmodificar.Enabled = false;
                    lblerror.Text = "No se encontró el viaje con el número " + txtNumeroViaje.Text + ", para realizar nueva búsqueda presione 'Limpiar Formulario', de lo contrario agregue un nuevo Viaje.";
                    txtNumeroViaje.Text = "";
                }
                txtCantAsientos.Enabled = true;
                txtDocumentacion.Enabled = true;
                chkServicio.Enabled = true;
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