using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaViajes : InterfaceLogicaViajes
    {
        public void AltaViaje(Viaje viaje) 
        {
            if (viaje == null)
            {
                throw new ExcepcionPersonalizada("El Viaje es nulo.");
            }

            if (viaje is Nacional)
            {
                IPersistenciaViaje interViaje = FabPersistencia.getPersistenciaViaje();
                interViaje.AltaViajeNacional((Nacional)viaje);
            }
            else if (viaje is Internacional)
            {
                IPersistenciaViaje interViaje = FabPersistencia.getPersistenciaViaje();
                interViaje.AltaViajeInternacional((Internacional)viaje);
            }

            else
            {
                throw new ExcepcionPersonalizada("El tipo de Viaje no es valido");
            }

        }

        public void BajaViaje(Viaje viaje)
        {
            IPersistenciaViaje interViaje = FabPersistencia.getPersistenciaViaje();
                interViaje.BajaViaje(viaje);
        }

        public void ModificarViaje(Viaje viaje) 
        {
            if (viaje == null)
            {
                throw new ExcepcionPersonalizada("El Viaje es nulo.");
            }

            if (viaje is Nacional)
            {
                IPersistenciaViaje interViaje = FabPersistencia.getPersistenciaViaje();
                interViaje.ModificarViajeNacional((Nacional)viaje);
            }
            else if (viaje is Internacional)
            {
                IPersistenciaViaje interViaje = FabPersistencia.getPersistenciaViaje();
                interViaje.ModificarViajeInternacional((Internacional)viaje);
            }

            else
            {
                throw new ExcepcionPersonalizada("El tipo de Viaje no es valido");
            }
        }

        public List<Viaje> ListaViajes()
        {
            List<Viaje> Viajes = new List<Viaje>();

             IPersistenciaViaje interViaje = FabPersistencia.getPersistenciaViaje();

            foreach (Viaje v in interViaje.ListaViajes())
            {
                Viajes.Add(v);
            }

            return Viajes;
        }

         //Singleton
        private static LogicaViajes _instancia = null;

        private LogicaViajes()
        { }

        public static LogicaViajes GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaViajes();
            }

            return _instancia;
        }

        public Viaje BuscarViaje(int numeroViaje)
        {
            IPersistenciaViaje interViaje = FabPersistencia.getPersistenciaViaje();
            Viaje viaje = interViaje.BuscarViajeInternacional(numeroViaje);
            if(viaje == null)
                viaje = interViaje.BuscarViajeNacional(numeroViaje);

            return viaje;
        }
    }
}
