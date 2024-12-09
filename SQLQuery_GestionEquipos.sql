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

CREATE TABLE Prestamo (
    id_prestamo INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
apellido NVARCHAR(100) NOT NULL,
    dni NVARCHAR(100),
funcion NVARCHAR(20),
fecha_prestamo DATE NOT NULL,

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
estado NVARCHAR(50),
num_serie NVARCHAR(50) UNIQUE NOT NULL,
    matricula NVARCHAR(20) UNIQUE,
);

CREATE TABLE Equipos (
    id_equipo INT IDENTITY(1,1) PRIMARY KEY,
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
    estado NVARCHAR(50) NOT NULL,
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

CREATE TABLE Detalle_Prestamo_Equipo (
    id_detalle INT IDENTITY(1,1) PRIMARY KEY,
    id_prestamo INT NOT NULL,
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

ALTER TABLE ADMoviles
ADD id_tipo_equipo INT;

ALTER TABLE ADMoviles
ADD CONSTRAINT fk_tipoequipo_admoviles
FOREIGN KEY (id_tipo_equipo) REFERENCES tipo_equipo(id_tipo_equipo)
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

ALTER TABLE Detalle_Prestamo_Equipo 
ADD CONSTRAINT fk_detalle_prestamo_equipo 
FOREIGN KEY (id_equipo) REFERENCES Equipos(id_equipo) 
ON DELETE CASCADE;
GO

ALTER TABLE Detalle_Prestamo_Equipo 
ADD CONSTRAINT fk_detalle_prestamo
FOREIGN KEY (id_prestamo) REFERENCES Prestamo(id_prestamo) 
ON DELETE CASCADE;
GO

INSERT INTO Tipo_equipos (tipo, marca, modelo, detalle_tecnico) VALUES 
('Netbook', 'Exo', 'Conectar Igualdad G1', 
 'Procesador Intel Atom N450, 1.66 GHz, RAM DDR2 1 GB, almacenamiento SATA 160 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows XP y Linux, refrigeración activa, conectores VGA, 3 USB 2.0, LAN RJ-45, lector SD, expansión PCIe Mini Card.'),

('Netbook', 'Exo', 'Conectar Igualdad G2', 
 'Procesador Intel Atom N455, 1.66 GHz, RAM DDR3 1 GB, almacenamiento SATA 160-250 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows XP y Linux, refrigeración activa, conectores VGA, 3 USB 2.0, LAN RJ-45, lector SD opcional, expansión PCIe Mini Card.'),

('Netbook', 'Exo', 'Conectar Igualdad G3', 
 'Procesador Intel Atom N455, 1.66 GHz, RAM DDR3 1 GB, almacenamiento SATA 160-250 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows XP y Linux, refrigeración activa, conectores VGA, 3 USB 2.0, LAN RJ-45, lector SD opcional, expansión PCIe Mini Card.'),

('Netbook', 'Samsung', 'Conectar Igualdad NP100', 
 'Procesador Intel Atom N2600, 1.6 GHz, RAM DDR3 2 GB, almacenamiento SATA 250-320 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows 7 y Linux, refrigeración activa, conectores VGA y HDMI, 3 USB 2.0, LAN RJ-45, lector SD, expansión PCIe Mini Card.'),

('Netbook', 'Samsung', 'Conectar Igualdad N150', 
 'Procesador Intel Atom N450/N455, 1.66 GHz, RAM DDR3 1 GB, almacenamiento SATA 160 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows 7 Starter y Linux, refrigeración activa, conector VGA, 3 USB 2.0, LAN RJ-45, lector SD.'),

('Netbook', 'Exo', 'Conectar Igualdad G4', 
 'Procesador Intel Atom N2600, 1.6 GHz, RAM DDR3 2 GB, almacenamiento SATA 320 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows 7 SP1 y Linux, refrigeración activa, conectores VGA y HDMI, 2 USB 2.0, LAN RJ-45, expansión PCIe Mini Card.'),

('Netbook', 'Exo', 'Conectar Igualdad G5', 
 'Procesador Intel Celeron N2806/N2807, 1.6 GHz (2 GHz turbo), RAM DDR3L 2-4 GB, almacenamiento SATA 320 GB, pantalla LED 10.1" Slim HD 1366 x 768, sistema operativo Windows 8.1 y Linux, refrigeración pasiva, conector HDMI, 1 USB 3.0, 1 USB 2.0, LAN RJ-45.'),

('Netbook', 'Positivo BGH', 'Conectar Igualdad G6', 
 'Procesador Intel Celeron N2808, 1.58 GHz (2.25 GHz turbo), RAM DDR3L 2-4 GB, almacenamiento SATA 500 GB, pantalla LED 10.1" WSVGA 1024 x 600, sistema operativo Windows 8.1 y Linux, refrigeración pasiva, conector HDMI, 2 USB, LAN RJ-45, expansión PCIe Mini Card.'),

('Netbook', 'Positivo BGH', 'Conectar Igualdad G7', 
 'Procesador Intel Celeron N3060, 1.6 GHz (2.48 GHz turbo), RAM DDR3L 4 GB, almacenamiento eMMC 128 GB, pantalla LED 11.6" HD 1366 x 768, sistema operativo Windows 10, refrigeración pasiva, conector HDMI, 1 USB 2.0, 1 USB 3.0, lector MicroSD.'),

('Netbook', 'Positivo BGH', 'Conectar Igualdad G8', 
 'Procesador Intel Celeron N3060, 1.6 GHz (2.48 GHz turbo), RAM DDR3L 4 GB, almacenamiento M.2 Foresee 128 GB, pantalla LED 11.6" HD 1366 x 768, sistema operativo Windows 10, refrigeración pasiva, conector HDMI, 1 USB 2.0, 1 USB 3.0, lector MicroSD.'),

('Netbook', 'Positivo BGH', 'Conectar Igualdad G9', 
 'Procesador Intel Celeron N3060, 1.6 GHz (2.48 GHz turbo), RAM DDR3L 4 GB, almacenamiento eMMC 128 GB, pantalla LED 11.6" HD 1366 x 768, sistema operativo Windows 10, refrigeración pasiva, conector HDMI, 1 USB 2.0, 1 USB 3.0, lector MicroSD.'),

('Netbook', 'Positivo BGH', 'Conectar Igualdad G10', 
 'Procesador Intel Celeron N3350, 1.1 GHz (2.4 GHz turbo), RAM DDR3L 4 GB, almacenamiento eMMC 128 GB, pantalla LED 11.6" HD 1366 x 768, sistema operativo Windows 10, refrigeración pasiva, conector HDMI, 1 USB 2.0, 1 USB 3.0, lector MicroSD.'),

('Netbook', 'Bangho', 'Conectar Igualdad SF20GM7', 
 'Procesador Intel Celeron N4020, 1.1 GHz (2.8 GHz turbo), RAM DDR4 4 GB, almacenamiento M.2 240-256 GB, pantalla LED 11.6" HD 1366 x 768, sistema operativo Linux Huayra 5, refrigeración pasiva, conectores HDMI, 2 USB 3.0, USB Tipo C, lector MicroSD.'),

('Notebook', 'HP', 'Aprender Conectados 240 G6', 
 'Procesador Intel i3-7020U, 2.3 GHz, RAM DDR4 4 GB (expandible a 16 GB), almacenamiento SATA o M.2, pantalla LED 14" HD 1366 x 768, sistema operativo Windows 10 Pro Education, refrigeración activa, conectores VGA y HDMI, 3 puertos USB.'),

('Netbook ', 'Dell', 'Conectar Igualdad Latitude', 
 'Procesador Celeron N5100, 1.1 GHz (2.8 GHz turbo), RAM DDR4 4 GB no expandible, almacenamiento NAND Flash 128 GB NVMe M.2, pantalla LED 11.6" HD 1366 x 768, sistema operativo Windows 11 Pro Education, refrigeración pasiva, conector HDMI, 2 USB 3.2, USB Tipo-C.'),



INSERT INTO Tipo_equipos (tipo, marca, modelo, detalle_tecnico) VALUES 
('GABINETE MOVIL', 'Exo', 'ADM_v5', 
 'Contiene 5 notebooks para alumnos, 1 Notebook para el do- cente, 1 Proyector, 1 Pizarra digital, 1 Punto de acceso y 1 Carro de carga y transporte.'),

INSERT INTO Tipo_equipos (tipo, marca, modelo, detalle_tecnico) VALUES 
('GABINETE MOVIL', 'Exo', 'ADM_v10', 
 'Contiene 10 notebooks para alumnos, 1 Notebook para el do- cente, 1 Proyector, 1 Pizarra digital, 1 Punto de acceso y 1 Carro de carga y transporte.'),

INSERT INTO Tipo_equipos (tipo, marca, modelo, detalle_tecnico) VALUES 
('GABINETE MOVIL', 'Exo', 'ADM_v20', 
 'Contiene 20 notebooks para alumnos, 1 Notebook para el do- cente, 1 Proyector, 1 Pizarra digital, 1 Punto de acceso y 1 Carro de carga y transporte.'),



INSERT INTO Tipo_equipos (tipo, marca, modelo, detalle_tecnico) VALUES 
('Proyector', 'Benq', 'MX825STH', 
 'Brillo: 3.500 ANSI lúmenes, Relación de contraste: 2.0000:1, Resolución: XGA -XGA (1024 x 768) (nativo) / 1920 x 1200, 2 x entrada VGA / video componente - HD D-Sub de 15 clavijas (HD-15).'),


INSERT INTO Tipo_equipos (tipo, marca, modelo, detalle_tecnico) VALUES 
('Pointwriter', 'Benq', 'PW03', 
 'Contiene PointWrite sensor de pizarra interactiva, Cable mini USB, Dos lápices PointWrite, Puntas de lápiz adicionales, Pilas de tipo AAA, Tornillos.'),


INSERT INTO Tipo_equipos (tipo, marca, modelo, detalle_tecnico) VALUES 
('AP DUAL CONFIG 50U', 'EXOnet', 'GWF4', 
 'Soporta dual banda 2.4 GHz y 5.8 GHz simultánea, Soporte seguridad inalámbrica 64/128- bit DMZ, SPI, WPA,WPA2, WPS, WPA3, Alta tasa de transferencia de datos de hasta 600 Mbps.');

 







CREATE PROCEDURE sp_AgregarTipoEquipo
    @tipo NVARCHAR(100),
    @marca NVARCHAR(50),
    @modelo NVARCHAR(50),
    @detalle_tecnico NVARCHAR(300) = NULL,
    @foto VARBINARY(MAX) = NULL
AS
BEGIN

    BEGIN TRY
        -- Verifica que no exista un registro con el mismo tipo y modelo
        IF EXISTS (SELECT 1 FROM Tipo_equipos WHERE tipo = @tipo AND modelo = @modelo)
        BEGIN
            THROW 50001, 'Ya existe un equipo con el mismo tipo y modelo.', 1;
        END

        -- Inserta el nuevo registro
        INSERT INTO Tipo_equipos (tipo, marca, modelo, detalle_tecnico, foto)
        VALUES (@tipo, @marca, @modelo, @detalle_tecnico, @foto);

        PRINT 'El tipo de equipo se agregó correctamente.';
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO



CREATE PROCEDURE sp_EliminarTipoEquipo
    @id_tipo_equipo INT
AS
BEGIN
    DELETE FROM Tipo_equipos WHERE id_tipo_equipo = @id_tipo_equipo;


END;



CREATE PROCEDURE sp_EditarTipoEquipo
    @id_tipo_equipo INT,
    @tipo NVARCHAR(100),
    @marca NVARCHAR(50),
    @modelo NVARCHAR(50),
    @detalle_tecnico NVARCHAR(300) = NULL,
    @foto VARBINARY(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verifica que el registro exista
        IF NOT EXISTS (SELECT 1 FROM Tipo_equipos WHERE id_tipo_equipo = @id_tipo_equipo)
        BEGIN
            THROW 50001, 'El tipo de equipo especificado no existe.', 1;
        END

        -- Verifica que el nuevo tipo y modelo no coincidan con otro registro existente
        IF EXISTS (SELECT 1 FROM Tipo_equipos 
                   WHERE tipo = @tipo AND modelo = @modelo AND id_tipo_equipo <> @id_tipo_equipo)
        BEGIN
            THROW 50002, 'Ya existe un equipo con el mismo tipo y modelo.', 1;
        END

        -- Actualiza el registro
        UPDATE Tipo_equipos
        SET
            tipo = @tipo,
            marca = @marca,
            modelo = @modelo,
            detalle_tecnico = @detalle_tecnico,
            foto = @foto
        WHERE 
            id_tipo_equipo = @id_tipo_equipo;

        PRINT 'El tipo de equipo se actualizó correctamente.';
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO






 CREATE PROCEDURE sp_ListarTipoEquipos
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM 
        Tipo_equipos;
END;



CREATE PROCEDURE sp_FiltrarTipoEquipos
    @texto NVARCHAR(100) = NULL 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        *
    FROM 
        Tipo_equipos
    WHERE 
        (@texto IS NULL OR 
         tipo LIKE '%' + @texto + '%' OR 
         marca LIKE '%' + @texto + '%' OR 
         modelo LIKE '%' + @texto + '%')
    ORDER BY 
        tipo; 
END;
GO




 CREATE PROCEDURE sp_TraerTipoEquipo
@id_tipo_equipo INT
AS
BEGIN
    SELECT *
    FROM 
        tipo_equipos
WHERE 
        id_tipo_equipo = @id_tipo_equipo;
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
        Destino,
        Id_Tipo_Equipo,
        Id_Acta
    FROM 
        Equipos
END;


CREATE PROCEDURE sp_ListarEquiposEnServicioTecnico
AS
BEGIN
    SELECT 
        Id_equipo,
        Num_serie,
        Matricula,
        Estado,
        Observacion,
        Destino,
        Id_Tipo_Equipo,
        Id_Acta
    FROM 
        Equipos
    WHERE 
        Estado = 'En soporte Técnico'; 
END;




CREATE PROCEDURE sp_ListarEquiposporNumSerie
    @var NVARCHAR(50)
AS
BEGIN
    SELECT 
        e.Id_equipo,
        e.Num_serie,
        e.Matricula,
        e.Estado,
        e.Observacion,
        e.Destino,
        t.Tipo AS Tipo_Equipo, 
        e.Id_Acta
    FROM 
        Equipos e
    INNER JOIN 
        Tipo_Equipos t ON e.Id_Tipo_Equipo = t.Id_Tipo_Equipo 
    WHERE 
        (e.Estado IN ('Entregado', 'En Deposito', 'Recibida en Migracion', 'En Prestamo', 'Rechazada x Soporte Tecnico')) 
        AND 
        (e.Num_serie LIKE '%' + @var + '%' OR e.Matricula LIKE '%' + @var + '%') 
END;


CREATE PROCEDURE sp_ListarEquiposporNumSerieCompleto
    @texto NVARCHAR(50)
AS
BEGIN
    SELECT 
        e.id_equipo,
        e.num_serie,
        e.matricula,
        e.estado,
        e.destino,
        e.observacion,
        e.id_tipo_equipo,
        te.tipo AS tipo_equipo,
        te.marca AS marca_equipo,
        te.modelo AS modelo_equipo,
        te.detalle_tecnico AS detalle_equipo
    FROM 
        Equipos e
    LEFT JOIN 
        Tipo_equipos te ON e.id_tipo_equipo = te.id_tipo_equipo
    WHERE 
        (@texto IS NULL OR 
         e.num_serie LIKE '%' + @texto + '%' OR 
         e.matricula LIKE '%' + @texto + '%' )
    ORDER BY 
        e.num_serie; -- Ordena por número de serie para mayor claridad

END;







CREATE PROCEDURE sp_ListarEquiposPorActa
    @id_acta INT
AS
BEGIN

    SELECT 
	e.id_equipo,
        e.num_serie,
        e.matricula,
        e.estado,
        e.destino,
        e.observacion,
        e.id_tipo_equipo,
e.id_acta,
        te.tipo AS tipo_equipo,
te.Modelo AS modelo_equipo
    FROM 
        Equipos e
	LEFT JOIN 
        Tipo_equipos te ON e.id_tipo_equipo = te.id_tipo_equipo
        WHERE 
        e.id_acta = @id_acta;
END;






-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


CREATE PROCEDURE sp_ListarEquiposAdmPorActa
    @id_acta INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Selección de Equipos
    SELECT DISTINCT 
        e.id_equipo AS id_equipo,
        e.num_serie,
        e.matricula,
        e.estado,
        e.observacion,
        e.id_tipo_equipo,
        e.id_acta,
        te.tipo AS tipo_equipo,
        te.modelo AS modelo_equipo

    FROM 
        Equipos e
    INNER JOIN 
        Tipo_equipos te ON e.id_tipo_equipo = te.id_tipo_equipo
    WHERE 
        e.id_acta = @id_acta

    UNION ALL

    -- Selección de Móviles Administrativos
    SELECT DISTINCT 
        adm.id_admovil AS id_equipo,
        adm.num_serie,
        adm.matricula,
        adm.estado,
        adm.observacion,
        adm.id_tipo_equipo,
        adm.id_acta,
        te.tipo AS tipo_equipo,
        te.modelo AS modelo_equipo
    FROM 
        ADMoviles adm
    INNER JOIN 
        Detalle_ADMovil_Equipo dam ON adm.id_admovil = dam.id_admovil
    INNER JOIN 
        Equipos e ON dam.id_equipo = e.id_equipo
	INNER JOIN 
        Tipo_equipos te ON adm.id_tipo_equipo = te.id_tipo_equipo
    WHERE 
        adm.id_acta = @id_acta;
END;
GO





















CREATE PROCEDURE sp_ComprobarEquipo
    @num_serie NVARCHAR(50), -- Número de serie a buscar
    @matricula NVARCHAR(20), -- Matrícula a buscar
    @resultado BIT OUTPUT    -- Resultado de la búsqueda
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si existe un equipo con el num_serie o matricula proporcionados
    IF EXISTS (
        SELECT 1
        FROM Equipos
        WHERE num_serie = @num_serie OR matricula = @matricula
    )
    BEGIN
        SET @resultado = 1; -- True si se encontró
    END
    ELSE
    BEGIN
        SET @resultado = 0; -- False si no se encontró
    END
END;
GO






CREATE PROCEDURE sp_AgregarEquipo
    @Num_serie NVARCHAR(50),
    @Matricula NVARCHAR(50),
    @Estado NVARCHAR(50),
    @Observacion NVARCHAR(255),
    @Destino NVARCHAR(100),
    @Id_Tipo_Equipo INT,
    @Id_Acta INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Insertar el nuevo equipo en la tabla Equipos
    INSERT INTO Equipos (
        Num_serie,
        Matricula,
        Estado,
        Observacion,
        Destino,
        Id_Tipo_Equipo,
        Id_Acta
    )
    VALUES (
        @Num_serie,
        @Matricula,
        @Estado,
        @Observacion,
        @Destino,
        @Id_Tipo_Equipo,
        @Id_Acta
    );

    -- Devolver el ID del equipo recién insertado
    SELECT SCOPE_IDENTITY() AS Id_Equipo;
END;
GO


-----------------------------------------------------------------------------------------------------------------------------------------------------------------------EQUIPO-




CREATE PROCEDURE sp_EditarEquipo
    @Id_Equipo INT, 
    @Num_serie NVARCHAR(50),
    @Matricula NVARCHAR(50),
    @Estado NVARCHAR(50),
    @Observacion NVARCHAR(255),
    @Destino NVARCHAR(100),
    @Id_Tipo_Equipo INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verifica que el equipo existe
        IF NOT EXISTS (SELECT 1 FROM Equipos WHERE Id_Equipo = @Id_Equipo)
        BEGIN
            THROW 50001, 'El equipo especificado no existe.', 1;
        END

        -- Verifica que el número de serie no esté duplicado (excepto si es vacío)
        IF LTRIM(RTRIM(@Num_serie)) <> '' AND EXISTS (SELECT 1 FROM Equipos WHERE Num_serie = @Num_serie AND Id_Equipo <> @Id_Equipo)
        BEGIN
            THROW 50002, 'El número de serie ya está en uso por otro equipo.', 1;
        END

       
        -- Verifica que el tipo de equipo existe
        IF NOT EXISTS (SELECT 1 FROM Tipo_equipos WHERE Id_Tipo_Equipo = @Id_Tipo_Equipo)
        BEGIN
            THROW 50004, 'El tipo de equipo especificado no existe.', 1;
        END

        -- Actualiza los datos del equipo
        UPDATE Equipos
        SET
            Num_serie = @Num_serie,
            Matricula = @Matricula,
            Estado = @Estado,
            Observacion = @Observacion,
            Destino = @Destino,
            Id_Tipo_Equipo = @Id_Tipo_Equipo
        WHERE
            Id_Equipo = @Id_Equipo;

        PRINT 'El equipo se actualizó correctamente.';
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

















CREATE PROCEDURE sp_ActualizarEstadoEquipo
    @Id_Equipo INT,       
    @Estado NVARCHAR(50)  
AS
BEGIN

    UPDATE Equipos
    SET
        Estado = @Estado
    WHERE
        Id_Equipo = @Id_Equipo; 

END;



CREATE PROCEDURE sp_EliminarEquipo
    @id_equipo INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Equipos WHERE id_equipo = @id_equipo)
    BEGIN
        DELETE FROM Equipos WHERE id_equipo = @id_equipo;
        PRINT 'Equipo eliminado correctamente';
    END
    ELSE
    BEGIN
        PRINT 'No se encontró el equipo con el ID especificado';
    END
END;



CREATE PROCEDURE sp_FiltrarInstituciones
    @texto NVARCHAR(100) = NULL

AS
BEGIN
    SET NOCOUNT ON;

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
    FROM Institucion
    WHERE 
        (@texto IS NULL OR nombre LIKE '%' + @texto + '%') OR
        (@texto IS NULL OR cue LIKE '%' + @texto + '%')
    ORDER BY nombre;
END;
GO


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


CREATE PROCEDURE sp_FiltrarAlumnos
    @texto NVARCHAR(100) 
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
    WHERE 
        (@texto IS NULL OR nombres LIKE '%' + @texto + '%') OR
        (@texto IS NULL OR cuil LIKE '%' + @texto + '%')
    ORDER BY 
        apellidos, nombres; 
END;
GO





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
    SET NOCOUNT ON;

    INSERT INTO Alumnos (apellidos, nombres, curso, cuil, foto, telefono, id_institucion)
    VALUES (@apellidos, @nombres, @curso, @cuil, @foto, @telefono, @id_institucion);

     -- Devolver el ID del alumn recién insertado
    SELECT SCOPE_IDENTITY() AS Id_Alumno;
END;
GO


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
        *
    FROM 
        Proveedor
END



CREATE PROCEDURE sp_FiltrarProveedores
    @texto NVARCHAR(100) = NULL 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        id_proveedor,
        nombre,
        jurisdiccion,
        telefono,
        correo,
        direccion
    FROM 
        Proveedor
    WHERE 
        (@texto IS NULL OR 
         nombre LIKE '%' + @texto + '%' OR 
         jurisdiccion LIKE '%' + @texto + '%')
    ORDER BY 
        nombre; 
END;
GO





CREATE PROCEDURE sp_AgregarProveedor
    @nombre NVARCHAR(100),
    @jurisdiccion NVARCHAR(100) = NULL,
    @Telefono NVARCHAR(20) = NULL,
    @Correo NVARCHAR(100) = NULL,
    @Direccion NVARCHAR(255) = NULL
AS
BEGIN

        -- Verifica que el nombre no se repita
        IF EXISTS (SELECT 1 FROM Proveedor WHERE nombre = @nombre)
        BEGIN
            THROW 50001, 'El nombre del proveedor ya existe.', 1;
        END

        -- Verifica que el teléfono no se repita, si no es NULL
        IF @Telefono IS NOT NULL AND EXISTS (SELECT 1 FROM Proveedor WHERE Telefono = @Telefono)
        BEGIN
            THROW 50002, 'El teléfono del proveedor ya está en uso.', 1;
        END

        -- Verifica que el correo no se repita, si no es NULL
        IF @Correo IS NOT NULL AND EXISTS (SELECT 1 FROM Proveedor WHERE Correo = @Correo)
        BEGIN
            THROW 50003, 'El correo del proveedor ya está en uso.', 1;
        END

        -- Inserta el nuevo proveedor
        INSERT INTO Proveedor (
            nombre,
            jurisdiccion,
            Telefono,
            Correo,
            Direccion
        )
        VALUES (
            @nombre,
            @jurisdiccion,
            @Telefono,
            @Correo,
            @Direccion
        );

        PRINT 'Proveedor agregado correctamente.';

END;
GO









CREATE PROCEDURE sp_EditarProveedor
    @id_proveedor INT,
    @nombre NVARCHAR(100),
    @jurisdiccion NVARCHAR(100) = NULL,
    @Telefono NVARCHAR(20) = NULL,
    @Correo NVARCHAR(100) = NULL,
    @Direccion NVARCHAR(255) = NULL
AS
BEGIN
    -- Verifica que el proveedor exista
        IF NOT EXISTS (SELECT 1 FROM Proveedor WHERE id_proveedor = @id_proveedor)
        BEGIN
            THROW 50001, 'El proveedor especificado no existe.', 1;
        END

        -- Verifica que el nombre no se repita en otro proveedor
        IF EXISTS (SELECT 1 FROM Proveedor WHERE nombre = @nombre AND id_proveedor <> @id_proveedor)
        BEGIN
            THROW 50002, 'El nombre del proveedor ya está en uso por otro proveedor.', 1;
        END

        -- Verifica que el teléfono no se repita en otro proveedor
        IF @Telefono IS NOT NULL AND EXISTS (SELECT 1 FROM Proveedor WHERE Telefono = @Telefono AND id_proveedor <> @id_proveedor)
        BEGIN
            THROW 50003, 'El teléfono ya está en uso por otro proveedor.', 1;
        END

        -- Verifica que el correo no se repita en otro proveedor
        IF @Correo IS NOT NULL AND EXISTS (SELECT 1 FROM Proveedor WHERE Correo = @Correo AND id_proveedor <> @id_proveedor)
        BEGIN
            THROW 50004, 'El correo ya está en uso por otro proveedor.', 1;
        END

        -- Actualiza los datos del proveedor
        UPDATE Proveedor
        SET 
            nombre = @nombre,
            jurisdiccion = @jurisdiccion,
            Telefono = @Telefono,
            Correo = @Correo,
            Direccion = @Direccion
        WHERE id_proveedor = @id_proveedor;

        PRINT 'Proveedor actualizado correctamente.';
END;
GO

CREATE PROCEDURE sp_EliminarProveedor
    @id_proveedor INT
AS
BEGIN
    DELETE FROM Proveedor
    WHERE id_proveedor = @id_proveedor
END




CREATE PROCEDURE sp_FiltrarActasPorFecha
    @fecha NVARCHAR(10) 
AS
BEGIN
   SELECT 
            id_acta, 
            num_acta, 
            fecha_entrega, 
            responsable, 
            estado, 
            foto, 
            id_proveedor, 
            id_institucion
        FROM Actas
        WHERE FORMAT(fecha_entrega, 'dd-MM-yyyy') LIKE '%' + @fecha + '%'; 

END;
GO










CREATE PROCEDURE sp_ListarActas
AS
BEGIN
    SELECT 
        id_acta, 
        num_acta, 
        fecha_entrega, 
        responsable, 
        estado, 
        foto, 
        id_proveedor, 
        id_institucion
    FROM Actas
END


CREATE PROCEDURE sp_AgregarActa
    @num_acta NVARCHAR(50),
    @fecha_entrega DATE,
    @responsable NVARCHAR(100),
    @estado NVARCHAR(50),
    @foto VARBINARY(MAX),
    @id_proveedor INT,
    @id_institucion INT
AS
BEGIN
    INSERT INTO Actas (num_acta, fecha_entrega, responsable, estado, foto, id_proveedor, id_institucion)
    VALUES (@num_acta, @fecha_entrega, @responsable, @estado, @foto, @id_proveedor, @id_institucion)

 -- Devolver el ID generado
    SELECT SCOPE_IDENTITY()
END



CREATE PROCEDURE sp_ListarActasPorFechas
    @FechaDesde DATE,
    @FechaHasta DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        id_acta, 
        num_acta, 
        fecha_entrega, 
        responsable, 
        estado, 
        foto, 
        id_proveedor, 
        id_institucion
    FROM Actas
    WHERE fecha_entrega BETWEEN @FechaDesde AND @FechaHasta
    ORDER BY fecha_entrega; -- Orden opcional por fecha
END;
GO





CREATE PROCEDURE sp_EditarActa
    @id_acta INT,
    @num_acta NVARCHAR(50),
    @fecha_entrega DATE,
    @responsable NVARCHAR(100),
    @estado NVARCHAR(50),
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
        estado = @estado,
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
    SELECT * FROM ADMoviles
END


CREATE PROCEDURE sp_AgregarAdMovil
    @matricula NVARCHAR(20),
@num_serie NVARCHAR(20),
@estado NVARCHAR(20),
    @observacion NVARCHAR(200),
    @id_acta INT,
@id_tipo_equipo INT
AS
BEGIN
    INSERT INTO ADMoviles (matricula, num_serie, estado, observacion, id_acta, id_tipo_equipo)
    VALUES (@matricula, @num_serie, @estado, @observacion, @id_acta, @id_tipo_equipo)
END


CREATE PROCEDURE sp_EditarAdMovil
    @id_admovil INT,
    @matricula NVARCHAR(20),
@num_serie NVARCHAR(20),
@estado NVARCHAR(20),
    @observacion NVARCHAR(200),
    @id_acta INT,
@id_tipo_equipo INT

AS
BEGIN
    UPDATE ADMoviles
    SET 
        matricula = @matricula,
num_serie = @num_serie,
estado = @estado,
        observacion = @observacion,
        id_acta = @id_acta,
id_tipo_equipo = @id_tipo_equipo
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



CREATE PROCEDURE sp_ListarServiciosTecnicosConEquipos
AS
BEGIN
    SELECT 
        st.Id_Servicio_Tecnico,
        st.Falla,
        st.Fecha_Envio,
        st.Foto,
st.Responsable,
        e.Num_serie,
        e.Matricula,
        te.Tipo
    FROM 
        Servicio_Tecnico st
    INNER JOIN 
        Equipos e ON st.Id_Equipo = e.Id_Equipo
    INNER JOIN 
        Tipo_Equipos te ON e.Id_Tipo_Equipo = te.Id_Tipo_Equipo;
END;
GO



CREATE PROCEDURE sp_FiltrarServiciosTecnicos
    @texto NVARCHAR(100) = NULL 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        st.Id_Servicio_Tecnico,
        st.Falla,
        st.Fecha_Envio,
        st.Foto,
        e.Num_serie,
        e.Matricula,
        te.Tipo
    FROM 
        Servicio_Tecnico st
    INNER JOIN 
        Equipos e ON st.Id_Equipo = e.Id_Equipo
    INNER JOIN 
        Tipo_Equipos te ON e.Id_Tipo_Equipo = te.Id_Tipo_Equipo
    WHERE 
        (@texto IS NULL OR 
         e.Num_serie LIKE '%' + @texto + '%' OR 
         FORMAT(st.Fecha_Envio, 'dd-MM-yyyy') LIKE '%' + @texto + '%')
    ORDER BY 
        st.Fecha_Envio; 
END;
GO





CREATE PROCEDURE sp_ObtenerDetalleEquipo
    @IdEquipo INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        e.Id_Equipo,
        e.Num_serie,
        e.Matricula,
        e.Estado,
        e.Observacion,
        e.Destino,
        te.tipo AS TipoEquipo,
	te.modelo AS ModeloEquipo,
        a.Fecha_Entrega,
        DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, 1, a.Fecha_Entrega)) AS DiasGarantiaRestantes
    FROM 
        Equipos e
    INNER JOIN 
        Actas a ON e.Id_Acta = a.Id_Acta
    INNER JOIN 
        Tipo_Equipos te ON e.Id_Tipo_Equipo = te.Id_Tipo_Equipo
    WHERE 
        e.Id_Equipo = @IdEquipo;
