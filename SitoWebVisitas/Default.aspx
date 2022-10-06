<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register src="UserControl/CalendarioPersonalizado.ascx" tagname="CalendarioPersonalizado" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .style9
        {
            width: 12%;
            font-size: x-large;
            font-family: Calibri;
            height: 86px;
        }
        .style10
        {
            width: 12%;
            height: 30px;
            font-family: Calibri;
            font-size: large;
        }
        .style12
        {
            width: 150px;
            height: 84px;
        }
        .style25
        {
            width: 1%;
            height: 30px;
        }
        .style31
        {
            width: 799px;
            font-size: x-large;
            font-family: Calibri;
            height: 86px;
        }
        .style32
        {
            width: 799px;
            height: 30px;
            font-family: Calibri;
            font-size: large;
        }
        .style35
        {
            width: 100%;
            height: 803px;
        }
        .style36
        {
            width: 17%;
            height: 86px;
        }
        .style37
        {
            width: 1%;
            height: 86px;
        }
        .style38
        {
            width: 17%;
            height: 30px;
        }
        .style43
        {
            width: 17%;
            height: 34px;
        }
        .style44
        {
            width: 14%;
            height: 34px;
        }
        .style45
        {
            height: 34px;
            width: 638px;
        }
        .style47
        {
            width: 17%;
            height: 32px;
        }
        .style48
        {
            width: 1163px;
            height: 34px;
        }
        .style49
        {
            width: 1%;
            height: 34px;
        }
        .style51
        {
            width: 14%;
            height: 32px;
        }
        .style52
        {
            height: 32px;
            width: 638px;
        }
        .style53
        {
            height: 24px;
        }
        .style54
        {
            width: 1163px;
            height: 24px;
        }
        .style55
        {
            height: 34px;
            width: 12%;
        }
        .style56
        {
            height: 32px;
            width: 12%;
        }
        .style57
        {
            width: 1%;
            height: 24px;
        }
        .style63
        {
            width: 17%;
            height: 24px;
        }
        .style64
        {
            width: 17%;
            height: 46px;
        }
        .style65
        {
            width: 14%;
            height: 46px;
        }
        .style66
        {
            height: 46px;
        }
        .style72
        {
            height: 200px;
        }
        </style>
