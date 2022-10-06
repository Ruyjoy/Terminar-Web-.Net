USE master
GO

IF exists(Select * FROM SysDataBases WHERE name='TerminalURU')
BEGIN
	DROP DATABASE TerminalURU
END
GO

CREATE DATABASE TerminalURU
GO

USE TerminalURU
GO

--Creacion de las tablas

CREATE TABLE Companias
(
	Nombre VARCHAR(15) NOT NULL PRIMARY KEY,
	Direccion VARCHAR(40) NOT NULL,
	Telefono VARCHAR(12) NOT NULL,
	Activa BIT Default 1
)
GO

CREATE TABLE Empleados
(
	Cedula INT NOT NULL PRIMARY KEY CHECK(Cedula > 99999 AND Cedula < 100000000),
	Nombre VARCHAR(40) NOT NULL,
	Pass VARCHAR(6) NOT NULL CHECK(LEN(LTRIM(RTRIM(Pass)))=6),
	Activo BIT Default 1
)
GO

CREATE TABLE Terminales
(
	Codigo VARCHAR(3) NOT NULL PRIMARY KEY CHECK(LEN(LTRIM(RTRIM(Codigo)))=3),
	NombreCiudad VARCHAR(20) NOT NULL,
	Pais VARCHAR(20) NOT NULL,
	Activo BIT Default 1
)
GO

CREATE TABLE Facilidades
(
	Codigo VARCHAR(3) NOT NULL FOREIGN KEY REFERENCES Terminales(Codigo),
	Descripcion VARCHAR(50) NOT NULL,
	PRIMARY KEY(Codigo, Descripcion)
)
GO

CREATE TABLE Viajes
(
	NumeroViaje INT NOT NULL PRIMARY KEY,
	CantidadAsientos INT NOT NULL,
	FechaHoraPartida SMALLDATETIME NOT NULL,
	FechaHoraArribo SMALLDATETIME NOT NULL,
	NombreCompania VARCHAR(15) FOREIGN KEY REFERENCES Companias(Nombre),
	CedulaEmpleado INT FOREIGN KEY REFERENCES Empleados(Cedula),
	Destino VARCHAR(3) FOREIGN KEY REFERENCES Terminales(Codigo)
)
GO

CREATE TABLE ViajesNacionales
(
	NumeroViaje INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Viajes(NumeroViaje),
	Paradas INT NOT NULL DEFAULT 0	
)
GO

CREATE TABLE ViajesInternacionales
(
	NumeroViaje INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Viajes(NumeroViaje),
	ServicoAbordo BIT NOT NULL,
	Documentacion VARCHAR(100)
)
GO


-------------------------Empleados-------------------------------------------------------
/**
Devuelvo -1 si la cedula del empleado ya existe
Devuelvo -2 si el pass tiene menos de 6 caracteres sin contar los espacios
Devuelvo -3 si al intentar reactivar el usuario ocurrió un error
Devuelvo 0 si se dió de alta correctamente
Devuelvo 1 si se reactivó el usuario anteriormente dado de baja
Devuelvo cualquier otro número si ocurrió un error al insertar
*/
CREATE PROCEDURE AltaEmpleado 
@Cedula INT, 
@Nombre VARCHAR(40), 
@Pass VARCHAR(6) 
AS
BEGIN
	IF EXISTS(Select * from Empleados where Cedula=@Cedula and Activo=1)
	BEGIN
		RETURN -1
	END
	ELSE 
	BEGIN
		IF (LEN(RTRIM(LTRIM(@Pass)))!=6)
		RETURN -2
	END
	IF EXISTS(Select * from Empleados where Cedula=@Cedula and Activo=0)
	BEGIN
		UPDATE Empleados SET Pass=@Pass, Nombre=@Nombre, Activo=1 where Cedula=@Cedula
		IF(@@ERROR=0)
		BEGIN
			RETURN 1
		END
		ELSE
		BEGIN
			RETURN -3
		END
	END
	ELSE
	BEGIN
		INSERT Empleados(Cedula, Nombre, Pass) VALUES (@Cedula, @Nombre, @Pass)
		RETURN @@ERROR
	END
END
GO