END;


CREATE PROCEDURE sp_AgregarServicioTecnico
    @falla NVARCHAR(MAX),
@responsable NVARCHAR(100),
    @fecha_envio DATETIME,
    @foto VARBINARY(MAX),
    @id_equipo INT
AS
BEGIN
    INSERT INTO Servicio_tecnico (Falla, Fecha_Envio, Foto, Id_Equipo, Responsable)
    VALUES (@falla, @fecha_envio, @foto, @id_equipo,@responsable);
END;
GO


CREATE PROCEDURE sp_EditarServicioTecnico
    @id_servicio_tecnico INT,
    @falla NVARCHAR(MAX),
    @foto VARBINARY(MAX)

AS
BEGIN
    UPDATE Servicio_Tecnico
    SET falla = @falla,
        foto = @foto

    WHERE id_servicio_tecnico = @id_servicio_tecnico;
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

ALTER TABLE Equipos ALTER COLUMN id_acta INT NULL;

ALTER TABLE ADMoviles ALTER COLUMN id_acta INT NULL;


CREATE PROCEDURE sp_AgregarPrestamo
    @Nombre NVARCHAR(100),
    @Apellido NVARCHAR(100),
    @Dni NVARCHAR(100) = NULL,
    @Funcion NVARCHAR(30) = NULL,
    @FechaPrestamo DATE
