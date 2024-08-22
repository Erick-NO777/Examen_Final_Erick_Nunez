
-- Se crea la base de datos 'ControldeMigracion'
CREATE DATABASE ControldeMigracion;
GO

-- Se usa la base de datos 'ControldeMigracion'
USE ControldeMigracion;
GO

-- Creacion de la tabla 'Viajeros' para guardar los datos personales de los viajeros
CREATE TABLE Viajeros
(
    VIAJERO_ID INT IDENTITY(1,1) PRIMARY KEY,
    NOMBRE NVARCHAR(100) NOT NULL,
    APELLIDO NVARCHAR(100) NOT NULL,
    FECHA_NACIMIENTO DATE NOT NULL CHECK (FECHA_NACIMIENTO <= GETDATE()),
    NACIONALIDAD NVARCHAR(50) NOT NULL,
    EMAIL NVARCHAR(100) NOT NULL UNIQUE
);

-- Creacion de la tabla 'Pasaportes' para guardar los datos del pasaporte de cada viajero
CREATE TABLE Pasaportes
(
    PASAPORTE_ID INT IDENTITY(1,1) PRIMARY KEY,
    VIAJERO_ID INT NOT NULL,
    NUMERO_PASAPORTE NVARCHAR(20) NOT NULL UNIQUE,
    FECHA_EMISION DATE NOT NULL,
    FECHA_EXPIRACION DATE NOT NULL,
    FOREIGN KEY (VIAJERO_ID) REFERENCES Viajeros(VIAJERO_ID) ON DELETE CASCADE,
    CONSTRAINT CHK_FECHA_EXPIRACION CHECK (FECHA_EMISION <= FECHA_EXPIRACION)
);

-- Creacion de la tabla 'Entradas' para registrar las entradas de los viajeros al país
CREATE TABLE Entradas
(
    ENTRADA_ID INT IDENTITY(1,1) PRIMARY KEY,
    VIAJERO_ID INT NOT NULL,
    FECHA_ENTRADA DATETIME NOT NULL CHECK (FECHA_ENTRADA <= GETDATE()),
    LUGAR_ENTRADA NVARCHAR(100) NOT NULL,
    FOREIGN KEY (VIAJERO_ID) REFERENCES Viajeros(VIAJERO_ID) ON DELETE CASCADE
);

-- Creacion de la tabla 'Salidas' para registrar las salidas de los viajeros del país
CREATE TABLE Salidas
(
    SALIDA_ID INT IDENTITY(1,1) PRIMARY KEY,
    VIAJERO_ID INT NOT NULL,
    FECHA_SALIDA DATETIME NOT NULL,
    LUGAR_SALIDA NVARCHAR(100) NOT NULL,
    FOREIGN KEY (VIAJERO_ID) REFERENCES Viajeros(VIAJERO_ID) ON DELETE CASCADE
);

-- Creacion de el trigger para validar que la fecha de salida de los viajeros sea despues de la fecha de entrada
CREATE TRIGGER trg_CheckFechaSalida
ON Salidas
INSTEAD OF INSERT
AS
BEGIN
-- Cuerpo del trigger que verifica y maneja la inserción
    DECLARE @ViajeroID INT, @FechaSalida DATETIME;
    
    -- Obtener los valores de la inserción
    SELECT @ViajeroID = VIAJERO_ID, @FechaSalida = FECHA_SALIDA 
    FROM inserted;
    
    -- Verificar si la fecha de salida es válida
    IF @FechaSalida < (SELECT MAX(FECHA_ENTRADA) 
                       FROM Entradas 
                       WHERE VIAJERO_ID = @ViajeroID)
    BEGIN
        -- Lanzar un error si la fecha de salida no es válida
        RAISERROR('La fecha de salida no puede ser anterior a la ultima fecha de entrada.', 16, 1);
    END
    ELSE
    BEGIN
        -- Insertar los datos si la fecha de salida es válida
        INSERT INTO Salidas(VIAJERO_ID, FECHA_SALIDA, LUGAR_SALIDA)
        SELECT VIAJERO_ID, FECHA_SALIDA, LUGAR_SALIDA 
        FROM inserted;
    END
END;

-- Creacion de la tabla 'Usuarios' para manejar la autenticación y roles en la aplicación
CREATE TABLE Usuarios
(
    USUARIO_ID INT IDENTITY(1,1) PRIMARY KEY,
    USUARIO VARCHAR(50) NOT NULL,
    CONTRASENA VARCHAR(256) NOT NULL,
);

-- Insertar datos en la tabla 'Viajeros'
INSERT INTO Viajeros (VIAJERO_ID, NOMBRE, APELLIDO, FECHA_NACIMIENTO, NACIONALIDAD, EMAIL)
VALUES 
('Celenia', 'Vega', '1999-05-20', 'Costarricense', 'cele.vega@gnail.com'),
('Jazmin', 'Brenes', '2001-11-15', 'Mexicana', 'jaz.brenes@gnail.com');

-- Insertar datos en la tabla 'Pasaportes'
INSERT INTO Pasaportes (VIAJERO_ID, NUMERO_PASAPORTE, FECHA_EMISION, FECHA_EXPIRACION)
VALUES 
(1, 'A001', '2020-01-01', '2030-01-01'),
(2, 'A002', '2019-06-15', '2029-06-15');

-- Insertar datos en la tabla 'Entradas'
INSERT INTO Entradas (VIAJERO_ID, FECHA_ENTRADA, LUGAR_ENTRADA)
VALUES 
(1, '2023-08-01 10:00:00', 'Aeropuerto Juan Santamaría'),
(2, '2023-08-03 14:30:00', 'Aeropuerto Internacional de la Ciudad de México');

-- Insertar datos en la tabla 'Salidas'
INSERT INTO Salidas (VIAJERO_ID, FECHA_SALIDA, LUGAR_SALIDA)
VALUES 
(1, '2023-08-10 15:45:00', 'Aeropuerto Juan Santamaría'),
(2, '2023-08-20 11:00:00', 'Aeropuerto Internacional de la Ciudad de México');

-- Insertar datos en la tabla 'Usuarios'
INSERT INTO Usuarios (USUARIO, CONTRASENA)
VALUES 
('Erick', '1234'),
('Alex', '4321');
