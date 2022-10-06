<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEmpleado.master" AutoEventWireup="true" CodeFile="ABMTerminales.aspx.cs" Inherits="Terminales" 
EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style12
        {
            width: 150px;
            height: 84px;
        }
        .style20
    {
        width: 95px;
        height: 28px;
    }
    .style22
    {
        width: 95px;
        height: 24px;
    }
    .style26
    {
        width: 95px;
        height: 31px;
    }
    .style27
    {
        width: 95px;
        height: 27px;
    }
    .style32
    {
        width: 95px;
        height: 84px;
    }
    .style34
    {
        width: 410px;
        height: 84px;
    }
    .style38
    {
        width: 95px;
        height: 36px;
    }
    .style43
    {
        width: 95px;
        height: 25px;
    }
    .style45
    {
        width: 100%;
        height: 411px;
    }
        .style46
    {
        width: 95px;
        height: 28px;
    }
        .style56
    {
            width: 410px;
            height: 24px;
        }
    .style57
    {
            height: 31px;
            width: 410px;
        }
    .style58
    {
            width: 410px;
            height: 27px;
        }
        .style60
        {
            width: 210px;
            height: 31px;
        }
        .style61
        {
            width: 210px;
            height: 28px;
        }
        .style62
        {
            width: 210px;
            height: 24px;
        }
        .style63
        {
            width: 210px;
            height: 27px;
        }
        .style64
        {
            width: 210px;
            height: 84px;
        }
        .style65
        {
            height: 36px;
        }
        .style66
        {
            height: 25px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style45">
        <tr>
            <td class="style46">
                </td>
            <td class="style61">
                </td>
            <td>
                ABM Terminales</td>
        </tr>
        <tr>
            <td class="style22">
                </td>
            <td class="style62">
                </td>
            <td class="style56">
                </td>
        </tr>
        <tr>
            <td class="style26">
                </td>
            <td class="style60">
                Código:</td>
            <td class="style57">
                <asp:TextBox ID="txtCodigo" runat="server" Height="25px" Width="171px" 
                    MaxLength="3"></asp:TextBox>
                <asp:Button ID="btnbuscar" runat="server" onclick="btnbuscar_Click" 
                    Text="Buscar" />
            </td>
        </tr>
        <tr>
            <td class="style27">
                </td>
            <td class="style63">
                País</td>
            <td class="style58">
                <asp:DropDownList ID="ddlPaises" runat="server" Height="25px" Width="171px">
                    <asp:ListItem Selected="True" Value="URUGUAY">Uruguay</asp:ListItem>
                    <asp:ListItem Value="ARGENTINA">Argentina</asp:ListItem>
                    <asp:ListItem Value="BRASIL">Brasil</asp:ListItem>
                    <asp:ListItem Value="PARAGUAY">Paraguay</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style27">
                </td>
            <td class="style63">
                Ciudad:</td>
            <td class="style58">
                <asp:TextBox ID="txtnombre" runat="server" Enabled="False" Height="25px" 
                    Width="171px" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style26">
                </td>
            <td class="style60">
                Facilidades:</td>
            <td class="style57">
                <asp:TextBox ID="txtFacilidades" runat="server" Enabled="False" 
                    ondatabinding="BtnAgregarFacilidad_Click" Height="25px" Width="171px" 
                    MaxLength="50"></asp:TextBox>
                <asp:Button ID="BtnAgregarFacilidad" runat="server" 
                    onclick="BtnAgregarFacilidad_Click" Text="Agregar" />
            </td>
        </tr>
        <tr>
            <td class="style32">
                </td>
            <td class="style64">
                </td>
            <td class="style34">
                <asp:ListBox ID="LbFacilidades" runat="server" Width="171px"></asp:ListBox>
                <asp:Button ID="btnBorrar" runat="server" onclick="btnBorrar_Click" 
                    Text="Borrar seleccionado" Width="170px" />
            </td>
        </tr>
        <tr>
            <td class="style22">
                </td>
            <td class="style62">
                </td>
            <td class="style56">
                </td>
        </tr>
        <tr>
            <td class="style38">
                </td>
            <td colspan="2" class="style65">
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
            <td class="style22">
                </td>
            <td class="style62">
                </td>
            <td class="style56">
                </td>
        </tr>
        <tr>
            <td class="style43">
                </td>
            <td colspan="2" class="style66">
                <asp:Label ID="lblerror" runat="server" EnableViewState="False" 
                    style="font-weight: 700; color: #000000; background-color: #FFFFFF"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style22">
                </td>
            <td class="style62">
                </td>
            <td class="style56">
                </td>
        </tr>
    </table>
</asp:Content>

