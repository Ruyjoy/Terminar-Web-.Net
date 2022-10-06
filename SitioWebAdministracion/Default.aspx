<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">



        .style12
        {
            font-size: x-large;
            font-family: Calibri;
        }
        .style1
        {
            height: 23px;
            text-align: left;
        }
        .style7
        {
            height: 50px;
            width: 191px;
            background-color: #66CCFF;
        }
        .style5
        {
            height: 26px;
            width: 19%;
        }
        .style13
        {
            height: 50px;
            text-align: left;
            width: 268435376px;
        }
        .style14
        {
            width: 150px;
            height: 84px;
        }
        .style15
        {
            height: 50px;
            text-align: left;
            width: 19%;
        }
        .style17
        {
            height: 50px;
            text-align: left;
            width: 42%;
        }
        .style18
        {
            height: 26px;
            text-align: left;
            width: 42%;
        }
        .style20
        {
            width: 191px;
            background-color: #66CCFF;
            height: 46px;
        }
        .style21
        {
            font-size: x-large;
            font-family: Calibri;
            width: 364px;
        }
        .style22
        {
            width: 42%;
        }
        .style23
        {
            width: 19%;
        }
        .style24
        {
            width: 42%;
            height: 46px;
        }
        .style25
        {
            height: 46px;
            width: 268435376px;
        }
        .style26
        {
            width: 19%;
            height: 46px;
        }
        .style27
        {
            height: 26px;
            text-align: left;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 87%;">
            <tr>
                <td class="style22">
                    &nbsp;</td>
                <td colspan="4">
                    &nbsp;</td>
                <td class="style23">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style12" colspan="2" style="text-align: center">
                    &nbsp;</td>
                <td class="style21" style="text-align: center" align="left">
                    <img alt="" class="style14" src="Imagenes/Terminal%20Logo.png" /></td>
                <td class="style21" style="text-align: center" align="left">
                    &nbsp;</td>
                <td class="style12" colspan="2" style="text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style22">
                    &nbsp;</td>
                <td colspan="4">
                    &nbsp;</td>
                <td class="style23">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style22">
                    &nbsp;</td>
                <td colspan="4">
                    &nbsp;</td>
                <td class="style23">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1" colspan="6" align="right">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblbienvenida" runat="server" 
                        style="text-align: center; color: #000000;" 
                        
                        Text="Bienvenido/a al sitio web de TerminalURU por favor ingrese su cédula y contraseña para acceder , muchas gracias."></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style24">
                    </td>
                <td class="style20" style="text-align: left" colspan="2">
                    <asp:Label ID="lblnombre" runat="server" CssClass="style11" 
                        style="text-align: center; " Text="Cédula:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtCedula" runat="server" Height="22px" Width="198px"></asp:TextBox>
                </td>
                <td style="text-align: center" class="style25" colspan="2">
                    </td>
                <td class="style26">
                    </td>
            </tr>
            <tr>
                <td class="style17">
                </td>
                <td class="style7" colspan="2">
                    <asp:Label ID="lblcontraseña" runat="server" CssClass="style11" 
                        Text="Contraseña:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   
                    <asp:TextBox ID="txtContraseña" runat="server" Height="22px" 
                        TextMode="Password" Width="198px"></asp:TextBox>
                   
                </td>
                <td class="style13" colspan="2">
                    <asp:ImageButton ID="btnir" runat="server" Height="48px" 
                        ImageUrl="~/Imagenes/login.jpg" onclick="btnir_Click" ToolTip="Acceder" 
                        Width="61px" />
                </td>
                <td class="style15">
                    </td>
            </tr>
            <tr>
                <td class="style18">
                </td>
                <td class="style27" colspan="4">
                </td>
                <td class="style5">
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="6">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblMensaje" runat="server" EnableViewState="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style22">
                    &nbsp;</td>
                <td colspan="4">
                    &nbsp;</td>
                <td class="style23">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
