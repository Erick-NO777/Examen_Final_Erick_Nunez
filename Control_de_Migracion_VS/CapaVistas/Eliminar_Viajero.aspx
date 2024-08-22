<%@ Page Title="" Language="C#" MasterPageFile="~/CapaVistas/Pagina_Principal.Master" AutoEventWireup="true" CodeBehind="Eliminar_Viajero.aspx.cs" Inherits="Control_de_Migracion_VS.CapaVistas.Eliminar_Viajero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/Pagina_Principal.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>ELIMINAR VIAJERO</h1>
    
    <asp:Label ID="lblID" runat="server" Text="ID del Viajero:"></asp:Label>
    <asp:TextBox ID="txtID" runat="server"></asp:TextBox>    
    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
    <br /><br />

    <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server" ReadOnly="True"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblApellido" runat="server" Text="Apellido:"></asp:Label>
    <asp:TextBox ID="txtApellido" runat="server" ReadOnly="True"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblFechaNacimiento" runat="server" Text="Fecha de Nacimiento:"></asp:Label>
    <asp:TextBox ID="txtFechaNacimiento" runat="server" ReadOnly="True"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblNacionalidad" runat="server" Text="Nacionalidad:"></asp:Label>
    <asp:TextBox ID="txtNacionalidad" runat="server" ReadOnly="True"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblEmail" runat="server" Text="Correo:"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox>
    <br /><br />

    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar Viajero" OnClick="btnEliminar_Click" />
    <br /><br />

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
