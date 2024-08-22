<%@ Page Title="" Language="C#" MasterPageFile="~/CapaVistas/Pagina_Principal.Master" AutoEventWireup="true" CodeBehind="Nuevo_Viajero.aspx.cs" Inherits="Control_de_Migracion_VS.CapaVistas.Nuevo_Viajero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/Pagina_Principal.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>INGRESO DE VIAJEROS</h1>
    
    <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblApellido" runat="server" Text="Apellido:"></asp:Label>
    <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblFechaNacimiento" runat="server" Text="Fecha de Nacimiento (Año-mes-dia):"></asp:Label>
    <asp:TextBox ID="txtFechaNacimiento" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblNacionalidad" runat="server" Text="Nacionalidad:"></asp:Label>
    <asp:TextBox ID="txtNacionalidad" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblEmail" runat="server" Text="Correo:"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblNumeroPasaporte" runat="server" Text="Numero de Pasaporte:"></asp:Label>
    <asp:TextBox ID="txtNumeroPasaporte" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblFechaEmision" runat="server" Text="Fecha de Emision (Año-mes-dia):"></asp:Label>
    <asp:TextBox ID="txtFechaEmision" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="lblFechaExpiracion" runat="server" Text="Fecha de Expiracion (Año-mes-dia):"></asp:Label>
    <asp:TextBox ID="txtFechaExpiracion" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Green"></asp:Label>
</asp:Content>