/**
Devuelvo -1 si el empleado no existe
Devuelvo -2 si solo queda un empleado en la tabla
Devuelvo -3 si no pude hacer la baja lógica
Devuelvo 0 si pude dar la baja física correctamente
Devuelvo 1 si pude dar la baja lógica correctamente
Devuelvo cualquier otro número si ocurrió un error al eliminar
*/
CREATE PROCEDURE BajaEmpleado
@Cedula INT
AS
BEGIN
	IF NOT EXISTS(Select * from Empleados where Cedula=@Cedula)
	BEGIN
		RETURN -1
	END
	
	IF (Select COUNT(*) from Empleados)=1
	BEGIN
		RETURN -2
	END
	
	IF EXISTS(Select * from Viajes where CedulaEmpleado=@Cedula)
	BEGIN
		UPDATE Empleados SET Activo=0 where Cedula=@Cedula
		IF(@@ERROR=0)
		BEGIN
			RETURN 1
		END
		ELSE
		BEGIN
			RETURN -3
		END
	END
	ELSE
	BEGIN
		DELETE from Empleados where Cedula=@Cedula
		RETURN @@ERROR	
	END
END
GO

/**
Devuelvo -1 si no existe empleado con esa cedula
Devuelvo -2 si el pass no tiene largo igual a 6 quitando los espacios
Devuelvo -3 si el empleado que intento modificar no está activo
Devuelvo 0 si se modificó el empleado correctamente
Devuelvo cualquier otro error si ocurrió un error al modificar
*/
CREATE PROCEDURE ModificarEmpleado 
@Cedula INT, 
@Nombre VARCHAR(40), 
@Pass VARCHAR(6) 
AS
BEGIN
	IF NOT EXISTS(Select * from Empleados where Cedula=@Cedula)
	BEGIN
		RETURN -1
	END
	ELSE 
	BEGIN
		IF (LEN(RTRIM(LTRIM(@Pass)))!=6)
		RETURN -2
	END
	IF EXISTS(Select * from Empleados where Cedula=@Cedula and Activo=1)
	BEGIN
		UPDATE Empleados SET Nombre=@Nombre, Pass=@Pass where Cedula=@Cedula
		RETURN @@ERROR
	END
	ELSE
	BEGIN
		RETURN -3
	END
END
GO

/**
Devuelvo lista de empleados activos
*/
CREATE PROCEDURE ListaEmpleados
AS
BEGIN
	Select * from Empleados where Activo=1
END
GO

/**
Devuelvo el empleado cuya cédula coincida y esté activo
*/
CREATE PROCEDURE BuscarEmpleado
@Cedula INT
AS
BEGIN
	Select * from Empleados where Cedula=@Cedula and Activo=1
END
GO

/**
Devuelvo -1 si el logueo dio error
Devuelvo -2 si el logueo es correcto pero el usuario no está activo
Devuelvo 0 si el logueo es correcto
*/
CREATE PROCEDURE LoginEmpleado
@Cedula INT,
@Pass VARCHAR(6)
AS
BEGIN
	IF NOT EXISTS(Select * from Empleados where Cedula=@Cedula and Pass=@Pass)
	BEGIN
		RETURN -1
	END
	ELSE
	BEGIN
		IF EXISTS(Select * from Empleados where Cedula=@Cedula and Pass=@Pass and Activo=0)
		BEGIN
			RETURN -2
		END
	END
	Select * from Empleados where Cedula=@Cedula and Pass=@Pass 
	RETURN 0
END
GO


----------------------------Companias----------------------------------------------------

/**
Devuelvo -1 si el nombre de la compania ya existe
Devuelvo 0 si puedo darla de alta correctamente
Devuelvo cualquier otro número si dió error al insertar en la base de datos
*/
CREATE PROCEDURE AltaCompania
@Nombre VARCHAR(15),
@Direccion VARCHAR(40),
@Telefono VARCHAR(10)
AS
BEGIN
	IF EXISTS(Select * from Companias where UPPER(Nombre)=UPPER(@Nombre) and Activa=1)
	BEGIN
		RETURN -1
	END
	IF EXISTS(Select * from Companias where UPPER(Nombre)=UPPER(@Nombre) and Activa=0)
	BEGIN
		UPDATE Companias SET Nombre=@Nombre, Direccion=@Direccion, Telefono=@Telefono, Activa=1 where UPPER(Nombre)=UPPER(@Nombre)
	END
	ELSE
	BEGIN
		INSERT Companias (Nombre, Direccion, Telefono) values (@Nombre, @Direccion, @Telefono)
	END
	RETURN @@ERROR
END
GO

/**
Devuelvo -1 si la compania no existe
Devuelvo 0 si pude modificar la compania
Devuelvo cualquier otro número si hay error al modificar en la base de datos
*/
CREATE PROCEDURE ModificarCompania
@Nombre VARCHAR(15),
@Direccion VARCHAR(40),
@Telefono VARCHAR(10)
AS
BEGIN
	IF NOT EXISTS(Select * from Companias where UPPER(Nombre)=UPPER(@Nombre) and Activa=1)
	BEGIN
		RETURN -1
	END
	UPDATE Companias SET Nombre=@Nombre, Direccion=@Direccion, Telefono=@Telefono where UPPER(Nombre)=UPPER(@Nombre)
	RETURN @@ERROR