AS
BEGIN
    
        -- Inserta el nuevo préstamo
        INSERT INTO Prestamo (nombre, apellido, dni, funcion, fecha_prestamo)
        VALUES (@Nombre, @Apellido, @Dni, @Funcion, @FechaPrestamo);
-- Devolver el ID generado
    SELECT SCOPE_IDENTITY()
 END;




CREATE PROCEDURE sp_AgregarEquipoPrestamo
    @id_equipo INT,
    @id_prestamo INT
AS
BEGIN
  -- Inserta el equipo asociado al préstamo en la tabla intermedia
        INSERT INTO Detalle_Prestamo_Equipo (id_prestamo, id_equipo)
        VALUES (@id_prestamo,@id_equipo);
END;


CREATE PROCEDURE sp_ListarPrestamos
AS
BEGIN
    SELECT 
            *
        FROM 
            Prestamo;
END;


CREATE PROCEDURE sp_ReporteSinide
    @IdPrestamo INT
AS
BEGIN
    -- Evitar que se muestren mensajes de conteo de filas afectadas
    SET NOCOUNT ON;

    -- Consulta principal filtrada por IdPrestamo
    SELECT 
        p.nombre AS NombrePrestamo,
        p.apellido AS ApellidoPrestamo,
        DAY(p.fecha_prestamo) AS DiaPrestamo,
        MONTH(p.fecha_prestamo) AS MesPrestamo,
        YEAR(p.fecha_prestamo) AS AnioPrestamo,
        p.dni AS DNI,
        p.funcion AS Funcion,
        e.num_serie AS NumeroSerie,
        t.tipo AS TipoEquipo,
        t.marca AS MarcaEquipo,
        t.modelo AS ModeloEquipo,
        i.nombre AS NombreInstitucion
    FROM 
        dbo.Prestamo AS p
    JOIN 
        dbo.Detalle_Prestamo_Equipo AS dpe ON p.id_prestamo = dpe.id_prestamo
    JOIN 
        dbo.Equipos AS e ON dpe.id_equipo = e.id_equipo
    JOIN 
        dbo.Tipo_equipos AS t ON e.id_tipo_equipo = t.id_tipo_equipo
    JOIN 
        dbo.Actas AS a ON e.id_acta = a.id_acta
    JOIN 
        dbo.Institucion AS i ON a.id_institucion = i.id_institucion
    WHERE 
        p.id_prestamo = @IdPrestamo;
