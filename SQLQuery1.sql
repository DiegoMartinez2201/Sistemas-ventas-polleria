create database DB_Polleria
use DB_Polleria
create table TipoCliente(
	idTipoCliente int primary key identity (1,1) not null,
	nombre varchar(50) not null
)

create table Cliente(
	idCliente int primary key identity (1,1) not null,
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