END
GO

/**
Devuelvo -1 si no existe la compania
Devuelvo 0 si puedo eliminar correctamente la compania
Devuelvo cualquier otro número si ocurre algún error al eliminar o actualizar la base de datos
*/
CREATE PROCEDURE BajaCompania
@Nombre VARCHAR(15)
AS
BEGIN
	IF NOT EXISTS(Select * from Companias where UPPER(Nombre)=UPPER(@Nombre) and Activa=1)
	BEGIN
		RETURN -1
	END
	IF EXISTS(Select * from Viajes where UPPER(NombreCompania)=UPPER(@Nombre))
	BEGIN
		UPDATE Companias SET Activa=0 where UPPER(Nombre)=UPPER(@Nombre)
	END
	ELSE
	BEGIN
		DELETE from Companias where UPPER(Nombre)=UPPER(@Nombre)
	END	
	RETURN @@ERROR
END
GO

CREATE PROCEDURE BuscarCompania
@Nombre Varchar (15)
AS
BEGIN
	Select * from Companias where UPPER(Nombre)=UPPER(@Nombre) and Activa=1	
END
GO

CREATE PROCEDURE ListaCompanias
AS
BEGIN
	Select * from Companias where Activa=1
END
GO

CREATE PROCEDURE ListaCompaniasInactivas
AS
BEGIN
	Select * from Companias where Activa=0
END
GO

------------------------Terminales-------------------------------------------------------

/**
Devuelvo -1 si el código de terminal ya existe
Devuelvo -2 si el código no es igual a tres carácteres
Devuelvo 0 si puede dar de alta o activar la terminal
Devuelvo cualquier otro número si se produce un error al insertar o modificar en la base de datos
*/
CREATE PROCEDURE AltaTerminal
@Codigo VARCHAR(3),
@Pais VARCHAR(20),
@NombreCiudad VARCHAR(20)
AS
BEGIN
	IF EXISTS(Select * from Terminales where UPPER(Codigo)=UPPER(@Codigo) and Activo = 1)
	BEGIN
		RETURN -1
	END
	IF (LEN(LTRIM(RTRIM(@Codigo)))!=3)
	BEGIN
		RETURN -2
	END
	IF EXISTS(Select * from Terminales where UPPER(Codigo)=UPPER(@Codigo) and Activo = 0)
	BEGIN
		UPDATE Terminales SET Codigo=@Codigo, NombreCiudad=@NombreCiudad, Pais=@Pais, Activo=1 where UPPER(Codigo)=UPPER(@Codigo)
	END
	ELSE
	BEGIN
		INSERT Terminales (Codigo, NombreCiudad, Pais) values (@Codigo, @NombreCiudad, @Pais)
	END
	RETURN @@ERROR
END
GO


/**
Devuelvo -1 si el código de terminal no existe
Devuelvo -2 si el código no es igual a tres carácteres
Devuelvo 0 si pude modificar correctamente
Devuelvo cualquier otro error si ocurre un error al modificar la terminal
*/
CREATE PROCEDURE ModificarTerminal
@Codigo VARCHAR(3),
@Pais VARCHAR(20),
@NombreCiudad VARCHAR(20)
AS
BEGIN
	IF NOT EXISTS(Select * from Terminales where UPPER(Codigo)=UPPER(@Codigo))
	BEGIN
		RETURN -1
	END
	IF (LEN(LTRIM(RTRIM(@Codigo)))!=3)
	BEGIN
		RETURN -2
	END
	UPDATE Terminales SET Codigo=@Codigo, NombreCiudad=@NombreCiudad, Pais=@Pais where UPPER(Codigo)=UPPER(@Codigo)
	RETURN @@ERROR
END
GO

/**
Devuelvo -1 si no existe el código de terminal
Devuelvo 0 si puedo eliminar la terminal con todos los viajes relacionados como destino
Devuelvo cualquier otro número si ocurre algún error al realizar la baja física o lógica
*/
CREATE PROCEDURE BajaTerminal
@Codigo VARCHAR(3)
AS
BEGIN
	IF NOT EXISTS(Select * from Terminales where UPPER(Codigo)=UPPER(@Codigo))
	BEGIN
		RETURN -1		
	END
	IF EXISTS(Select * from Viajes where UPPER(Destino)=UPPER(@Codigo))
	BEGIN
		UPDATE Terminales SET Activo=0 where UPPER(Codigo)=UPPER(@Codigo)
	END
	ELSE
	BEGIN
		DELETE from Terminales where UPPER(Codigo)=UPPER(@Codigo)
	END
	
	RETURN @@ERROR	