END;

CREATE PROCEDURE sp_ListarPrestamos
AS
BEGIN

    SET NOCOUNT ON;

    SELECT 
        p.id_prestamo AS IdPrestamo,
        p.nombre AS NombrePrestamo,
        p.apellido AS ApellidoPrestamo,
        p.fecha_prestamo AS FechaPrestamo,
        p.dni AS DNI,
        p.funcion AS Funcion,
        e.num_serie AS NumeroSerie,
        t.tipo AS TipoEquipo
    FROM 
        dbo.Prestamo AS p
    JOIN 
        dbo.Detalle_Prestamo_Equipo AS dpe ON p.id_prestamo = dpe.id_prestamo
    JOIN 
        dbo.Equipos AS e ON dpe.id_equipo = e.id_equipo
    JOIN 
        dbo.Tipo_equipos AS t ON e.id_tipo_equipo = t.id_tipo_equipo
END;




CREATE PROCEDURE sp_FiltrarPrestamos
    @texto NVARCHAR(100) 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        p.id_prestamo AS IdPrestamo,
        p.nombre AS NombrePrestamo,
        p.apellido AS ApellidoPrestamo,
        p.fecha_prestamo AS FechaPrestamo,
        p.dni AS DNI,
        p.funcion AS Funcion,
        e.num_serie AS NumeroSerie,
        t.tipo AS TipoEquipo
    FROM 
        Prestamo AS p
    JOIN 
        Detalle_Prestamo_Equipo AS dpe ON p.id_prestamo = dpe.id_prestamo
    JOIN 
        Equipos AS e ON dpe.id_equipo = e.id_equipo
    JOIN 
        Tipo_equipos AS t ON e.id_tipo_equipo = t.id_tipo_equipo
    WHERE 
        p.apellido LIKE '%' + @texto + '%' OR 
        p.dni LIKE '%' + @texto + '%' OR 
        FORMAT(p.fecha_prestamo, 'dd-MM-yyyy') LIKE '%' + @texto + '%'

    ORDER BY 
        p.apellido, p.nombre; -- Ordena por apellido y nombre para mayor claridad
