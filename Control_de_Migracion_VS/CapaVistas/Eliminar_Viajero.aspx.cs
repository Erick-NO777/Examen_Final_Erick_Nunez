using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Control_de_Migracion_VS.CapaVistas
{
    public partial class Eliminar_Viajero : System.Web.UI.Page
    {
        // Se ejecuta al cargar la página, solo la primera vez
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMensaje.Text = string.Empty;
            }
        }

        // Maneja el clic en el botón de búsqueda para cargar los datos del viajero
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

        // Carga los datos del viajero desde la base de datos
        private void CargarDatosViajero(int viajeroId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            string query = @"SELECT V.Nombre, V.Apellido, V.Fecha_Nacimiento, V.Nacionalidad, V.Email 
                            FROM Viajeros V 
                            WHERE V.VIAJERO_ID = @ViajeroId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ViajeroId", viajeroId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Muestra los datos del viajero si se encuentran
                if (reader.Read())
                {
                    txtNombre.Text = reader["Nombre"].ToString();
                    txtApellido.Text = reader["Apellido"].ToString();
                    txtFechaNacimiento.Text = Convert.ToDateTime(reader["Fecha_Nacimiento"]).ToString("yyyy-MM-dd");
                    txtNacionalidad.Text = reader["Nacionalidad"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    lblMensaje.Text = string.Empty;
                }
                else
                {
                    lblMensaje.Text = "No se encontró ningún viajero con ese ID.";
                    LimpiarCampos();
                }
            }
        }

        // Maneja el clic en el botón de eliminar para borrar el viajero
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int viajeroId;
            if (int.TryParse(txtID.Text, out viajeroId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Inicia una transacción para asegurar la consistencia de los datos
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Elimina los pasaportes asociados al viajero
                        string deletePasaportesQuery = @"DELETE FROM Pasaportes WHERE VIAJERO_ID = @ViajeroId";
                        SqlCommand deletePasaportesCommand = new SqlCommand(deletePasaportesQuery, connection, transaction);
                        deletePasaportesCommand.Parameters.AddWithValue("@ViajeroId", viajeroId);
                        deletePasaportesCommand.ExecuteNonQuery();

                        // Elimina al viajero de la base de datos
                        string deleteViajeroQuery = @"DELETE FROM Viajeros WHERE VIAJERO_ID = @ViajeroId";
                        SqlCommand deleteViajeroCommand = new SqlCommand(deleteViajeroQuery, connection, transaction);
                        deleteViajeroCommand.Parameters.AddWithValue("@ViajeroId", viajeroId);
                        int rowsAffected = deleteViajeroCommand.ExecuteNonQuery();

                        // Confirma la transacción si todo salió bien
                        transaction.Commit();

                        if (rowsAffected > 0)
                        {
                            lblMensaje.Text = "Viajero y datos de pasaporte eliminados exitosamente.";
                            LimpiarCampos();
                        }
                        else
                        {
                            lblMensaje.Text = "Ocurrió un error al intentar eliminar el viajero.";
                        }
                    }
                    catch (Exception ex)
                    {
                        // Deshace la transacción en caso de error
                        transaction.Rollback();
                        lblMensaje.Text = "Error al eliminar: " + ex.Message;
                    }
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
            txtID.Text = string.Empty;
        }
    }
}
