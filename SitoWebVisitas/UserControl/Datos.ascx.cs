using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;

public partial class UserControl_Datos : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Viaje v = (Viaje)Session["Viaje"];

                if (v != null)
                    Cargar(v);
            }
            catch
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }

    public void Cargar(Viaje v)
    {
        Panel panel = new Panel();
        Table tabla = new Table();
        TableRow fila1 = new TableRow();
        TableRow fila2 = new TableRow();
        TableRow fila3 = new TableRow();
        TableRow fila4 = new TableRow();
        TableRow fila5 = new TableRow();
        TableRow fila6 = new TableRow();
        TableRow fila7 = new TableRow();
        TableRow fila8 = new TableRow();
        TableRow fila9 = new TableRow();
        TableRow fila10 = new TableRow();
        TableRow fila11 = new TableRow();
        TableRow fila12 = new TableRow();
        TableRow fila13 = new TableRow();
        TableRow fila14 = new TableRow();
        TableRow fila15 = new TableRow();
        TableRow fila16 = new TableRow();
        TableRow fila17 = new TableRow();
        TableRow fila18 = new TableRow();
        TableRow fila19 = new TableRow();
        TableRow fila20 = new TableRow();
        tabla.ID = "Tabla1";
        tabla.BorderStyle = BorderStyle.Solid;

        TableCell celda1 = new TableCell();
        TableCell celda2 = new TableCell();
        TableCell celda3 = new TableCell();
        TableCell celda4 = new TableCell();
        TableCell celda5 = new TableCell();
        TableCell celda6 = new TableCell();
        TableCell celda7 = new TableCell();
        TableCell celda8 = new TableCell();
        TableCell celda9 = new TableCell();
        TableCell celda10 = new TableCell();
        TableCell celda11 = new TableCell();
        TableCell celda12 = new TableCell();
        TableCell celda13 = new TableCell();
        TableCell celda14 = new TableCell();
        TableCell celda15 = new TableCell();
        TableCell celda16 = new TableCell();
        TableCell celda17 = new TableCell();
        TableCell celda18 = new TableCell();
        TableCell celda19 = new TableCell();
        TableCell celda20 = new TableCell();
        TableCell celda21 = new TableCell();
        TableCell celda22 = new TableCell();
        TableCell celda23 = new TableCell();
        TableCell celda24 = new TableCell();
        TableCell celda25 = new TableCell();
        TableCell celda26 = new TableCell();
        TableCell celda27 = new TableCell();
        TableCell celda28 = new TableCell();
        TableCell celda29 = new TableCell();
        TableCell celda30 = new TableCell();
        TableCell celda31 = new TableCell();
        TableCell celda32 = new TableCell();
        TableCell celda33 = new TableCell();
        TableCell celda34 = new TableCell();
        TableCell celda35 = new TableCell();
        TableCell celda36 = new TableCell();
        TableCell celda37 = new TableCell();

        Label lblNumeroviaje = new Label();
        lblNumeroviaje.Text = v.NumeroViaje.ToString();
        Label lblasientos = new Label();
        lblasientos.Text = v.Asientos.ToString();
        Label lblpaartida = new Label();
        lblpaartida.Text = v.Partida.ToString();
        Label lblArribo = new Label();
        lblArribo.Text = v.Arribo.ToString();

        Label lblTerminal = new Label();
        lblTerminal.Text = v.Terminal.Codigo.ToString();
        Label lblterminal1 = new Label();
        lblterminal1.Text = v.Terminal.Pais.ToString();
        Label lblterminal2 = new Label();
        lblterminal2.Text = v.Terminal.NombreCiudad.ToString();



        Label lblCompania = new Label();
        lblCompania.Text = v.Compania.Nombre;
        Label lblCompania1 = new Label();
        lblCompania1.Text = v.Compania.Direccion;
        Label lblCompania2 = new Label();
        lblCompania2.Text = v.Compania.Telefono.ToString();

        Label lblterminal3 = new Label();
        Label lblterminal4 = new Label();
        Label lblterminal5 = new Label();
        Label lblterminal6 = new Label();
        //lblterminal3.Text = v.Terminal.Servicios.ToString();

        ListBox servicios = new ListBox();
        foreach (Facilidad s in v.Terminal.Facilidades)
        {
            servicios.Items.Add(s.Descripcion);
        }
        
        celda1.Text = "Número de Viaje :";
        celda2.Controls.Add(lblNumeroviaje);
        celda3.Text = "Asientos: ";
        celda4.Controls.Add(lblasientos);
        celda5.Text = "Partida: ";
        celda6.Controls.Add(lblpaartida);
        celda7.Text = "Arribo:";
        celda8.Controls.Add(lblArribo);

        if (v is Nacional)
        {
            Nacional unViaje = (Nacional)v;
            celda9.Text = "Paradas";
            celda10.Text = unViaje.Paradas.ToString();
            
        }
        else if (v is Internacional)
        {
            Internacional unViaje = (Internacional)v;
            celda11.Text = "Documentación";
            celda12.Text = unViaje.Documentacion.ToString();
            if (unViaje.Servicios == true)
            {
                celda13.Text = "Servicios";
                celda14.Text = "Si";
            }
            if (unViaje.Servicios == false)
            {
                celda13.Text = "Servicios";
                celda14.Text = "Si";
            }
        }

        celda15.Text = "COMPAÑIA";
        celda16.Text = "";
        celda17.Text = "Nombre:";
        celda18.Controls.Add(lblCompania);
        celda19.Text = "Dirección:";
        celda20.Controls.Add(lblCompania1);
        celda21.Text = "Teléfono";
        celda22.Controls.Add(lblCompania2);

        celda23.Text = "";
        celda24.Text = "";
        celda25.Text = "TERMINAL";
        celda26.Text = "";
        celda27.Text = "Nombre:";
        celda28.Controls.Add(lblTerminal);
        celda29.Text = "Ciudad:";
        celda30.Controls.Add(lblterminal2);
        celda31.Text = "País:";
        celda32.Controls.Add(lblterminal1);
        celda33.Text = "Facilidades:";
        celda34.Controls.Add(servicios);
        celda35.Controls.Add(lblterminal4);
        celda36.Controls.Add(lblterminal5);
        celda37.Controls.Add(lblterminal6);

        

        
       
        fila1.Cells.Add(celda1);
        fila1.Cells.Add(celda2);
        fila2.Cells.Add(celda3);
        fila2.Cells.Add(celda4);
        fila3.Cells.Add(celda5);
        fila3.Cells.Add(celda6);
        fila4.Cells.Add(celda7);
        fila4.Cells.Add(celda8);
        fila5.Cells.Add(celda9);
        fila5.Cells.Add(celda10);
        fila6.Cells.Add(celda11);
        fila6.Cells.Add(celda12);
        fila7.Cells.Add(celda13);
        fila7.Cells.Add(celda14);
        fila8.Cells.Add(celda15);
        fila8.Cells.Add(celda16);
        fila9.Cells.Add(celda17);
        fila9.Cells.Add(celda18);
        fila10.Cells.Add(celda19);
        fila10.Cells.Add(celda20);
        fila11.Cells.Add(celda21);
        fila11.Cells.Add(celda22);
        fila12.Cells.Add(celda23);
        fila12.Cells.Add(celda24);
        fila13.Cells.Add(celda25);
        fila13.Cells.Add(celda26);
        fila14.Cells.Add(celda27);
        fila14.Cells.Add(celda28);
        fila15.Cells.Add(celda29);
        fila15.Cells.Add(celda30);
        fila16.Cells.Add(celda31);
        fila16.Cells.Add(celda32);
        fila17.Cells.Add(celda33);
        fila17.Cells.Add(celda34);
        fila17.Cells.Add(celda35);
        fila17.Cells.Add(celda36);
        fila17.Cells.Add(celda37);

       
        tabla.Rows.Add(fila1);
        tabla.Rows.Add(fila2);
        tabla.Rows.Add(fila3);
        tabla.Rows.Add(fila4);
        tabla.Rows.Add(fila5);
        tabla.Rows.Add(fila6);
        tabla.Rows.Add(fila7);
        tabla.Rows.Add(fila8);
        tabla.Rows.Add(fila9);
        tabla.Rows.Add(fila10);
        tabla.Rows.Add(fila11);
        tabla.Rows.Add(fila12);
        tabla.Rows.Add(fila13);
        tabla.Rows.Add(fila14);
        tabla.Rows.Add(fila15);
        tabla.Rows.Add(fila16);
        tabla.Rows.Add(fila17);
        panel.Controls.Add(tabla);

        this.Controls.Add(panel);
    }
}