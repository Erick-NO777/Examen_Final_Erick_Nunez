using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Control_de_Migracion_VS.CapaVistas
{
    public partial class Registrar_Entrada : System.Web.UI.Page
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
            DateTime fechaEntrada;
            string lugarEntrada = txtLugarEntrada.Text;

            // Valida el ID del viajero y la fecha de entrada
            if (int.TryParse(txtViajeroID.Text, out viajeroId) && DateTime.TryParse(txtFechaEntrada.Text, out fechaEntrada))
            {
                // Verifica que la fecha de entrada no sea futura
                if (fechaEntrada <= DateTime.Now)
                {
                    RegistrarEntrada(viajeroId, fechaEntrada, lugarEntrada);
                }
                else
                {
                    lblMensaje.Text = "La fecha de entrada no puede ser futura.";
                }
            }
            else
            {
                lblMensaje.Text = "Por favor ingrese un ID de viajero válido y una fecha válida.";
            }
        }

        private void RegistrarEntrada(int viajeroId, DateTime fechaEntrada, string lugarEntrada)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            string query = @"INSERT INTO Entradas (VIAJERO_ID, FECHA_ENTRADA, LUGAR_ENTRADA) 
                             VALUES (@ViajeroId, @FechaEntrada, @LugarEntrada)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ViajeroId", viajeroId);
                command.Parameters.AddWithValue("@FechaEntrada", fechaEntrada);
                command.Parameters.AddWithValue("@LugarEntrada", lugarEntrada);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                // Verifica si la inserción fue exitosa
                if (rowsAffected > 0)
                {
                    lblMensaje.Text = "Entrada registrada exitosamente.";
                    LimpiarCampos();
                }
                else
                {
                    lblMensaje.Text = "Ocurrió un error al intentar registrar la entrada.";
                }
            }
        }

        private void LimpiarCampos()
        {
            // Limpia los campos del formulario
            txtViajeroID.Text = string.Empty;
            txtFechaEntrada.Text = string.Empty;
            txtLugarEntrada.Text = string.Empty;
        }
    }
}
