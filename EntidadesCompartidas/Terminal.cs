using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntidadesCompartidas
{
    public class Terminal
    {

        private string _codigo;
        private string _nombreciudad;
        private string _pais;
        private List<Facilidad> facilidades;




        public string Codigo
        {
            get
            {
                return _codigo;
            }
            set
            {
                if (value.Trim().Length!=3)
                {
                    throw new Exception("El codigo de la Terminal deben ser tres letras.");
                }
                _codigo = value.Trim().ToUpper();
            }
        }

        public string NombreCiudad
        {
            get
            {
                return _nombreciudad;
            }
            set
            {
                if (!String.IsNullOrEmpty(value.Trim()))
                {
                    if (value.Trim().Length > 20)
                    {
                        throw new Exception("El nombre de la Ciudad no puede tener más de 20 carácteres.");
                    }
                }
                else
                {
                    throw new Exception("El nombre de la Ciudad no puede quedar vacío.");
                }
                
                _nombreciudad = value.Trim();
            }
        }

        public string Pais
        {
            get
            {
                return _pais;
            }
            set
            {
                if (value == "URUGUAY" || value == "ARGENTINA" || value == "BRASIL" || value == "PARAGUAY")
                {
                    _pais = value;
                }
                else
                {
                    throw new Exception("Solamente se permiten países del Mercosur (Uruguay, Argentina, Brasil o Paraguay).");
                }
                
            }
        }

        public List<Facilidad> Facilidades
        {
            get { return facilidades; }
            
        }

        public void AgregarFacilidad(Facilidad facilidad)
        {
            foreach (Facilidad fac in facilidades)
            {
                if(fac.Descripcion.ToUpper().Equals(facilidad.Descripcion.Trim().ToUpper()))
                {
                    throw new Exception("No se puede agregar dos veces la misma Facilidad ('"+facilidad.Descripcion.Trim()+"').");
                }
            }
            Facilidades.Add(facilidad);
        }

        public Terminal(string codigo, string nombreCiudad, string pais)
        {
            Codigo = codigo;
            NombreCiudad = nombreCiudad;
            Pais = pais;
            facilidades = new List<Facilidad>();
        }
        
        public override string ToString()
        {
            return ("Codigo: " + Codigo + " | País: " + Pais + " | Ciudad: " + NombreCiudad);
        }

    }
}
