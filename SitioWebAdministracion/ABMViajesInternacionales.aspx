<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpleado.master" AutoEventWireup="true" CodeFile="ABMViajesInternacionales.aspx.cs" Inherits="ABMViajesInternacionales" %>

<%@ Register src="UserControl/CalendarioPersonalizado.ascx" tagname="CalendarioPersonalizado" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">



        .style2
        {
            width: 95px;
            height: 24px;
        }
        .style12
        {
            width: 111px;
            height: 23px;
        }
        .style21
    {
        width: 210px;
        height: 24px;
    }
    .style22
    {
        height: 36px;
    }
    .style23
    {
        width: 210px;
        height: 36px;
    }
    .style24
    {
        width: 782px;
        height: 36px;
    }
    .style25
    {
        width: 95px;
        height: 27px;
    }
    .style39
    {
        width: 210px;
        height: 86px;
    }
    .style40
    {
        width: 782px;
        height: 86px;
    }
    .style41
    {
        height: 86px;
    }
    .style42
    {
        height: 24px;
    }
    .style45
    {
        width: 1100px;
    }
    .style49
    {
        width: 210px;
    }
    .style51
    {
        width: 210px;
        height: 27px;
    }
    .style61
    {
        width: 782px;
    }
    .style62
    {
        height: 24px;
        width: 782px;
    }
    .style63
    {
        width: 782px;
        height: 27px;
    }
    .style64
    {
        width: 95px;
        height: 37px;
    }
    .style65
    {
        width: 210px;
        height: 37px;
    }
    .style66
    {
        width: 782px;
        height: 37px;
    }
        .style68
        {
            height: 32px;
        }
        .style69
        {
            width: 210px;
            height: 32px;
        }
        .style70
        {
            width: 782px;
            height: 32px;
        }
        .style71
        {
            height: 26px;
        }
        .style72
        {
            width: 210px;
            height: 26px;
        }
        .style73
        {
            width: 782px;
            height: 26px;
        }
        .style74
        {
            height: 25px;
        }
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style45">
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style49">
                &nbsp;</td>
            <td class="style61">
                ABM Viajes Internacionales</td>
        </tr>
        <tr>
            <td class="style2">
                </td>
            <td class="style21">
                </td>
            <td class="style62">
                </td>
        </tr>
        <tr>
            <td class="style22">
                </td>
            <td class="style23">
                Numero De Viaje:</td>
            <td class="style24">
                <asp:TextBox ID="txtNumeroViaje" runat="server" Height="25px" Width="171px" 
                    MaxLength="5"></asp:TextBox>
&nbsp;<asp:Button ID="btnbuscar" runat="server" onclick="btnbuscar_Click" Text="Buscar" />
            </td>
        </tr>
        <tr>
            <td class="style64">
                </td>
            <td class="style65">
                Compañía:</td>
            <td class="style66">
                <asp:DropDownList ID="ddlCompanias" runat="server" Height="25px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style25">
                </td>
            <td class="style51">
                Destino:</td>
            <td class="style63">
                <asp:DropDownList ID="ddlTerminales" runat="server" Height="25px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style25">
                </td>
            <td class="style51">
                Asientos:</td>
            <td class="style63">
                <asp:TextBox ID="txtCantAsientos" runat="server" Height="25px" Width="171px" 
                    MaxLength="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style68">
                </td>
            <td class="style69">
                Partida:</td>
            <td class="style70">
                <uc1:CalendarioPersonalizado ID="cpFechaPartida" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style68">
                </td>
            <td class="style69">
                Arribo:</td>
            <td class="style70">
                <uc1:CalendarioPersonalizado ID="cpFechaArribo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style71">
                </td>
            <td class="style72">
                Servicio abordo:</td>
            <td class="style73">
                <asp:CheckBox ID="chkServicio" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style41">
                </td>
            <td class="style39">
                Documentación necesaria:</td>
            <td class="style40">
                <asp:TextBox ID="txtDocumentacion" runat="server" Rows="4" TextMode="MultiLine" 
                    Width="171px" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style42">
                </td>
            <td class="style21">
                </td>
            <td class="style62">
                </td>
        </tr>
        <tr>
            <td class="style22">
                </td>
            <td class="style22" colspan="2">
                <asp:Button ID="btnagregar" runat="server" Enabled="False" 
                    onclick="btnagregar_Click" Text="Agregar" />
                &nbsp;
                <asp:Button ID="btnmodificar" runat="server" Enabled="False" 
                    onclick="btnmodificar_Click" Text="Modificar" />
                &nbsp;
                <asp:Button ID="btneliminar" runat="server" Enabled="False" 
                    onclick="btneliminar_Click" Text="Eliminar" />
                &nbsp;
                <asp:Button ID="btnlimpiar" runat="server" onclick="btnlimpiar_Click" 
                    Text="Limpar Formulario" />
            </td>
        </tr>
        <tr>
            <td class="style42">
                </td>
            <td class="style21">
                </td>
            <td class="style62">
                </td>
        </tr>
        <tr>
            <td class="style74">
                </td>
            <td class="style74" colspan="2">
                <asp:Label ID="lblerror" runat="server" EnableViewState="False" 
                    style="font-weight: 700; color: #000000; background-color: #FFFFFF"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style42">
                </td>
            <td class="style42" colspan="2">
            </td>
        </tr>
    </table>
    </asp:Content>