</head>
<body bgcolor="#669999">
    <form id="form1" runat="server">
    <div>
    
        <table class="style35">
            <tr>
                <td class="style36">
                    &nbsp;</td>
                <td class="style31" style="text-align: center" colspan="2">
                    <img alt="" class="style12" 
                        src="http://localhost:2250/SitioWebAdministracion/Imagenes/Terminal%20Logo.png" /></td>
                <td class="style9" style="text-align: center">
                    &nbsp;</td>
                <td class="style37">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style38">
                </td>
                <td class="style32" style="text-align: center" colspan="2">
                    <strong>CONSULTA DE VIAJES</strong></td>
                <td class="style10" style="text-align: center">
                    </td>
                <td class="style25">
                </td>
            </tr>
            <tr>
                <td class="style63">
                    </td>
                <td class="style54" colspan="3">
                    Filatrar por Destino</td>
                <td class="style57">
                    </td>
            </tr>
            <tr>
                <td class="style64">
                    </td>
                <td class="style65">
                    Destino :&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                <td class="style66" colspan="2">
                    <asp:DropDownList ID="ddlpais" runat="server" Height="23px" Width="170px">
                        <asp:ListItem>Todos los Pasies</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnfiltrodestino" runat="server" Height="27px" 
                        onclick="btnfiltrodestino_Click" Text="Filtro Destino" Width="137px" />
                </td>
            </tr>
            <tr>
                <td class="style63">
                    </td>
                <td class="style53" colspan="3">
                    Filtrar por Compañía</td>
            </tr>
            <tr>
                <td class="style64">
                    </td>
                <td class="style65">
                    Compañía:&nbsp;&nbsp;&nbsp; 
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                <td class="style66" colspan="2">
                    <asp:DropDownList ID="ddlcomp" runat="server" Height="23px" Width="170px">
                        <asp:ListItem>Todas Las Companias</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnfiltrocompania" runat="server" Height="27px" 
                        onclick="btnfiltrocompania_Click" Text="Filtro Compania" Width="137px" />
                </td>
            </tr>
            <tr>
                <td class="style63">
                    </td>
                <td class="style53" colspan="3">
                   Filtrar Por Rango de Fechas de Partida</td>
            </tr>
            <tr>
                <td class="style47">
                    </td>
                <td class="style51">
                    Fecha
                    Inicial:&nbsp; 
                    </td>
                <td class="style52">
                    <uc1:CalendarioPersonalizado ID="calFechaInicial" runat="server" />
                    </td>
                <td class="style56" align="left">
                    </td>
            </tr>
            <tr>
                <td class="style43">
                </td>
                <td class="style44">
                    Fecha Final: &nbsp;&nbsp;&nbsp; 
                     &nbsp;&nbsp;&nbsp;
                    </td>
                <td class="style45">
                     <uc1:CalendarioPersonalizado ID="calFechaFinal" runat="server" />
                    <asp:Button ID="btnfiltrofecha" runat="server" Height="27px" 
                        onclick="btnfiltrofecha_Click" Text="Filtro Fecha" Width="137px" />
                </td>
                <td class="style55" align="left">
                    </td>
            </tr>
            <tr>
                <td class="style53">
                    </td>
                <td class="style54" colspan="3">
                    </td>
                <td class="style57">
                    </td>
            </tr>
            <tr>
                <td class="style43">
                    </td>
                <td class="style48" colspan="3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnfiltro" runat="server" onclick="btnfiltro_Click" 
                        Text="Filtro Completo " Height="27px" style="margin-top: 0px" 
                        Width="142px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;<asp:Button ID="btnlimpiar" runat="server" onclick="btnlimpiar_Click" 
                        Text="Limpiar Filtro" Height="27px" Width="137px" />
                </td>
                <td class="style49">
                    </td>
            </tr>
            <tr>
                <td class="style72" colspan="5">
                    <asp:Repeater ID="rpViajes" runat="server" 
                        onitemcommand="rpViajes_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr bgcolor="#419683">
                                    <td>
                                        Nro:
                                        <asp:TextBox ID="txtnumero" runat="server" ReadOnly="true" 
                                            Text='<%# Bind("NumeroViaje") %>'></asp:TextBox>
                                        <br /> 
                                    </td>
                                    <td>
                                        Compañía:
                                        <asp:TextBox ID="txtcompania" runat="server" ReadOnly="true" 
                                            Text='<%# Bind("Compania.Nombre") %>'></asp:TextBox>
                                        <br /> 
                                    </td>
                                    <td>
                                       Partida:
                                        <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" 
                                            Text='<%# Bind("Partida") %>'></asp:TextBox>
                                        <br /> 
                                    </td>
                                    <td>
                                        Arribo:
                                        <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true" 
                                            Text='<%# Bind("Arribo") %>'></asp:TextBox>
                                        <br /> 
                                    </td>
                                    <td>
                                        Destino:
                                        <asp:TextBox ID="Texarribo" runat="server" ReadOnly="true" 
                                            Text='<%# Bind("Terminal") %>'></asp:TextBox>
                                        <br /> 
                                    </td>
                                    <td>
                                        <asp:Button ID="Button" runat="server" CommandName="VerViaje" 
                                            style="text-align: center" Text="Ver El Viaje" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <table>
                               <tr bgcolor="#9FE9D8">
                                    <td>
                                        Nro:
                                        <asp:TextBox ID="txtnumero" runat="server" ReadOnly="true" 
                                            Text='<%# Bind("NumeroViaje") %>'></asp:TextBox>
                                        <br /> 
                                    </td>
                                    <td>
                                        Compañía:
                                        <asp:TextBox ID="txtcompania" runat="server" ReadOnly="true" 
                                            Text='<%# Bind("Compania.Nombre") %>'></asp:TextBox>
                                        <br /> 
                                    </td>
                                    <td>
                                       Partida:
                                        <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" 
                                            Text='<%# Bind("Partida") %>'></asp:TextBox>
                                        <br /> 
                                    </td>
                                    <td>
                                        Arribo:
                                        <asp:TextBox ID="TextBox2" runat="server" ReadOnly="true" 
                                            Text='<%# Bind("Arribo") %>'></asp:TextBox>
                                        <br /> 
                                    </td>
                                    <td>
                                        Destino:
                                        <asp:TextBox ID="Texarribo" runat="server" ReadOnly="true" 
                                            Text='<%# Bind("Terminal") %>'></asp:TextBox>
                                        <br /> 
                                    </td>
                                    <td>
                                        <asp:Button ID="Button" runat="server" CommandName="VerViaje" 
                                            style="text-align: center" Text="Ver El Viaje" />
                                    </td>
                                </tr>
                            </table>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td class="style53" colspan="4">
                    <asp:Label ID="lblerror" runat="server" EnableViewState="False"></asp:Label>
                </td>
                <td class="style57">
                    </td>
            </tr>
            <tr>
                <td class="style53">
                    &nbsp;</td>
                <td class="style54" colspan="3">
                    &nbsp;</td>
                <td class="style57">
                    &nbsp;</td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
