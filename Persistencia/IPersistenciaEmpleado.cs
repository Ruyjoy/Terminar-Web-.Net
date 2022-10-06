using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaEmpleado
    {
        Empleado LoginEmpleado(int cedula, string pass);
        void AltaEmpleado(Empleado empleado);
        void ModificarEmpleado(Empleado empleado);
        void BajaEmpleado(Empleado empleado);
        List<Empleado> ListaEmpleados();
        Empleado BuscarEmpleado(int cedula);
    }
}
