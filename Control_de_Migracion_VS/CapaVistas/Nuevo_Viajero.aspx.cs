using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace Control_de_Migracion_VS.CapaVistas
{
    public partial class Nuevo_Viajero : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // No se realizan acciones al cargar la página en este caso
        }

        // Maneja el evento de clic del botón Guardar
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            DateTime fechaNacimiento;
            string nacionalidad = txtNacionalidad.Text;
            string email = txtEmail.Text;
            string numeroPasaporte = txtNumeroPasaporte.Text;
            DateTime fechaEmision;
            DateTime fechaExpiracion;

            // Validar las fechas de nacimiento, emisión y expiración
            if (!DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento) ||
                !DateTime.TryParse(txtFechaEmision.Text, out fechaEmision) ||
                !DateTime.TryParse(txtFechaExpiracion.Text, out fechaExpiracion))
            {
                lblMensaje.Text = "Por favor, ingrese fechas válidas."; // Mensaje de error si las fechas no son válidas
                return;
            }

            // Cadena de conexión a la base de datos
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Abre la conexión a la base de datos

                // Insertar el nuevo viajero y obtener el ID generado
                string query = "INSERT INTO Viajeros (NOMBRE, APELLIDO, FECHA_NACIMIENTO, NACIONALIDAD, EMAIL) " +
                               "OUTPUT INSERTED.VIAJERO_ID " +
                               "VALUES (@Nombre, @Apellido, @FechaNacimiento, @Nacionalidad, @Email)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Apellido", apellido);
                    command.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                    command.Parameters.AddWithValue("@Nacionalidad", nacionalidad);
                    command.Parameters.AddWithValue("@Email", email);

                    // Ejecuta el comando y obtiene el ID del nuevo viajero
                    int viajeroId = (int)command.ExecuteScalar();

                    // Insertar el pasaporte para el nuevo viajero
                    query = "INSERT INTO Pasaportes (VIAJERO_ID, NUMERO_PASAPORTE, FECHA_EMISION, FECHA_EXPIRACION) " +
                            "VALUES (@ViajeroId, @NumeroPasaporte, @FechaEmision, @FechaExpiracion)";

                    using (SqlCommand commandPasaporte = new SqlCommand(query, connection))
                    {
                        commandPasaporte.Parameters.AddWithValue("@ViajeroId", viajeroId);
                        commandPasaporte.Parameters.AddWithValue("@NumeroPasaporte", numeroPasaporte);
                        commandPasaporte.Parameters.AddWithValue("@FechaEmision", fechaEmision);
                        commandPasaporte.Parameters.AddWithValue("@FechaExpiracion", fechaExpiracion);

                        commandPasaporte.ExecuteNonQuery(); // Ejecuta el comando para insertar el pasaporte
                    }
                }
            }

            lblMensaje.Text = "Viajero y pasaporte añadidos exitosamente."; // Mensaje de éxito al finalizar
        }
    }
}
