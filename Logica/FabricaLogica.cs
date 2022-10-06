using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class FabricaLogica
    {

        public static InterfaceLogicaEmpleado GetLogicaEmpleado()
        {
            return LogicaEmpleado.GetInstancia();
        }

        public static InterfaceLogicaCompania GetLogicaCompania() 
        {
            return LogicaCompania.GetInstancia();
        }

        public static InterfaceLogicaTerminal GetLogicaTerminal()
        {
            return LogicaTerminal.GetInstancia();
        }

        public static InterfaceLogicaViajes GetLogicaViaje()
        {
            return LogicaViajes.GetInstancia();
        }
    }
}
