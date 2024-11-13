CREATE DATABASE DB_GestionEquipos;
GO
USE DB_GestionEquipos;
GO

CREATE TABLE Institucion (
    id_institucion INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    localidad NVARCHAR(100),
    codigopostal NVARCHAR(10),
    provincia NVARCHAR(100),
    cue NVARCHAR(20),
    turno NVARCHAR(50),
    director NVARCHAR(100),
    region NVARCHAR(50),
    nivel NVARCHAR(50),
    barrio NVARCHAR(100),
    calle NVARCHAR(100),
    numerodecalle NVARCHAR(10),
    telefono NVARCHAR(20)
);

CREATE TABLE Proveedor (
    id_proveedor INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    jurisdiccion NVARCHAR(100),
Telefono NVARCHAR(20),
Correo NVARCHAR(100),
Direccion NVARCHAR(255)
);

CREATE TABLE Alumnos (
    id_alumno INT IDENTITY(1,1) PRIMARY KEY,
    Apellidos NVARCHAR(100) NOT NULL,
    Nombres NVARCHAR(100) NOT NULL,
    curso NVARCHAR(50),
    cuil NVARCHAR(20) UNIQUE,
    foto VARBINARY(MAX),
    telefono NVARCHAR(20)
);

CREATE TABLE Usuarios (
    id_usuario INT IDENTITY(1,1) PRIMARY KEY,
    apellidos NVARCHAR(100) NOT NULL,
    nombres NVARCHAR(100) NOT NULL,
    cuil NVARCHAR(20) UNIQUE,
    foto VARBINARY(MAX),
    correo NVARCHAR(50) UNIQUE NOT NULL,
    loginName NVARCHAR(50) UNIQUE NOT NULL,
    password NVARCHAR(255) NOT NULL,
    rol NVARCHAR(50)
);

CREATE TABLE ADMoviles (
    id_admovil INT IDENTITY(1,1) PRIMARY KEY,
    tipo NVARCHAR(50) NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(200),
    matricula NVARCHAR(20) UNIQUE,
    foto VARBINARY(MAX)
);

CREATE TABLE Equipos (
    id_equipo INT IDENTITY(1,1) PRIMARY KEY,
    fecha_ingreso DATE NOT NULL,
    num_serie NVARCHAR(50) UNIQUE NOT NULL,
    matricula NVARCHAR(20),
    estado NVARCHAR(100),
    destino NVARCHAR(50),
    observacion NVARCHAR(200)
);

CREATE TABLE Tipo_equipos (
    id_tipo_equipo INT IDENTITY(1,1) PRIMARY KEY,
    tipo NVARCHAR(100) NOT NULL,
    marca NVARCHAR(50) NOT NULL,
    modelo NVARCHAR(50) NOT NULL,
    detalle_tecnico NVARCHAR(300),
    foto VARBINARY(MAX)
);

CREATE TABLE Actas (
    id_acta INT IDENTITY(1,1) PRIMARY KEY,
    num_acta NVARCHAR(50) UNIQUE NOT NULL,
    fecha_entrega DATE NOT NULL,
    responsable NVARCHAR(100) NOT NULL,
    num_expediente NVARCHAR(50),
    foto VARBINARY(MAX)
);

CREATE TABLE Remitos (
    id_remito INT IDENTITY(1,1) PRIMARY KEY,
    foto VARBINARY(MAX)
);

CREATE TABLE Servicio_tecnico (
    id_servicio_tecnico INT IDENTITY(1,1) PRIMARY KEY,
    falla NVARCHAR(MAX) NOT NULL,
    responsable NVARCHAR(60) NOT NULL,
    foto VARBINARY(MAX),
    fecha_envio DATE NOT NULL
);

/**CREATE TABLE Detalle_Equipo_Acta (
    id_detalle INT IDENTITY(1,1) PRIMARY KEY,
    id_acta INT NOT NULL,
    id_equipo INT NOT NULL
);**/

CREATE TABLE Detalle_Alumno_Equipo (
    id_detalle INT IDENTITY(1,1) PRIMARY KEY,
    id_alumno INT NOT NULL,
    id_equipo INT NOT NULL
);

CREATE TABLE Detalle_ADMovil_Equipo (
    id_detalle INT IDENTITY(1,1) PRIMARY KEY,
    id_admovil INT NOT NULL,
    id_equipo INT NOT NULL
);

-- Alteraciones de tablas para agregar claves foráneas

ALTER TABLE Usuarios 
ADD id_institucion INT;

