using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control_de_Migracion_VS.CapaVistas
{
    public partial class Login : System.Web.UI.Page
    {
        // Se ejecuta al cargar la página
        protected void Page_Load(object sender, EventArgs e)
        {
            // No se realizan acciones al cargar la página en este caso
        }

        // Maneja el clic en el botón de ingreso
        protected void bingresar_Click(object sender, EventArgs e)
        {
            int usuarioId = ValidarUsuario(tusuario.Text, tcontrasena.Text); // Valida el usuario y obtiene el ID

            if (usuarioId != -1) // Si el ID es válido, inicia la sesión
            {
                Session["UsuarioID"] = usuarioId; // Guarda el ID del usuario en la sesión
                Session["UsuarioNombre"] = tusuario.Text; // Guarda el nombre del usuario en la sesión (ajustar según se necesite)

                Response.Redirect("Inicio.aspx"); // Redirige a la página de inicio
            }
            else
            {
                lerror.Text = "Usuario o contraseña incorrectos"; // Muestra un mensaje de error si la validación falla
            }
        }

        // Valida el usuario en la base de datos
        protected int ValidarUsuario(string username, string password)
        {
            try
            {
                // Obtiene la cadena de conexión desde el archivo de configuración
                String s = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(s))
                {
                    conexion.Open(); // Abre la conexión a la base de datos

                    // Ejecuta la consulta para verificar el usuario
                    using (SqlCommand comando = new SqlCommand("SELECT USUARIO_ID FROM Usuarios WHERE USUARIO = @Username AND CONTRASENA = @Password", conexion))
                    {
                        // Agrega parámetros para evitar inyección SQL
                        comando.Parameters.AddWithValue("@Username", username);
                        comando.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader registro = comando.ExecuteReader())
                        {
                            if (registro.Read()) // Si se encuentra un registro
                            {
                                return Convert.ToInt32(registro["USUARIO_ID"]); // Retorna el ID del usuario
                            }
                            else
                            {
                                return -1; // Retorna -1 si no se encuentra el usuario
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lerror.Text = "Ocurrió un error: " + ex.Message; // Muestra un mensaje de error en caso de excepción
                return -1;
            }
        }
    }
}
