using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;
using EntidadesCompartidas;

namespace Persistencia
{
    internal class PersistenciaViaje : IPersistenciaViaje
    {
        //singleton
        private static PersistenciaViaje _instancia = null;
        private PersistenciaViaje() { }
        public static PersistenciaViaje GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PersistenciaViaje();
            return _instancia;
        }

        public void AltaViajeNacional(Nacional viaje)
        {
            SqlConnection oConexion = null;
            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = null;

                oComando = new SqlCommand("AltaViajeNacional", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@CantidadAsientos", viaje.Asientos);
                oComando.Parameters.AddWithValue("@FechaHoraPartida", viaje.Partida);
                oComando.Parameters.AddWithValue("@FechaHoraArribo", viaje.Arribo);
                oComando.Parameters.AddWithValue("@NombreCompania", viaje.Compania.Nombre);
                oComando.Parameters.AddWithValue("@CedulaEmpleado", viaje.Empleado.Cedula);
                oComando.Parameters.AddWithValue("@Destino", viaje.Terminal.Codigo);
                oComando.Parameters.AddWithValue("@Paradas", viaje.Paradas);

                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                SqlParameter _Salida = new SqlParameter("@NumeroViaje", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                _Salida.Direction = ParameterDirection.Output;
                oComando.Parameters.Add(_Salida);
                oComando.Parameters.Add(_Retorno);

                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;

                if (resultado == -1)
                {
                    throw new Exception("No pueden existir dos viajes al mismo destino con menos de dos horas de diferencia en la partida.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("Ocurrió un error al agregar el viaje.");
                }
                else if (resultado == -3)
                {
                    throw new Exception("Ocurrió un error al agregar el viaje nacional.");
                }
                else if (resultado == -4)
                {
                    throw new Exception("La fecha de partida no puede haber pasado.");
                }
                int numeroViaje = (int)oComando.Parameters["@NumeroViaje"].Value;
                viaje.NumeroViaje = numeroViaje;
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

        public void AltaViajeInternacional(Internacional viaje)
        {
            SqlConnection oConexion = null;
            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = null;
                oComando = new SqlCommand("AltaViajeInternacional", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@CantidadAsientos", viaje.Asientos);
                oComando.Parameters.AddWithValue("@FechaHoraPartida", viaje.Partida);
                oComando.Parameters.AddWithValue("@FechaHoraArribo", viaje.Arribo);
                oComando.Parameters.AddWithValue("@NombreCompania", viaje.Compania.Nombre);
                oComando.Parameters.AddWithValue("@CedulaEmpleado", viaje.Empleado.Cedula);
                oComando.Parameters.AddWithValue("@Destino", viaje.Terminal.Codigo);
                oComando.Parameters.AddWithValue("@Documentacion", viaje.Documentacion);
                oComando.Parameters.AddWithValue("@ServicoAbordo", viaje.Servicios);

                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                SqlParameter _Salida = new SqlParameter("@NumeroViaje", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                _Salida.Direction = ParameterDirection.Output;
                oComando.Parameters.Add(_Salida);
                oComando.Parameters.Add(_Retorno);

                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;

                if (resultado == -1)
                {
                    throw new Exception("No pueden existir dos viajes al mismo destino con menos de dos horas de diferencia en la partida.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("Ocurrió un error al agregar el viaje.");
                }
                else if (resultado == -3)
                {
                    throw new Exception("Ocurrió un error al agregar el viaje internacional.");
                }
                else if (resultado == -4)
                {
                    throw new Exception("La fecha de partida no puede haber pasado.");
                }
                int numeroViaje = (int)oComando.Parameters["@NumeroViaje"].Value;
                viaje.NumeroViaje = numeroViaje;
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

        public void BajaViaje(Viaje viaje)
        {
            SqlConnection oConexion = null;
            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = null;

                oComando = new SqlCommand("BajaViaje", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@NumeroViaje", viaje.NumeroViaje);

                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;

                oComando.Parameters.Add(_Retorno);

                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;

                if (resultado == -1)
                {
                    throw new Exception("Error al eliminar viaje internacional.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("Error al eliminar viaje nacional.");
                }
                else if (resultado == -3)
                {
                    throw new Exception("Ocurrió un error eliminar el viaje.");
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

        public void ModificarViajeNacional(Nacional viaje)
        {
            SqlConnection oConexion = null;
            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = null;

                oComando = new SqlCommand("ModificarViajeNacional", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@NumeroViaje", viaje.NumeroViaje);
                oComando.Parameters.AddWithValue("@CantidadAsientos", viaje.Asientos);
                oComando.Parameters.AddWithValue("@FechaHoraPartida", viaje.Partida);
                oComando.Parameters.AddWithValue("@FechaHoraArribo", viaje.Arribo);
                oComando.Parameters.AddWithValue("@NombreCompania", viaje.Compania.Nombre);
                oComando.Parameters.AddWithValue("@CedulaEmpleado", viaje.Empleado.Cedula);
                oComando.Parameters.AddWithValue("@Destino", viaje.Terminal.Codigo);

                oComando.Parameters.AddWithValue("@Paradas", (viaje).Paradas);

                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);

                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;

                if (resultado == -1)
                {
                    throw new Exception("El viaje no existe.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("No pueden existir dos viajes al mismo destino con menos de dos horas de diferencia en la partida.");
                }
                else if (resultado == -3)
                {
                    throw new Exception("La fecha de partida no puede haber pasado.");
                }
                else if (resultado == -4)
                {
                    throw new Exception("Ocurrió un error al modificar el viaje.");
                }
                else if (resultado == -5)
                {
                    throw new Exception("Ocurrió un error al modificar el viaje Nacional.");
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

        public void ModificarViajeInternacional(Internacional viaje)
        {
            SqlConnection oConexion = null;
            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = null;

                oComando = new SqlCommand("ModificarViajeInternacional", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@NumeroViaje", viaje.NumeroViaje);
                oComando.Parameters.AddWithValue("@CantidadAsientos", viaje.Asientos);
                oComando.Parameters.AddWithValue("@FechaHoraPartida", viaje.Partida);
                oComando.Parameters.AddWithValue("@FechaHoraArribo", viaje.Arribo);
                oComando.Parameters.AddWithValue("@NombreCompania", viaje.Compania.Nombre);
                oComando.Parameters.AddWithValue("@CedulaEmpleado", viaje.Empleado.Cedula);
                oComando.Parameters.AddWithValue("@Destino", viaje.Terminal.Codigo);

                oComando.Parameters.AddWithValue("@Documentacion", viaje.Documentacion);
                oComando.Parameters.AddWithValue("@ServicoAbordo", viaje.Servicios);

                SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
                _Retorno.Direction = ParameterDirection.ReturnValue;
                oComando.Parameters.Add(_Retorno);

                oConexion.Open();
                oComando.ExecuteNonQuery();

                int resultado = (int)oComando.Parameters["@Retorno"].Value;

                if (resultado == -1)
                {
                    throw new Exception("El viaje no existe.");
                }
                else if (resultado == -2)
                {
                    throw new Exception("No pueden existir dos viajes al mismo destino con menos de dos horas de diferencia en la partida.");
                }
                else if (resultado == -3)
                {
                    throw new Exception("La fecha de partida no puede haber pasado.");
                }
                else if (resultado == -4)
                {
                    throw new Exception("Ocurrió un error al modificar el viaje.");
                }
                else if (resultado == -5)
                {
                    throw new Exception("Ocurrió un error al modificar el viaje Internacional.");
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

        public List<Viaje> ListaViajes()
        {
            List<Viaje> listaViajes = new List<Viaje>();

            SqlConnection oConexion = null;

            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = new SqlCommand("ListaViajesNacionales", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;
                SqlDataReader _Reader;


                oConexion.Open();
                _Reader = oComando.ExecuteReader();
                while (_Reader.Read())
                {
                    Compania compania = new Compania((string)_Reader["NombreCompania"], (string)_Reader["Direccion"], (string)_Reader["Telefono"]);
                    Terminal terminal = new Terminal((string)_Reader["Destino"], (string)_Reader["NombreCiudad"], (string)_Reader["Pais"]);
                    Empleado empleado = new Empleado((int)_Reader["CedulaEmpleado"], "111111", "-");
                    Viaje viaje = new Nacional((int)_Reader["Paradas"], (int)_Reader["NumeroViaje"], (int)_Reader["CantidadAsientos"],
                        (DateTime)_Reader["FechaHoraPartida"], (DateTime)_Reader["FechaHoraArribo"], compania, terminal, empleado);
                    listaViajes.Add(viaje);
                }
                _Reader.Close();
                oComando = new SqlCommand("ListaViajesInternacionales", oConexion);
                _Reader = oComando.ExecuteReader();
                while (_Reader.Read())
                {
                    Compania compania = new Compania((string)_Reader["NombreCompania"], (string)_Reader["Direccion"], (string)_Reader["Telefono"]);
                    Terminal terminal = new Terminal((string)_Reader["Destino"], (string)_Reader["NombreCiudad"], (string)_Reader["Pais"]);
                    Empleado empleado = new Empleado((int)_Reader["CedulaEmpleado"], "111111", "-");
                    Viaje viaje = new Internacional((string)_Reader["Documentacion"], (bool)_Reader["ServicoAbordo"], (int)_Reader["NumeroViaje"], (int)_Reader["CantidadAsientos"],
                        (DateTime)_Reader["FechaHoraPartida"], (DateTime)_Reader["FechaHoraArribo"], compania, terminal, empleado);
                    listaViajes.Add(viaje);
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

            return listaViajes;
        }

        public Nacional BuscarViajeNacional(int numeroViaje)
        {
            Nacional viaje = null;
            SqlConnection oConexion = null;
            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = null;

                oComando = new SqlCommand("BuscarViajeNacional", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@NumeroViaje", numeroViaje);

                SqlDataReader _Reader;
                oConexion.Open();
                _Reader = oComando.ExecuteReader();
                while (_Reader.Read())
                {
                    Compania compania = new Compania((string)_Reader["NombreCompania"], (string)_Reader["Direccion"], (string)_Reader["Telefono"]);
                    Terminal terminal = new Terminal((string)_Reader["Destino"], (string)_Reader["NombreCiudad"], (string)_Reader["Pais"]);
                    Empleado empleado = new Empleado((int)_Reader["CedulaEmpleado"], "111111", "-");
                    if (viaje == null)
                    {
                        viaje = new Nacional((int)_Reader["Paradas"], (int)_Reader["NumeroViaje"], (int)_Reader["CantidadAsientos"],
                            (DateTime)_Reader["FechaHoraPartida"], (DateTime)_Reader["FechaHoraArribo"], compania, terminal, empleado);
                    }
                    if (!DBNull.Value.Equals(_Reader["Descripcion"]))
                        viaje.Terminal.AgregarFacilidad(new Facilidad((string)_Reader["Descripcion"]));
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

            return viaje;
        }

        public Internacional BuscarViajeInternacional(int numeroViaje)
        {
            Internacional viaje = null;
            SqlConnection oConexion = null;
            try
            {
                oConexion = new SqlConnection(Conexion.Cnn);
                SqlCommand oComando = null;

                oComando = new SqlCommand("BuscarViajeInternacional", oConexion);
                oComando.CommandType = CommandType.StoredProcedure;

                oComando.Parameters.AddWithValue("@NumeroViaje", numeroViaje);

                SqlDataReader _Reader;
                oConexion.Open();
                _Reader = oComando.ExecuteReader();
                while (_Reader.Read())
                {
                    Compania compania = new Compania((string)_Reader["NombreCompania"], (string)_Reader["Direccion"], (string)_Reader["Telefono"]);
                    Terminal terminal = new Terminal((string)_Reader["Destino"], (string)_Reader["NombreCiudad"], (string)_Reader["Pais"]);
                    Empleado empleado = new Empleado((int)_Reader["CedulaEmpleado"], "111111", "-");
                    if (viaje == null)
                    {
                        viaje = new Internacional((string)_Reader["Documentacion"], (bool)_Reader["ServicoAbordo"], (int)_Reader["NumeroViaje"], (int)_Reader["CantidadAsientos"],
                            (DateTime)_Reader["FechaHoraPartida"], (DateTime)_Reader["FechaHoraArribo"], compania, terminal, empleado);
                    }
                    if (!DBNull.Value.Equals(_Reader["Descripcion"]))
                        viaje.Terminal.AgregarFacilidad(new Facilidad((string)_Reader["Descripcion"]));

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

            return viaje;
        }

    }

}
