﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia
{
    public class FabPersistencia
    {
        public static IPersistenciaEmpleado getPersistenciaEmpleado()
        {
            return (PersistenciaEmpleado.GetInstancia());
        }

        public static IPersistenciaTerminal getPersistenciaTerminal()
        {
            return (PersistenciaTerminal.GetInstancia());
        }

        public static IPersistenciaCompania getPersistenciaCompania()
        {
            return (PersistenciaCompania.GetInstancia());
        }

        public static IPersistenciaViaje getPersistenciaViaje()
        {
            return (PersistenciaViaje.GetInstancia());
        }
    }
}