ALTER TABLE Usuarios 
ADD CONSTRAINT fk_usuario_institucion 
FOREIGN KEY (id_institucion) REFERENCES Institucion(id_institucion) 
ON DELETE CASCADE;


ALTER TABLE Alumnos 
ADD id_institucion INT;

ALTER TABLE Alumnos 
ADD CONSTRAINT fk_institucion_alumnos 
FOREIGN KEY (id_institucion) REFERENCES Institucion(id_institucion) 
ON DELETE CASCADE;

ALTER TABLE Actas 
ADD id_proveedor INT;

ALTER TABLE Actas 
ADD CONSTRAINT fk_proveedor_actas 
FOREIGN KEY (id_proveedor) REFERENCES Proveedor(id_proveedor) 
ON DELETE CASCADE;


ALTER TABLE Equipos 
ADD id_tipo_equipo INT;

ALTER TABLE Equipos 
ADD CONSTRAINT fk_tipo_equipo 
FOREIGN KEY (id_tipo_equipo) REFERENCES Tipo_equipos(id_tipo_equipo) 
ON DELETE CASCADE;

ALTER TABLE Equipos 
ADD id_acta INT;

ALTER TABLE Equipos 
ADD CONSTRAINT fk_acta_equipo 
FOREIGN KEY (id_acta) REFERENCES Actas(id_acta) 
ON DELETE CASCADE;

ALTER TABLE Remitos 
ADD id_acta INT;

ALTER TABLE Remitos 
ADD CONSTRAINT fk_acta_remitos 
FOREIGN KEY (id_acta) REFERENCES Actas(id_acta) 
ON DELETE CASCADE;


ALTER TABLE Servicio_tecnico 
ADD id_equipo INT;

ALTER TABLE Servicio_tecnico 
ADD CONSTRAINT fk_equipo_servicio_tecnico 
FOREIGN KEY (id_equipo) REFERENCES Equipos(id_equipo) 
ON DELETE CASCADE;


ALTER TABLE ADMoviles
ADD id_acta INT;

ALTER TABLE ADMoviles
ADD CONSTRAINT fk_acta_admoviles
FOREIGN KEY (id_acta) REFERENCES Actas(id_acta)
ON DELETE CASCADE;



ALTER TABLE Actas 
ADD id_institucion INT;

ALTER TABLE Actas 
ADD CONSTRAINT fk_institucion_actas 
FOREIGN KEY (id_institucion) REFERENCES Institucion(id_institucion) 
ON DELETE CASCADE;


ALTER TABLE Detalle_Equipo_Acta 
ADD CONSTRAINT fk_detalle_acta 
FOREIGN KEY (id_acta) REFERENCES Actas(id_acta) 
ON DELETE CASCADE;

ALTER TABLE Detalle_Equipo_Acta 
ADD CONSTRAINT fk_detalle_equipo 
FOREIGN KEY (id_equipo) REFERENCES Equipos(id_equipo) 
ON DELETE CASCADE;


ALTER TABLE Detalle_Alumno_Equipo 
ADD CONSTRAINT fk_detalle_alumno 
FOREIGN KEY (id_alumno) REFERENCES Alumnos(id_alumno) 
ON DELETE CASCADE;

ALTER TABLE Detalle_Alumno_Equipo 
ADD CONSTRAINT fk_detalle_alumno_equipo 
FOREIGN KEY (id_equipo) REFERENCES Equipos(id_equipo) 
ON DELETE CASCADE;


ALTER TABLE Detalle_ADMovil_Equipo 
ADD CONSTRAINT fk_detalle_admovil 
FOREIGN KEY (id_admovil) REFERENCES ADMoviles(id_admovil) 
ON DELETE CASCADE;

ALTER TABLE Detalle_ADMovil_Equipo 
ADD CONSTRAINT fk_detalle_admovil_equipo 
FOREIGN KEY (id_equipo) REFERENCES Equipos(id_equipo) 
ON DELETE CASCADE;
GO

INSERT INTO Tipo_equipos (tipo, marca, modelo, detalle_tecnico) VALUES 
('Netbook Conectar Igualdad G1', 'Exo', 'X352', 
 'Procesador Intel Atom N450, 1.66 GHz, RAM DDR2 1 GB, almacenamiento SATA 160 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows XP y Linux, refrigeración activa, conectores VGA, 3 USB 2.0, LAN RJ-45, lector SD, expansión PCIe Mini Card.'),

