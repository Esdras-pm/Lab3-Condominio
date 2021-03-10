using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3_Condominio
{
    class Casa
    {
        string nombre;
        string apellido;
        string nocasa;
        float cuota;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Nocasa { get => nocasa; set => nocasa = value; }
        public float Cuota { get => cuota; set => cuota = value; }
    }
}