END
GO

/**
Devuelvo -1 si no existe la terminal
Devuelvo -2 si ya existe esa facilidad para la terminal
Devulevo 0 si pude dar de alta la facilidad
Devuelvo cualquier otro número si ocurre algún error al insertar en la base de datos
*/
CREATE PROCEDURE AltaFacilidad
@Codigo VARCHAR(3),
@Descripcion VARCHAR(50)
AS
BEGIN
	IF NOT EXISTS(Select * from Terminales where UPPER(Codigo)=UPPER(@Codigo))
	BEGIN
		RETURN -1
	END
	IF EXISTS(Select * from Facilidades where UPPER(Codigo)=UPPER(@Codigo) and UPPER(Descripcion)=UPPER(@Descripcion))
	BEGIN
		RETURN -2
	END
	INSERT Facilidades (Codigo, Descripcion) VALUES (@Codigo, @Descripcion)
	RETURN @@ERROR
END
GO

/**
Devuelvo -1 si no existe la terminal
Devulevo 0 si pude eliminar todas las Facilidades de la terminal
Devuelvo cualquier otro número si ocurre algún error al insertar en la base de datos
*/
CREATE PROCEDURE BajaFacilidadesTerminal
@Codigo VARCHAR(3)
AS
BEGIN
	IF NOT EXISTS(Select * from Terminales where UPPER(Codigo)=UPPER(@Codigo))
	BEGIN
		RETURN -1
	END
	DELETE FROM Facilidades where UPPER(Codigo)=UPPER(@Codigo)
	RETURN @@ERROR
END
GO

CREATE PROCEDURE BuscarTerminal
@Codigo VARCHAR(3)
AS
BEGIN
	Select Terminales.Codigo, Terminales.NombreCiudad, Terminales.Pais, Facilidades.Descripcion
	 from Terminales LEFT JOIN Facilidades ON Terminales.Codigo=Facilidades.Codigo where UPPER(Terminales.Codigo)=UPPER(@Codigo) and Terminales.Activo = 1
END
GO

CREATE PROCEDURE ListaTerminales
AS
BEGIN
	Select * from Terminales where Activo=1
END
GO

CREATE PROCEDURE ListaTerminalesInactivas
AS
BEGIN
	Select * from Terminales where Activo=0
END
GO

-----------------------Viajes------------------------------------------------------------

/**
Devuelvo -1 si hay un horario de partida a un mismo destino con diferencia menor a 2 horas
Devuelvo -2 si ocurre un error al insertar en la tabla viajes
Devuelvo -3 si ocurre un error al insertar en la tabla viajes nacionales
Devuelvo -4 si la fecha de partida es anterior a ahora (GETDATE())
Devuelvo 0 si puede dar de alta el viaje y en @NumeroViaje devuelvo el número de viaje generado
*/
CREATE PROCEDURE AltaViajeNacional
@CantidadAsientos INT,
@FechaHoraPartida SMALLDATETIME,
@FechaHoraArribo SMALLDATETIME,
@NombreCompania VARCHAR(15),
@CedulaEmpleado INT,
@Destino VARCHAR(3),
@Paradas INT,
@NumeroViaje INT OUTPUT
AS
BEGIN
	IF EXISTS(Select * from Viajes where Destino=@Destino and FechaHoraPartida 
		BETWEEN DATEADD(hh,-2,@FechaHoraPartida) and DATEADD(hh,2,@FechaHoraPartida)) 
	BEGIN
		RETURN -1
	END
	IF (@FechaHoraPartida<GETDATE())
	BEGIN
		RETURN -4
	END
	IF((Select COUNT(NumeroViaje) from Viajes)=0)
	BEGIN
		SET @NumeroViaje = 1
	END
	ELSE
	BEGIN
		SET @NumeroViaje = (Select MAX(NumeroViaje)+1 from Viajes)
	END
	BEGIN TRAN
		INSERT Viajes (NumeroViaje, CantidadAsientos, FechaHoraPartida, FechaHoraArribo, NombreCompania, CedulaEmpleado, Destino)
			values (@NumeroViaje, @CantidadAsientos, @FechaHoraPartida, @FechaHoraArribo, @NombreCompania, @CedulaEmpleado, @Destino)
		IF @@ERROR!=0
		BEGIN
			RETURN -2
		END
		INSERT ViajesNacionales (NumeroViaje, Paradas) values (@NumeroViaje, @Paradas)
		IF @@ERROR!=0
		BEGIN
			ROLLBACK TRAN
			RETURN -3
		END
	COMMIT TRAN
	RETURN 0