('Netbook Conectar Igualdad G2', 'Exo', 'X355', 
 'Procesador Intel Atom N455, 1.66 GHz, RAM DDR3 1 GB, almacenamiento SATA 160-250 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows XP y Linux, refrigeración activa, conectores VGA, 3 USB 2.0, LAN RJ-45, lector SD opcional, expansión PCIe Mini Card.'),

('Netbook Conectar Igualdad G3', 'Exo', 'X355', 
 'Procesador Intel Atom N455, 1.66 GHz, RAM DDR3 1 GB, almacenamiento SATA 160-250 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows XP y Linux, refrigeración activa, conectores VGA, 3 USB 2.0, LAN RJ-45, lector SD opcional, expansión PCIe Mini Card.'),

('Netbook Conectar Igualdad Samsung NP100', 'Samsung', 'NP100', 
 'Procesador Intel Atom N2600, 1.6 GHz, RAM DDR3 2 GB, almacenamiento SATA 250-320 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows 7 y Linux, refrigeración activa, conectores VGA y HDMI, 3 USB 2.0, LAN RJ-45, lector SD, expansión PCIe Mini Card.'),

('Netbook Conectar Igualdad Samsung N150', 'Samsung', 'N150', 
 'Procesador Intel Atom N450/N455, 1.66 GHz, RAM DDR3 1 GB, almacenamiento SATA 160 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows 7 Starter y Linux, refrigeración activa, conector VGA, 3 USB 2.0, LAN RJ-45, lector SD.'),

('Netbook Conectar Igualdad G4', 'Exo', 'NT1010E', 
 'Procesador Intel Atom N2600, 1.6 GHz, RAM DDR3 2 GB, almacenamiento SATA 320 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows 7 SP1 y Linux, refrigeración activa, conectores VGA y HDMI, 2 USB 2.0, LAN RJ-45, expansión PCIe Mini Card.'),

('Netbook Conectar Igualdad G5', 'Exo', 'NT1010E', 
 'Procesador Intel Celeron N2806/N2807, 1.6 GHz (2 GHz turbo), RAM DDR3L 2-4 GB, almacenamiento SATA 320 GB, pantalla LED 10.1" Slim HD 1366 x 768, sistema operativo Windows 8.1 y Linux, refrigeración pasiva, conector HDMI, 1 USB 3.0, 1 USB 2.0, LAN RJ-45.'),

('Netbook Conectar Igualdad G6', 'Positivo BGH', 'G6', 
 'Procesador Intel Celeron N2808, 1.58 GHz (2.25 GHz turbo), RAM DDR3L 2-4 GB, almacenamiento SATA 500 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows 8.1 y Linux, refrigeración pasiva, conector HDMI, 2 USB, LAN RJ-45, expansión PCIe Mini Card.'),

('Netbook Conectar Igualdad G7', 'Positivo BGH', 'G7', 
 'Procesador Intel Celeron N3060, 1.6 GHz (2.48 GHz turbo), RAM DDR3L 4 GB, almacenamiento eMMC 128 GB, pantalla LED 11.6" HD 1366 x 768, sistema operativo Windows 10, refrigeración pasiva, conector HDMI, 1 USB 2.0, 1 USB 3.0, lector MicroSD.'),

('Netbook Conectar Igualdad G8', 'Positivo BGH', 'G8', 
 'Procesador Intel Celeron N3060, 1.6 GHz (2.48 GHz turbo), RAM DDR3L 4 GB, almacenamiento M.2 Foresee 128 GB, pantalla LED 11.6" HD 1366 x 768, sistema operativo Windows 10, refrigeración pasiva, conector HDMI, 1 USB 2.0, 1 USB 3.0, lector MicroSD.'),

('Netbook Conectar Igualdad G9', 'Positivo BGH', 'G9', 
 'Procesador Intel Celeron N3060, 1.6 GHz (2.48 GHz turbo), RAM DDR3L 4 GB, almacenamiento eMMC 128 GB, pantalla LED 11.6" HD 1366 x 768, sistema operativo Windows 10, refrigeración pasiva, conector HDMI, 1 USB 2.0, 1 USB 3.0, lector MicroSD.'),

('Netbook Conectar Igualdad G10', 'Positivo BGH', 'G10', 
 'Procesador Intel Celeron N3350, 1.1 GHz (2.4 GHz turbo), RAM DDR3L 4 GB, almacenamiento eMMC 128 GB, pantalla LED 11.6" HD 1366 x 768, sistema operativo Windows 10, refrigeración pasiva, conector HDMI, 1 USB 2.0, 1 USB 3.0, lector MicroSD.'),

