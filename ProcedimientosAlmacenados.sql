--Procedimientos Almacenados
--CATEGORIA
--LISTAR CATEGORIA
CREATE PROCEDURE spListarCategoria
as 
	select idCategoria, nombreCategoria, estado from Categoria
	where estado = 1
--INSERTAR CATEGORIA
CREATE PROCEDURE spInsertarCategoria
	@nombreCategoria varchar(100),
	@estado int
as
begin
	insert into Categoria (nombreCategoria,estado)
	values (@nombreCategoria,1);
end
--EDITAR CATEGORIA
CREATE PROCEDURE spEditarCategoria
	@idCategoria int,
	@nombreCategoria varchar(100)
as
begin
	update Categoria set
	nombreCategoria = @nombreCategoria
	where idCategoria = @idCategoria
end
--DESHABILITAR CATEGORIA
CREATE PROCEDURE spDeshabilitarCategoria
	@idCategoria int
as
begin
	update Categoria set
	estado = 0
	where idCategoria = @idCategoria
end
--BUSCAR CATEGORIA
create procedure spBuscarCategoria
	@idCategoria int
as
begin
	select idCategoria, nombreCategoria, estado from Categoria
	where idCategoria =@idCategoria
end
----------------------------------------------------------------------------------------------------------------------
--TAMA�O
--LISTAR TAMA�O
CREATE PROCEDURE spListarTama�o
as 
	select idTama�o, nombre, estado from Tama�o
	where estado = 1
--INSERTAR TAMA�O
CREATE PROCEDURE spInsertarTama�o
	@nombre varchar(100),
	@estado int
as
begin
	insert into Tama�o (nombre,estado)
	values (@nombre,1);
end
--EDITAR TAMA�O
CREATE PROCEDURE spEditarTama�o
	@idTama�o int,
	@nombre varchar(100)
as
begin
	update Tama�o set
	nombre = @nombre
	where idTama�o = @idTama�o
end
--DESHABILITAR TAMA�O
CREATE PROCEDURE spDeshabilitarTama�o
	@idTama�o int
as
begin
	update Tama�o set
	estado = 0
	where idTama�o = @idTama�o
end
--BUSCAR TAMA�O
create procedure spBuscarTama�o
	@idTama�o int
as
begin
	select idTama�o, nombre, estado from Tama�o
	where idTama�o = @idTama�o
end
----------------------------------------------------------------------------------------------------------------------
--MARCA
--LISTAR MARCA
CREATE PROCEDURE spListarMarca
as 
	select idMarca, nombreMarca, estado from Marca
	where estado = 1
--INSERTAR MARCA
CREATE PROCEDURE spInsertarMarca
	@nombreMarca varchar(100),
	@estado int
as
begin
	insert into Marca (nombreMarca,estado)
	values (@nombreMarca,1);
end
--EDITAR MARCA
CREATE PROCEDURE spEditarMarca
	@idMarca int,
	@nombreMarca varchar(100)
as
begin
	update Marca set
	nombreMarca = @nombreMarca
	where idMarca = @idMarca
end
--DESHABILITAR MARCA
CREATE PROCEDURE spDeshabilitarMarca
	@idMarca int
as
begin
	update Marca set
	estado = 0
	where idMarca = @idMarca
end
--BUSCAR MARCA
CREATE PROCEDURE spBuscarMarca
	@idMarca int
as
begin
	select idMarca, nombreMarca, estado from Marca
	where idMarca = @idMarca
end
----------------------------------------------------------------------------------------------------------
--METODO DE PAGO
--LISTAR METODO DE PAGO
CREATE PROCEDURE spListarMetodoDePago
as 
	select idMetodoPago, nombreMetodo, estado from MetodoDePago
	where estado = 1
--INSERTAR METODO DE PAGO
CREATE PROCEDURE spInsertarMetodoDePago
	@nombreMetodo varchar(100),
	@estado int
as
begin
	insert into MetodoDePago(nombreMetodo,estado)
	values (@nombreMetodo,1);
end
--EDITAR METODO DE PAGO
CREATE PROCEDURE spEditarMetodoDePago
	@idMetodoPago int,
	@nombreMetodo varchar(100)
as
begin
	update MetodoDePago set
	nombreMetodo = @nombreMetodo
	where idMetodoPago = @idMetodoPago