END
GO

/**
Devuelvo -1 si hay un horario de partida a un mismo destino con diferencia menor a 2 horas
Devuelvo -2 si ocurre un error al insertar en la tabla viajes
Devuelvo -3 si ocurre un error al insertar en la tabla viajes nacionales
Devuelvo -4 si la fecha de partida es anterior a ahora (GETDATE())
Devuelvo 0 si puede dar de alta el viaje y en @NumeroViaje devuelvo el número de viaje generado
*/
CREATE PROCEDURE AltaViajeInternacional
@CantidadAsientos INT,
@FechaHoraPartida SMALLDATETIME,
@FechaHoraArribo SMALLDATETIME,
@NombreCompania VARCHAR(15),
@CedulaEmpleado INT,
@Destino VARCHAR(3),
@ServicoAbordo BIT,
@Documentacion VARCHAR(100),
@NumeroViaje INT OUTPUT
AS
BEGIN
	IF EXISTS(Select * from Viajes where Destino=@Destino and FechaHoraPartida 
		BETWEEN DATEADD(hh,-2,@FechaHoraPartida) and DATEADD(hh,2,@FechaHoraPartida)) 
	BEGIN
		RETURN -1
	END
	IF (@FechaHoraPartida<GETDATE())
	BEGIN
		RETURN -4
	END
	IF((Select COUNT(NumeroViaje) from Viajes)=0)
	BEGIN
		SET @NumeroViaje = 1
	END
	ELSE
	BEGIN
		SET @NumeroViaje = (Select MAX(NumeroViaje)+1 from Viajes)
	END
	BEGIN TRAN
		INSERT Viajes (NumeroViaje, CantidadAsientos, FechaHoraPartida, FechaHoraArribo, NombreCompania, CedulaEmpleado, Destino)
			values (@NumeroViaje, @CantidadAsientos, @FechaHoraPartida, @FechaHoraArribo, @NombreCompania, @CedulaEmpleado, @Destino)
		IF @@ERROR!=0
		BEGIN
			RETURN -2
		END
		INSERT ViajesInternacionales (NumeroViaje, ServicoAbordo, Documentacion) values (@NumeroViaje, @ServicoAbordo, @Documentacion)
		IF @@ERROR!=0
		BEGIN
			ROLLBACK TRAN
			RETURN -3
		END
	COMMIT TRAN
	RETURN 0
END
GO

/**
Devuelvo -1 si ocurre un error al eliminar en Viajes internacionales
Devuelvo -2 si ocurre un error al eliminar en Viajes nacionales
Devuelvo -3 si ocurre un error al eliminar en Viajes
Devuelvo 0 si pude eliminar el viaje correctamente
*/
CREATE PROCEDURE BajaViaje
@NumeroViaje INT
AS
BEGIN
	BEGIN TRAN
		DELETE from ViajesInternacionales where NumeroViaje=@NumeroViaje
		IF @@ERROR!=0
		BEGIN
			RETURN -1
		END
		IF @@ROWCOUNT=0
		BEGIN
			DELETE from ViajesNacionales where NumeroViaje=@NumeroViaje
		END	
		IF @@ERROR!=0
		BEGIN
			ROLLBACK TRAN
			RETURN -2
		END
		DELETE from Viajes where NumeroViaje=@NumeroViaje
		IF @@ERROR!=0
		BEGIN
			ROLLBACK TRAN
			RETURN -3
		END
	COMMIT TRAN
	RETURN 0
END
GO

