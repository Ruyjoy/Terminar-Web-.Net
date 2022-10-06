using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class ExcepcionPersonalizada : Exception
    {
        public ExcepcionPersonalizada()
        {

        }

        public ExcepcionPersonalizada(string mensaje)
            : base(mensaje)
        {

        }

        public ExcepcionPersonalizada(string mensaje, Exception excepcionInterna)
            : base(mensaje, excepcionInterna)
        {

        }
    }
}
