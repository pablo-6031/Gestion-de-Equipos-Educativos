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
    jurisdiccion NVARCHAR(100)
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
    foto VARBINARY(MAX),
    fecha_envio DATE NOT NULL
);

CREATE TABLE Detalle_Equipo_Acta (
    id_detalle INT IDENTITY(1,1) PRIMARY KEY,
    id_acta INT NOT NULL,
    id_equipo INT NOT NULL
);

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