('Netbook Conectar Igualdad SF20GM7', 'Bangho', 'SF20GM7', 
 'Procesador Intel Celeron N4020, 1.1 GHz (2.8 GHz turbo), RAM DDR4 4 GB, almacenamiento M.2 240-256 GB, pantalla LED 11.6" HD 1366 x 768, sistema operativo Linux Huayra 5, refrigeración pasiva, conectores HDMI, 2 USB 3.0, USB Tipo C, lector MicroSD.'),

('Notebook Aprender Conectados', 'HP', '240 G6', 
 'Procesador Intel i3-7020U, 2.3 GHz, RAM DDR4 4 GB (expandible a 16 GB), almacenamiento SATA o M.2, pantalla LED 14" HD 1366 x 768, sistema operativo Windows 10 Pro Education, refrigeración activa, conectores VGA y HDMI, 3 puertos USB.'),

('Notebook Plan S@rmiento BA', 'EXO', 'NG180', 
 'Procesador desconocido, RAM no expandible, almacenamiento SATA 2.5" estándar, pantalla LED 11.6" Slim, sistema operativo Windows 10, refrigeración pasiva, conector HDMI, 1 USB 2.0, 1 USB 3.0, lector MicroSD, LAN RJ-45.'),

('Netbook Conectar Igualdad Dell Latitude', 'Dell', '3120 P32T', 
 'Procesador Celeron N5100, 1.1 GHz (2.8 GHz turbo), RAM DDR4 4 GB no expandible, almacenamiento NAND Flash 128 GB NVMe M.2, pantalla LED 11.6" HD 1366 x 768, sistema operativo Windows 11 Pro Education, refrigeración pasiva, conector HDMI, 2 USB 3.2, USB Tipo-C.');


 
 CREATE PROCEDURE sp_ListarTipoEquipos
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM 
        Tipo_equipos;
END;

CREATE PROCEDURE sp_ListarEquipos
AS
BEGIN
    SELECT 
        Id_equipo,
        Num_serie,
        Matricula,
        Estado,
        Observacion,
        fecha_ingreso,
        Destino,
        Id_Tipo_Equipo,
        Id_Acta
    FROM 
        Equipos
    ORDER BY 
        Fecha_Ingreso DESC; 
END;

CREATE PROCEDURE sp_AgregarEquipo
    @Num_serie NVARCHAR(50),
    @Matricula NVARCHAR(50),
    @Estado NVARCHAR(50),
    @Observacion NVARCHAR(255),
    @Fecha_ingreso DATETIME,
    @Destino NVARCHAR(100),
    @Id_Tipo_Equipo INT,
    @Id_Acta INT
AS
BEGIN

    INSERT INTO Equipos (
        Num_serie,
        Matricula,
        Estado,
        Observacion,
        Fecha_ingreso,
        Destino,
        Id_Tipo_Equipo,
        Id_Acta
    )
    VALUES (
        @Num_serie,
        @Matricula,
        @Estado,
        @Observacion,
        @Fecha_ingreso,
        @Destino,
        @Id_Tipo_Equipo,
        @Id_Acta
    );
END;

CREATE PROCEDURE sp_ListarInstituciones
AS
BEGIN
    SELECT 
        id_institucion,
        nombre,
        localidad,
        codigopostal,
        provincia,
        cue,
        turno,
        director,
        region,
        nivel,
        barrio,
        calle,
        numerodecalle,
        telefono
    FROM Institucion;
END;

CREATE PROCEDURE sp_AgregarInstitucion
    @nombre NVARCHAR(100),
    @localidad NVARCHAR(100),
    @codigopostal NVARCHAR(10),
    @provincia NVARCHAR(100),
    @cue NVARCHAR(20),
    @turno NVARCHAR(50),
    @director NVARCHAR(100),
    @region NVARCHAR(50),
    @nivel NVARCHAR(50),
    @barrio NVARCHAR(100),
    @calle NVARCHAR(100),
    @numeroCalle NVARCHAR(10),
    @telefono NVARCHAR(20)
AS
BEGIN
    INSERT INTO Institucion (nombre, localidad, codigopostal, provincia, cue, turno, director, region, nivel, barrio, calle, numerodecalle, telefono)
    VALUES (@nombre, @localidad, @codigopostal, @provincia, @cue, @turno, @director, @region, @nivel, @barrio, @calle, @numeroCalle, @telefono);
END;