end
--DESHABILITAR METODO DE PAGO
CREATE PROCEDURE spDeshabilitarMetodoDePago
	@idMetodoPago int
as
begin
	update MetodoDePago set
	estado = 0
	where idMetodoPago = @idMetodoPago
end
--BUSCAR METODO DE PAGO
CREATE PROCEDURE spBuscarMetodoDePago
	@idMetodoPago int
as
begin
	select idMetodoPago, nombreMetodo, estado from MetodoDePago
	where idMetodoPago = @idMetodoPago
end
----------------------------------------------------------------------------------------------------------
--Listar Usuario
create or alter procedure [dbo].[spListarUsuario]
as
	select idUsuario, dni, nombreCli,apellidoCli,correo,contrase�a,ruc,razonSocial, direccion, telefono,idRol, estado from Usuario
	where estado = 1
select * from Usuario
--Insertar Usuario
create or alter procedure [dbo].[spInsertarUsuario]
	@dni varchar (8),
	@nombreCli varchar(100),
	@apellidoCli varchar(100),
	@correo varchar(100),
	@contrase�a varchar(100),
	@ruc varchar (11),
	@razonSocial varchar(200),
	@direccion varchar(200),
	@telefono varchar(11),
	@idRol int
as
begin
	insert into Usuario(dni,nombreCli,apellidoCli,correo, contrase�a, ruc, razonSocial, direccion, telefono,idRol,estado)
	values (@dni,@nombreCli,@apellidoCli,@correo, @contrase�a, @ruc, @razonSocial, @direccion, @telefono, @idRol,1);
end
--Editar Usuario
create or alter procedure [dbo].[spEditaUsuario]
	@idUsuario int,
	@dni varchar(8),
	@nombreCli varchar(100),
	@apellidoCli varchar(100),
	@correo varchar(100),
	@contrase�a varchar(100),
	@ruc varchar(11),
	@razonSocial varchar(200),
	@direccion varchar(200),
	@telefono varchar(11)
as
begin
	update Usuario set
	dni = @idUsuario,
	nombreCli = @nombreCli,
	apellidoCli = @apellidoCli,
	correo = @correo,
	contrase�a = @contrase�a,
	ruc = @ruc,
	razonSocial = @razonSocial,
	direccion = @direccion,
	telefono = @telefono
	where
	idUsuario=@idUsuario
end
--Deshabilitar Usuario
create or alter procedure [dbo].[spDeshabilitarUsuario]
	@idUsuario int
as
begin
	update Usuario set
	estado = 0
	where idUsuario = @idUsuario
end
select * from Usuario
--Buscar Usuario
create or alter procedure [dbo].[spBuscarUsuario]
	@idUsuario int
as
begin
	select idUsuario,dni, nombreCli,apellidoCli, correo, contrase�a, ruc, razonSocial, direccion, telefono, idRol, estado from Usuario
	where idUsuario=@idUsuario
end
--Rol
--ListarRol
create or ALTER   procedure [dbo].[spListarRol]
as
	select idRol, nombreRol,  estado from Rol
	where estado = 1
--InsertarRol
create or ALTER   procedure [dbo].[spInsertarRol]
	@nombreRol varchar (50)
	
as
begin
	insert into Rol(nombreRol, estado)
	values (@nombreRol,1);
end

--Editar
create or ALTER   procedure [dbo].[spEditaUsuario]
	@idUsuario int,
	@dni varchar(8),
	@nombreCli varchar(100),
	@apellidoCli varchar(100),
	@correo varchar(100),
	@contrase�a varchar(100),
	@ruc varchar(11),
	@razonSocial varchar(200),
	@direccion varchar(200),
	@telefono varchar(11)
as
begin
	update Usuario set
	dni = @idUsuario,
	nombreCli = @nombreCli,
	apellidoCli = @apellidoCli,
	correo = @correo,
	contrase�a = @contrase�a,
	ruc = @ruc,
	razonSocial = @razonSocial,
	direccion = @direccion,
	telefono = @telefono
	where
	idUsuario=@idUsuario
end
----deshabilitar
create or ALTER   procedure [dbo].[spDeshabilitarRol]
	@idRol int
as
begin
	update Rol set
	estado = 0
	where idRol = @idRol
end
---buscar
create or ALTER   procedure [dbo].[spBuscarRol]
	@idRol int
as
begin
	select idRol,nombreRol from Rol
	where idRol=@idRol
end
