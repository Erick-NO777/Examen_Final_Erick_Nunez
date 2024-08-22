using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_de_Migracion_VS.CapaDatos
{
    public class CLS_Entradas
    {
        // Variables de la tabla Entradas
        public static int ENTRADA_ID { get; set; }
        public static int VIAJERO_ID { get; set; }
        public DateTime FECHA_ENTRADA { get; set; }
        public static string LUGAR_ENTRADA { get; set; }
    }
}