CREATE PROCEDURE sp_EditarInstitucion
    @idInstitucion INT,
    @nombre NVARCHAR(100),
    @localidad NVARCHAR(100),
    @codigopostal NVARCHAR(10),
    @provincia NVARCHAR(100),
    @cue NVARCHAR(20),
    @turno NVARCHAR(50),
    @director NVARCHAR(100),
    @region NVARCHAR(50),
    @nivel NVARCHAR(50),
    @barrio NVARCHAR(100),
    @calle NVARCHAR(100),
    @numeroCalle NVARCHAR(10),
    @telefono NVARCHAR(20)
AS
BEGIN
    UPDATE Institucion
    SET nombre = @nombre,
        localidad = @localidad,
        codigopostal = @codigopostal,
        provincia = @provincia,
        cue = @cue,
        turno = @turno,
        director = @director,
        region = @region,
        nivel = @nivel,
        barrio = @barrio,
        calle = @calle,
        numerodecalle = @numeroCalle,
        telefono = @telefono
    WHERE id_institucion = @idInstitucion;
END;

CREATE PROCEDURE sp_EliminarInstitucion
    @idInstitucion INT
AS
BEGIN
    DELETE FROM Institucion
    WHERE id_institucion = @idInstitucion;
END;


CREATE PROCEDURE sp_ListarAlumnos
AS
BEGIN
    SELECT 
        id_alumno,
        apellidos,
        nombres,
        curso,
        cuil,
        foto,
        telefono,
        id_institucion
    FROM 
        Alumnos
END

CREATE PROCEDURE sp_AgregarAlumno
    @apellidos NVARCHAR(50),
    @nombres NVARCHAR(50),
    @curso NVARCHAR(50),
    @cuil NVARCHAR(20),
    @foto VARBINARY(MAX),
    @telefono NVARCHAR(20),
    @id_institucion INT
AS
BEGIN
    INSERT INTO Alumnos (apellidos, nombres, curso, cuil, foto, telefono, id_institucion)
    VALUES (@apellidos, @nombres, @curso, @cuil, @foto, @telefono, @id_institucion)
END

CREATE PROCEDURE sp_EditarAlumno
    @id_alumno INT,
    @apellidos NVARCHAR(50),
    @nombres NVARCHAR(50),
    @curso NVARCHAR(50),
    @cuil NVARCHAR(20),
    @foto VARBINARY(MAX),
    @telefono NVARCHAR(20),
    @id_institucion INT
AS
BEGIN
    UPDATE Alumnos
    SET 
        apellidos = @apellidos,
        nombres = @nombres,
        curso = @curso,
        cuil = @cuil,
        foto = @foto,
        telefono = @telefono,
        id_institucion = @id_institucion
    WHERE 
        id_alumno = @id_alumno
END

CREATE PROCEDURE sp_EliminarAlumno
    @id_alumno INT
AS
BEGIN
    DELETE FROM Alumnos
    WHERE id_alumno = @id_alumno
END

CREATE PROCEDURE sp_ListarProveedores
AS
BEGIN
    SELECT 
        id_proveedor,
        nombre,
        telefono,
        correo,
        direccion
    FROM 
        Proveedor
END

CREATE PROCEDURE sp_AgregarProveedor
    @nombre NVARCHAR(100),
    @telefono NVARCHAR(20),
    @correo NVARCHAR(100),
    @direccion NVARCHAR(255)
AS
BEGIN
    INSERT INTO Proveedor (nombre, telefono, correo, direccion)
    VALUES (@nombre, @telefono, @correo, @direccion)
END

CREATE PROCEDURE sp_EditarProveedor
    @id_proveedor INT,
    @nombre NVARCHAR(100),
    @telefono NVARCHAR(20),
    @correo NVARCHAR(100),
    @direccion NVARCHAR(255)
AS
BEGIN
    UPDATE Proveedor
    SET 
        nombre = @nombre,
        telefono = @telefono,
        correo = @correo,
        direccion = @direccion
    WHERE 
        id_proveedor = @id_proveedor
END

CREATE PROCEDURE sp_EliminarProveedor
    @id_proveedor INT
AS
BEGIN
    DELETE FROM Proveedor
    WHERE id_proveedor = @id_proveedor
END


CREATE PROCEDURE sp_ListarActas
AS
BEGIN
    SELECT 
        id_acta, 
        num_acta, 
        fecha_entrega, 
        responsable, 
        num_expediente, 
        foto, 
        id_proveedor, 
        id_institucion
    FROM Actas