END;
GO






CREATE PROCEDURE sp_AgregarUsuario
    @apellidos NVARCHAR(100),
    @nombres NVARCHAR(100),
    @cuil NVARCHAR(20) = NULL,
    @foto VARBINARY(MAX) = NULL,
    @correo NVARCHAR(50),
    @loginName NVARCHAR(50),
    @password NVARCHAR(255),
    @rol NVARCHAR(50) = NULL,
    @id_institucion INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verifica que el loginName no se repita
        IF EXISTS (SELECT 1 FROM Usuarios WHERE loginName = @loginName)
        BEGIN
            THROW 50001, 'El nombre de usuario ya existe.', 1;
        END
        
        -- Verifica que el correo no se repita
        IF EXISTS (SELECT 1 FROM Usuarios WHERE correo = @correo)
        BEGIN
            THROW 50002, 'El correo ya está en uso.', 1;
        END

        -- Verifica que el cuil no se repita si no es NULL
        IF @cuil IS NOT NULL AND EXISTS (SELECT 1 FROM Usuarios WHERE cuil = @cuil)
        BEGIN
            THROW 50003, 'El CUIL ya está en uso.', 1;
        END

        -- Inserta el nuevo usuario
        INSERT INTO Usuarios (
            apellidos,
            nombres,
            cuil,
            foto,
            correo,
            loginName,
            password,
            rol,
            id_institucion
        )
        VALUES (
            @apellidos,
            @nombres,
            @cuil,
            @foto,
            @correo,
            @loginName,
            @password,
            @rol,
            @id_institucion
        );

        PRINT 'Usuario agregado correctamente.';
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO



