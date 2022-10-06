using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface InterfaceLogicaViajes
    {
        void AltaViaje(Viaje viaje);
        void BajaViaje(Viaje viaje);
        void ModificarViaje(Viaje viaje);
        Viaje BuscarViaje(int numeroViaje);
        List<Viaje> ListaViajes();
    }
}