END


CREATE PROCEDURE sp_AgregarActa
    @num_acta NVARCHAR(50),
    @fecha_entrega DATE,
    @responsable NVARCHAR(100),
    @num_expediente NVARCHAR(50),
    @foto VARBINARY(MAX),
    @id_proveedor INT,
    @id_institucion INT
AS
BEGIN
    INSERT INTO Actas (num_acta, fecha_entrega, responsable, num_expediente, foto, id_proveedor, id_institucion)
    VALUES (@num_acta, @fecha_entrega, @responsable, @num_expediente, @foto, @id_proveedor, @id_institucion)
END


CREATE PROCEDURE sp_EditarActa
    @id_acta INT,
    @num_acta NVARCHAR(50),
    @fecha_entrega DATE,
    @responsable NVARCHAR(100),
    @num_expediente NVARCHAR(50),
    @foto VARBINARY(MAX),
    @id_proveedor INT,
    @id_institucion INT
AS
BEGIN
    UPDATE Actas
    SET 
        num_acta = @num_acta,
        fecha_entrega = @fecha_entrega,
        responsable = @responsable,
        num_expediente = @num_expediente,
        foto = @foto,
        id_proveedor = @id_proveedor,
        id_institucion = @id_institucion
    WHERE 
        id_acta = @id_acta
END

CREATE PROCEDURE sp_EliminarActa
    @id_acta INT
AS
BEGIN
    DELETE FROM Actas
    WHERE id_acta = @id_acta
END

CREATE PROCEDURE sp_ListarAdMoviles
AS
BEGIN
    SELECT id_admovil, tipo, nombre, matricula, descripcion, foto, id_acta FROM ADMoviles
END


CREATE PROCEDURE sp_AgregarAdMovil
    @tipo NVARCHAR(50),
    @nombre NVARCHAR(100),
    @matricula NVARCHAR(20),
    @descripcion NVARCHAR(200),
    @foto VARBINARY(MAX),
    @id_acta INT
AS
BEGIN
    INSERT INTO ADMoviles (tipo, nombre, matricula, descripcion, foto, id_acta)
    VALUES (@tipo, @nombre, @matricula, @descripcion, @foto, @id_acta)
END


CREATE PROCEDURE sp_EditarAdMovil
    @id_admovil INT,
    @tipo NVARCHAR(50),
    @nombre NVARCHAR(100),
    @matricula NVARCHAR(20),
    @descripcion NVARCHAR(200),
    @foto VARBINARY(MAX),
    @id_acta INT
AS
BEGIN
    UPDATE ADMoviles
    SET 
        tipo = @tipo,
        nombre = @nombre,
        matricula = @matricula,
        descripcion = @descripcion,
        foto = @foto,
        id_acta = @id_acta
    WHERE 
        id_admovil = @id_admovil
END


CREATE PROCEDURE sp_EliminarAdMovil
    @id_admovil INT
AS
BEGIN
    DELETE FROM ADMoviles
    WHERE id_admovil = @id_admovil
END


CREATE PROCEDURE sp_ListarServiciosTecnicos
AS
BEGIN
    SELECT IdServicioTecnico, Falla, FechaEnvio, Foto, IdEquipo
    FROM ServiciosTecnicos;
END;
GO

CREATE PROCEDURE sp_AgregarServicioTecnico
    @falla NVARCHAR(MAX),
    @fecha_envio DATETIME,
    @foto VARBINARY(MAX),
    @id_equipo INT
AS
BEGIN
    INSERT INTO ServiciosTecnicos (Falla, FechaEnvio, Foto, IdEquipo)
    VALUES (@falla, @fecha_envio, @foto, @id_equipo);
END;
GO

CREATE PROCEDURE sp_EditarServicioTecnico
    @id_servicio_tecnico INT,
    @falla NVARCHAR(MAX),
    @fecha_envio DATETIME,
    @foto VARBINARY(MAX),
    @id_equipo INT
AS
BEGIN
    UPDATE ServiciosTecnicos
    SET Falla = @falla,
        FechaEnvio = @fecha_envio,
        Foto = @foto,
        IdEquipo = @id_equipo
    WHERE IdServicioTecnico = @id_servicio_tecnico;
END;
GO

CREATE PROCEDURE sp_EliminarServicioTecnico
    @id_servicio_tecnico INT
AS
BEGIN
    DELETE FROM ServiciosTecnicos
    WHERE IdServicioTecnico = @id_servicio_tecnico;
END;
GO
