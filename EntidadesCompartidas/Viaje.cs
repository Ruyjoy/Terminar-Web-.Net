using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public abstract class Viaje
    {
        private int _numeroViaje;
        private int _asientos;
        private DateTime _partida;
        private DateTime _arribo;
        private Compania _compania;
        private Terminal _destino;
        private Empleado _empleado;

        public Compania Compania
        {
            get { return _compania; }
            set
            {
                if (value != null)
                    _compania = value;
                else
                    throw new Exception("La compañía no puede ser nula.");
            }
        }

        public Terminal Terminal
        {
            get { return _destino; }
            set
            {
                if (value != null)
                    _destino = value;
                else
                    throw new Exception("El destino no puede ser nulo.");
            }
        }

        public Empleado Empleado
        {
            get { return _empleado; }
            set
            {
                if (value != null)
                    _empleado = value;
                else
                    throw new Exception("El empleado no puede ser nulo.");
            }
        }


        public int NumeroViaje
        {
            get { return _numeroViaje; }
            set
            {
                if (value > 0)
                    _numeroViaje = value;
                else
                    throw new Exception("El número de viaje debe ser mayor a cero.");
            }
        }

        public int Asientos
        {
            get { return _asientos; }
            set
            {
                if (value < 0)
                    throw new Exception("La cantidad de asientos no puede ser negativa.");

                _asientos = value;
            }
        }

        public DateTime Partida
        {
            get
            {
                return _partida;
            }
            set
            {                
                _partida = value;
            }
        }

        public DateTime Arribo
        {
            get
            {
                return _arribo;
            }
            set
            {

                if (value.CompareTo(Partida) <= 0)
                {
                    throw new Exception("Fecha de Arribo debe ser mayor a la fecha de Partida.");
                }

                _arribo = value;
            }
        }

        public Viaje(int numeroViaje, int asientos, DateTime partida, DateTime destino, Compania compania, Terminal terminal, Empleado empleado)
        {
            NumeroViaje = numeroViaje;
            Asientos = asientos;
            Partida = partida;
            Arribo = destino;
            Compania = compania;
            Terminal = terminal;
            Empleado = empleado;
            
        }
    }
}