/**
Devuelvo -1 si no existe el viaje nacional
Devuelvo -2 si la hora de partida no respeta las dos horas de diferencia para el mismo destino
Devuelvo -3 Si la fecha de partida ya pasó
Devuelvo -4 si ocurre algún error al modificar Viajes
Devuelvo -5 si ocurre algún error al modificar ViajesNacionales
Devuelvo 0 si puedo modificar correctamente
*/
CREATE PROCEDURE ModificarViajeNacional
@CantidadAsientos INT,
@FechaHoraPartida SMALLDATETIME,
@FechaHoraArribo SMALLDATETIME,
@NombreCompania VARCHAR(15),
@CedulaEmpleado INT,
@Destino VARCHAR(3),
@Paradas INT,
@NumeroViaje INT
AS
BEGIN
	IF NOT EXISTS(Select * from Viajes inner join ViajesNacionales ON Viajes.NumeroViaje = ViajesNacionales.NumeroViaje where Viajes.NumeroViaje=@NumeroViaje)
	BEGIN
		RETURN -1
	END
	IF EXISTS(Select * from Viajes where NumeroViaje != @NumeroViaje and Destino=@Destino and FechaHoraPartida 
		BETWEEN DATEADD(hh,-2,@FechaHoraPartida) and DATEADD(hh,2,@FechaHoraPartida))
	BEGIN
		RETURN -2
	END
	IF (@FechaHoraPartida<GETDATE())
	BEGIN
		RETURN -3
	END
	BEGIN TRAN
		UPDATE Viajes SET CantidadAsientos=@CantidadAsientos, FechaHoraPartida=@FechaHoraPartida, FechaHoraArribo=@FechaHoraArribo,
			NombreCompania=@NombreCompania, CedulaEmpleado=@CedulaEmpleado, Destino=@Destino where NumeroViaje=@NumeroViaje
		IF @@ERROR!=0
		BEGIN
			RETURN -4
		END
		UPDATE ViajesNacionales SET Paradas=@Paradas where NumeroViaje=@NumeroViaje
		IF @@ERROR!=0
		BEGIN
			ROLLBACK TRAN
			RETURN -5
		END
	COMMIT TRAN	
	RETURN 0
END
GO


/**
Devuelvo -1 si no existe el viaje Internacional
Devuelvo -2 si la hora de partida no respeta las dos horas de diferencia para el mismo destino
Devuelvo -3 Si la fecha de partida ya pasó
Devuelvo -4 si ocurre algún error al modificar Viajes
Devuelvo -5 si ocurre algún error al modificar ViajesInternacionales
Devuelvo 0 si puedo modificar correctamente
*/
CREATE PROCEDURE ModificarViajeInternacional
@CantidadAsientos INT,
@FechaHoraPartida SMALLDATETIME,
@FechaHoraArribo SMALLDATETIME,
@NombreCompania VARCHAR(15),
@CedulaEmpleado INT,
@Destino VARCHAR(3),
@ServicoAbordo BIT,
@Documentacion VARCHAR(100),
@NumeroViaje INT
AS
BEGIN
	IF NOT EXISTS(Select * from Viajes inner join ViajesInternacionales ON Viajes.NumeroViaje = ViajesInternacionales.NumeroViaje where Viajes.NumeroViaje=@NumeroViaje)
	BEGIN
		RETURN -1
	END
	IF EXISTS(Select * from Viajes where NumeroViaje != @NumeroViaje and Destino=@Destino and FechaHoraPartida 
		BETWEEN DATEADD(hh,-2,@FechaHoraPartida) and DATEADD(hh,2,@FechaHoraPartida)) 
	BEGIN
		RETURN -2
	END
	IF (@FechaHoraPartida<GETDATE())
	BEGIN
		RETURN -3
	END
	BEGIN TRAN
		UPDATE Viajes SET CantidadAsientos=@CantidadAsientos, FechaHoraPartida=@FechaHoraPartida, FechaHoraArribo=@FechaHoraArribo,
			NombreCompania=@NombreCompania, CedulaEmpleado=@CedulaEmpleado, Destino=@Destino where NumeroViaje=@NumeroViaje
		IF @@ERROR!=0
		BEGIN
			RETURN -4
		END
		UPDATE ViajesInternacionales SET ServicoAbordo=@ServicoAbordo, Documentacion=@Documentacion where NumeroViaje=@NumeroViaje
		IF @@ERROR!=0
		BEGIN
			ROLLBACK TRAN
			RETURN -5
		END
	COMMIT TRAN
	RETURN 0
END
GO

CREATE PROCEDURE BuscarViajeNacional
@NumeroViaje INT
AS
BEGIN
	IF NOT EXISTS(Select * from ViajesNacionales where NumeroViaje=@NumeroViaje)
	BEGIN
		RETURN -1
	END
	
	Select Viajes.NumeroViaje, Viajes.CantidadAsientos, Viajes.FechaHoraPartida, Viajes.FechaHoraArribo,
		Viajes.NombreCompania, Companias.Direccion, Companias.Telefono, Viajes.CedulaEmpleado, Viajes.Destino, 
		Terminales.Pais, Terminales.NombreCiudad, ViajesNacionales.Paradas, Facilidades.Descripcion
	from ViajesNacionales INNER JOIN Viajes ON ViajesNacionales.NumeroViaje=Viajes.NumeroViaje
		INNER JOIN Companias ON Viajes.NombreCompania=Companias.Nombre
		INNER JOIN Terminales ON Viajes.Destino=Terminales.Codigo
		LEFT JOIN Facilidades ON Terminales.Codigo=Facilidades.Codigo
	where Viajes.NumeroViaje = @NumeroViaje 
