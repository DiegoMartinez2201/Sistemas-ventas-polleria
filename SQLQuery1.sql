create database DB_Polleria
use DB_Polleria
create table TipoCliente(
	idTipoCliente int primary key identity (1,1) not null,
	nombre varchar(50) not null,
	estado int 
)

create table Cliente(
	idCliente int primary key identity (1,1) not null,
	correo varchar(100),
	contraseña varchar(100),
	dni varchar(8),
	primerNombre varchar(20),
	segundoNombre varchar(20),
	apellidoPaterno varchar(20),
	apellidoMaterno varchar (20),
	ruc varchar(11),
	razonSocial varchar(100),
	direccion varchar(200) not null,
	telefono varchar(9),
	idTipoCliente int not null,
	estado int,
	foreign key (idTipoCliente) references TipoCliente(idTipoCliente)
)

create table Producto(
	idProducto int primary key identity (1,1),
	nombre varchar(50),
	descripcion varchar(100),
	precio decimal(2),
	stock int
)
create table ordenVenta(
	idOrdenVenta int primary key identity (1,1),
	fecha datetime,
	idCliente int not null,
	Foreign key (idCliente) references Cliente(idCliente),

)
create table ordenVentaDetalle(
	idOrdenVentaDetalle int primary key identity (1,1),
	idProducto int not null,
	cantidad int,
	Foreign key (idProducto) references  Producto (idProducto)
)
create table comprobantePago(
	idComprobantePago int primary key identity (1,1),
	monto decimal(2),
	idOrdenVenta int,
	Foreign key (idOrdenVenta) references OrdenVenta(idOrdenVenta),
	fecha datetime
)
----Procedimientos almacenados
--Listar Tipo Cliente
create or alter procedure [dbo].[spListarTipoCliente]
as
	select idTipoCliente, nombre, estado from TipoCliente
	where estado = 1
--Insertar Tipo Cliente
create or alter procedure [dbo].[spInsertarTipoCliente]
	@nombre varchar(50),
	@estado int
as
begin
	insert into TipoCliente (nombre,estado)
	values (@nombre,1);
end
--Editar TipoCliente
create or alter procedure [dbo].[spEditaTipoCliente]
	@idTipoCliente int,
	@nombre varchar(50)
as
begin
	update TipoCliente set
	nombre = @nombre
	where
	idTipoCliente=@idTipoCliente
end
--Deshabilitar TipoCliente
create or alter procedure [dbo].[spDeshabilitarTipoCliente]
	@idTipoCliente int
as
begin
	update TipoCliente set
	estado = 0
	where idTipoCliente = @idTipoCliente
end
--Buscar TipoCliente
create or alter procedure [dbo].[spBuscarTipoCliente]
	@idTipoCliente int
as
begin
	select idTipoCliente, nombre, estado from TipoCliente
	where idTipoCliente=@idTipoCliente
end
