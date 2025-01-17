USE [DBChallenge]
GO
/****** Object:  Table [dbo].[tblMetas]    Script Date: 8/8/2024 5:10:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMetas](
	[MetaId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](80) NOT NULL,
	[Creada] [datetime] NOT NULL,
	[TotalTareas] [int] NOT NULL,
	[Porcentaje] [decimal](6, 2) NOT NULL,
 CONSTRAINT [PK_tblMetas] PRIMARY KEY CLUSTERED 
(
	[MetaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTareas]    Script Date: 8/8/2024 5:10:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTareas](
	[TareaId] [int] IDENTITY(1,1) NOT NULL,
	[MetaId] [int] NOT NULL,
	[Nombre] [varchar](80) NOT NULL,
	[Creada] [datetime] NOT NULL,
	[Estado] [bit] NOT NULL,
	[Favorita] [bit] NOT NULL,
 CONSTRAINT [PK_tblTareas] PRIMARY KEY CLUSTERED 
(
	[TareaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblMetas] ON 

INSERT [dbo].[tblMetas] ([MetaId], [Nombre], [Creada], [TotalTareas], [Porcentaje]) VALUES (1, N'Testing 1', CAST(N'2024-07-31T19:13:18.853' AS DateTime), 0, CAST(0.00 AS Decimal(6, 2)))
INSERT [dbo].[tblMetas] ([MetaId], [Nombre], [Creada], [TotalTareas], [Porcentaje]) VALUES (2, N'Probando 2', CAST(N'2024-07-31T19:13:34.973' AS DateTime), 0, CAST(0.00 AS Decimal(6, 2)))
INSERT [dbo].[tblMetas] ([MetaId], [Nombre], [Creada], [TotalTareas], [Porcentaje]) VALUES (3, N'Testing 3', CAST(N'2024-07-31T19:13:51.847' AS DateTime), 0, CAST(0.00 AS Decimal(6, 2)))
SET IDENTITY_INSERT [dbo].[tblMetas] OFF
GO
SET IDENTITY_INSERT [dbo].[tblTareas] ON 

INSERT [dbo].[tblTareas] ([TareaId], [MetaId], [Nombre], [Creada], [Estado], [Favorita]) VALUES (1, 2, N'Task 2A', CAST(N'2024-07-31T19:16:25.890' AS DateTime), 0, 0)
INSERT [dbo].[tblTareas] ([TareaId], [MetaId], [Nombre], [Creada], [Estado], [Favorita]) VALUES (2, 2, N'Task 2B', CAST(N'2024-07-31T19:16:25.890' AS DateTime), 1, 1)
INSERT [dbo].[tblTareas] ([TareaId], [MetaId], [Nombre], [Creada], [Estado], [Favorita]) VALUES (3, 2, N'Task 2C', CAST(N'2024-07-31T19:16:25.890' AS DateTime), 0, 0)
SET IDENTITY_INSERT [dbo].[tblTareas] OFF
GO
ALTER TABLE [dbo].[tblMetas] ADD  CONSTRAINT [DF_tblMetas_Creada]  DEFAULT (getdate()) FOR [Creada]
GO
ALTER TABLE [dbo].[tblMetas] ADD  CONSTRAINT [DF_tblMetas_TotalTareas]  DEFAULT ((0)) FOR [TotalTareas]
GO
ALTER TABLE [dbo].[tblMetas] ADD  CONSTRAINT [DF_tblMetas_Porcentaje]  DEFAULT ((0)) FOR [Porcentaje]
GO
ALTER TABLE [dbo].[tblTareas] ADD  CONSTRAINT [DF_tblTareas_Creada]  DEFAULT (getdate()) FOR [Creada]
GO
ALTER TABLE [dbo].[tblTareas] ADD  CONSTRAINT [DF_tblTareas_Estado]  DEFAULT ((0)) FOR [Estado]
GO
ALTER TABLE [dbo].[tblTareas] ADD  CONSTRAINT [DF_tblTareas_Favorita]  DEFAULT ((0)) FOR [Favorita]
GO
ALTER TABLE [dbo].[tblTareas]  WITH CHECK ADD  CONSTRAINT [FK_tblTareas_tblMetas] FOREIGN KEY([MetaId])
REFERENCES [dbo].[tblMetas] ([MetaId])
GO
ALTER TABLE [dbo].[tblTareas] CHECK CONSTRAINT [FK_tblTareas_tblMetas]
GO
