<%@ Page Title="" Language="C#" MasterPageFile="~/CapaVistas/Pagina_Principal.Master" AutoEventWireup="true" CodeBehind="Registrar_Salida.aspx.cs" Inherits="Control_de_Migracion_VS.CapaVistas.Registrar_Salida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/Pagina_Principal.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>REGISTRO DE SALIDA DE VIAJEROS</h1>

    <asp:Label ID="lblViajeroID" runat="server" Text="ID del Viajero:"></asp:Label>
    <asp:TextBox ID="txtViajeroID" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblFechaSalida" runat="server" Text="Fecha de Salida:"></asp:Label>
    <asp:TextBox ID="txtFechaSalida" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblLugarSalida" runat="server" Text="Lugar de Salida:"></asp:Label>
    <asp:TextBox ID="txtLugarSalida" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" />
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Green"></asp:Label>
</asp:Content>
