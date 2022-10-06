using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntidadesCompartidas;
using System.Data.SqlClient;
using System.Data;

namespace Persistencia
{
    internal class PersistenciaEmpleado : IPersistenciaEmpleado
    {
         //singleton
        private static PersistenciaEmpleado _instancia = null;
        private PersistenciaEmpleado() { }
        public static PersistenciaEmpleado GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaEmpleado();
            return _instancia;
        }
        public Empleado LoginEmpleado(int cedula, string pass)
        {           
            SqlConnection oConexion = null;
            Empleado empleado = null;
            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("LoginEmpleado", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@Cedula", cedula);
                oComando.Parameters.AddWithValue("@Pass", pass);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);

                oConexion.Open();
  
                SqlDataReader drEmpleado = oComando.ExecuteReader();


                if (drEmpleado.HasRows)
                {
                    drEmpleado.Read();
                    empleado = new Empleado((int)drEmpleado["Cedula"], (string)drEmpleado["Pass"], (string)drEmpleado["Nombre"]);
                }
                else
                {
                    int resultado = (int)oComando.Parameters["@Retorno"].Value;
                    if (resultado == -1)
                        throw new Exception("Usuario y/o contraseñas incorrectos.");
                    else if (resultado == -2)
                        throw new Exception("El Usuario con el que intenta loguearse no está activo.");
                }
                drEmpleado.Close();
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
            return empleado;
        }

        public Empleado BuscarEmpleado(int Cedula)
        {
            SqlConnection conexion = null;
             
            try
            {
                conexion = new SqlConnection(Conexion.Cnn);

                SqlCommand cmdBuscarEmpleado = new SqlCommand("BuscarEmpleado", conexion);
                cmdBuscarEmpleado.CommandType = CommandType.StoredProcedure;

                cmdBuscarEmpleado.Parameters.AddWithValue("@Cedula", Cedula);

                SqlParameter valorRetorno = new SqlParameter("@valorRetorno", SqlDbType.Int);
                valorRetorno.Direction = ParameterDirection.ReturnValue;
                cmdBuscarEmpleado.Parameters.Add(valorRetorno);

                conexion.Open();
                SqlDataReader drEmpleado = cmdBuscarEmpleado.ExecuteReader();

                Empleado empleado = null;

                if (drEmpleado.HasRows)
                {
                    drEmpleado.Read();
                    empleado = new Empleado((int)drEmpleado["Cedula"], (string)drEmpleado["Pass"], (string)drEmpleado["Nombre"]);
                }
                drEmpleado.Close();
                return empleado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            finally
            {
                if (conexion != null)
                {
                    conexion.Close();
                }
            }
        }

        public void BajaEmpleado(Empleado empleado)
        {
            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("BajaEmpleado", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@Cedula", empleado.Cedula);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);


                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;
                if (resultado == -1)
                {
                    throw new Exception("No existe Empleado con esa Cédula.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("No es posible eliminar el último Empleado.");
                }
                else if (resultado == -3)
                {
                    throw new Exception("El Empleado que intenta eliminar no está activo.");
                }
                else if (resultado != 0 && resultado !=1)
                {
                    throw new Exception("Ocurrió un error inesperado al eliminar el Empleado.");
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

        public void AltaEmpleado(Empleado empleado)
        {
            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("AltaEmpleado", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@Cedula", empleado.Cedula);
                oComando.Parameters.AddWithValue("@Pass", empleado.Pass);
                oComando.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);

                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;
                if (resultado == -1)
                {
                    throw new Exception("El Empleado que intenta dar de alta ya existe.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("La contraseña debe tener 6 carácteres.");
                }
                else if (resultado == -3)
                {
                    throw new Exception("Problemas al activar el Empleado.");
                }
                else if (resultado != 0 && resultado != 1)
                {
                    throw new Exception("Ocurrió un error inesperado al dar de alta el Empleado.");
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

        public void ModificarEmpleado(Empleado empleado)
        {
            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("ModificarEmpleado", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@Cedula", empleado.Cedula);
                oComando.Parameters.AddWithValue("@Pass", empleado.Pass);
                oComando.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);


                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;
                if (resultado == -1)
                {
                    throw new Exception("No existe el Empleado.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("La contraseña debe tener 6 carácteres.");
                }
                else if (resultado == -3)
                {
                    throw new Exception("El Empleado no está activo.");
                }
                else if (resultado != 0)
                {
                    throw new Exception("Ocurrió un error inesperado al modifcar el Empleado.");
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

        public List<Empleado> ListaEmpleados()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();

            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("ListaEmpleados", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader;


                oConexion.Open();
                _Reader = oComando.ExecuteReader();
                while (_Reader.Read())
                {
                    Empleado emp = new Empleado((int)_Reader["Cedula"], (string)_Reader["Pass"], (string)_Reader["Nombre"]);
                    listaEmpleados.Add(emp);
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

            return listaEmpleados;
        }

    }
}
