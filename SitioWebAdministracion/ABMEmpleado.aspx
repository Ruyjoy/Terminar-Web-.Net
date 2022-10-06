<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpleado.master" AutoEventWireup="true" CodeFile="ABMEmpleado.aspx.cs" Inherits="ABMEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style22
    {
        width: 95px;
    }
    .style23
    {
        width: 210px;
    }
    .style24
    {
        width: 95px;
        height: 27px;
    }
    .style25
    {
        width: 210px;
        height: 27px;
    }
    .style26
    {
        height: 27px;
        width: 410px;
    }
    .style27
    {
        width: 410px;
    }
        .style28
        {
            width: 95px;
            height: 36px;
        }
        .style29
        {
            width: 210px;
            height: 36px;
        }
        .style30
        {
            width: 410px;
            height: 36px;
        }
        .style31
        {
            width: 95px;
            height: 24px;
        }
        .style32
        {
            width: 210px;
            height: 24px;
        }
        .style33
        {
            width: 410px;
            height: 24px;
        }
        .style34
        {
            width: 95px;
            height: 26px;
        }
        .style35
        {
            width: 210px;
            height: 26px;
        }
        .style36
        {
            width: 410px;
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td class="style22">
                &nbsp;</td>
            <td class="style23">
                &nbsp;&nbsp;&nbsp;
            </td>
            <td class="style27">
                ABM Empleado&nbsp;&nbsp;&nbsp;
                </td>
        </tr>
        <tr>
            <td class="style34">
                </td>
            <td class="style35">
                </td>
            <td class="style36">
                </td>
        </tr>
        <tr>
            <td class="style28">
                </td>
            <td class="style29">
                Cedula:
            </td>
            <td class="style30">
                <asp:TextBox ID="txtcedula" runat="server" Height="25px" Width="171px" 
                    MaxLength="8"></asp:TextBox>
                &nbsp;
                <asp:Button ID="btnbuscar" runat="server" onclick="btnbuscar_Click" 
                    Text="Buscar" />
            </td>
        </tr>
        <tr>
            <td class="style24">
                </td>
            <td class="style25">
                Nombre</td>
            <td class="style26">
                <asp:TextBox ID="txtnombre" runat="server" Height="25px" Width="171px" 
                    MaxLength="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style31">
                </td>
            <td class="style32">
                Contraseña: 
            </td>
            <td class="style33">
                <asp:TextBox ID="txtcontraseña" runat="server" Height="22px" 
                    TextMode="Password" Width="171px" MaxLength="6"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style31">
                </td>
            <td class="style32">
                Repita la contraseña:</td>
            <td class="style33">
                <asp:TextBox ID="txtRepetir" runat="server" Height="22px" TextMode="Password" 
                    Width="171px" MaxLength="6"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style31">
                </td>
            <td class="style32">
                </td>
            <td class="style33">
                </td>
        </tr>
        <tr>
            <td class="style22">
                &nbsp;</td>
            <td colspan="2">
                <asp:Button ID="btnagregar" runat="server" onclick="btnagregar_Click" 
                    Text="Agregar" />
                &nbsp;
                <asp:Button ID="btnmodificar" runat="server" onclick="btnmodificar_Click" 
                    Text="Modificar" />
                &nbsp;
                <asp:Button ID="btneliminar" runat="server" onclick="btneliminar_Click" 
                    Text="Eliminar" />
                &nbsp;
                <asp:Button ID="btnlimpiar" runat="server" 
                    onclick="btnlimpiar_Click" Text="Limpiar Formulario" />
            </td>
        </tr>
        <tr>
            <td class="style22">
                &nbsp;</td>
            <td class="style23">
                &nbsp;</td>
            <td class="style27">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style22">
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="lblerror" runat="server" EnableViewState="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style22">
                &nbsp;</td>
            <td class="style23">
                &nbsp;</td>
            <td class="style27">
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>

