using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Facilidad
    {
        string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                descripcion = value;
            }
        }

        public Facilidad(string descripcion)
        {
            Descripcion = descripcion;
        }
    }
}
