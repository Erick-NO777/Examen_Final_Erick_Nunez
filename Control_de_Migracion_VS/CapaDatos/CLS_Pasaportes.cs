using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_de_Migracion_VS.CapaDatos
{
    public class CLS_Pasaportes
    {
        // Variables de la tabla Pasaportes
        public static int PASAPORTE_ID { get; set; }
        public static int VIAJERO_ID { get; set; }
        public static string NUMERO_PASAPORTE { get; set; }
        public DateTime FECHA_EMISION { get; set; }
        public DateTime FECHA_EXPIRACION { get; set; }
    }
}