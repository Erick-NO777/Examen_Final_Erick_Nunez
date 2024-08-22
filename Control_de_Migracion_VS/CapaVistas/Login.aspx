<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Control_de_Migracion_VS.CapaVistas.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="../CSS/Login.css" rel="stylesheet" />
</head>
<body>
    <section class="container">
        <div class="login-container">
            <div class="circle circle-one"></div>
            <div class="form-container">
                <img src="https://raw.githubusercontent.com/hicodersofficial/glassmorphism-login-form/master/assets/illustration.png" alt="illustration" class="illustration" />
                <h1 class="opacity">LOGIN</h1>
                <form id="form1" runat="server" >
                    <asp:TextBox ID="tusuario" runat="server" placeholder="USUARIO"></asp:TextBox>
                    <asp:TextBox ID="tcontrasena" type ="Password" runat="server" placeholder="CONTRASEÑA"></asp:TextBox>
                    <asp:Button ID="bingresar" runat="server" Text="Iniciar Sesion" OnClick="bingresar_Click"/>
                    <asp:Label ID="lerror" runat="server" Text=""></asp:Label>
                </form>
            </div>
            <div class="circle circle-two"></div>
        </div>
        <div class="theme-btn-container"></div>
    </section>
    <script src="../java/Theme.js"></script>
</body>
</html>