CREATE PROCEDURE sp_EditarUsuario
    @id_usuario INT,
    @apellidos NVARCHAR(100),
    @nombres NVARCHAR(100),
    @cuil NVARCHAR(20) = NULL,
    @foto VARBINARY(MAX) = NULL,
    @correo NVARCHAR(50),
    @loginName NVARCHAR(50),
    @password NVARCHAR(255),
    @rol NVARCHAR(50) = NULL,
    @id_institucion INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verifica que el usuario existe
        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE id_usuario = @id_usuario)
        BEGIN
            THROW 50001, 'El usuario especificado no existe.', 1;
        END

        -- Verifica que el loginName no esté en uso por otro usuario
        IF EXISTS (SELECT 1 FROM Usuarios WHERE loginName = @loginName AND id_usuario <> @id_usuario)
        BEGIN
            THROW 50002, 'El nombre de usuario ya está en uso por otro usuario.', 1;
        END

        -- Verifica que el correo no esté en uso por otro usuario
        IF EXISTS (SELECT 1 FROM Usuarios WHERE correo = @correo AND id_usuario <> @id_usuario)
        BEGIN
            THROW 50003, 'El correo ya está en uso por otro usuario.', 1;
        END

        -- Verifica que el cuil no esté en uso por otro usuario (si no es NULL)
        IF @cuil IS NOT NULL AND EXISTS (SELECT 1 FROM Usuarios WHERE cuil = @cuil AND id_usuario <> @id_usuario)
        BEGIN
            THROW 50004, 'El CUIL ya está en uso por otro usuario.', 1;
        END

        -- Actualiza el usuario
        UPDATE Usuarios
        SET 
            apellidos = @apellidos,
            nombres = @nombres,
            cuil = @cuil,
            foto = @foto,
            correo = @correo,
            loginName = @loginName,
            password = @password,
            rol = @rol,
            id_institucion = @id_institucion
        WHERE 
            id_usuario = @id_usuario;

        PRINT 'Usuario actualizado correctamente.';
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO



