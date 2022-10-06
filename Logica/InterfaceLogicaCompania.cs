using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia;
using EntidadesCompartidas;

namespace Logica
{
    public interface InterfaceLogicaCompania
    {
        Compania BuscarCompania(string nombre);
        void BajaCompania(Compania compania);
        void AltaCompania(Compania compania);
        void ModificarCompania(Compania compania);
        List<Compania> ListaCompanias();
    }
}
