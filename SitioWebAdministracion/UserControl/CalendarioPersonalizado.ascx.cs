using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class CalendarioPersonalizado : System.Web.UI.UserControl
{
    private int _anioDesde;
    private int _anioHasta;
    private DateTime _fechaSeleccionada;
    private bool enabled;

    public int AnioDesde
    {
        get
        {
            return _anioDesde;
        }

        set
        {
            _anioDesde = value;

            cargarAnios();
        }
    }

    public int AnioHasta
    {
        get
        {
            return _anioHasta;
        }

        set
        {
            _anioHasta = value;

            cargarAnios();
        }
    }

    public DateTime FechaSeleccionada
    {
        get
        {
            return _fechaSeleccionada;
        }

        set
        {
            
            _fechaSeleccionada = value;

            int dia = _fechaSeleccionada.Day;
            int mes = _fechaSeleccionada.Month;
            int anio = _fechaSeleccionada.Year;
            int hora = _fechaSeleccionada.Hour;
            int minuto = _fechaSeleccionada.Minute;

            cargarDias(DateTime.DaysInMonth(anio, mes));

            ddlDia.SelectedValue = dia.ToString();
            ddlMes.SelectedValue = mes.ToString();
            ddlAnio.SelectedValue = anio.ToString();
            ddlHora.SelectedValue = hora.ToString();
            ddlMinuto.SelectedValue = minuto.ToString();
        }
    }

    public CalendarioPersonalizado()
    {
        _anioDesde = DateTime.Today.Year;
        _anioHasta = DateTime.Today.Year + 1;
    }

    public void Cargardecero()
    {
            cargarAnios();
            cargarMeses();
            cargarHora();

            FechaSeleccionada = DateTime.Today;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarAnios();
            cargarMeses();
            cargarHora();

            FechaSeleccionada = DateTime.Today;
        }
        else
        {
            int diaSeleccionado = Convert.ToInt32(ddlDia.SelectedValue);
            int mesSeleccionado = Convert.ToInt32(ddlMes.SelectedValue);
            int anioSeleccionado = Convert.ToInt32(ddlAnio.SelectedValue);
            int horaSeleccionada = Convert.ToInt32(ddlHora.SelectedValue);
            int minutoSeleccionado = Convert.ToInt32(ddlMinuto.SelectedValue);

            int ultimoDiaMes = DateTime.DaysInMonth(anioSeleccionado, mesSeleccionado);
            diaSeleccionado = diaSeleccionado <= ultimoDiaMes ? diaSeleccionado : ultimoDiaMes;

            FechaSeleccionada = new DateTime(anioSeleccionado, mesSeleccionado, diaSeleccionado,horaSeleccionada,minutoSeleccionado,0);
        }
      
    }

    protected void cargarAnios()
    {
        ddlAnio.Items.Clear();

        for (int i = AnioDesde; i <= AnioHasta; i++)
        {
            ddlAnio.Items.Add(i.ToString());
        }
    }

    protected void cargarMeses()
    {
        ddlMes.Items.Clear();

        for (int i = 1; i <= 12; i++)
        {
            ddlMes.Items.Add(i.ToString());
        }
    }

    protected void cargarDias(int ultimoDiaMes)
    {
        ddlDia.Items.Clear();

        for (int i = 1; i <= ultimoDiaMes; i++)
        {
            ddlDia.Items.Add(i.ToString());
        }
    }

    protected void cargarHora()
    {
        ddlHora.Items.Clear();
        ddlMinuto.Items.Clear();
        for (int i = 0; i < 60; i++)
        {
            if (i < 24)
            {
                ddlHora.Items.Add(i.ToString());
                ddlMinuto.Items.Add(i.ToString());
            }
            else 
            {
                ddlMinuto.Items.Add(i.ToString());
            }
        }
        
    }

    public bool Enabled
    {
        set 
        {
            ddlAnio.Enabled = value;
            ddlMes.Enabled = value;
            ddlDia.Enabled = value;
            ddlHora.Enabled = value;
            ddlMinuto.Enabled = value;
            enabled = value;
        }

    }
}