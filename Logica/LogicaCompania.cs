using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaCompania : InterfaceLogicaCompania
    {
        public Compania BuscarCompania(string nombre)
        {
            IPersistenciaCompania IPerCompania = FabPersistencia.getPersistenciaCompania();
            return IPerCompania.BuscarCompania(nombre);
        }

        public void AltaCompania(Compania compania) 
        {
            if (compania == null)
            {
                throw new ExcepcionPersonalizada("La Compania es nula.");
            }
            IPersistenciaCompania IPerCompania = FabPersistencia.getPersistenciaCompania();
            IPerCompania.AltaCompania(compania);
        }

        public void BajaCompania(Compania compania)
        {
            IPersistenciaCompania IPerCompania = FabPersistencia.getPersistenciaCompania();
            IPerCompania.BajaCompania(compania);
        }


        public void ModificarCompania(Compania compania) 
        {
            IPersistenciaCompania IPerCompania = FabPersistencia.getPersistenciaCompania();
            IPerCompania.ModificarCompania(compania);
        }

        public List<Compania> ListaCompanias()
        {
            IPersistenciaCompania IPerCompania = FabPersistencia.getPersistenciaCompania();
            return IPerCompania.ListaCompanias();
        }


        //Singleton
        private static LogicaCompania _instancia = null;

        private LogicaCompania()
        { }

        public static LogicaCompania GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaCompania();
            }

            return _instancia;
        }
    }
}
