-- 1) Listar “cabeceras” de pedidos (tabla PedidoOnline)
CREATE PROCEDURE spListarRealizaPedido
AS
BEGIN
    SELECT
      po.idPedidoOnline,
      po.idUsuario,
      po.idEstado,
      po.idFormaEnvio  AS idFormaEnvio,
      po.idMetodoPago  AS idMetodoPago,
      po.fecha,
      po.hora,
      po.observaciones,
      po.direccion,

      -- Datos de Usuario
      u.correo        AS correoUsuario,
      u.nombreCli     AS nombreUsuario,
      u.apellidoCli   AS apellidoUsuario,

      -- Estado
      e.nombreEstado  AS nombreEstado,
      e.estado        AS activoEstado,

      -- Forma de envío
      fe.nombreForma  AS nombreFormaEnvio,
      fe.estado       AS activoFormaEnvio,

      -- Método de pago
      mp.nombreMetodo AS nombreMetodoPago,
      mp.estado       AS activoMetodoPago

    FROM dbo.PedidoOnline po
    INNER JOIN dbo.Usuario       u   ON u.idUsuario     = po.idUsuario
    INNER JOIN dbo.Estado        e   ON e.idEstado      = po.idEstado
    INNER JOIN dbo.FormaEnvio    fe  ON fe.idFormaEnvio  = po.idFormaEnvio
    INNER JOIN dbo.MetodoDePago  mp  ON mp.idMetodoPago  = po.idMetodoPago
    ORDER BY po.fecha DESC, po.hora DESC;
END
GO

-- 2) Actualizar el estado de un PedidoOnline
CREATE PROCEDURE spActualizarEstadoRealizaPedido
    @idRealizaPedido INT,     -- corresponde a idPedidoOnline
    @idEstado        INT
AS
BEGIN
    UPDATE dbo.PedidoOnline
    SET idEstado = @idEstado
    WHERE idPedidoOnline = @idRealizaPedido;
END
GO

-- 3) Detalles Combo por PedidoOnline
CREATE PROCEDURE spListarPedidoComboPorPedido
    @idPedidoOnline INT
AS
BEGIN
    SELECT
      pc.idPedidoCombo,
      pc.idPedidoOnline,
      pc.idCombo,
      pc.cantidad
    FROM dbo.PedidoCombo pc
    WHERE pc.idPedidoOnline = @idPedidoOnline;
END
GO

-- 4) Detalles Producto por PedidoOnline
CREATE PROCEDURE spListarPedidoProductoPorPedido
    @idPedidoOnline INT
AS
BEGIN
    SELECT
      pp.idPedidoProducto,
      pp.idPedidoOnline,
      pp.idProducto,
      pp.cantidad
    FROM dbo.PedidoProducto pp
    WHERE pp.idPedidoOnline = @idPedidoOnline;
END
GO
--LISTAR PEDIDOS DASHBOARD
CREATE PROCEDURE spActualizarEstadoPedido
    @idPedido INT,
    @idEstado INT
AS
BEGIN
    UPDATE Pedido
    SET idEstado = @idEstado
    WHERE idPedido = @idPedido;
END