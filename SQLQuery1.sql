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
ALTER TABLE Producto
ADD estado bit;

create table Producto(
	idProducto int primary key identity (1,1),
	nombre varchar(50),
	descripcion varchar(100),
	precio decimal(2),
	stock int	
)
select * from Producto
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

--Procedimientos de Cliente
--Listar Cliente 
create or alter procedure [dbo].[spListarCliente]
as
	select idCliente, correo, contraseña, dni, primerNombre, segundoNombre, apellidoPaterno, apellidoMaterno, ruc, razonSocial, direccion, telefono, idTipoCliente, estado from Cliente 
---------------------------------------------------------------------
--Mantenedor Producto
--Listar Producto
create or alter procedure [dbo].[spListarProducto]
as
	select idProducto, nombre, descripcion, precio, stock, estado from Producto
	
--Insertar Producto
alter or alter procedure [dbo].[spInsertarProducto]
	@nombre varchar(50),
	@descripcion varchar(100),
	@precio decimal(2,0),
	@stock int,
	@estado bit
as
begin
	insert into Producto(nombre,descripcion,precio,stock,estado)
	values (@nombre,@descripcion,@precio,@stock,@estado);
end
--Editar Producto
create or alter procedure [dbo].[spEditaProducto]
	@idProducto int,
	@nombre varchar(50),
	@descripcion varchar(100),
	@precio decimal(2,0),
	@stock int
as
begin
	update Producto set
	nombre = @nombre,
	descripcion = @descripcion,
	precio = @precio,
	stock = @stock
	where
	idProducto=@idProducto
end
--Deshabilitar Producto
create or alter procedure [dbo].[spDeshabilitarProducto]
	@idProducto int
as
begin
	update Producto set
	estado = '0'
	where idProducto =  @idProducto
end
--Buscar Producto
create or alter procedure [dbo].[spBuscarProducto]
	@idProducto int
as
begin
	select idProducto, nombre, descripcion,precio,stock from Producto
	where idProducto=@idProducto
end

