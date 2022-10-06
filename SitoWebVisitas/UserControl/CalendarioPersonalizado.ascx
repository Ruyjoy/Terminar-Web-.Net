<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CalendarioPersonalizado.ascx.cs" Inherits="CalendarioPersonalizado" %>

<span style="margin: 5px; padding: 5px; border: solid 1px #CCCCCC; background-color: #EEEEEE;">
    día: 
    <asp:DropDownList ID="ddlDia" AutoPostBack="True" runat="server">
    </asp:DropDownList>
    
    mes:
    <asp:DropDownList ID="ddlMes" AutoPostBack="True" runat="server">
    </asp:DropDownList>
    
    año:
    <asp:DropDownList ID="ddlAnio" AutoPostBack="True" runat="server">
    </asp:DropDownList>
&nbsp;
</span>