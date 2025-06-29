--Procedimientos Almacenados
--Listar Usuario
create or alter procedure [dbo].[spListarUsuario]
as
	select idUsuario, dni, nombreCli,apellidoCli,correo,contraseña,ruc,razonSocial, direccion, telefono,idRol, estado from Usuario
	where estado = 1
select * from Usuario
--Insertar Tipo Cliente
create or alter procedure [dbo].[spInsertarUsuario]
	@dni varchar (8),
	@nombreCli varchar(100),
	@apellidoCli varchar(100),
	@correo varchar(100),
	@contraseña varchar(100),
	@ruc varchar (11),
	@razonSocial varchar(200),
	@direccion varchar(200),
	@telefono varchar(11),
	@idRol int
as
begin
	insert into Usuario(dni,nombreCli,apellidoCli,correo, contraseña, ruc, razonSocial, direccion, telefono,idRol,estado)
	values (@dni,@nombreCli,@apellidoCli,@correo, @contraseña, @ruc, @razonSocial, @direccion, @telefono, @idRol,1);
end
--Editar Usuario
create or alter procedure [dbo].[spEditaUsuario]
	@idUsuario int,
	@dni varchar(8),
	@nombreCli varchar(100),
	@apellidoCli varchar(100),
	@correo varchar(100),
	@contraseña varchar(100),
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
	contraseña = @contraseña,
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
--Buscar Usuario
create or alter procedure [dbo].[spBuscarUsuario]
	@idUsuario int
as
begin
	select idUsuario,dni, nombreCli,apellidoCli, correo, contraseña, ruc, razonSocial, direccion, telefono, idRol, estado from Usuario
	where idUsuario=@idUsuario
end

