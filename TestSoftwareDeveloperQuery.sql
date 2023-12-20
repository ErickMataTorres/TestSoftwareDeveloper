-- Crear base de datos
CREATE DATABASE TestSoftwareDeveloper
-- Usar la base de datos creada
USE TestSoftwareDeveloper
-- Creaci�n de la tabla personas con sus campos correspondientes
CREATE TABLE Personas
(
	Id INT NOT NULL IDENTITY,
	Nombre VARCHAR(100) NOT NULL,
	ApellidoPaterno VARCHAR(100) NOT NULL,
	ApellidoMaterno VARCHAR(100),
	Identificacion VARCHAR(100) PRIMARY KEY
)
-- Procedimiento almacenado con validaciones para el registro y modificaci�n de una persona
ALTER PROCEDURE spRegistrarPersona
@Id INT,
@Nombre VARCHAR(100),
@ApellidoPaterno VARCHAR(100),
@ApellidoMaterno VARCHAR(100),
@Identificacion VARCHAR(100)
AS
BEGIN
	-- Condici�n de si no existe el Id ingresado entrar� aqu�
	IF NOT EXISTS(SELECT Id FROM Personas WHERE Id=@Id)
	BEGIN
		-- Condici�n de si existe la identificaci�n ingresada entrar� aqu�
		IF NOT EXISTS (SELECT Identificacion FROM Personas WHERE Identificacion=@Identificacion)
		BEGIN
			/* Instrucci�n para guardar los datos de la persona de acuerdo a los datos ingresados
			  en la p�gina web de directorio de personas */
			INSERT INTO Personas (Nombre, ApellidoPaterno, ApellidoMaterno, Identificacion)
			VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Identificacion);
			/* Se selecciona este mensaje con el fin de enviarlo como respuesta a la p�gina web de directorio de personas
			   al registrar la persona satisfactoriamente */
			SELECT '1' AS [Id], 'Se ha registrado correctamente' AS [Nombre];	
		END
		ELSE
		BEGIN
			/* Si ya existe la identificaci�n que se esta intentando registrar se seleccionar� este mensaje
				con el fin de enviarlo como respuesta a la p�gina web de diretorio de personas */
			SELECT '2' AS [Id], 'Ya se ha registrado esta identificaci�n' AS [Nombre];
		END
	END
	ELSE
	BEGIN
		-- Si no existe la identificaci�n en la tabla personas entrar� en esta condici�n
		IF NOT EXISTS (SELECT Identificacion FROM Personas WHERE Identificacion=@Identificacion)
		BEGIN
			-- Instrucci�n update para modificar los campos correspondientes de acuerdo al Id espec�ficado
			UPDATE Personas SET Nombre=@Nombre, ApellidoPaterno=@ApellidoPaterno, ApellidoMaterno=@ApellidoMaterno,
			Identificacion=@Identificacion WHERE Id=@Id;
			-- Seleccionar una respuesta como mensaje para devolver de un id y un nombre
			SELECT '3' AS [Id], 'Se ha modificado correctamente' AS [Nombre];
		END
		ELSE
		BEGIN
			/* Si existe el id y la identificaci�n de la tabla personas donde el id es igual al id ingresado
		       y la identificaci�n es igual a la identificaci�n ingresada */
			IF EXISTS (SELECT Id, Identificacion FROM Personas WHERE Id=@Id AND Identificacion=@Identificacion)
			BEGIN
				-- Instrucci�n para actualizar los campos espec�ficados donde el id es igual al id ingresado
				UPDATE Personas SET Nombre=@Nombre, ApellidoPaterno=@ApellidoPaterno, ApellidoMaterno=@ApellidoMaterno,
				Identificacion=@Identificacion WHERE Id=@Id;	
				-- Se selecciona un id y un nombre para devolver como respuesta
				SELECT '4' AS [Id], 'Se ha modificado correctamente' AS [Nombre];
			END
			ELSE
			BEGIN
				/* Si no existe el id y la identificacion de la tabla personas donde el id es igual al id ingresado
				   y la identificaci�n es igual a la identificaci�n ingresada, regresa este select de id y nombre
				   como respuesta */
				SELECT '5' AS [Id], 'Ya se ha registrado esta identificaci�n' AS [Nombre];
			END
		END
	END
END

-- Procedimiento almacenado para borrar una persona
ALTER PROCEDURE spBorrarPersona
@Id INT
AS
BEGIN
	-- Instrucci�n para borrar la persona en la tabla Personas siendo el Id igual al parametro @Id
	DELETE FROM Personas WHERE Id=@Id;
	/* Condici�n de si existe la persona con el Id ingresado en la tabla Facturas,
	   si existe entrar� aqu� */
	IF EXISTS (SELECT IdPersona FROM Facturas WHERE IdPersona=@Id)
	BEGIN
		/* Instrucci�n para borrar a la persona en la tabla Facturas siendo el IdPersona de la tabla Facturas
		   igual al Id ingresado */
		DELETE FROM Facturas WHERE IdPersona=@Id
	END
	SELECT 'Se ha borrado correctamente';
END
-- Procedimiento almacenado para la lista de la tabla Personas
CREATE PROCEDURE spListaPersonas
AS
BEGIN
	-- Instrucci�n para seleccionar todos los datos de la tabla Persona
	SELECT * FROM Personas
END

-- Creaci�n de la tabla Facturas con sus campos correspondientes
CREATE TABLE Facturas
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	Fecha DATETIME NOT NULL,
	Monto NUMERIC(12,2) NOT NULL,
	IdPersona INT NOT NULL
)

ALTER PROCEDURE spListaFacturas
AS
BEGIN
	SELECT Facturas.Id, Facturas.Fecha, Facturas.Monto, Personas.Id AS [IdPersona], Personas.Nombre AS [NombrePersona] FROM Facturas INNER JOIN Personas
		ON Facturas.IdPersona=Personas.Id
END

INSERT INTO Facturas (Fecha, Monto, IdPersona) VALUES (SYSDATETIME(), 30, 109)