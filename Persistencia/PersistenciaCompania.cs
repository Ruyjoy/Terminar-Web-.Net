using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    class PersistenciaCompania : IPersistenciaCompania
    {
        public void AltaCompania(Compania compania)
        {
            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("AltaCompania", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                oComando.Parameters.AddWithValue("@Nombre", compania.Nombre);
                oComando.Parameters.AddWithValue("@Direccion", compania.Direccion);
                oComando.Parameters.AddWithValue("@Telefono", compania.Telefono);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);

                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;
                if (resultado == -1)
                {
                    throw new Exception("El Nombre de la Compañía ya existe.");
                }
                else if (resultado != 0)
                {
                    throw new Exception("Ocurrió un error inesperado al dar de alta la Compañía.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }
        }

        public void ModificarCompania(Compania compania)
        {
            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("ModificarCompania", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                oComando.Parameters.AddWithValue("@Nombre", compania.Nombre);
                oComando.Parameters.AddWithValue("@Direccion", compania.Direccion);
                oComando.Parameters.AddWithValue("@Telefono", compania.Telefono);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);

                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;
                if (resultado == -1)
                {
                    throw new Exception("La Compañía no existe o está inactiva.");
                }
                else if (resultado != 0)
                {
                    throw new Exception("Ocurrió un error inesperado al modificar la Compañía.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }
        }

        public void BajaCompania(Compania compania)
        {
            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("BajaCompania", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                oComando.Parameters.AddWithValue("@Nombre", compania.Nombre);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);

                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;
                if (resultado == -1)
                {
                    throw new Exception("La Compañía no existe o está inactiva.");
                }
                else if (resultado != 0)
                {
                    throw new Exception("Ocurrió un error inesperado al dar de baja la Compañía.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }
        }

        public List<Compania> ListaCompanias()
        {
            List<Compania> listaCompanias = new List<Compania>();

            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("ListaCompanias", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader;


                oConexion.Open();
                _Reader = oComando.ExecuteReader();
                while (_Reader.Read())
                {
                    Compania comp = new Compania((string)_Reader["Nombre"], (string)_Reader["Direccion"], (string)_Reader["Telefono"]);
                    listaCompanias.Add(comp);
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }

            return listaCompanias;
        }

        public List<Compania> ListaCompaniasInactivas()
        {
            List<Compania> listaCompanias = new List<Compania>();

            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("ListaCompaniasInactivas", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader;


                oConexion.Open();
                _Reader = oComando.ExecuteReader();
                while (_Reader.Read())
                {
                    Compania comp = new Compania((string)_Reader["Nombre"], (string)_Reader["Direccion"], (string)_Reader["Telefono"]);
                    listaCompanias.Add(comp);
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }

            return listaCompanias;
        }

        public Compania BuscarCompania(string nombre)
        {
            Compania compania = null;
            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);

                SqlCommand oComando = new SqlCommand("BuscarCompania", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

               // SqlDataReader _Reader;
                oComando.Parameters.AddWithValue("@Nombre", nombre);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(valorRetorno);


                oConexion.Open();
                SqlDataReader _Reader;
                _Reader = oComando.ExecuteReader();
                while (_Reader.Read())
                {
                    compania = new Compania((string)_Reader["Nombre"], (string)_Reader["Direccion"], (string)_Reader["Telefono"]);                 
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (oConexion != null)
                    oConexion.Close();
            }

            return compania;
            
        }

        //singleton
        private static PersistenciaCompania _instancia = null;

        private PersistenciaCompania() { }

        public static PersistenciaCompania GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaCompania();
            return _instancia;
        }
    }
}
