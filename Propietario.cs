﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3_Condominio
{
    class Propietario
    {
        string dpi;
        string nombre;
        string apellido;
        int cont=0;
        public string Dpi { get => dpi; set => dpi = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public int Cont { get => cont; set => cont = value; }
    }
}
