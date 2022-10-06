using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface InterfaceLogicaEmpleado
    {
        Empleado Login(int cedula, string pass);

        Empleado Buscar(int cedula);

        void Agregar(Empleado empleado);

        void Modificar(Empleado empleado);

        void Eliminar(Empleado empleado);
    }
}
