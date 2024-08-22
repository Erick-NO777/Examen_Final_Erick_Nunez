<%@ Page Title="Registrar Entrada" Language="C#" MasterPageFile="~/CapaVistas/Pagina_Principal.Master" AutoEventWireup="true" CodeBehind="Registrar_Entrada.aspx.cs" Inherits="Control_de_Migracion_VS.CapaVistas.Registrar_Entrada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/Pagina_Principal.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Registrar Entrada</h1>

    <asp:Label ID="lblViajeroID" runat="server" Text="ID del Viajero:"></asp:Label>
    <asp:TextBox ID="txtViajeroID" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblFechaEntrada" runat="server" Text="Fecha de Entrada:"></asp:Label>
    <asp:TextBox ID="txtFechaEntrada" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblLugarEntrada" runat="server" Text="Lugar de Entrada:"></asp:Label>
    <asp:TextBox ID="txtLugarEntrada" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Entrada" OnClick="btnRegistrar_Click" />
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Green"></asp:Label>
</asp:Content>
