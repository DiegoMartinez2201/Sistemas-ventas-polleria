USE [DB_PolleriaAdv]
GO
/****** Object:  Table [dbo].[ComprobanteDePago]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComprobanteDePago](
	[idComprobantePago] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[fecha] [date] NOT NULL,
	[hora] [time](7) NOT NULL,
	[monto] [decimal](18, 0) NOT NULL,
	[idPedidoOnline] [int] NOT NULL,
 CONSTRAINT [PK_ComprobanteDePago] PRIMARY KEY CLUSTERED 
(
	[idComprobantePago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedidoonline]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidoonline](
	[idPedidoOnline] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[idUsuario] [int] NOT NULL,
	[idEstado] [int] NOT NULL,
	[idFormaEnvio] [int] NOT NULL,
	[idMetodoPago] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[hora] [time](7) NOT NULL,
	[observaciones] [varchar](250) NULL,
	[createdBy] [int] NULL,
	[createdAt] [datetime] NULL,
	[updatedBy] [int] NULL,
	[updatedAt] [datetime] NULL,
	[direccion] [varchar](250) NULL,
 CONSTRAINT [PK_PedidoOnline] PRIMARY KEY CLUSTERED 
(
	[idPedidoOnline] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_VentasPorPeriodo]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 8. VISTAS PARA REPORTES
-- =============================================

-- Vista de ventas por período
CREATE VIEW [dbo].[vw_VentasPorPeriodo] AS
SELECT 
	CAST(p.fecha AS DATE) as fecha,
	COUNT(DISTINCT p.idPedidoOnline) as totalPedidos,
	SUM(cp.monto) as totalVentas,
	AVG(cp.monto) as promedioVenta
FROM Pedidoonline p
JOIN ComprobanteDePago cp ON p.idPedidoOnline = cp.idPedidoOnline
WHERE p.fecha >= DATEADD(DAY, -30, GETDATE())
GROUP BY CAST(p.fecha AS DATE)
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[idCategoria] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[nombreCategoria] [varchar](100) NOT NULL,
	[estado] [int] NOT NULL,
	[createdBy] [int] NULL,
	[createdAt] [datetime] NULL,
	[updatedBy] [int] NULL,
	[updatedAt] [datetime] NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marca]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marca](
	[idMarca] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[nombreMarca] [varchar](100) NOT NULL,
	[estado] [int] NULL,
 CONSTRAINT [PK_Marca] PRIMARY KEY CLUSTERED 
(
	[idMarca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidoProducto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidoProducto](
	[idPedidoProducto] [int] IDENTITY(1,1) NOT NULL,
	[idPedidoOnline] [int] NULL,
	[idProducto] [int] NULL,
	[cantidad] [int] NULL,
	[precioUnitario] [decimal](10, 2) NULL,
	[subtotal] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPedidoProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[idProducto] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[idCategoria] [int] NOT NULL,
	[idMarca] [int] NOT NULL,
	[idTamaño] [int] NOT NULL,
	[nombreProducto] [varchar](100) NOT NULL,
	[precioVenta] [decimal](4, 0) NOT NULL,
	[stock] [int] NOT NULL,
	[estado] [int] NOT NULL,
	[descripcionP] [varchar](200) NULL,
	[imagen] [varchar](200) NULL,
	[createdBy] [int] NULL,
	[createdAt] [datetime] NULL,
	[updatedBy] [int] NULL,
	[updatedAt] [datetime] NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tamaño]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tamaño](
	[idTamaño] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Tamaño] PRIMARY KEY CLUSTERED 
(
	[idTamaño] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_ProductosMasVendidos]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Vista de productos más vendidos
CREATE VIEW [dbo].[vw_ProductosMasVendidos] AS
SELECT 
	pr.idProducto,
	pr.nombreProducto,
	c.nombreCategoria,
	m.nombreMarca,
	t.nombre as nombreTamaño,
	SUM(pp.cantidad) as totalVendido,
	SUM(pp.subtotal) as totalIngresos
FROM PedidoProducto pp
JOIN Producto pr ON pp.idProducto = pr.idProducto
JOIN Categoria c ON pr.idCategoria = c.idCategoria
JOIN Marca m ON pr.idMarca = m.idMarca
JOIN Tamaño t ON pr.idTamaño = t.idTamaño
JOIN Pedidoonline p ON pp.idPedidoOnline = p.idPedidoOnline
WHERE p.fecha >= DATEADD(DAY, -30, GETDATE())
GROUP BY pr.idProducto, pr.nombreProducto, c.nombreCategoria, m.nombreMarca, t.nombre
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[idUsuario] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[dni] [varchar](8) NULL,
	[nombreCli] [varchar](100) NULL,
	[apellidoCli] [varchar](100) NULL,
	[correo] [varchar](100) NOT NULL,
	[contraseña] [varchar](100) NOT NULL,
	[ruc] [varchar](11) NULL,
	[razonSocial] [varchar](200) NULL,
	[direccion] [varchar](200) NULL,
	[telefono] [varchar](11) NULL,
	[idRol] [int] NOT NULL,
	[estado] [int] NOT NULL,
	[createdBy] [int] NULL,
	[createdAt] [datetime] NULL,
	[updatedBy] [int] NULL,
	[updatedAt] [datetime] NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_ClientesMasActivos]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Vista de clientes más activos
CREATE VIEW [dbo].[vw_ClientesMasActivos] AS
SELECT 
	u.idUsuario,
	u.nombreCli + ' ' + u.apellidoCli as nombreCompleto,
	u.correo,
	u.telefono,
	COUNT(p.idPedidoOnline) as totalPedidos,
	SUM(cp.monto) as totalGastado,
	MAX(p.fecha) as ultimaCompra
FROM Usuario u
JOIN Pedidoonline p ON u.idUsuario = p.idUsuario
JOIN ComprobanteDePago cp ON p.idPedidoOnline = cp.idPedidoOnline
WHERE u.idRol = 3 -- Solo clientes
GROUP BY u.idUsuario, u.nombreCli, u.apellidoCli, u.correo, u.telefono
GO
/****** Object:  View [dbo].[vw_EstadoInventario]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Vista de estado de inventario
CREATE VIEW [dbo].[vw_EstadoInventario] AS
SELECT 
	pr.idProducto,
	pr.nombreProducto,
	c.nombreCategoria,
	m.nombreMarca,
	t.nombre as nombreTamaño,
	pr.stock,
	pr.precioVenta,
	CASE 
		WHEN pr.stock = 0 THEN 'Agotado'
		WHEN pr.stock <= 10 THEN 'Stock Bajo'
		ELSE 'Stock Normal'
	END as estadoStock
FROM Producto pr
JOIN Categoria c ON pr.idCategoria = c.idCategoria
JOIN Marca m ON pr.idMarca = m.idMarca
JOIN Tamaño t ON pr.idTamaño = t.idTamaño
WHERE pr.estado = 1
GO
/****** Object:  Table [dbo].[Combo]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Combo](
	[idCombo] [int] IDENTITY(1,1) NOT NULL,
	[nombreCombo] [varchar](100) NOT NULL,
	[descripcion] [varchar](255) NULL,
	[precioVenta] [decimal](10, 2) NULL,
	[estado] [int] NULL,
	[createdBy] [int] NULL,
	[createdAt] [datetime] NULL,
	[updatedBy] [int] NULL,
	[updatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idCombo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComboProducto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComboProducto](
	[idComboProducto] [int] IDENTITY(1,1) NOT NULL,
	[idCombo] [int] NULL,
	[idProducto] [int] NULL,
	[cantidad] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idComboProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado](
	[idEstado] [int] NOT NULL,
	[nombreEstado] [varchar](50) NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[idEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Formaenvio]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Formaenvio](
	[idFormaEnvio] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[nombreForma] [varchar](50) NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Formaenvio] PRIMARY KEY CLUSTERED 
(
	[idFormaEnvio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogActividad]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogActividad](
	[idLog] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NULL,
	[accion] [varchar](100) NOT NULL,
	[tabla] [varchar](100) NULL,
	[idRegistro] [int] NULL,
	[datosAnteriores] [text] NULL,
	[datosNuevos] [text] NULL,
	[ipAddress] [varchar](45) NULL,
	[fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_LogActividad] PRIMARY KEY CLUSTERED 
(
	[idLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MetodoDePago]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MetodoDePago](
	[idMetodoPago] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[nombreMetodo] [varchar](100) NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_MetodoDePago] PRIMARY KEY CLUSTERED 
(
	[idMetodoPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidoCombo]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidoCombo](
	[idPedidoCombo] [int] IDENTITY(1,1) NOT NULL,
	[idPedidoOnline] [int] NULL,
	[idCombo] [int] NULL,
	[cantidad] [int] NULL,
	[precioUnitario] [decimal](10, 2) NULL,
	[subtotal] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPedidoCombo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permiso]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permiso](
	[idPermiso] [int] IDENTITY(1,1) NOT NULL,
	[nombrePermiso] [varchar](100) NOT NULL,
	[descripcion] [varchar](255) NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Permiso] PRIMARY KEY CLUSTERED 
(
	[idPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[idRol] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[nombreRol] [varchar](50) NOT NULL,
	[estado] [int] NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolPermiso]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolPermiso](
	[idRol] [int] NOT NULL,
	[idPermiso] [int] NOT NULL,
 CONSTRAINT [PK_RolPermiso] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC,
	[idPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SesionUsuario]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SesionUsuario](
	[idSesion] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[token] [varchar](255) NOT NULL,
	[fechaInicio] [datetime] NOT NULL,
	[fechaExpiracion] [datetime] NOT NULL,
	[ipAddress] [varchar](45) NULL,
	[userAgent] [varchar](500) NULL,
	[activa] [bit] NOT NULL,
 CONSTRAINT [PK_SesionUsuario] PRIMARY KEY CLUSTERED 
(
	[idSesion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([idCategoria], [nombreCategoria], [estado], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (1, N'Bebida', 1, NULL, CAST(N'2025-07-08T12:54:48.833' AS DateTime), NULL, NULL)
INSERT [dbo].[Categoria] ([idCategoria], [nombreCategoria], [estado], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (2, N'Pollo', 1, NULL, CAST(N'2025-07-08T21:40:57.820' AS DateTime), NULL, NULL)
INSERT [dbo].[Categoria] ([idCategoria], [nombreCategoria], [estado], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (3, N'Extras', 1, NULL, CAST(N'2025-07-08T21:59:15.300' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Combo] ON 

INSERT [dbo].[Combo] ([idCombo], [nombreCombo], [descripcion], [precioVenta], [estado], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (1, N'Combo Familiar', N'1 pollo entero + 2 gaseosas personales', CAST(75.00 AS Decimal(10, 2)), 1, NULL, CAST(N'2025-07-08T21:39:57.837' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Combo] OFF
GO
SET IDENTITY_INSERT [dbo].[ComboProducto] ON 

INSERT [dbo].[ComboProducto] ([idComboProducto], [idCombo], [idProducto], [cantidad]) VALUES (1, 1, 1, 2)
INSERT [dbo].[ComboProducto] ([idComboProducto], [idCombo], [idProducto], [cantidad]) VALUES (2, 1, 2, 1)
SET IDENTITY_INSERT [dbo].[ComboProducto] OFF
GO
INSERT [dbo].[Estado] ([idEstado], [nombreEstado], [estado]) VALUES (1, N'Preparando', 1)
INSERT [dbo].[Estado] ([idEstado], [nombreEstado], [estado]) VALUES (2, N'En camino', 1)
INSERT [dbo].[Estado] ([idEstado], [nombreEstado], [estado]) VALUES (3, N'Entregado', 1)
GO
SET IDENTITY_INSERT [dbo].[Formaenvio] ON 

INSERT [dbo].[Formaenvio] ([idFormaEnvio], [nombreForma], [estado]) VALUES (1, N'Delivery', 1)
INSERT [dbo].[Formaenvio] ([idFormaEnvio], [nombreForma], [estado]) VALUES (2, N'Recojo', 1)
SET IDENTITY_INSERT [dbo].[Formaenvio] OFF
GO
SET IDENTITY_INSERT [dbo].[LogActividad] ON 

INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (1, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T12:44:15.420' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (2, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T12:45:43.440' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (3, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T12:51:03.383' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (4, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T12:53:13.063' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (5, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T20:35:22.817' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (6, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T20:43:19.050' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (7, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T20:44:16.060' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (8, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T20:44:56.763' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (9, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T20:49:13.453' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (10, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T21:18:26.290' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (11, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T21:18:50.863' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (12, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T21:20:13.500' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (13, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T21:22:47.870' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (14, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T21:25:29.583' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (15, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T21:39:03.493' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (16, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T21:50:38.300' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (17, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T21:55:51.933' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (18, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T22:14:58.133' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (19, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T22:15:34.170' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (20, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T22:18:26.873' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (21, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T22:31:39.497' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (22, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T22:31:48.343' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (23, 1, N'LOGIN', N'Usuario', 1, NULL, NULL, N'::1', CAST(N'2025-07-08T22:37:39.483' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (24, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T22:56:54.890' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (25, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T23:23:47.427' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (26, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T23:45:37.000' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (27, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T23:49:37.590' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (28, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-08T23:55:47.770' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (29, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T09:10:35.850' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (30, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T09:15:43.887' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (31, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T09:19:05.523' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (32, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T09:22:14.470' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (33, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T09:50:13.963' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (34, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T09:52:44.860' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (35, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T09:58:00.607' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (36, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T10:01:23.940' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (37, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T10:03:37.340' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (38, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T10:09:56.630' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (39, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T10:15:56.637' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (40, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T10:23:23.870' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (41, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T10:28:34.440' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (42, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T10:44:51.710' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (43, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T10:47:50.907' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (44, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T10:49:30.640' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (45, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T10:54:00.427' AS DateTime))
INSERT [dbo].[LogActividad] ([idLog], [idUsuario], [accion], [tabla], [idRegistro], [datosAnteriores], [datosNuevos], [ipAddress], [fecha]) VALUES (46, 2, N'LOGIN', N'Usuario', 2, NULL, NULL, N'::1', CAST(N'2025-07-09T10:57:35.867' AS DateTime))
SET IDENTITY_INSERT [dbo].[LogActividad] OFF
GO
SET IDENTITY_INSERT [dbo].[Marca] ON 

INSERT [dbo].[Marca] ([idMarca], [nombreMarca], [estado]) VALUES (1, N'Inka Kola', 1)
INSERT [dbo].[Marca] ([idMarca], [nombreMarca], [estado]) VALUES (2, N'Coca Cola', 1)
INSERT [dbo].[Marca] ([idMarca], [nombreMarca], [estado]) VALUES (3, N'Kial Chicken', 1)
INSERT [dbo].[Marca] ([idMarca], [nombreMarca], [estado]) VALUES (4, N'Pepsi', 1)
SET IDENTITY_INSERT [dbo].[Marca] OFF
GO
SET IDENTITY_INSERT [dbo].[MetodoDePago] ON 

INSERT [dbo].[MetodoDePago] ([idMetodoPago], [nombreMetodo], [estado]) VALUES (1, N'Efectivo', 1)
INSERT [dbo].[MetodoDePago] ([idMetodoPago], [nombreMetodo], [estado]) VALUES (2, N'Tarjeta', 1)
SET IDENTITY_INSERT [dbo].[MetodoDePago] OFF
GO
SET IDENTITY_INSERT [dbo].[Pedidoonline] ON 

INSERT [dbo].[Pedidoonline] ([idPedidoOnline], [idUsuario], [idEstado], [idFormaEnvio], [idMetodoPago], [fecha], [hora], [observaciones], [createdBy], [createdAt], [updatedBy], [updatedAt], [direccion]) VALUES (5, 2, 1, 1, 1, CAST(N'2025-07-09' AS Date), CAST(N'10:57:57.3100000' AS Time), N'Av virú 1950 | Google Maps: https://maps.app.goo.gl/n2sX5uHZerXkPhv36', NULL, CAST(N'2025-07-09T10:57:57.310' AS DateTime), NULL, NULL, N'Av virú 1950')
SET IDENTITY_INSERT [dbo].[Pedidoonline] OFF
GO
SET IDENTITY_INSERT [dbo].[Permiso] ON 

INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (1, N'USUARIO_CREAR', N'Crear usuarios', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (2, N'USUARIO_EDITAR', N'Editar usuarios', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (3, N'USUARIO_ELIMINAR', N'Eliminar usuarios', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (4, N'USUARIO_VER', N'Ver usuarios', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (5, N'PRODUCTO_CREAR', N'Crear productos', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (6, N'PRODUCTO_EDITAR', N'Editar productos', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (7, N'PRODUCTO_ELIMINAR', N'Eliminar productos', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (8, N'PRODUCTO_VER', N'Ver productos', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (9, N'PEDIDO_CREAR', N'Crear pedidos', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (10, N'PEDIDO_EDITAR', N'Editar pedidos', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (11, N'PEDIDO_ELIMINAR', N'Eliminar pedidos', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (12, N'PEDIDO_VER', N'Ver pedidos', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (13, N'REPORTE_VER', N'Ver reportes', 1)
INSERT [dbo].[Permiso] ([idPermiso], [nombrePermiso], [descripcion], [estado]) VALUES (14, N'ADMIN_SISTEMA', N'Administración del sistema', 1)
SET IDENTITY_INSERT [dbo].[Permiso] OFF
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([idProducto], [idCategoria], [idMarca], [idTamaño], [nombreProducto], [precioVenta], [stock], [estado], [descripcionP], [imagen], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (1, 1, 1, 1, N'Gaseosa Inka Kola 500 ml', CAST(3 AS Decimal(4, 0)), 3, 1, N'Gaseosa personal de 500 ml inka kola', N'/img/inka500ml.png', NULL, CAST(N'2025-07-08T12:57:56.950' AS DateTime), NULL, NULL)
INSERT [dbo].[Producto] ([idProducto], [idCategoria], [idMarca], [idTamaño], [nombreProducto], [precioVenta], [stock], [estado], [descripcionP], [imagen], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (2, 2, 3, 2, N'Pollo entero ', CAST(60 AS Decimal(4, 0)), 5, 1, N'Pollo entero a la braza realizado por kial chicken', N'/img/Polloentero.jpg', NULL, CAST(N'2025-07-08T22:01:05.247' AS DateTime), NULL, NULL)
INSERT [dbo].[Producto] ([idProducto], [idCategoria], [idMarca], [idTamaño], [nombreProducto], [precioVenta], [stock], [estado], [descripcionP], [imagen], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (3, 2, 3, 5, N'1/4 de pollo', CAST(20 AS Decimal(4, 0)), 5, 1, N'1/4 de pollo a la braza ', N'/img/1sobre4 pollo.png', NULL, CAST(N'2025-07-08T22:08:52.700' AS DateTime), NULL, NULL)
INSERT [dbo].[Producto] ([idProducto], [idCategoria], [idMarca], [idTamaño], [nombreProducto], [precioVenta], [stock], [estado], [descripcionP], [imagen], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (4, 2, 3, 6, N'1/2 Pollo', CAST(40 AS Decimal(4, 0)), 9, 1, N'1/2 Pollo a la braza realizado por Kial Chicken', N'/img/medioPollo.png', NULL, CAST(N'2025-07-08T22:14:43.150' AS DateTime), NULL, NULL)
INSERT [dbo].[Producto] ([idProducto], [idCategoria], [idMarca], [idTamaño], [nombreProducto], [precioVenta], [stock], [estado], [descripcionP], [imagen], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (5, 1, 4, 7, N'Pepsi de 450 ml', CAST(3 AS Decimal(4, 0)), 10, 1, N'Pepsi de 450 ml gaseosa personal', N'/img/pepsi 450ml.png', NULL, CAST(N'2025-07-08T22:18:04.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Producto] ([idProducto], [idCategoria], [idMarca], [idTamaño], [nombreProducto], [precioVenta], [stock], [estado], [descripcionP], [imagen], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (6, 3, 3, 8, N'Papas Fritas', CAST(8 AS Decimal(4, 0)), 15, 1, N'Papas fritas hechas por Kial Chicken', N'/img/PapasFritas.webp', NULL, CAST(N'2025-07-08T22:50:24.930' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([idRol], [nombreRol], [estado]) VALUES (1, N'Administrador', 1)
INSERT [dbo].[Rol] ([idRol], [nombreRol], [estado]) VALUES (2, N'Vendedor', 1)
INSERT [dbo].[Rol] ([idRol], [nombreRol], [estado]) VALUES (3, N'Cliente', 1)
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 1)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 2)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 3)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 4)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 5)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 6)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 7)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 8)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 9)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 10)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 11)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 12)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 13)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (1, 14)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (2, 5)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (2, 6)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (2, 8)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (2, 9)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (2, 10)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (2, 12)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (2, 13)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (3, 8)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (3, 9)
INSERT [dbo].[RolPermiso] ([idRol], [idPermiso]) VALUES (3, 12)
GO
SET IDENTITY_INSERT [dbo].[SesionUsuario] ON 

INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (1, 1, N'6be45b4a-79c0-4796-8057-f0e89141a44f', CAST(N'2025-07-08T12:44:15.407' AS DateTime), CAST(N'2025-07-09T12:44:15.380' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (2, 1, N'59e00391-0265-4377-9360-9aa0013b6311', CAST(N'2025-07-08T12:45:43.433' AS DateTime), CAST(N'2025-07-09T12:45:43.373' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (3, 1, N'9540c944-1259-45f8-94d3-6f9c8eeb80ff', CAST(N'2025-07-08T12:51:03.380' AS DateTime), CAST(N'2025-07-09T12:51:03.363' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (4, 1, N'fd311d39-f92c-4707-b138-958b517057d7', CAST(N'2025-07-08T12:53:13.060' AS DateTime), CAST(N'2025-07-09T12:53:13.043' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (5, 1, N'92825484-a959-4c1d-8845-a89dd696f6b3', CAST(N'2025-07-08T20:35:22.810' AS DateTime), CAST(N'2025-07-09T20:35:22.797' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (6, 1, N'b9d3913e-24de-4889-9592-6d8930933017', CAST(N'2025-07-08T20:43:19.043' AS DateTime), CAST(N'2025-07-09T20:43:19.023' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (7, 1, N'3caeb739-2f60-4300-99d0-35d7ce02346b', CAST(N'2025-07-08T20:44:16.057' AS DateTime), CAST(N'2025-07-09T20:44:16.043' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (8, 2, N'b010b960-80a5-4a1b-9458-3d9dc4eda313', CAST(N'2025-07-08T20:44:56.763' AS DateTime), CAST(N'2025-07-09T20:44:56.763' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (9, 1, N'1b49ee32-b006-464b-8939-375c9df4a6dc', CAST(N'2025-07-08T20:49:13.450' AS DateTime), CAST(N'2025-07-09T20:49:13.433' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (10, 1, N'b3bb222c-3fc8-4338-a9d7-0285ec38b3f6', CAST(N'2025-07-08T21:18:26.283' AS DateTime), CAST(N'2025-07-09T21:18:26.263' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (11, 2, N'00fac985-1692-4dee-9cdd-056bd3088bde', CAST(N'2025-07-08T21:18:50.863' AS DateTime), CAST(N'2025-07-09T21:18:50.863' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (12, 2, N'ce8c3762-9aeb-4047-a04d-687a44015d4f', CAST(N'2025-07-08T21:20:13.493' AS DateTime), CAST(N'2025-07-09T21:20:13.470' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (13, 2, N'b69f502e-9966-45cc-81ad-107b1ee6351d', CAST(N'2025-07-08T21:22:47.863' AS DateTime), CAST(N'2025-07-09T21:22:47.843' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (14, 2, N'a36cd092-7c16-4ab9-bb85-f88995ee779a', CAST(N'2025-07-08T21:25:29.580' AS DateTime), CAST(N'2025-07-09T21:25:29.557' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (15, 1, N'97527a13-18c0-438e-8658-a061d82dc190', CAST(N'2025-07-08T21:39:03.487' AS DateTime), CAST(N'2025-07-09T21:39:03.457' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (16, 1, N'db84c410-960b-4b50-ba8e-9d7e68e826d9', CAST(N'2025-07-08T21:50:38.290' AS DateTime), CAST(N'2025-07-09T21:50:38.273' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (17, 1, N'fb2dadd9-626f-4210-89d2-03ce1083cc37', CAST(N'2025-07-08T21:55:51.927' AS DateTime), CAST(N'2025-07-09T21:55:51.903' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (18, 2, N'7bcc7e0e-6b99-4cd3-8609-2fba3096e096', CAST(N'2025-07-08T22:14:58.130' AS DateTime), CAST(N'2025-07-09T22:14:58.127' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (19, 1, N'74c6f93b-2044-4f3f-8578-28e749a788a6', CAST(N'2025-07-08T22:15:34.167' AS DateTime), CAST(N'2025-07-09T22:15:34.167' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (20, 2, N'ee95ed94-56de-4c7b-af40-793a7ebb4a8b', CAST(N'2025-07-08T22:18:26.870' AS DateTime), CAST(N'2025-07-09T22:18:26.863' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (21, 1, N'e0cbc715-5717-42be-8bb5-8fb0eb4a64c7', CAST(N'2025-07-08T22:31:39.490' AS DateTime), CAST(N'2025-07-09T22:31:39.473' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (22, 2, N'6df84579-3ee1-454d-be56-1c5234a08161', CAST(N'2025-07-08T22:31:48.340' AS DateTime), CAST(N'2025-07-09T22:31:48.340' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (23, 1, N'46812644-dbbc-4b22-a61e-67e472572104', CAST(N'2025-07-08T22:37:39.480' AS DateTime), CAST(N'2025-07-09T22:37:39.477' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 1)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (24, 2, N'9a6325bd-c003-4f3c-a3bb-85eff94e17c3', CAST(N'2025-07-08T22:56:54.887' AS DateTime), CAST(N'2025-07-09T22:56:54.863' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (25, 2, N'47570b00-2eab-40fb-bbfe-71dcb468876e', CAST(N'2025-07-08T23:23:47.423' AS DateTime), CAST(N'2025-07-09T23:23:47.403' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (26, 2, N'44555730-a453-44fb-ba20-702a484303d7', CAST(N'2025-07-08T23:45:36.993' AS DateTime), CAST(N'2025-07-09T23:45:36.963' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (27, 2, N'4522807d-8a88-48de-a3f1-179ce0c0b96b', CAST(N'2025-07-08T23:49:37.583' AS DateTime), CAST(N'2025-07-09T23:49:37.530' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (28, 2, N'e0eed51d-3da8-4b6b-9f67-d5a3640f3e59', CAST(N'2025-07-08T23:55:47.767' AS DateTime), CAST(N'2025-07-09T23:55:47.747' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (29, 2, N'4ba72992-d9c9-41dc-974f-331aa63aaf7d', CAST(N'2025-07-09T09:10:35.840' AS DateTime), CAST(N'2025-07-10T09:10:35.823' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (30, 2, N'e6fd5402-5ab6-4c8f-b3d8-f952ad913f6d', CAST(N'2025-07-09T09:15:43.887' AS DateTime), CAST(N'2025-07-10T09:15:43.877' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (31, 2, N'f6fc4bd8-990e-4591-b977-222a1567bad0', CAST(N'2025-07-09T09:19:05.520' AS DateTime), CAST(N'2025-07-10T09:19:05.510' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (32, 2, N'89a9a70b-3a1a-47d8-89e7-0e48b45828f6', CAST(N'2025-07-09T09:22:14.467' AS DateTime), CAST(N'2025-07-10T09:22:14.430' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (33, 2, N'82df55a2-d41c-446d-a5e2-2346a69462d8', CAST(N'2025-07-09T09:50:13.950' AS DateTime), CAST(N'2025-07-10T09:50:13.937' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (34, 2, N'1a14de6f-4ae3-4f58-a1f9-aa8c7b7b9724', CAST(N'2025-07-09T09:52:44.853' AS DateTime), CAST(N'2025-07-10T09:52:44.797' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (35, 2, N'646a3db6-e1d7-4be2-91f8-82f6760392f8', CAST(N'2025-07-09T09:58:00.600' AS DateTime), CAST(N'2025-07-10T09:58:00.583' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (36, 2, N'07ad79d8-2a8e-4936-b782-ddadd43004cd', CAST(N'2025-07-09T10:01:23.937' AS DateTime), CAST(N'2025-07-10T10:01:23.920' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (37, 2, N'9cd30ccf-bc52-4de0-ba04-ac1ec12dfd75', CAST(N'2025-07-09T10:03:37.333' AS DateTime), CAST(N'2025-07-10T10:03:37.283' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (38, 2, N'337bb49a-869d-447b-b0ba-d338adf43b62', CAST(N'2025-07-09T10:09:56.593' AS DateTime), CAST(N'2025-07-10T10:09:56.580' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (39, 2, N'39d41cfc-ca2a-408f-a619-35f76472eb5b', CAST(N'2025-07-09T10:15:56.630' AS DateTime), CAST(N'2025-07-10T10:15:56.613' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (40, 2, N'0dd09627-f314-4424-a90d-0774f982ce02', CAST(N'2025-07-09T10:23:23.863' AS DateTime), CAST(N'2025-07-10T10:23:23.837' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (41, 2, N'a5bf2a84-6b11-4db2-a455-be4b3c122826', CAST(N'2025-07-09T10:28:34.437' AS DateTime), CAST(N'2025-07-10T10:28:34.417' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (42, 2, N'fab4d7ab-8b86-493e-9dc5-9dd889532edf', CAST(N'2025-07-09T10:44:51.707' AS DateTime), CAST(N'2025-07-10T10:44:51.693' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (43, 2, N'b149abdb-ed0e-4c48-938b-42c96dc66ff4', CAST(N'2025-07-09T10:47:50.893' AS DateTime), CAST(N'2025-07-10T10:47:50.880' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (44, 2, N'c8277fc2-b08a-4aff-b72a-a7800653b178', CAST(N'2025-07-09T10:49:30.637' AS DateTime), CAST(N'2025-07-10T10:49:30.620' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (45, 2, N'5f60e826-eb8f-4dcd-a4a4-f38c0c0f89f8', CAST(N'2025-07-09T10:54:00.423' AS DateTime), CAST(N'2025-07-10T10:54:00.413' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 0)
INSERT [dbo].[SesionUsuario] ([idSesion], [idUsuario], [token], [fechaInicio], [fechaExpiracion], [ipAddress], [userAgent], [activa]) VALUES (46, 2, N'0a219747-4878-424a-a98f-3fab371ef8fc', CAST(N'2025-07-09T10:57:35.860' AS DateTime), CAST(N'2025-07-10T10:57:35.850' AS DateTime), N'::1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36', 1)
SET IDENTITY_INSERT [dbo].[SesionUsuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Tamaño] ON 

INSERT [dbo].[Tamaño] ([idTamaño], [nombre], [estado]) VALUES (1, N'500 ml', 1)
INSERT [dbo].[Tamaño] ([idTamaño], [nombre], [estado]) VALUES (2, N'1 entero', 1)
INSERT [dbo].[Tamaño] ([idTamaño], [nombre], [estado]) VALUES (3, N'1/8', 1)
INSERT [dbo].[Tamaño] ([idTamaño], [nombre], [estado]) VALUES (4, N'1L', 1)
INSERT [dbo].[Tamaño] ([idTamaño], [nombre], [estado]) VALUES (5, N'1/4', 1)
INSERT [dbo].[Tamaño] ([idTamaño], [nombre], [estado]) VALUES (6, N'1/2', 1)
INSERT [dbo].[Tamaño] ([idTamaño], [nombre], [estado]) VALUES (7, N'450 ml', 1)
INSERT [dbo].[Tamaño] ([idTamaño], [nombre], [estado]) VALUES (8, N'Individual', 1)
SET IDENTITY_INSERT [dbo].[Tamaño] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([idUsuario], [dni], [nombreCli], [apellidoCli], [correo], [contraseña], [ruc], [razonSocial], [direccion], [telefono], [idRol], [estado], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (1, N'70691629', N'Diego', N'Martinez', N'admin@admin.com', N'123', NULL, NULL, NULL, NULL, 1, 1, NULL, CAST(N'2025-07-08T12:43:03.050' AS DateTime), NULL, NULL)
INSERT [dbo].[Usuario] ([idUsuario], [dni], [nombreCli], [apellidoCli], [correo], [contraseña], [ruc], [razonSocial], [direccion], [telefono], [idRol], [estado], [createdBy], [createdAt], [updatedBy], [updatedAt]) VALUES (2, N'70691629', N'Diego', N'Martinez', N'diego_two2@hotmail.com', N'123', NULL, NULL, N'Av virú 1950', N'933611593', 3, 1, NULL, CAST(N'2025-07-08T20:44:53.500' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
ALTER TABLE [dbo].[Categoria] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Combo] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[LogActividad] ADD  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[Pedidoonline] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Permiso] ADD  DEFAULT ((1)) FOR [estado]
GO
ALTER TABLE [dbo].[Producto] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[SesionUsuario] ADD  DEFAULT (getdate()) FOR [fechaInicio]
GO
ALTER TABLE [dbo].[SesionUsuario] ADD  DEFAULT ((1)) FOR [activa]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[ComboProducto]  WITH CHECK ADD FOREIGN KEY([idCombo])
REFERENCES [dbo].[Combo] ([idCombo])
GO
ALTER TABLE [dbo].[ComboProducto]  WITH CHECK ADD FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
GO
ALTER TABLE [dbo].[ComprobanteDePago]  WITH CHECK ADD  CONSTRAINT [FK_ComprobanteDePago_Pedidoonline] FOREIGN KEY([idPedidoOnline])
REFERENCES [dbo].[Pedidoonline] ([idPedidoOnline])
GO
ALTER TABLE [dbo].[ComprobanteDePago] CHECK CONSTRAINT [FK_ComprobanteDePago_Pedidoonline]
GO
ALTER TABLE [dbo].[LogActividad]  WITH CHECK ADD  CONSTRAINT [FK_LogActividad_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[LogActividad] CHECK CONSTRAINT [FK_LogActividad_Usuario]
GO
ALTER TABLE [dbo].[PedidoCombo]  WITH CHECK ADD FOREIGN KEY([idCombo])
REFERENCES [dbo].[Combo] ([idCombo])
GO
ALTER TABLE [dbo].[PedidoCombo]  WITH CHECK ADD FOREIGN KEY([idPedidoOnline])
REFERENCES [dbo].[Pedidoonline] ([idPedidoOnline])
GO
ALTER TABLE [dbo].[Pedidoonline]  WITH CHECK ADD  CONSTRAINT [FK_PedidoOnline_Cliente] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[Pedidoonline] CHECK CONSTRAINT [FK_PedidoOnline_Cliente]
GO
ALTER TABLE [dbo].[Pedidoonline]  WITH CHECK ADD  CONSTRAINT [FK_PedidoOnline_Estado] FOREIGN KEY([idEstado])
REFERENCES [dbo].[Estado] ([idEstado])
GO
ALTER TABLE [dbo].[Pedidoonline] CHECK CONSTRAINT [FK_PedidoOnline_Estado]
GO
ALTER TABLE [dbo].[Pedidoonline]  WITH CHECK ADD  CONSTRAINT [FK_PedidoOnline_Formaenvio] FOREIGN KEY([idFormaEnvio])
REFERENCES [dbo].[Formaenvio] ([idFormaEnvio])
GO
ALTER TABLE [dbo].[Pedidoonline] CHECK CONSTRAINT [FK_PedidoOnline_Formaenvio]
GO
ALTER TABLE [dbo].[Pedidoonline]  WITH CHECK ADD  CONSTRAINT [FK_PedidoOnline_MetodoDePago] FOREIGN KEY([idMetodoPago])
REFERENCES [dbo].[MetodoDePago] ([idMetodoPago])
GO
ALTER TABLE [dbo].[Pedidoonline] CHECK CONSTRAINT [FK_PedidoOnline_MetodoDePago]
GO
ALTER TABLE [dbo].[PedidoProducto]  WITH CHECK ADD FOREIGN KEY([idPedidoOnline])
REFERENCES [dbo].[Pedidoonline] ([idPedidoOnline])
GO
ALTER TABLE [dbo].[PedidoProducto]  WITH CHECK ADD FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[Categoria] ([idCategoria])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categoria]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Marca] FOREIGN KEY([idMarca])
REFERENCES [dbo].[Marca] ([idMarca])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Marca]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Tamaño] FOREIGN KEY([idTamaño])
REFERENCES [dbo].[Tamaño] ([idTamaño])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Tamaño]
GO
ALTER TABLE [dbo].[RolPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolPermiso_Permiso] FOREIGN KEY([idPermiso])
REFERENCES [dbo].[Permiso] ([idPermiso])
GO
ALTER TABLE [dbo].[RolPermiso] CHECK CONSTRAINT [FK_RolPermiso_Permiso]
GO
ALTER TABLE [dbo].[RolPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolPermiso_Rol] FOREIGN KEY([idRol])
REFERENCES [dbo].[Rol] ([idRol])
GO
ALTER TABLE [dbo].[RolPermiso] CHECK CONSTRAINT [FK_RolPermiso_Rol]
GO
ALTER TABLE [dbo].[SesionUsuario]  WITH CHECK ADD  CONSTRAINT [FK_SesionUsuario_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[SesionUsuario] CHECK CONSTRAINT [FK_SesionUsuario_Usuario]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Rol] FOREIGN KEY([idRol])
REFERENCES [dbo].[Rol] ([idRol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Cliente_Rol]
GO
/****** Object:  StoredProcedure [dbo].[spAsignarPermisoRol]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spAsignarPermisoRol]
    @idRol int,
    @idPermiso int
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM RolPermiso WHERE idRol = @idRol AND idPermiso = @idPermiso)
    BEGIN
        INSERT INTO RolPermiso (idRol, idPermiso)
        VALUES (@idRol, @idPermiso)
    END
END
GO
/****** Object:  StoredProcedure [dbo].[spBackupDatosCriticos]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 13. PROCEDIMIENTO PARA BACKUP DE DATOS CRÍTICOS
-- =============================================

CREATE PROCEDURE [dbo].[spBackupDatosCriticos]
AS
BEGIN
	-- Crear tabla de respaldo con timestamp
	DECLARE @fechaBackup varchar(20) = CONVERT(varchar(20), GETDATE(), 112) + '_' + REPLACE(CONVERT(varchar(8), GETDATE(), 108), ':', '')
	
	-- Backup de usuarios
	EXEC('SELECT * INTO Usuario_Backup_' + @fechaBackup + ' FROM Usuario')
	
	-- Backup de productos
	EXEC('SELECT * INTO Producto_Backup_' + @fechaBackup + ' FROM Producto')
	
	-- Backup de pedidos
	EXEC('SELECT * INTO Pedidoonline_Backup_' + @fechaBackup + ' FROM Pedidoonline')
END
GO
/****** Object:  StoredProcedure [dbo].[spBuscarCategoria]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spBuscarCategoria]
	@idCategoria int
as
begin
	select idCategoria, nombreCategoria, estado from Categoria
	where idCategoria =@idCategoria
end
GO
/****** Object:  StoredProcedure [dbo].[spBuscarCombo]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spBuscarCombo]
	@idCombo int
as
begin
	select idCombo, nombreCombo, descripcion, precioVenta, estado from Combo
	where idCombo=@idCombo
end
GO
/****** Object:  StoredProcedure [dbo].[spBuscarComboProducto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spBuscarComboProducto]
	@idComboProducto int
as
begin
	select idComboProducto, idCombo, idProducto, cantidad from ComboProducto
	where idComboProducto=@idComboProducto
end
GO
/****** Object:  StoredProcedure [dbo].[spBuscarEstado]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     procedure [dbo].[spBuscarEstado]
	@idEstado int
as
begin
	select idEstado,nombreEstado, estado from Estado
	where idEstado=@idEstado
end
GO
/****** Object:  StoredProcedure [dbo].[spBuscarFormaEnvio]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spBuscarFormaEnvio]
	@idFormaEnvio int
as
begin
	select idFormaEnvio, nombreForma from Formaenvio
	where idFormaEnvio = @idFormaEnvio
end
GO
/****** Object:  StoredProcedure [dbo].[spBuscarMarca]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spBuscarMarca]
	@idMarca int
as
begin
	select idMarca, nombreMarca, estado from Marca
	where idMarca = @idMarca
end
GO
/****** Object:  StoredProcedure [dbo].[spBuscarMetodoDePago]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spBuscarMetodoDePago]
	@idMetodoPago int
as
begin
	select idMetodoPago, nombreMetodo, estado from MetodoDePago
	where idMetodoPago = @idMetodoPago
end
GO
/****** Object:  StoredProcedure [dbo].[spBuscarPermiso]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spBuscarPermiso]
    @idPermiso int
AS
BEGIN
    SELECT idPermiso, nombrePermiso, descripcion, estado 
    FROM Permiso 
    WHERE idPermiso = @idPermiso
END
GO
/****** Object:  StoredProcedure [dbo].[spBuscarProducto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create    procedure [dbo].[spBuscarProducto]
	  @idProducto int
AS
BEGIN
    SELECT idProducto, idCategoria, idMarca, idTamaño, nombreProducto, precioVenta, stock, estado, descripcionP, imagen
    FROM Producto
    WHERE idProducto = @idProducto
END
GO
/****** Object:  StoredProcedure [dbo].[spBuscarRol]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spBuscarRol]
	@idRol int
as
begin
	select idRol,nombreRol from Rol
	where idRol=@idRol
end
GO
/****** Object:  StoredProcedure [dbo].[spBuscarTamaño]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spBuscarTamaño]
	@idTamaño int
as
begin
	select idTamaño, nombre, estado from Tamaño
	where idTamaño = @idTamaño
end
GO
/****** Object:  StoredProcedure [dbo].[spBuscarUsuario]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spBuscarUsuario]
	@idUsuario int
as
begin
	select idUsuario,dni, nombreCli,apellidoCli, correo, contraseña, ruc, razonSocial, direccion, telefono, idRol, estado from Usuario
	where idUsuario=@idUsuario
end
GO
/****** Object:  StoredProcedure [dbo].[spCerrarSesion]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para cerrar sesión
CREATE PROCEDURE [dbo].[spCerrarSesion]
	@token varchar(255)
AS
BEGIN
	UPDATE SesionUsuario 
	SET activa = 0 
	WHERE token = @token
END
GO
/****** Object:  StoredProcedure [dbo].[spCerrarSesionesUsuario]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCerrarSesionesUsuario]
    @idUsuario int
AS
BEGIN
    UPDATE SesionUsuario 
    SET activa = 0 
    WHERE idUsuario = @idUsuario AND activa = 1
END
GO
/****** Object:  StoredProcedure [dbo].[spCrearSesion]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para crear sesión de usuario
CREATE PROCEDURE [dbo].[spCrearSesion]
	@idUsuario int,
	@token varchar(255),
	@fechaExpiracion datetime,
	@ipAddress varchar(45) = NULL,
	@userAgent varchar(500) = NULL
AS
BEGIN
	-- Desactivar sesiones anteriores del usuario
	UPDATE SesionUsuario 
	SET activa = 0 
	WHERE idUsuario = @idUsuario AND activa = 1
	
	-- Crear nueva sesión
	INSERT INTO SesionUsuario (idUsuario, token, fechaExpiracion, ipAddress, userAgent)
	VALUES (@idUsuario, @token, @fechaExpiracion, @ipAddress, @userAgent)
END
GO
/****** Object:  StoredProcedure [dbo].[spDeshabilitarCategoria]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDeshabilitarCategoria]
	@idCategoria int
as
begin
	update Categoria set
	estado = 0
	where idCategoria = @idCategoria
end
GO
/****** Object:  StoredProcedure [dbo].[spDeshabilitarCombo]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spDeshabilitarCombo]
	@idCombo int
as
begin
	update Combo set
	estado = 0
	where idCombo = @idCombo
end
GO
/****** Object:  StoredProcedure [dbo].[spDeshabilitarEstado]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create     procedure [dbo].[spDeshabilitarEstado]
	@idEstado int
as
begin
	update Estado set
	estado = 0
	where idEstado = @idEstado
end
GO
/****** Object:  StoredProcedure [dbo].[spDeshabilitarFormaEnvio]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spDeshabilitarFormaEnvio]
	@idFormaEnvio int

as
begin
	update Formaenvio set
	estado = 0
	where idFormaEnvio = @idFormaEnvio
end
GO
/****** Object:  StoredProcedure [dbo].[spDeshabilitarMarca]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDeshabilitarMarca]
	@idMarca int
as
begin
	update Marca set
	estado = 0
	where idMarca = @idMarca
end
GO
/****** Object:  StoredProcedure [dbo].[spDeshabilitarMetodoDePago]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDeshabilitarMetodoDePago]
	@idMetodoPago int
as
begin
	update MetodoDePago set
	estado = 0
	where idMetodoPago = @idMetodoPago
end
GO
/****** Object:  StoredProcedure [dbo].[spDeshabilitarPermiso]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDeshabilitarPermiso]
    @idPermiso int
AS
BEGIN
    UPDATE Permiso 
    SET estado = 0 
    WHERE idPermiso = @idPermiso
END
GO
/****** Object:  StoredProcedure [dbo].[spDeshabilitarProducto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spDeshabilitarProducto]
	@idProducto int
as
begin
	update Producto set
	estado = 0
	where idProducto = @idProducto
end
GO
/****** Object:  StoredProcedure [dbo].[spDeshabilitarRol]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spDeshabilitarRol]
	@idRol int
as
begin
	update Rol set
	estado = 0
	where idRol = @idRol
end
GO
/****** Object:  StoredProcedure [dbo].[spDeshabilitarTamaño]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDeshabilitarTamaño]
	@idTamaño int
as
begin
	update Tamaño set
	estado = 0
	where idTamaño = @idTamaño
end
GO
/****** Object:  StoredProcedure [dbo].[spDeshabilitarUsuario]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spDeshabilitarUsuario]
	@idUsuario int
as
begin
	update Usuario set
	estado = 0
	where idUsuario = @idUsuario
end
GO
/****** Object:  StoredProcedure [dbo].[spEditaCombo]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spEditaCombo]
	@idCombo int,
	@nombreCombo varchar(100),
	@descripcion varchar(255),
	@precioVenta decimal(10,2)
as
begin
	update Combo set
	nombreCombo = @nombreCombo,
	descripcion = @descripcion,
	precioVenta = @precioVenta
	where
	idCombo=@idCombo
end
GO
/****** Object:  StoredProcedure [dbo].[spEditaComboProducto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spEditaComboProducto]
	@idComboProducto INT,
    @nuevoIdProducto INT,
    @nuevaCantidad INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @idCombo INT;

    -- 1. Obtener idCombo asociado al idComboProducto
    SELECT @idCombo = idCombo
    FROM ComboProducto
    WHERE idComboProducto = @idComboProducto;

    IF @idCombo IS NULL
    BEGIN
        RAISERROR('El registro ComboProducto no existe.', 16, 1);
        RETURN;
    END

    -- 2. Verificar que el nuevo producto exista
    IF NOT EXISTS (SELECT 1 FROM Producto WHERE idProducto = @nuevoIdProducto)
    BEGIN
        RAISERROR('El producto especificado no existe.', 16, 1);
        RETURN;
    END

    -- 3. Validar que el nuevo producto no esté ya en el mismo combo (excepto el actual)
    IF EXISTS (
        SELECT 1 FROM ComboProducto
        WHERE idCombo = @idCombo
          AND idProducto = @nuevoIdProducto
          AND idComboProducto <> @idComboProducto
    )
    BEGIN
        RAISERROR('Este producto ya está asignado al combo. No puede duplicarse.', 16, 1);
        RETURN;
    END

    -- 4. Actualizar el registro
    UPDATE ComboProducto
    SET idProducto = @nuevoIdProducto,
        cantidad = @nuevaCantidad
    WHERE idComboProducto = @idComboProducto;
END;
GO
/****** Object:  StoredProcedure [dbo].[spEditaEstado]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     procedure [dbo].[spEditaEstado]
	@idEstado int,
	@nombreEstado varchar(50)
as
begin
	update Estado set
	nombreEstado = @nombreEstado
	
	where
	idEstado=@idEstado
end
GO
/****** Object:  StoredProcedure [dbo].[spEditaFormaEnvio]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure[dbo].[spEditaFormaEnvio]
	@idFormaEnvio int,
	@nombreForma varchar(50)
as
begin
	update FormaEnvio 
	set nombreForma = @nombreForma
	where idFormaEnvio = @idFormaEnvio
end
GO
/****** Object:  StoredProcedure [dbo].[spEditaProducto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spEditaProducto]
	@idProducto int,
    @idCategoria int,
    @idMarca int,
    @idTamaño int,
    @nombreProducto varchar(100),
    @precioVenta decimal(10,2),
    @stock int,
    @descripcionP varchar(200),
    @imagen varchar(200) = NULL
AS
BEGIN
    UPDATE Producto SET
        idCategoria = @idCategoria,
        idMarca = @idMarca,
        idTamaño = @idTamaño,
        nombreProducto = @nombreProducto,
        precioVenta = @precioVenta,
        stock = @stock,
        descripcionP = @descripcionP,
        imagen = @imagen
    WHERE idProducto = @idProducto
END
GO
/****** Object:  StoredProcedure [dbo].[spEditarCategoria]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarCategoria]
	@idCategoria int,
	@nombreCategoria varchar(100)
as
begin
	update Categoria set
	nombreCategoria = @nombreCategoria
	where idCategoria = @idCategoria
end
GO
/****** Object:  StoredProcedure [dbo].[spEditarMarca]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarMarca]
	@idMarca int,
	@nombreMarca varchar(100)
as
begin
	update Marca set
	nombreMarca = @nombreMarca
	where idMarca = @idMarca
end
GO
/****** Object:  StoredProcedure [dbo].[spEditarMetodoDePago]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarMetodoDePago]
	@idMetodoPago int,
	@nombreMetodo varchar(100)
as
begin
	update MetodoDePago set
	nombreMetodo = @nombreMetodo
	where idMetodoPago = @idMetodoPago
end
GO
/****** Object:  StoredProcedure [dbo].[spEditaRol]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spEditaRol]
	@idRol int,
	@nombreRol varchar(50)
	
as
begin
	update Rol set
	nombreRol = @nombreRol
	
	where
	idRol=@idRol
end
GO
/****** Object:  StoredProcedure [dbo].[spEditarPermiso]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarPermiso]
    @idPermiso int,
    @nombrePermiso varchar(100),
    @descripcion varchar(255) = NULL
AS
BEGIN
    UPDATE Permiso 
    SET nombrePermiso = @nombrePermiso,
        descripcion = @descripcion
    WHERE idPermiso = @idPermiso
END
GO
/****** Object:  StoredProcedure [dbo].[spEditarTamaño]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarTamaño]
	@idTamaño int,
	@nombre varchar(100)
as
begin
	update Tamaño set
	nombre = @nombre
	where idTamaño = @idTamaño
end
GO
/****** Object:  StoredProcedure [dbo].[spEditaUsuario]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spEditaUsuario]
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
	dni = @dni,
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
GO
/****** Object:  StoredProcedure [dbo].[spEstadisticasActividad]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEstadisticasActividad]
    @fechaInicio datetime,
    @fechaFin datetime
AS
BEGIN
    SELECT 
        l.accion,
        COUNT(*) as totalAcciones,
        COUNT(DISTINCT l.idUsuario) as usuariosUnicos
    FROM LogActividad l
    WHERE l.fecha BETWEEN @fechaInicio AND @fechaFin
    GROUP BY l.accion
    ORDER BY totalAcciones DESC
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertarCategoria]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertarCategoria]
	@nombreCategoria varchar(100),
	@estado int
as
begin
	insert into Categoria (nombreCategoria,estado)
	values (@nombreCategoria,1);
end
GO
/****** Object:  StoredProcedure [dbo].[spInsertarCombo]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spInsertarCombo]
	@nombreCombo varchar(100),
	@descripcion varchar(255),
	@precioVenta decimal(10,2)
as
begin
	insert into Combo(nombreCombo,descripcion, precioVenta,estado)
	values (@nombreCombo, @descripcion,@precioVenta, 1);
end
GO
/****** Object:  StoredProcedure [dbo].[spInsertarComboProducto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spInsertarComboProducto]
	@idCombo int,
	@idProducto int,
	@cantidad int
as
begin
	insert into ComboProducto(idCombo,idProducto, cantidad)
	values (@idCombo,@idProducto,@cantidad);
end
GO
/****** Object:  StoredProcedure [dbo].[spInsertarEstado]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create     procedure [dbo].[spInsertarEstado]
	@nombreEstado varchar (50)
	
as
begin
	insert into Estado(nombreEstado, estado)
	values (@nombreEstado,1);
end
GO
/****** Object:  StoredProcedure [dbo].[spInsertarFormaEnvio]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spInsertarFormaEnvio]
	@nombreForma varchar(50)
as
begin
	insert into Formaenvio(nombreForma, estado)
	values(@nombreForma, 1);

end
GO
/****** Object:  StoredProcedure [dbo].[spInsertarMarca]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertarMarca]
	@nombreMarca varchar(100),
	@estado int
as
begin
	insert into Marca (nombreMarca,estado)
	values (@nombreMarca,1);
end
GO
/****** Object:  StoredProcedure [dbo].[spInsertarMetodoDePago]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertarMetodoDePago]
	@nombreMetodo varchar(100),
	@estado int
as
begin
	insert into MetodoDePago(nombreMetodo,estado)
	values (@nombreMetodo,1);
end
GO
/****** Object:  StoredProcedure [dbo].[spInsertarPedidoOnline]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[spInsertarPedidoOnline]
    @idUsuario int,
    @idEstado int,
    @idFormaEnvio int,
    @idMetodoPago int,
    @fecha date,
    @hora time,
    @observaciones varchar(250) = NULL,
    @direccion varchar(250) = NULL, -- <-- Agregado
    @idPedidoOnline int OUTPUT
AS
BEGIN
    INSERT INTO Pedidoonline (idUsuario, idEstado, idFormaEnvio, idMetodoPago, fecha, hora, observaciones, direccion)
    VALUES (@idUsuario, @idEstado, @idFormaEnvio, @idMetodoPago, @fecha, @hora, @observaciones, @direccion);

    SET @idPedidoOnline = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertarPedidoProducto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- PROCEDIMIENTO PARA INSERTAR PEDIDO PRODUCTO (DETALLE)
-- =============================================
CREATE   PROCEDURE [dbo].[spInsertarPedidoProducto]
    @idPedidoOnline int,
    @idProducto int,
    @cantidad int,
    @precioUnitario decimal(10,2),
    @subtotal decimal(10,2)
AS
BEGIN
    INSERT INTO PedidoProducto (idPedidoOnline, idProducto, cantidad, precioUnitario, subtotal)
    VALUES (@idPedidoOnline, @idProducto, @cantidad, @precioUnitario, @subtotal);
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertarPermiso]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertarPermiso]
    @nombrePermiso varchar(100),
    @descripcion varchar(255) = NULL
AS
BEGIN
    INSERT INTO Permiso (nombrePermiso, descripcion, estado)
    VALUES (@nombrePermiso, @descripcion, 1)
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertarProducto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create    procedure [dbo].[spInsertarProducto]
	@idCategoria int,
    @idMarca int,
    @idTamaño int,
    @nombreProducto varchar(100),
    @precioVenta decimal(10,2),
    @stock int,
    @descripcionP varchar(200),
    @imagen varchar(200) = NULL
AS
BEGIN
    INSERT INTO Producto(idCategoria, idMarca, idTamaño, nombreProducto, precioVenta, stock, estado, descripcionP, imagen)
    VALUES (@idCategoria, @idMarca, @idTamaño, @nombreProducto, @precioVenta, @stock, 1, @descripcionP, @imagen);
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertarRol]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spInsertarRol]
	@nombreRol varchar (50)
	
as
begin
	insert into Rol(nombreRol, estado)
	values (@nombreRol,1);
end
GO
/****** Object:  StoredProcedure [dbo].[spInsertarTamaño]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertarTamaño]
	@nombre varchar(100),
	@estado int
as
begin
	insert into Tamaño (nombre,estado)
	values (@nombre,1);
end
GO
/****** Object:  StoredProcedure [dbo].[spInsertarUsuario]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spInsertarUsuario]
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
GO
/****** Object:  StoredProcedure [dbo].[spLimpiarLogsAntiguos]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 12. PROCEDIMIENTO PARA LIMPIAR LOGS ANTIGUOS
-- =============================================

CREATE PROCEDURE [dbo].[spLimpiarLogsAntiguos]
	@diasRetener int = 90
AS
BEGIN
	DELETE FROM LogActividad 
	WHERE fecha < DATEADD(DAY, -@diasRetener, GETDATE())
	
	DELETE FROM SesionUsuario 
	WHERE fechaExpiracion < GETDATE() AND activa = 0
END
GO
/****** Object:  StoredProcedure [dbo].[spListarCategoria]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spListarCategoria]
as 
	select idCategoria, nombreCategoria, estado from Categoria
	where estado = 1
GO
/****** Object:  StoredProcedure [dbo].[spListarCombo]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spListarCombo]
as
	select idCombo, nombreCombo, descripcion, precioVenta, estado from Combo
	where estado = 1
GO
/****** Object:  StoredProcedure [dbo].[spListarComboProducto]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spListarComboProducto]
	@idCombo int
as
	select idComboProducto, idCombo, idProducto, cantidad from ComboProducto
	where idCombo = @idCombo
GO
/****** Object:  StoredProcedure [dbo].[spListarEstado]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create     procedure [dbo].[spListarEstado]
as
	select idEstado, nombreEstado,  estado from Estado
	where estado = 1
GO
/****** Object:  StoredProcedure [dbo].[spListarFormaEnvio]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spListarFormaEnvio]
as
	Select idFormaEnvio, nombreForma, estado from FormaEnvio
	where estado = 1
GO
/****** Object:  StoredProcedure [dbo].[spListarLogs]    Script Date: 09/07/2025 23:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spListarLogs]
    @fechaInicio datetime,
    @fechaFin datetime
AS
BEGIN
    SELECT 
        l.idLog,
        l.idUsuario,
        u.nombreCli + ' ' + u.apellidoCli as nombreUsuario,
        l.accion,
        l.tabla,
        l.idRegistro,
        l.datosAnteriores,
        l.datosNuevos,
        l.ipAddress,
        l.fecha
    FROM LogActividad l
    LEFT JOIN Usuario u ON l.idUsuario = u.idUsuario
    WHERE l.fecha BETWEEN @fechaInicio AND @fechaFin
    ORDER BY l.fecha DESC
END
GO
/****** Object:  StoredProcedure [dbo].[spListarMarca]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spListarMarca]
as 
	select idMarca, nombreMarca, estado from Marca
	where estado = 1
GO
/****** Object:  StoredProcedure [dbo].[spListarMetodoDePago]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spListarMetodoDePago]
as 
	select idMetodoPago, nombreMetodo, estado from MetodoDePago
	where estado = 1
GO
/****** Object:  StoredProcedure [dbo].[spListarPedidoProducto]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create     procedure [dbo].[spListarPedidoProducto]
as
	select idPedidoProducto, idPedidoOnline,  idProducto, cantidad, precioUnitario, subtotal from PedidoProducto
	
GO
/****** Object:  StoredProcedure [dbo].[spListarPermisos]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spListarPermisos]
AS
BEGIN
    SELECT idPermiso, nombrePermiso, descripcion, estado 
    FROM Permiso 
    WHERE estado = 1
    ORDER BY nombrePermiso
END
GO
/****** Object:  StoredProcedure [dbo].[spListarProducto]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create    procedure [dbo].[spListarProducto]
AS
BEGIN
    SELECT idProducto, idCategoria, idMarca, idTamaño, nombreProducto, precioVenta, stock, estado, descripcionP, imagen
    FROM Producto
    WHERE estado = 1
END
GO
/****** Object:  StoredProcedure [dbo].[spListarProductoPorCategoria]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[spListarProductoPorCategoria]
    @idCategoria int
AS
BEGIN
    SELECT idProducto, idCategoria, nombreProducto, precioVenta, descripcionP, imagen
    FROM Producto
    WHERE estado = 1 AND idCategoria = @idCategoria
END
GO
/****** Object:  StoredProcedure [dbo].[spListarRol]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spListarRol]
as
	select idRol, nombreRol,  estado from Rol
	where estado = 1
GO
/****** Object:  StoredProcedure [dbo].[spListarTamaño]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spListarTamaño]
as 
	select idTamaño, nombre, estado from Tamaño
	where estado = 1
GO
/****** Object:  StoredProcedure [dbo].[spListarUsuario]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[spListarUsuario]
as
	select idUsuario, dni, nombreCli,apellidoCli,correo,contraseña,ruc,razonSocial, direccion, telefono,idRol, estado from Usuario
	where estado = 1
GO
/****** Object:  StoredProcedure [dbo].[spObtenerPermisosRol]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObtenerPermisosRol]
    @idRol int
AS
BEGIN
    SELECT p.idPermiso, p.nombrePermiso, p.descripcion
    FROM Permiso p
    INNER JOIN RolPermiso rp ON p.idPermiso = rp.idPermiso
    WHERE rp.idRol = @idRol AND p.estado = 1
    ORDER BY p.nombrePermiso
END
GO
/****** Object:  StoredProcedure [dbo].[spObtenerSesionesActivas]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spObtenerSesionesActivas]
AS
BEGIN
    SELECT 
        s.idSesion,
        s.idUsuario,
        u.nombreCli + ' ' + u.apellidoCli as nombreUsuario,
        s.token,
        s.fechaInicio,
        s.fechaExpiracion,
        s.ipAddress,
        s.userAgent
    FROM SesionUsuario s
    INNER JOIN Usuario u ON s.idUsuario = u.idUsuario
    WHERE s.activa = 1 AND s.fechaExpiracion > GETDATE()
    ORDER BY s.fechaInicio DESC
END
GO
/****** Object:  StoredProcedure [dbo].[spQuitarPermisoRol]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spQuitarPermisoRol]
    @idRol int,
    @idPermiso int
AS
BEGIN
    DELETE FROM RolPermiso 
    WHERE idRol = @idRol AND idPermiso = @idPermiso
END
GO
/****** Object:  StoredProcedure [dbo].[spRegistrarActividad]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para registrar actividad
CREATE PROCEDURE [dbo].[spRegistrarActividad]
	@idUsuario int = NULL,
	@accion varchar(100),
	@tabla varchar(100) = NULL,
	@idRegistro int = NULL,
	@datosAnteriores text = NULL,
	@datosNuevos text = NULL,
	@ipAddress varchar(45) = NULL
AS
BEGIN
	INSERT INTO LogActividad (idUsuario, accion, tabla, idRegistro, datosAnteriores, datosNuevos, ipAddress)
	VALUES (@idUsuario, @accion, @tabla, @idRegistro, @datosAnteriores, @datosNuevos, @ipAddress)
END
GO
/****** Object:  StoredProcedure [dbo].[spReporteActividadUsuario]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spReporteActividadUsuario]
    @idUsuario int,
    @fechaInicio datetime,
    @fechaFin datetime
AS
BEGIN
    SELECT 
        l.accion,
        l.tabla,
        l.idRegistro,
        l.ipAddress,
        l.fecha
    FROM LogActividad l
    WHERE l.idUsuario = @idUsuario 
    AND l.fecha BETWEEN @fechaInicio AND @fechaFin
    ORDER BY l.fecha DESC
END
GO
/****** Object:  StoredProcedure [dbo].[spReporteClientesPeriodo]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Reporte de clientes por período
CREATE PROCEDURE [dbo].[spReporteClientesPeriodo]
	@fechaInicio date,
	@fechaFin date
AS
BEGIN
	SELECT 
		u.idUsuario,
		u.nombreCli + ' ' + u.apellidoCli as nombreCompleto,
		u.correo,
		u.telefono,
		COUNT(p.idPedidoOnline) as pedidosEnPeriodo,
		SUM(cp.monto) as totalGastadoEnPeriodo,
		MAX(p.fecha) as ultimaCompra
	FROM Usuario u
	JOIN Pedidoonline p ON u.idUsuario = p.idUsuario
	JOIN ComprobanteDePago cp ON p.idPedidoOnline = cp.idPedidoOnline
	WHERE u.idRol = 3 
	AND p.fecha BETWEEN @fechaInicio AND @fechaFin
	GROUP BY u.idUsuario, u.nombreCli, u.apellidoCli, u.correo, u.telefono
	ORDER BY totalGastadoEnPeriodo DESC
END
GO
/****** Object:  StoredProcedure [dbo].[spReporteProductosPorCategoria]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Reporte de productos por categoría
CREATE PROCEDURE [dbo].[spReporteProductosPorCategoria]
AS
BEGIN
	SELECT 
		c.nombreCategoria,
		COUNT(pr.idProducto) as totalProductos,
		SUM(pr.stock) as stockTotal,
		AVG(pr.precioVenta) as precioPromedio
	FROM Categoria c
	LEFT JOIN Producto pr ON c.idCategoria = pr.idCategoria AND pr.estado = 1
	WHERE c.estado = 1
	GROUP BY c.idCategoria, c.nombreCategoria
	ORDER BY totalProductos DESC
END
GO
/****** Object:  StoredProcedure [dbo].[spReporteVentasDiarias]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 10. PROCEDIMIENTOS PARA REPORTES
-- =============================================

-- Reporte de ventas diarias
CREATE PROCEDURE [dbo].[spReporteVentasDiarias]
	@fechaInicio date,
	@fechaFin date
AS
BEGIN
	SELECT 
		CAST(p.fecha AS DATE) as fecha,
		COUNT(DISTINCT p.idPedidoOnline) as totalPedidos,
		SUM(cp.monto) as totalVentas,
		AVG(cp.monto) as promedioVenta,
		COUNT(DISTINCT p.idUsuario) as clientesUnicos
	FROM Pedidoonline p
	JOIN ComprobanteDePago cp ON p.idPedidoOnline = cp.idPedidoOnline
	WHERE p.fecha BETWEEN @fechaInicio AND @fechaFin
	GROUP BY CAST(p.fecha AS DATE)
	ORDER BY fecha
END
GO
/****** Object:  StoredProcedure [dbo].[spValidarSesion]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento para validar sesión
CREATE PROCEDURE [dbo].[spValidarSesion]
	@token varchar(255)
AS
BEGIN
	SELECT s.idSesion, s.idUsuario, u.nombreCli, u.apellidoCli, u.idRol, r.nombreRol
	FROM SesionUsuario s
	JOIN Usuario u ON s.idUsuario = u.idUsuario
	JOIN Rol r ON u.idRol = r.idRol
	WHERE s.token = @token 
	AND s.activa = 1 
	AND s.fechaExpiracion > GETDATE()
	AND u.estado = 1
END
GO
/****** Object:  StoredProcedure [dbo].[spVerificarPermiso]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 7. PROCEDIMIENTOS ALMACENADOS PARA SEGURIDAD
-- =============================================

-- Procedimiento para verificar permisos de usuario
CREATE PROCEDURE [dbo].[spVerificarPermiso]
	@idUsuario int,
	@nombrePermiso varchar(100)
AS
BEGIN
	SELECT COUNT(*) as tienePermiso
	FROM Usuario u
	JOIN RolPermiso rp ON u.idRol = rp.idRol
	JOIN Permiso p ON rp.idPermiso = p.idPermiso
	WHERE u.idUsuario = @idUsuario 
	AND p.nombrePermiso = @nombrePermiso
	AND u.estado = 1 
	AND p.estado = 1
END
GO
/****** Object:  Trigger [dbo].[tr_Producto_Audit]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Trigger para auditar cambios en Producto
CREATE TRIGGER [dbo].[tr_Producto_Audit] ON [dbo].[Producto]
AFTER UPDATE
AS
BEGIN
	DECLARE @idUsuario int
	
	-- Obtener el usuario de la sesión actual (esto se implementaría en la aplicación)
	SET @idUsuario = NULL
	
	INSERT INTO LogActividad (idUsuario, accion, tabla, idRegistro, datosAnteriores, datosNuevos)
	SELECT 
		@idUsuario,
		'UPDATE',
		'Producto',
		i.idProducto,
		(SELECT * FROM deleted FOR JSON PATH),
		(SELECT * FROM inserted FOR JSON PATH)
	FROM inserted i
END
GO
ALTER TABLE [dbo].[Producto] ENABLE TRIGGER [tr_Producto_Audit]
GO
/****** Object:  Trigger [dbo].[tr_Usuario_Audit]    Script Date: 09/07/2025 23:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 9. TRIGGERS PARA AUDITORÍA AUTOMÁTICA
-- =============================================

-- Trigger para auditar cambios en Usuario
CREATE TRIGGER [dbo].[tr_Usuario_Audit] ON [dbo].[Usuario]
AFTER UPDATE
AS
BEGIN
	DECLARE @idUsuario int, @accion varchar(100)
	
	SELECT @idUsuario = idUsuario FROM inserted
	
	INSERT INTO LogActividad (idUsuario, accion, tabla, idRegistro, datosAnteriores, datosNuevos)
	SELECT 
		@idUsuario,
		'UPDATE',
		'Usuario',
		i.idUsuario,
		(SELECT * FROM deleted FOR JSON PATH),
		(SELECT * FROM inserted FOR JSON PATH)
	FROM inserted i
END
GO
ALTER TABLE [dbo].[Usuario] ENABLE TRIGGER [tr_Usuario_Audit]
GO

-- PROCEDIMIENTOS ADICIONALES PARA LISTAR TODOS LOS ELEMENTOS
-- =============================================

-- LISTAR TODAS LAS CATEGORIAS (INCLUYENDO DESHABILITADAS)
CREATE PROCEDURE [dbo].[spListarTodasCategorias]
as 
	select idCategoria, nombreCategoria, estado from Categoria
GO

-- LISTAR TODOS LOS TAMAÑOS (INCLUYENDO DESHABILITADOS)
CREATE PROCEDURE [dbo].[spListarTodosTamaños]
as 
	select idTamaño, nombre, estado from Tamaño
GO

-- LISTAR TODAS LAS MARCAS (INCLUYENDO DESHABILITADAS)
CREATE PROCEDURE [dbo].[spListarTodasMarcas]
as 
	select idMarca, nombreMarca, estado from Marca
GO

-- LISTAR TODOS LOS METODOS DE PAGO (INCLUYENDO DESHABILITADOS)
CREATE PROCEDURE [dbo].[spListarTodosMetodosDePago]
as 
	select idMetodoPago, nombreMetodo, estado from MetodoDePago
GO

-- LISTAR TODOS LOS ROLES (INCLUYENDO DESHABILITADOS)
CREATE PROCEDURE [dbo].[spListarTodosRoles]
as 
	select idRol, nombreRol, estado from Rol
GO
