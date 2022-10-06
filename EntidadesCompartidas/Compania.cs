using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Compania
    {
        private string _nombre;
        private string _direccion;
        private string _telefono;

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if(!String.IsNullOrEmpty(value.Trim()))
                {
                    if (value.Trim().Length > 15)
                    {
                        throw new Exception("El Nombre no puede tener mas 15 carácteres.");
                    }               
                }
                else
                {
                    throw new Exception("El Nombre no puede quedar vacío.");
                }
                _nombre = value.Trim();
            }
        }

        public string Direccion
        {
            get { return _direccion; }
            set
            {
                if (!String.IsNullOrEmpty(value.Trim()))
                {
                    if (value.Trim().Length > 40)
                    {
                        throw new Exception("La Dirección no puede contener más de 40 carácteres.");
                    }
                }
                else
                {
                    throw new Exception("La Dirección no puede quedar vacía.");
                }
                _direccion = value.Trim();
            }
        }

        public string Telefono
        {
            get
            {
                return _telefono;
            }
            set
            {
                if (!String.IsNullOrEmpty(value.Trim()))
                {
                    if (value.Trim().Length < 12)
                    {
                        int n;
                        if (int.TryParse(value.Trim(), out n))
                        {
                            if (n < 1)
                            {
                                throw new Exception("El Teléfono debe ser un número entero y mayor a cero.");
                            }
                        }
                        else
                        {
                            throw new Exception("El Teléfono debe ser un número");
                        }
                    }
                    else
                    {
                        throw new Exception("El Teléfono no puede contener más de 12 números.");
                    }
                }
                else
                {
                    throw new Exception("El Teléfono debe no puede quedar vacío.");
                }               
                _telefono = value.Trim();
            }
    }


        public Compania (string nombre, string direccion , string telefono)
        {
            Nombre = nombre;
            Telefono = telefono;
            Direccion = direccion;           
        }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
