using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_de_Migracion_VS.CapaDatos
{
    public class CLS_Salidas
    {
        // Variables de la tabla Salidas
        public static int SALIDA_ID { get; set; }
        public static int VIAJERO_ID { get; set; }
        public DateTime FECHA_SALIDA { get; set; }
        public static string LUGAR_SALIDA { get; set; }
    }
}