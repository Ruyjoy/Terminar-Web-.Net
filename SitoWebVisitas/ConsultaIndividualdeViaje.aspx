<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaIndividualdeViaje.aspx.cs" Inherits="ConsultaIndividualdeViaje" %>

<%@ Register src="UserControl/Datos.ascx" tagname="Datos" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 150px;
            height: 84px;
        }
        .style2
        {
            width: 380px;
        }
        .style3
        {
            width: 223px;
        }
    </style>
</head>
<body bgcolor="#669999">
    <form id="form1" runat="server">
    <br />
    <table style="width:100%;">
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <img alt="" class="style1" 
        src="http://localhost:2250/SitioWebAdministracion/Imagenes/Terminal%20Logo.png" /><br />
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Ver El Viaje</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
    <uc1:Datos ID="Datos1" runat="server" />
            </td>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Default.aspx">Volver</asp:LinkButton>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
