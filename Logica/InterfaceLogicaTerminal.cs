using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface InterfaceLogicaTerminal
    {
        Terminal BuscarTerminal(string codigo);
        void AltaTerminal(Terminal terminal);
        void ModificarTerminal(Terminal terminal);
        void BajaTerminal(Terminal terminal);
        List<Terminal> ListaTerminales();
        List<Terminal> ListaTerminalesInactivas();
    }
}
