using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Nacional : Viaje
    {
        private int _paradas;

        public int Paradas
        {
            get
            {
                return _paradas;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Número de Paradas tiene que ser mayor o igual a cero.");
                }
                
                _paradas = value;
            }
        }

        public Nacional(int paradas, int numeroViaje, int asientos, DateTime partida, DateTime destino, Compania compania, Terminal terminal, Empleado empleado)
            : base(numeroViaje, asientos, partida, destino, compania, terminal, empleado)
        {
            Paradas = paradas;
        }
    }
}
