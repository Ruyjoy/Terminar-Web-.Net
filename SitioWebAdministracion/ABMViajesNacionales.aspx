<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpleado.master" AutoEventWireup="true" CodeFile="ABMViajesNacionales.aspx.cs" Inherits="ABMViajesNacionales" %>

<%@ Register src="UserControl/CalendarioPersonalizado.ascx" tagname="CalendarioPersonalizado" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


        .style23
    {
        height: 24px;
    }
    .style25
    {
        width: 95px;
        height: 24px;
    }
    .style30
    {
        height: 24px;
        width: 210px;
    }
    .style34
    {
        height: 29px;
        width: 210px;
    }
    .style35
    {
        height: 36px;
    }
    .style36
    {
        width: 210px;
        height: 36px;
    }
    .style37
    {
        width: 782px;
        height: 36px;
    }
    .style38
    {
        width: 95px;
        height: 27px;
    }
    .style39
    {
        height: 27px;
        width: 210px;
    }
    .style41
    {
        width: 95px;
        height: 29px;
    }
    .style43
    {
        height: 27px;
    }
    .style50
    {
        width: 1100px;
        height: 421px;
    }
    .style51
    {
        width: 782px;
        height: 24px;
    }
    .style52
    {
        width: 782px;
        height: 27px;
    }
    .style53
    {
        width: 782px;
        height: 29px;
    }
    .style57
    {
        height: 32px;
    }
    .style58
    {
        height: 32px;
        width: 210px;
    }
    .style59
    {
        width: 782px;
        height: 32px;
    }
    .style61
    {
        height: 25px;
    }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style50">
        <tr>
            <td class="style23">
                </td>
            <td class="style30">
                </td>
            <td class="style51">
                ABM Viajes Nacionales</td>
        </tr>
        <tr>
            <td class="style25">
                </td>
            <td class="style30">
                </td>
            <td class="style51">
                </td>
        </tr>
        <tr>
            <td class="style35">
                </td>
            <td class="style36">
                Numero De Viaje:</td>
            <td class="style37">
                <asp:TextBox ID="txtNumeroViaje" runat="server" Height="25px" Width="171px" 
                    MaxLength="5"></asp:TextBox>
&nbsp;<asp:Button ID="btnbuscar" runat="server" onclick="btnbuscar_Click" Text="Buscar" />
            </td>
        </tr>
        <tr>
            <td class="style38">
                </td>
            <td class="style39">
                Compañía:</td>
            <td class="style52">
                <asp:DropDownList ID="ddlCompanias" runat="server" Height="25px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style38">
                </td>
            <td class="style39">
                Destino:</td>
            <td class="style52">
                <asp:DropDownList ID="ddlTerminales" runat="server" Height="25px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style41">
                </td>
            <td class="style34">
                Asientos:</td>
            <td class="style53">
                <asp:TextBox ID="txtCantAsientos" runat="server" Height="25px" Width="171px" 
                    MaxLength="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style57">
                </td>
            <td class="style58">
                Partida:</td>
            <td class="style59">
                <uc1:CalendarioPersonalizado ID="cpFechaPartida" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style57">
                </td>
            <td class="style58">
                Arribo:</td>
            <td class="style59">
                <uc1:CalendarioPersonalizado ID="cpFechaArribo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style43">
                </td>
            <td class="style39">
                Paradas:</td>
            <td class="style52">
                <asp:TextBox ID="txtCantParadas" runat="server" Height="25px" Width="171px" 
                    MaxLength="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style23">
                </td>
            <td class="style30">
                </td>
            <td class="style51">
                </td>
        </tr>
        <tr>
            <td class="style35">
                </td>
            <td class="style35" colspan="2">
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
            <td class="style23">
                </td>
            <td class="style30">
                </td>
            <td class="style51">
                </td>
        </tr>
        <tr>
            <td class="style61">
                </td>
            <td class="style61" colspan="2">
                <asp:Label ID="lblerror" runat="server" EnableViewState="False" 
                    style="font-weight: 700; color: #000000; background-color: #FFFFFF"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style23">
                </td>
            <td class="style23">
            </td>
            <td class="style51">
            </td>
        </tr>
    </table>
</asp:Content>

