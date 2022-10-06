using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Internacional : Viaje
    {
        private string _documentacion;
        private bool _servicios;


        public bool Servicios
        {
            get
            {
                return _servicios;
            }
            set
            {
                _servicios = value;
            }
        }

        public string Documentacion
        {
            get
            {
                return _documentacion;
            }
            set
            {
                if (!String.IsNullOrEmpty(value.Trim()))
                {
                    if (value.Trim().Length > 100)
                    {
                        throw new Exception("No puede tener mas de 100 carácteres.");
                    }
                }
                else
                {
                    throw new Exception("Debe indicar la Documentación necesaria, en caso de no necesitar indicarlo.");
                }
                _documentacion = value.Trim();
            }
        }

        public Internacional(string documentacion, bool servicios, int numeroViaje, int asientos, DateTime partida, DateTime destino, Compania compania, Terminal terminal, Empleado empleado)
            : base(numeroViaje, asientos, partida, destino, compania, terminal, empleado)
        {
            Documentacion = documentacion;
            Servicios = servicios;
        }
    }
}
