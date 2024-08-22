using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Control_de_Migracion_VS.CapaVistas
{
    public partial class Actualizar_Viajero : System.Web.UI.Page
    {
        // Se ejecuta al cargar la página, solo la primera vez
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensaje.Text = string.Empty;
            }
        }

        // Maneja el clic en el botón de búsqueda para cargar datos del viajero
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int viajeroId;
            if (int.TryParse(txtID.Text, out viajeroId))
            {
                CargarDatosViajero(viajeroId);
            }
            else
            {
                lblMensaje.Text = "Por favor ingrese un ID válido.";
            }
        }

        // Carga los datos del viajero y el pasaporte asociado desde la base de datos
        private void CargarDatosViajero(int viajeroId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            string query = @"SELECT V.Nombre, V.Apellido, V.Fecha_Nacimiento, V.Nacionalidad, V.Email, 
                            P.Numero_Pasaporte, P.Fecha_Emision, P.Fecha_Expiracion 
                            FROM Viajeros V 
                            LEFT JOIN Pasaportes P ON V.VIAJERO_ID = P.VIAJERO_ID 
                            WHERE V.VIAJERO_ID = @ViajeroId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ViajeroId", viajeroId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Lee y muestra los datos del viajero
                if (reader.Read())
                {
                    txtNombre.Text = reader["Nombre"].ToString();
                    txtApellido.Text = reader["Apellido"].ToString();
                    txtFechaNacimiento.Text = Convert.ToDateTime(reader["Fecha_Nacimiento"]).ToString("yyyy-MM-dd");
                    txtNacionalidad.Text = reader["Nacionalidad"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtNumeroPasaporte.Text = reader["Numero_Pasaporte"].ToString();
                    txtFechaEmision.Text = Convert.ToDateTime(reader["Fecha_Emision"]).ToString("yyyy-MM-dd");
                    txtFechaExpiracion.Text = Convert.ToDateTime(reader["Fecha_Expiracion"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    lblMensaje.Text = "No se encontró ningún viajero con ese ID.";
                    LimpiarCampos();
                }
            }
        }

        // Actualiza los datos del viajero y el pasaporte asociado
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            int viajeroId;
            if (int.TryParse(txtID.Text, out viajeroId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                // Consulta para actualizar los datos del viajero
                string queryViajeros = @"UPDATE Viajeros SET Nombre = @Nombre, Apellido = @Apellido, 
                                        Fecha_Nacimiento = @FechaNacimiento, Nacionalidad = @Nacionalidad, 
                                        Email = @Email WHERE VIAJERO_ID = @ViajeroId";

                // Consulta para actualizar o insertar los datos del pasaporte
                string queryPasaportes = @"IF EXISTS (SELECT 1 FROM Pasaportes WHERE VIAJERO_ID = @ViajeroId)
                                            BEGIN
                                                UPDATE Pasaportes SET Numero_Pasaporte = @NumeroPasaporte, 
                                                Fecha_Emision = @FechaEmision, Fecha_Expiracion = @FechaExpiracion 
                                                WHERE VIAJERO_ID = @ViajeroId
                                            END
                                            ELSE
                                            BEGIN
                                                INSERT INTO Pasaportes (VIAJERO_ID, Numero_Pasaporte, Fecha_Emision, Fecha_Expiracion) 
                                                VALUES (@ViajeroId, @NumeroPasaporte, @FechaEmision, @FechaExpiracion)
                                            END";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Ejecuta la actualización de los datos del viajero
                    SqlCommand commandViajeros = new SqlCommand(queryViajeros, connection);
                    commandViajeros.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    commandViajeros.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                    commandViajeros.Parameters.AddWithValue("@FechaNacimiento", Convert.ToDateTime(txtFechaNacimiento.Text));
                    commandViajeros.Parameters.AddWithValue("@Nacionalidad", txtNacionalidad.Text);
                    commandViajeros.Parameters.AddWithValue("@Email", txtEmail.Text);
                    commandViajeros.Parameters.AddWithValue("@ViajeroId", viajeroId);
                    commandViajeros.ExecuteNonQuery();

                    // Ejecuta la actualización o inserción del pasaporte
                    SqlCommand commandPasaportes = new SqlCommand(queryPasaportes, connection);
                    commandPasaportes.Parameters.AddWithValue("@NumeroPasaporte", txtNumeroPasaporte.Text);
                    commandPasaportes.Parameters.AddWithValue("@FechaEmision", Convert.ToDateTime(txtFechaEmision.Text));
                    commandPasaportes.Parameters.AddWithValue("@FechaExpiracion", Convert.ToDateTime(txtFechaExpiracion.Text));
                    commandPasaportes.Parameters.AddWithValue("@ViajeroId", viajeroId);
                    commandPasaportes.ExecuteNonQuery();

                    lblMensaje.Text = "Datos del viajero actualizados exitosamente.";
                }
            }
            else
            {
                lblMensaje.Text = "Por favor ingrese un ID válido.";
            }
        }

        // Limpia todos los campos de entrada
        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtNacionalidad.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtNumeroPasaporte.Text = string.Empty;
            txtFechaEmision.Text = string.Empty;
            txtFechaExpiracion.Text = string.Empty;
        }
    }
}
