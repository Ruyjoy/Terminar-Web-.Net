using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia;

namespace Logica
{
    internal class LogicaTerminal : InterfaceLogicaTerminal
    {
        public EntidadesCompartidas.Terminal BuscarTerminal(string codigo)
        {
            IPersistenciaTerminal IPerTerminal = FabPersistencia.getPersistenciaTerminal();
            return IPerTerminal.BuscarTerminal(codigo);
        }

        public void AltaTerminal(EntidadesCompartidas.Terminal terminal)
        {

            IPersistenciaTerminal IPerTerminal = FabPersistencia.getPersistenciaTerminal();
            IPerTerminal.AltaTerminal(terminal);

        }

        public void ModificarTerminal(EntidadesCompartidas.Terminal terminal)
        {
            IPersistenciaTerminal IPerTerminal = FabPersistencia.getPersistenciaTerminal();
            IPerTerminal.ModificarTerminal(terminal);
        }

        public void BajaTerminal(EntidadesCompartidas.Terminal terminal)
        {
            IPersistenciaTerminal IPerTerminal = FabPersistencia.getPersistenciaTerminal();
            IPerTerminal.BajaTerminal(terminal);
        }

        public List<EntidadesCompartidas.Terminal> ListaTerminales()
        {
            IPersistenciaTerminal IPerTerminal = FabPersistencia.getPersistenciaTerminal();
            return IPerTerminal.ListaTerminales();
        }

        public List<EntidadesCompartidas.Terminal> ListaTerminalesInactivas()
        {
            IPersistenciaTerminal IPerTerminal = FabPersistencia.getPersistenciaTerminal();
            return IPerTerminal.ListaTerminalesInactivas();
        }

        //Singleton
        private static LogicaTerminal _instancia = null;

        private LogicaTerminal()
        { }

        public static LogicaTerminal GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaTerminal();
            }

            return _instancia;
        }
    }
}
