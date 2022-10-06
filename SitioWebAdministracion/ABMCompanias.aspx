<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpleado.master" AutoEventWireup="true" CodeFile="ABMCompanias.aspx.cs" Inherits="ABMCompanias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style12
    {
        width: 43px;
        height: 26px;
    }
        .style26
    {
        width: 410px;
        height: 36px;
    }
    .style30
    {
        width: 95px;
        height: 36px;
    }
    .style31
    {
        height: 36px;
    }
    .style32
    {
        height: 295px;
    }
    .style36
    {
        height: 25px;
    }
    .style55
    {
        width: 410px;
        height: 24px;
    }
    .style57
    {
        width: 410px;
        height: 27px;
    }
    .style58
    {
        width: 95px;
        height: 24px;
    }
    .style60
    {
        width: 95px;
        height: 27px;
    }
    .style62
    {
        width: 210px;
        height: 24px;
    }
    .style63
    {
        width: 210px;
        height: 36px;
    }
    .style64
    {
        width: 210px;
        height: 27px;
    }
    .style66
    {
        width: 95px;
        height: 25px;
    }
    .style67
    {
        width: 95px;
        height: 29px;
    }
    .style68
    {
        width: 210px;
        height: 29px;
    }
    .style69
    {
        width: 410px;
        height: 29px;
    }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style32">
        <tr>
            <td class="style58">
                </td>
            <td class="style62">
                </td>
            <td>
                ABM Compañías</td>
        </tr>
        <tr>
            <td class="style58">
                </td>
            <td class="style62">
                </td>
            <td class="style55">
                </td>
        </tr>
        <tr>
            <td class="style30">
                </td>
            <td class="style63">
                Nombre:</td>
            <td class="style26">
                <asp:TextBox ID="txtnombre" runat="server" Height="25px" Width="171px" 
                    MaxLength="15"></asp:TextBox>
&nbsp;<asp:Button ID="btnbuscar" runat="server" onclick="btnbuscar_Click" Text="Buscar" />
            </td>
        </tr>
        <tr>
            <td class="style60">
                </td>
            <td class="style64">
                Dirección:</td>
            <td class="style57">
                <asp:TextBox ID="txtdireccion" runat="server" Height="25px" Width="171px" 
                    MaxLength="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style67">
                </td>
            <td class="style68">
                Teléfono:</td>
            <td class="style69">
                <asp:TextBox ID="txttelefono" runat="server" Height="25px" Width="171px" 
                    MaxLength="12"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style58">
                </td>
            <td class="style62">
                </td>
            <td class="style55">
                </td>
        </tr>
        <tr>
            <td class="style30">
                </td>
            <td class="style31" colspan="2">
                <asp:Button ID="btnagregar" runat="server" Enabled="False" 
                    onclick="btnagregar_Click" Text="Agregar" />
                &nbsp;
                <asp:Button ID="btnmodificar" runat="server" Enabled="False" 
                    onclick="btnmodificar_Click" Text="Modificar" />
                &nbsp;
                <asp:Button ID="btneliminar" runat="server" Enabled="False" 
                    onclick="btneliminar_Click" Text="Eliminar" />
                &nbsp;<asp:Button ID="btnlimpiar" runat="server" onclick="btnlimpiar_Click" 
                    Text="Limpar Formulario" />
            &nbsp;</td>
        </tr>
        <tr>
            <td class="style58">
                </td>
            <td class="style62">
                </td>
            <td class="style55">
                </td>
        </tr>
        <tr>
            <td class="style66">
                </td>
            <td class="style36" colspan="2">
                <asp:Label ID="lblerror" runat="server" EnableViewState="False" 
                    style="font-weight: 700; color: #000000; background-color: #FFFFFF"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style58">
                </td>
            <td class="style62">
            </td>
            <td class="style55">
            </td>
        </tr>
    </table>
</asp:Content>

