using System;
using System.Collections.Generic;
using System.Text;

namespace PM2E2GRUPOLENINS.Modelo
{
    public class Sitio
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public byte[] VideoDigital { get; set; }
        public byte[] AudioFile { get; set; }
    }
}
