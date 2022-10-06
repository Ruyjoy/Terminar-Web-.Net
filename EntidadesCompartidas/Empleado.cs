using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Empleado
    {
        private int _cedula;
        private string _pass;
        private string _nombre;
        
        public int Cedula
        {
            get { return _cedula; }
            set
            {
                if (value > 99999 && value < 100000000)
                    _cedula = value;
                else
                    throw new Exception("La Cédula debe tener entre 6 y 8 dígitos (incluído el dígito).");
            }
        }
        public string Pass
        {
            get { return _pass; }
            set
            {
                if (!String.IsNullOrEmpty(value.Trim()))
                {
                    if (value.Trim().Length != 6)
                    {
                        throw new ExcepcionPersonalizada("La Contraseña debe tener 6 carácteres.");
                    }
                }
                else
                {
                    throw new Exception("La Contraseña no puede quedar vacía.");
                }
                _pass = value;               
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (!String.IsNullOrEmpty(value.Trim()))
                {
                    if (value.Trim().Length > 40)
                    {
                        throw new Exception("El Nombre no puede tener más de 40 carácteres.");
                    }
                }
                else
                {
                    throw new Exception("El Nombre no puede quedar vacío.");
                }
                _nombre = value.Trim();
                    
            }
        }
       
        public Empleado(int cedula, string contraseña, string nombre)
        {
            Cedula = cedula;
            Pass = contraseña;
            Nombre = nombre;
        }
     
        public override string ToString()
        {
            return ("Cedula: " + Cedula + "Nombre"+ Nombre + "; Contraseña: " + Pass);
        }
    }
  }