END
GO

CREATE PROCEDURE BuscarViajeInternacional
@NumeroViaje INT
AS
BEGIN
	IF NOT EXISTS(Select * from ViajesInternacionales where NumeroViaje=@NumeroViaje)
	BEGIN
		RETURN -1
	END
	
	Select Viajes.NumeroViaje, Viajes.CantidadAsientos, Viajes.FechaHoraPartida, Viajes.FechaHoraArribo,
		Viajes.NombreCompania, Companias.Direccion, Companias.Telefono, Viajes.CedulaEmpleado, Viajes.Destino, 
		Terminales.Pais, Terminales.NombreCiudad, ViajesInternacionales.ServicoAbordo, ViajesInternacionales.Documentacion, Facilidades.Descripcion
	from ViajesInternacionales INNER JOIN Viajes ON ViajesInternacionales.NumeroViaje=Viajes.NumeroViaje
		INNER JOIN Companias ON Viajes.NombreCompania=Companias.Nombre
		INNER JOIN Terminales ON Viajes.Destino=Terminales.Codigo
		LEFT JOIN Facilidades ON Terminales.Codigo=Facilidades.Codigo
	where Viajes.NumeroViaje = @NumeroViaje 
END
GO

CREATE PROCEDURE ListaViajesNacionales
AS
BEGIN
	Select Viajes.NumeroViaje, Viajes.CantidadAsientos, Viajes.FechaHoraPartida, Viajes.FechaHoraArribo,
		Viajes.NombreCompania, Companias.Direccion, Companias.Telefono, Viajes.CedulaEmpleado, Viajes.Destino, 
		Terminales.Pais, Terminales.NombreCiudad, ViajesNacionales.Paradas
	from ViajesNacionales INNER JOIN Viajes ON ViajesNacionales.NumeroViaje=Viajes.NumeroViaje
		INNER JOIN Companias ON Viajes.NombreCompania=Companias.Nombre 
		INNER JOIN Terminales ON Viajes.Destino=Terminales.Codigo 
END
GO


CREATE PROCEDURE ListaViajesInternacionales
AS
BEGIN
	Select Viajes.NumeroViaje, Viajes.CantidadAsientos, Viajes.FechaHoraPartida, Viajes.FechaHoraArribo,
		Viajes.NombreCompania, Companias.Direccion, Companias.Telefono, Viajes.CedulaEmpleado, Viajes.Destino, 
		Terminales.Pais, Terminales.NombreCiudad, ViajesInternacionales.ServicoAbordo, ViajesInternacionales.Documentacion
	from ViajesInternacionales INNER JOIN Viajes ON ViajesInternacionales.NumeroViaje=Viajes.NumeroViaje
		INNER JOIN Companias ON Viajes.NombreCompania=Companias.Nombre 
		INNER JOIN Terminales ON Viajes.Destino=Terminales.Codigo 
END
GO
-----------------Pruebas-------------------------------------------------
DECLARE @retorno int;
EXEC @retorno=AltaEmpleado 38495916,"Alvaro Báez", "admin1";
select case when @retorno=0 then 'OK' else 'ERROR' end AS AltaEmpleado
EXEC @retorno= AltaEmpleado 38495917,"Alvaro Báez", "admin1";
EXEC @retorno= ModificarEmpleado 38495917,"Alvaro Báez", "admin2";
select case when @retorno=0 then 'OK' else 'ERROR' end AS ModificarEmpleado
EXEC @retorno= AltaEmpleado 34231867,"Jose Ferrer", "123456";
select case when @retorno=0 then 'OK' else 'ERROR' end AS AltaEmpleado
EXEC @retorno = BajaEmpleado 38495917;
select case when @retorno=0 then 'OK' else 'ERROR' end AS BajaEmpleado
EXEC @retorno= AltaEmpleado 38495917,"Alvaro Báez", "admin1";

