using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaViaje
    {
        void AltaViajeNacional(Nacional viaje);
        void AltaViajeInternacional(Internacional viaje);
        void BajaViaje(Viaje viaje);
        void ModificarViajeNacional(Nacional viaje);
        void ModificarViajeInternacional(Internacional viaje);
        Nacional BuscarViajeNacional(int numeroViaje);
        Internacional BuscarViajeInternacional(int numeroViaje);
        List<Viaje> ListaViajes();
    }
}
