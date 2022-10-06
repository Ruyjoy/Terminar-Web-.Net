using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaCompania
    {
        Compania BuscarCompania(string nombre);
        void AltaCompania(Compania compania);
        void ModificarCompania(Compania compania);
        void BajaCompania(Compania compania);
        List<Compania> ListaCompanias();
        List<Compania> ListaCompaniasInactivas();
    }
}
