using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logica;
using EntidadesCompartidas;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Session["Lista"] = FabricaLogica.GetLogicaViaje().ListaViajes();

                rpViajes.DataSource = (List<Viaje>)Session["Lista"];
                rpViajes.DataBind();


                List<Compania> _compa = FabricaLogica.GetLogicaCompania().ListaCompanias();
                 
                foreach (Compania comp in _compa)
                {
                    ddlcomp.Items.Add(new ListItem(comp.Nombre));
                }

                List<Terminal> _term = FabricaLogica.GetLogicaTerminal().ListaTerminales();

                foreach (Terminal ter in _term)
                {
                    ddlpais.Items.Add(new ListItem(ter.ToString()));
                }

               
            }
            catch
            {
                mostrarMensajeError("Ocurrio un problema al listar Los Viajes");
                return;
            }
        }
 
    }

    protected void mostrarMensajeError(string mensajeError)
    {
        lblerror.ForeColor = System.Drawing.Color.Red;
        lblerror.Text = "¡ERROR! " + mensajeError;
    }

    protected void btnfiltro_Click(object sender, EventArgs e)
    {
        List<Viaje> viajes= new List<Viaje>();
        List<Viaje> resultado = null;
        DateTime fechaInicial = calFechaInicial.FechaSeleccionada;
        DateTime fechaFinal = calFechaFinal.FechaSeleccionada.AddHours(23).AddMinutes(59).AddSeconds(59);

        try
        {
            viajes = (List<Viaje>)Session["Lista"];
            
        }
        catch
        {
            mostrarMensajeError("Problemas al listar Los Viajes.");
            return;
        }
        if (ddlpais.SelectedIndex == 0)
        {
            mostrarMensajeError("Debe seleccionar un Destino");
            return;
        }
        else
        {
            resultado = (from viaje in viajes
                         where viaje.Terminal.Pais == ddlpais.SelectedValue
                         select viaje).ToList<Viaje>();
        }
        if (ddlcomp.SelectedIndex == 0)
        {
            mostrarMensajeError("Debe seleccionar una compania");
            return;
        }
        else
        {
            resultado = (from viaje in viajes
                         where viaje.Compania.Nombre == ddlcomp.SelectedValue
                         select viaje).ToList<Viaje>();
        }

        if (fechaInicial > fechaFinal)
        {
            mostrarMensajeError("La fecha inicial no puede ser mayor o igual a la fecha final.");
            return;
        }
        if (fechaInicial == fechaFinal)
        {
            resultado = (from viaje in viajes
                         where viaje.Partida.ToShortDateString() == fechaInicial.ToShortDateString()
                         select viaje).ToList<Viaje>();
        }
        else
        {
            resultado = (from viaje in viajes
                         where viaje.Partida >= fechaInicial && viaje.Partida <= fechaFinal
                         select viaje).ToList<Viaje>();
        }
        try
        {
            rpViajes.DataSource = resultado;
            rpViajes.DataBind();
        }
        catch
        {
            mostrarMensajeError("Ocurrio un problema al listar los Viajes");
            return;
        }
    }

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {

        ddlpais.SelectedIndex = 0;
        ddlcomp.SelectedIndex = 0;

        calFechaInicial.FechaSeleccionada = DateTime.Today;
        calFechaFinal.FechaSeleccionada = DateTime.Today;
        
        

        try
        {
            rpViajes.DataSource = (List<Viaje>)Session["Lista"];
            rpViajes.DataBind();
        }
        catch
        {
            mostrarMensajeError("Ocurrio un problema al listar los Viajes");
            return;
        }
    }

    protected void rpViajes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "VerViaje")
        {
            try
            {
                Viaje via = Logica.FabricaLogica.GetLogicaViaje().BuscarViaje(Convert.ToInt32(((TextBox)(e.Item.Controls[1])).Text));

                if (via != null)
                {
                    Session["Viaje"] = via;
                    
                }
                else
                {
                    mostrarMensajeError("No se encontro El Viaje.");
                    return;
                }
            }
            catch (ExcepcionPersonalizada ex)
            {
                mostrarMensajeError(ex.Message);
                return;
            }
            catch (Exception)
            {
                mostrarMensajeError("Ocurrio un problema al buscar el Viaje.");
                return;
            }
            Response.Redirect("~/ConsultaIndividualdeViaje.aspx");
        }
    }

    protected void btnfiltrodestino_Click(object sender, EventArgs e)
    {
        List<Viaje> viajes = new List<Viaje>();
        List<Viaje> resultado = null;

        try
        {
            viajes = (List<Viaje>)Session["Lista"];

        }
        catch
        {
            mostrarMensajeError("Problemas al listar Los Viajes.");
            return;
        }
        if (ddlpais.SelectedIndex == 0)
        {
            mostrarMensajeError("Debe seleccionar un Destino");
            return;
        }
        else
        {
            resultado = (from viaje in viajes
                         where viaje.Terminal.ToString() == ddlpais.SelectedValue
                         select viaje).ToList<Viaje>();
        }
        try
        {
            rpViajes.DataSource = resultado;
            rpViajes.DataBind();
        }
        catch
        {
            mostrarMensajeError("Ocurrio un problema al listar los Viajes");
            return;
        }
    }

    protected void btnfiltrocompania_Click(object sender, EventArgs e)
    {
        List<Viaje> viajes = new List<Viaje>();
        List<Viaje> resultado = null;

        try
        {
            viajes = (List<Viaje>)Session["Lista"];

        }
        catch
        {
            mostrarMensajeError("Problemas al listar Los Viajes.");
            return;
        }

        if (ddlcomp.SelectedIndex == 0)
        {
            mostrarMensajeError("Debe seleccionar una compania");
            return;
        }
        else
        {
            resultado = (from viaje in viajes
                         where viaje.Compania.Nombre == ddlcomp.SelectedValue
                         select viaje).ToList<Viaje>();
        }
        try
        {
            rpViajes.DataSource = resultado;
            rpViajes.DataBind();
        }
        catch
        {
            mostrarMensajeError("Ocurrio un problema al listar los Viajes");
            return;
        }
    }

    protected void btnfiltrofecha_Click(object sender, EventArgs e)
    {
        DateTime fechaInicial = calFechaInicial.FechaSeleccionada;
        DateTime fechaFinal = calFechaFinal.FechaSeleccionada.AddHours(23).AddMinutes(59).AddSeconds(59);
        List<Viaje> viajes = new List<Viaje>();
        List<Viaje> resultado = null;

        try
        {
            viajes = (List<Viaje>)Session["Lista"];

        }
        catch
        {
            mostrarMensajeError("Problemas al listar Los Viajes.");
            return;
        }
        if (fechaInicial > fechaFinal)
        {
            mostrarMensajeError("La fecha inicial no puede ser mayor o igual a la fecha final.");
            return;
        }
        if (fechaInicial == fechaFinal)
        {
            resultado = (from viaje in viajes
                         where viaje.Partida.ToShortDateString() == fechaInicial.ToShortDateString()
                         select viaje).ToList<Viaje>();
        }
        else
        {
            resultado = (from viaje in viajes
                         where viaje.Partida >= fechaInicial && viaje.Partida <= fechaFinal 
                         select viaje).ToList<Viaje>();
        }
        try
        {
            rpViajes.DataSource = resultado;
            rpViajes.DataBind();
        }
        catch
        {
            mostrarMensajeError("Ocurrio un problema al listar los Viajes");
            return;
        }
    }
}