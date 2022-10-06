using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;


namespace Logica
{
    internal class LogicaEmpleado : InterfaceLogicaEmpleado
    {

        public Empleado Login(int cedula, string pass)
        {
            IPersistenciaEmpleado IPerEmpleado = FabPersistencia.getPersistenciaEmpleado();
            return IPerEmpleado.LoginEmpleado(cedula, pass);
        }

        public Empleado Buscar(int Cedula)
        {
            IPersistenciaEmpleado interEmpleado = FabPersistencia.getPersistenciaEmpleado();

            return interEmpleado.BuscarEmpleado(Cedula);
        }

        public void Agregar(Empleado empleado)
        {
            if (empleado == null)
            {
                throw new ExcepcionPersonalizada("El empleado es nulo.");
            }

            IPersistenciaEmpleado interEmpleado = FabPersistencia.getPersistenciaEmpleado();
            interEmpleado.AltaEmpleado(empleado);
        }

        public void Modificar(Empleado empleado)
        {
            if (empleado == null)
            {
                throw new ExcepcionPersonalizada("El empleado es nulo.");
            }

            IPersistenciaEmpleado interEmpleado = FabPersistencia.getPersistenciaEmpleado();

            interEmpleado.ModificarEmpleado(empleado);
        }

        public void Eliminar(Empleado empleado)
        {
            if (empleado == null)
            {
                throw new ExcepcionPersonalizada("El empleado es nulo.");
            }

            IPersistenciaEmpleado interEmpleado = FabPersistencia.getPersistenciaEmpleado();

            interEmpleado.BajaEmpleado(empleado);
        }

        //Singleton
        private static LogicaEmpleado _instancia = null;

        private LogicaEmpleado()
        { }

        public static LogicaEmpleado GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaEmpleado();
            }

            return _instancia;
        }
    }
}
