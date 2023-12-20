-- Crear base de datos
CREATE DATABASE TestSoftwareDeveloper
-- Usar la base de datos creada
USE TestSoftwareDeveloper
-- Creación de la tabla personas con sus campos correspondientes
CREATE TABLE Personas
(
	Id INT NOT NULL IDENTITY,
	Nombre VARCHAR(100) NOT NULL,
	ApellidoPaterno VARCHAR(100) NOT NULL,
	ApellidoMaterno VARCHAR(100),
	Identificacion VARCHAR(100) PRIMARY KEY
)
-- Procedimiento almacenado con validaciones para el registro y modificación de una persona
ALTER PROCEDURE spRegistrarPersona
@Id INT,
@Nombre VARCHAR(100),
@ApellidoPaterno VARCHAR(100),
@ApellidoMaterno VARCHAR(100),
@Identificacion VARCHAR(100)
AS
BEGIN
	-- Condición de si no existe el Id ingresado entrará aquí
	IF NOT EXISTS(SELECT Id FROM Personas WHERE Id=@Id)
	BEGIN
		-- Condición de si existe la identificación ingresada entrará aquí
		IF NOT EXISTS (SELECT Identificacion FROM Personas WHERE Identificacion=@Identificacion)
		BEGIN
			/* Instrucción para guardar los datos de la persona de acuerdo a los datos ingresados
			  en la página web de directorio de personas */
			INSERT INTO Personas (Nombre, ApellidoPaterno, ApellidoMaterno, Identificacion)
			VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Identificacion);
			/* Se selecciona este mensaje con el fin de enviarlo como respuesta a la página web de directorio de personas
			   al registrar la persona satisfactoriamente */
			SELECT '1' AS [Id], 'Se ha registrado correctamente' AS [Nombre];	
		END
		ELSE
		BEGIN
			/* Si ya existe la identificación que se esta intentando registrar se seleccionará este mensaje
				con el fin de enviarlo como respuesta a la página web de diretorio de personas */
			SELECT '2' AS [Id], 'Ya se ha registrado esta identificación' AS [Nombre];
		END
	END
	ELSE
	BEGIN
		-- Si no existe la identificación en la tabla personas entrará en esta condición
		IF NOT EXISTS (SELECT Identificacion FROM Personas WHERE Identificacion=@Identificacion)
		BEGIN
			-- Instrucción update para modificar los campos correspondientes de acuerdo al Id específicado
			UPDATE Personas SET Nombre=@Nombre, ApellidoPaterno=@ApellidoPaterno, ApellidoMaterno=@ApellidoMaterno,
			Identificacion=@Identificacion WHERE Id=@Id;
			-- Seleccionar una respuesta como mensaje para devolver de un id y un nombre
			SELECT '3' AS [Id], 'Se ha modificado correctamente' AS [Nombre];
		END
		ELSE
		BEGIN
			/* Si existe el id y la identificación de la tabla personas donde el id es igual al id ingresado
		       y la identificación es igual a la identificación ingresada */
			IF EXISTS (SELECT Id, Identificacion FROM Personas WHERE Id=@Id AND Identificacion=@Identificacion)
			BEGIN
				-- Instrucción para actualizar los campos específicados donde el id es igual al id ingresado
				UPDATE Personas SET Nombre=@Nombre, ApellidoPaterno=@ApellidoPaterno, ApellidoMaterno=@ApellidoMaterno,
				Identificacion=@Identificacion WHERE Id=@Id;	
				-- Se selecciona un id y un nombre para devolver como respuesta
				SELECT '4' AS [Id], 'Se ha modificado correctamente' AS [Nombre];
			END
			ELSE
			BEGIN
				/* Si no existe el id y la identificacion de la tabla personas donde el id es igual al id ingresado
				   y la identificación es igual a la identificación ingresada, regresa este select de id y nombre
				   como respuesta */
				SELECT '5' AS [Id], 'Ya se ha registrado esta identificación' AS [Nombre];
			END
		END
	END
END

-- Procedimiento almacenado para borrar una persona
ALTER PROCEDURE spBorrarPersona
@Id INT
AS
BEGIN
	-- Instrucción para borrar la persona en la tabla Personas siendo el Id igual al parametro @Id
	DELETE FROM Personas WHERE Id=@Id;
	/* Condición de si existe la persona con el Id ingresado en la tabla Facturas,
	   si existe entrará aquí */
	IF EXISTS (SELECT IdPersona FROM Facturas WHERE IdPersona=@Id)
	BEGIN
		/* Instrucción para borrar a la persona en la tabla Facturas siendo el IdPersona de la tabla Facturas
		   igual al Id ingresado */
		DELETE FROM Facturas WHERE IdPersona=@Id
	END
	SELECT 'Se ha borrado correctamente';
END
-- Procedimiento almacenado para la lista de la tabla Personas
CREATE PROCEDURE spListaPersonas
AS
BEGIN
	-- Instrucción para seleccionar todos los datos de la tabla Persona
	SELECT * FROM Personas
END

-- Creación de la tabla Facturas con sus campos correspondientes
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