CREATE PROCEDURE sp_ListarUsuarios
AS
BEGIN
    
    SELECT 
            *
        FROM 
            Usuarios

END;
GO



CREATE PROCEDURE sp_FiltrarUsuarios
    @texto NVARCHAR(100) = NULL 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
*
    FROM 
        Usuarios
    WHERE 
        (@texto IS NULL OR 
         loginName LIKE '%' + @texto + '%' OR 
         apellidos LIKE '%' + @texto + '%' OR 
         cuil LIKE '%' + @texto + '%')
    ORDER BY 
        apellidos, nombres; 
END;
GO




CREATE PROCEDURE sp_EditarUsuario
    @id_usuario INT,
    @apellidos NVARCHAR(100),
    @nombres NVARCHAR(100),
    @cuil NVARCHAR(20) = NULL,
    @foto VARBINARY(MAX) = NULL,
    @correo NVARCHAR(50),
    @loginName NVARCHAR(50),
    @password NVARCHAR(255),
    @rol NVARCHAR(50) = NULL,
    @id_institucion INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Verifica que el usuario exista
        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE id_usuario = @id_usuario)
        BEGIN
            THROW 50001, 'El usuario especificado no existe.', 1;
        END

        -- Verifica que el loginName no esté en uso por otro usuario
        IF EXISTS (SELECT 1 FROM Usuarios WHERE loginName = @loginName AND id_usuario <> @id_usuario)
        BEGIN
            THROW 50002, 'El nombre de usuario ya está en uso por otro usuario.', 1;
        END

        -- Verifica que el correo no esté en uso por otro usuario
        IF EXISTS (SELECT 1 FROM Usuarios WHERE correo = @correo AND id_usuario <> @id_usuario)
        BEGIN
            THROW 50003, 'El correo ya está en uso por otro usuario.', 1;
        END

        -- Verifica que el cuil no esté en uso por otro usuario (si no es NULL)
        IF @cuil IS NOT NULL AND EXISTS (SELECT 1 FROM dbo.Usuarios WHERE cuil = @cuil AND id_usuario <> @id_usuario)
        BEGIN
            THROW 50004, 'El CUIL ya está en uso por otro usuario.', 1;
        END

        -- Actualiza el usuario
        UPDATE Usuarios
        SET 
            apellidos = @apellidos,
            nombres = @nombres,
            cuil = @cuil,
            foto = @foto,
            correo = @correo,
            loginName = @loginName,
            password = @password,
            rol = @rol,
            id_institucion = @id_institucion
        WHERE 
            id_usuario = @id_usuario;

        PRINT 'Usuario actualizado correctamente.';
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO




CREATE PROCEDURE sp_EliminarUsuario
    @id_usuario INT
AS
BEGIN
    DELETE FROM Usuarios
        WHERE id_usuario = @id_usuario;

END;
GO





CREATE PROCEDURE sp_ListarEquiposConTipo
AS
BEGIN
    SELECT 
            e.id_equipo,
            e.num_serie,
            e.matricula,
            e.estado,
            e.destino,
            e.observacion,
            e.id_tipo_equipo,
            te.tipo AS tipo_equipo,
            te.marca AS marca_equipo,
            te.modelo AS modelo_equipo,
            te.detalle_tecnico AS detalle_equipo
        FROM 
            dbo.Equipos e
        LEFT JOIN dbo.Tipo_equipos te ON e.id_tipo_equipo = te.id_tipo_equipo
END;
GO


CREATE PROCEDURE sp_ObtenerTiposEquiposUnicos
AS
BEGIN
    SELECT DISTINCT tipo
    FROM Tipo_equipos
    ORDER BY tipo;
END;






CREATE PROCEDURE sp_AgregarDetalleADMovilEquipo
    @id_admovil INT,
    @id_equipo INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Detalle_ADMovil_Equipo (id_admovil, id_equipo)
    VALUES (@id_admovil, @id_equipo);

