CREATE DATABASE PintureriaDB;
GO

USE PintureriaDB;
GO

CREATE TABLE Categorias (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);

CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Category_ID INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    FOREIGN KEY (Category_ID) REFERENCES Categorias(Id)
);

CREATE TABLE Ventas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(10,2) NOT NULL
);

CREATE TABLE FormasPago (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL
);

-- Insertar los valores iniciales de FormasPago
INSERT INTO FormasPago (Nombre) VALUES
('Efectivo'),
('Débito'),
('Crédito'),
('Mercado Pago'),
('Cuenta corriente'),
('Transferencia');

CREATE TABLE DataVentas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Venta_Id INT NOT NULL,
    Categoria NVARCHAR(100) NOT NULL,
    Producto NVARCHAR(100) NOT NULL,
    Cantidad INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    FormaPago NVARCHAR(50) NOT NULL,
    FOREIGN KEY (Venta_Id) REFERENCES Ventas(Id)
);
