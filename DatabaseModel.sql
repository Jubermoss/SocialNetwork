﻿USE [Twitter]
GO
/****** Object:  User [sa_prueba]    ******/
CREATE USER [sa_prueba] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Usuario] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[usuario_id] [int] IDENTITY(1,1) NOT NULL,
	[alias] [varchar](20) NOT NULL,
	[password] [varchar](10) NOT NULL,
	[foto_usuario] [image] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[usuario_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Amigo]    Script Date: 04/13/2010 01:32:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Amigo](
	[amigo_id] [int] IDENTITY(1,1) NOT NULL,
	[usuario_seguido] [int] NOT NULL,
	[usuario_seguidor] [int] NOT NULL,
	[estado] [int] NOT NULL,
	[alias_seguido] [varchar](20) NOT NULL,
	[alias_seguidor] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Amigo] PRIMARY KEY CLUSTERED 
(
	[amigo_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Album]    Script Date: 04/13/2010 01:32:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Album](
	[album_id] [int] IDENTITY(1,1) NOT NULL,
	[album_nombre] [varchar](20) NOT NULL,
	[usuario_id] [int] NOT NULL,
	[tipo] [int] NOT NULL,
 CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED 
(
	[album_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Mensaje]    Script Date: 04/13/2010 01:32:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Mensaje](
	[mensaje_id] [int] IDENTITY(1,1) NOT NULL,
	[usuario_remitente_id] [int] NOT NULL,
	[usuario_destinatario_id] [int] NOT NULL,
	[descripcion] [text] NOT NULL,
	[alias_remitente] [varchar](20) NOT NULL,
	[alias_destinatario] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Mensaje] PRIMARY KEY CLUSTERED 
(
	[mensaje_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Imagen]    Script Date: 04/13/2010 01:32:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Imagen](
	[imagen_id] [int] IDENTITY(1,1) NOT NULL,
	[album_id] [int] NOT NULL,
	[imagen] [image] NOT NULL,
 CONSTRAINT [PK_Imagen] PRIMARY KEY CLUSTERED 
(
	[imagen_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Album_Usuario]    Script Date: 04/13/2010 01:32:28 ******/
ALTER TABLE [dbo].[Album]  WITH CHECK ADD  CONSTRAINT [FK_Album_Usuario] FOREIGN KEY([usuario_id])
REFERENCES [dbo].[Usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[Album] CHECK CONSTRAINT [FK_Album_Usuario]
GO
/****** Object:  ForeignKey [FK_Amigo_Usuario]    Script Date: 04/13/2010 01:32:28 ******/
ALTER TABLE [dbo].[Amigo]  WITH CHECK ADD  CONSTRAINT [FK_Amigo_Usuario] FOREIGN KEY([usuario_seguido])
REFERENCES [dbo].[Usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[Amigo] CHECK CONSTRAINT [FK_Amigo_Usuario]
GO
/****** Object:  ForeignKey [FK_Amigo_Usuario1]    Script Date: 04/13/2010 01:32:28 ******/
ALTER TABLE [dbo].[Amigo]  WITH CHECK ADD  CONSTRAINT [FK_Amigo_Usuario1] FOREIGN KEY([usuario_seguidor])
REFERENCES [dbo].[Usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[Amigo] CHECK CONSTRAINT [FK_Amigo_Usuario1]
GO
/****** Object:  ForeignKey [FK_Imagen_Album]    Script Date: 04/13/2010 01:32:28 ******/
ALTER TABLE [dbo].[Imagen]  WITH CHECK ADD  CONSTRAINT [FK_Imagen_Album] FOREIGN KEY([album_id])
REFERENCES [dbo].[Album] ([album_id])
GO
ALTER TABLE [dbo].[Imagen] CHECK CONSTRAINT [FK_Imagen_Album]
GO
/****** Object:  ForeignKey [FK_Mensaje_Usuario2]    Script Date: 04/13/2010 01:32:28 ******/
ALTER TABLE [dbo].[Mensaje]  WITH CHECK ADD  CONSTRAINT [FK_Mensaje_Usuario2] FOREIGN KEY([usuario_destinatario_id])
REFERENCES [dbo].[Usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[Mensaje] CHECK CONSTRAINT [FK_Mensaje_Usuario2]
GO
/****** Object:  ForeignKey [FK_Mensaje_Usuario3]    Script Date: 04/13/2010 01:32:28 ******/
ALTER TABLE [dbo].[Mensaje]  WITH CHECK ADD  CONSTRAINT [FK_Mensaje_Usuario3] FOREIGN KEY([usuario_remitente_id])
REFERENCES [dbo].[Usuario] ([usuario_id])
GO
ALTER TABLE [dbo].[Mensaje] CHECK CONSTRAINT [FK_Mensaje_Usuario3]
GO