END;
GO


CREATE PROCEDURE sp_AgregarDetalleAlumnoEquipo
    @id_alumno INT,
    @id_equipo INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Detalle_Alumno_Equipo (id_alumno, id_equipo)
    VALUES (@id_alumno, @id_equipo);

END;
GO



CREATE PROCEDURE sp_ListarEquiposPorADMovil
    @id_admovil INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        e.Id_Equipo,
e.Id_tipo_Equipo,
        e.Num_serie,
        e.Matricula,
        e.Estado,
        e.Observacion,
e.Destino
        te.tipo AS Tipo_Equipo,
        te.modelo AS Modelo,
        d.id_admovil
    FROM 
        Detalle_ADMovil_Equipo d
    INNER JOIN 
        Equipos e ON d.id_equipo = e.Id_Equipo
    INNER JOIN 
        Tipo_Equipos te ON e.Id_Tipo_Equipo = te.Id_Tipo_Equipo
 
    WHERE 
        d.id_admovil = 1;
END;
GO




CREATE PROCEDURE sp_VerificarGarantiaEquipo
    @IdEquipo INT,
    @TieneGarantia BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Variable para almacenar la diferencia de días
    DECLARE @DiasGarantiaRestantes INT;

    -- Calcular los días restantes de garantía
    SELECT 
        @DiasGarantiaRestantes = DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, 1, a.Fecha_Entrega))
    FROM 
        Equipos e
    INNER JOIN 
        Actas a ON e.Id_Acta = a.Id_Acta
    WHERE 
        e.Id_Equipo = @IdEquipo;

    -- Evaluar si tiene garantía
    IF @DiasGarantiaRestantes > 0
        SET @TieneGarantia = 1; -- Tiene garantía
    ELSE
        SET @TieneGarantia = 0; -- No tiene garantía
END;
GO














------------------------------------------------------

CREATE PROCEDURE sp_ListarEquiposActa
    @id_acta INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Crear una tabla temporal para combinar equipos y administrativos
    CREATE TABLE #EquiposCombinados (
        id_equipo INT,
        num_serie NVARCHAR(50),
        matricula NVARCHAR(20),
        especificaciones NVARCHAR(MAX),
        observacion NVARCHAR(200),
        tipo NVARCHAR(100),
        modelo NVARCHAR(50),
        detalle_tecnico NVARCHAR(300)
    );

    -- Insertar los equipos del acta
    INSERT INTO #EquiposCombinados (id_equipo, num_serie, matricula, especificaciones, observacion, tipo, modelo, detalle_tecnico)
    SELECT 
        e.id_equipo,
        e.num_serie,
        e.matricula,
        e.destino AS especificaciones,
        e.observacion,
        te.tipo,
        te.modelo,
        te.detalle_tecnico
    FROM 
        Equipos e
    INNER JOIN 
        Tipo_equipos te ON e.id_tipo_equipo = te.id_tipo_equipo
    WHERE 
        e.id_acta = 48;

    -- Insertar los administrativos del acta
    INSERT INTO #EquiposCombinados (id_equipo, num_serie, matricula, especificaciones, observacion, tipo, modelo, detalle_tecnico)
    SELECT 
        adm.id_admovil AS id_equipo,
        adm.num_serie,
        adm.matricula,
        NULL AS especificaciones,
        NULL AS observacion,
        te.tipo,
        te.modelo,
        te.detalle_tecnico
    FROM 
        ADMoviles adm
    INNER JOIN 
        Tipo_equipos te ON adm.id_tipo_equipo = te.id_tipo_equipo
    WHERE 
        adm.id_acta = 48;

    -- Generar el resultado final
    SELECT 
        ROW_NUMBER() OVER (ORDER BY tipo, modelo) AS Numeracion,    -- Enumeración
        COUNT(*) OVER (PARTITION BY tipo, modelo) AS Cantidad,      -- Cantidad por tipo y modelo
        CONCAT(tipo, ' - ', modelo) AS TipoYModelo,                -- Tipo y modelo concatenados
        DENSE_RANK() OVER (PARTITION BY tipo, modelo ORDER BY id_equipo) AS ContadorFila, -- Contador por tipo
        num_serie AS NumeroDeSerie,                                -- Número de serie
        matricula AS Matricula,                                    -- Matrícula
        detalle_tecnico AS EspecificacionesTecnicas,               -- Especificaciones técnicas
        observacion AS Observacion                                 -- Observación
    FROM 
        #EquiposCombinados
    ORDER BY 
        tipo, modelo, Numeracion;

    -- Limpiar la tabla temporal
    DROP TABLE #EquiposCombinados;
END;
GO

--------------------------------------------------------------


CREATE PROCEDURE sp_ListarEquiposActa
    @id_acta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ROW_NUMBER() OVER (ORDER BY tipo, modelo) AS Numeracion,          -- Enumeración
        COUNT(*) OVER (PARTITION BY tipo, modelo) AS Cantidad,            -- Cantidad por tipo y modelo
        CONCAT(tipo, ' - ', modelo) AS TipoYModelo,                      -- Tipo y modelo concatenados
        DENSE_RANK() OVER (PARTITION BY tipo, modelo ORDER BY num_serie) AS ContadorFila, -- Contador por tipo
        num_serie AS NumeroDeSerie,                                      -- Número de serie
        matricula AS Matricula,                                          -- Matrícula
        detalle_tecnico AS EspecificacionesTecnicas,                     -- Especificaciones técnicas
        observacion AS Observacion                                       -- Observación
    FROM (
        -- Equipos relacionados con el acta
        SELECT 
            e.id_equipo,
            e.num_serie,
            e.matricula,
            te.tipo,
            te.modelo,
            te.detalle_tecnico,
            e.observacion
        FROM 
            Equipos e
        INNER JOIN 
            Tipo_equipos te ON e.id_tipo_equipo = te.id_tipo_equipo
        WHERE 
            e.id_acta = @id_acta

        UNION ALL

        -- Móviles administrativos relacionados con el acta
        SELECT 
            adm.id_admovil AS id_equipo,
            adm.num_serie,
            adm.matricula,
            te.tipo,
            te.modelo,
            te.detalle_tecnico,
            NULL AS observacion
        FROM 
            ADMoviles adm
        INNER JOIN 
            Tipo_equipos te ON adm.id_tipo_equipo = te.id_tipo_equipo
        WHERE 
            adm.id_acta = @id_acta
    ) AS EquiposCombinados
    ORDER BY 
        tipo, modelo, Numeracion;
END;
GO






