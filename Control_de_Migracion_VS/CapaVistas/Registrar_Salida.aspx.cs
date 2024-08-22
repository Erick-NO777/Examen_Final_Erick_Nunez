using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Control_de_Migracion_VS.CapaVistas
{
    public partial class Registrar_Salida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Limpia el mensaje al cargar la página por primera vez
            if (!IsPostBack)
            {
                lblMensaje.Text = string.Empty;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            int viajeroId;
            DateTime fechaSalida;
            string lugarSalida = txtLugarSalida.Text;

            // Valida el ID del viajero y la fecha de salida
            if (int.TryParse(txtViajeroID.Text, out viajeroId) && DateTime.TryParse(txtFechaSalida.Text, out fechaSalida))
            {
                // Verifica que la fecha de salida no sea futura
                if (fechaSalida <= DateTime.Now)
                {
                    // Verifica si el viajero existe en la base de datos
                    if (ViajeroExiste(viajeroId))
                    {
                        RegistrarSalida(viajeroId, fechaSalida, lugarSalida);
                    }
                    else
                    {
                        lblMensaje.Text = "El ID del viajero no existe.";
                    }
                }
                else
                {
                    lblMensaje.Text = "La fecha de salida no puede ser futura.";
                }
            }
            else
            {
                lblMensaje.Text = "Por favor ingrese un ID de viajero válido y una fecha válida.";
            }
        }

        private bool ViajeroExiste(int viajeroId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            string query = @"SELECT COUNT(*) FROM Viajeros WHERE VIAJERO_ID = @ViajeroId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ViajeroId", viajeroId);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                // Retorna true si el viajero existe
                return count > 0;
            }
        }

        private void RegistrarSalida(int viajeroId, DateTime fechaSalida, string lugarSalida)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            string query = @"INSERT INTO Salidas (VIAJERO_ID, FECHA_SALIDA, LUGAR_SALIDA) 
                             VALUES (@ViajeroId, @FechaSalida, @LugarSalida)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ViajeroId", viajeroId);
                command.Parameters.AddWithValue("@FechaSalida", fechaSalida);
                command.Parameters.AddWithValue("@LugarSalida", lugarSalida);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                // Verifica si la inserción fue exitosa
                if (rowsAffected > 0)
                {
                    lblMensaje.Text = "Salida registrada exitosamente.";
                    LimpiarCampos();
                }
                else
                {
                    lblMensaje.Text = "Ocurrió un error al intentar registrar la salida.";
                }
            }
        }

        private void LimpiarCampos()
        {
            // Limpia los campos del formulario
            txtViajeroID.Text = string.Empty;
            txtFechaSalida.Text = string.Empty;
            txtLugarSalida.Text = string.Empty;
        }
    }
}
