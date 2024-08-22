using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Control_de_Migracion_VS.CapaDatos
{
    public class CLS_Viajeros
    {
        // Variables de la tabla Viajeros
        public static int VIAJERO_ID { get; set; }
        public static string NOMBRE { get; set; }
        public static string APELLIDO { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
        public static string NACIONALIDAD { get; set; }
        public static string EMAIL { get; set; }
    }
}   