EXEC @retorno = AltaCompania "Buquebus", "18 de Julio algo", "23121231"
select case when @retorno=0 then 'OK' else 'ERROR' end AS AltaCompania
EXEC @retorno = ModificarCompania "Buquebus", "18 de Julio algo", "23121232"
select case when @retorno=0 then 'OK' else 'ERROR' end AS ModificarCompania
EXEC @retorno = BajaCompania "Buquebus"
select case when @retorno=0 then 'OK' else 'ERROR' end AS BajaCompania
EXEC @retorno = AltaCompania "Buquebus", "18 de Julio algo", "23121231"
EXEC AltaCompania "Rutatur", "8 de Octubre", "34322334"
EXEC AltaCompania "Jetmar", "Agraciada 1212", "23061212"
EXEC @retorno = AltaCompania "Aleksin", "Rivera algo", "123456789"

EXEC @retorno = AltaTerminal "AAA", "URUGUAY", "Montevideo"
select case when @retorno=0 then 'OK' else 'ERROR' end AS AltaTerminal
EXEC AltaTerminal "BBB", "URUGUAY", "Canelones"
EXEC AltaTerminal "CCC", "ARGENTINA", "Buenos Aires"
EXEC AltaTerminal "DDD", "BRASIL", "Brasilia"
EXEC AltaTerminal "EEE", "PARAGUAY", "Asunción"
EXEC @retorno = ModificarTerminal "EEE", "PARAGUAY", "Asunción"
select case when @retorno=0 then 'OK' else 'ERROR' end AS ModificarTerminal
EXEC @retorno = BajaTerminal 'EEE'
select case when @retorno=0 then 'OK' else 'ERROR' end AS BajaTerminal

Declare @NumeroViaje int;
EXEC @retorno = AltaViajeNacional 48,"20180901 19:10:00","20180901 20:00:00", "Buquebus", 38495916, "AAA", 8, @NumeroViaje OUTPUT
select case when @retorno=0 then 'OK' else 'ERROR' end AS AltaViajeNacional
EXEC @retorno = AltaViajeNacional 44,"20180901 21:40:00","20180901 23:00:00", "Buquebus", 38495916, "AAA", 8, @NumeroViaje OUTPUT
select case when @NumeroViaje>0 then 'OK' else 'ERROR' end AS AltaViajeNacional;
EXEC @retorno = ModificarViajeNacional 50,"20180901 21:40:00","20180901 23:00:00", "Buquebus", 38495916, "AAA", 8, @NumeroViaje
select case when @retorno=0 then 'OK' else 'ERROR' end AS ModificarViajeNacional
EXEC @retorno = BajaViaje @NumeroViaje
select case when @retorno=0 then 'OK' else 'ERROR' end AS BajaViajeNacional

EXEC @retorno = AltaViajeInternacional 48,"20181001 19:10:00","20181001 20:00:00", "Buquebus", 38495917, "BBB", 1, "Documentos", @NumeroViaje OUTPUT
select case when @retorno=0 then 'OK' else 'ERROR' end AS AltaViajeInternacional
EXEC @retorno = AltaViajeInternacional 44,"20181001 21:40:00","20181001 23:00:00", "Rutatur", 38495916, "CCC", 1, "Documentos", @NumeroViaje OUTPUT
select case when @NumeroViaje>0 then 'OK' else 'ERROR' end AS AltaViajeInternacional;
EXEC @retorno = ModificarViajeInternacional 50,"20181001 21:40:00","20180901 23:00:00", "Buquebus", 38495916, "CCC", 0, "Documentos", @NumeroViaje
select case when @retorno=0 then 'OK' else 'ERROR' end AS ModificarViaje
EXEC @retorno = BajaViaje @NumeroViaje
select case when @retorno=0 then 'OK' else 'ERROR' end AS BajaViajeInternacional

EXEC @retorno = BajaTerminal 'CCC'
select case when @retorno=0 then 'OK' else 'ERROR' end AS BajaLogicaTerminal

EXEC @retorno = BajaCompania "Buquebus"
select case when @retorno=0 then 'OK' else 'ERROR' end AS BajaLogicaCompania

EXEC @retorno = BajaEmpleado 38495917;
select case when @retorno=1 then 'OK' else 'ERROR' end AS BajaLogicaEmpleado

EXEC @retorno = AltaViajeNacional 44,"20180901 19:40:00","20180901 23:00:00", "Buquebus", 38495916, "AAA", 8, @NumeroViaje OUTPUT
select case when @retorno!=0 then 'OK' else 'ERROR' end AS ViajeNacionalNoRespeta2Horas;

EXEC @retorno = AltaViajeInternacional 48,"20181001 19:10:00","20181001 20:00:00", "Buquebus", 38495916, "BBB", 1, "Documentos", @NumeroViaje OUTPUT
select case when @retorno!=0 then 'OK' else 'ERROR' end AS ViajeInternacionalNoRespeta2Horas

EXEC LoginEmpleado 38495916, "admin1";

