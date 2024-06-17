USE [SuppliesDB]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 17.06.2024 2:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fullName] [varchar](100) NOT NULL,
	[address] [varchar](100) NOT NULL,
	[phoneNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Components]    Script Date: 17.06.2024 2:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Components](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[supplies_ID] [int] NOT NULL,
	[type_ID] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[price] [int] NOT NULL,
 CONSTRAINT [PK_Components] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Components_type]    Script Date: 17.06.2024 2:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Components_type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Components_type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ordered_components]    Script Date: 17.06.2024 2:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ordered_components](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[orders_ID] [int] NOT NULL,
	[components_ID] [int] NOT NULL,
	[checkCode] [bigint] NOT NULL,
 CONSTRAINT [PK_Ordered_components_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 17.06.2024 2:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[client_ID] [int] NOT NULL,
	[createDate] [datetime] NULL,
	[deliveryDate] [date] NULL,
	[fullPrice] [int] NOT NULL,
	[orderStatus] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 17.06.2024 2:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplies]    Script Date: 17.06.2024 2:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplies](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[address] [varchar](100) NOT NULL,
	[phoneNumber] [bigint] NOT NULL,
	[deliveryTime] [int] NOT NULL,
 CONSTRAINT [PK_Supplies] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 17.06.2024 2:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[role_ID] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([ID], [fullName], [address], [phoneNumber]) VALUES (1, N'Шуметов Максим Сергеевич', N'ул. Димитрова, д.11б', 89121448745)
INSERT [dbo].[Clients] ([ID], [fullName], [address], [phoneNumber]) VALUES (3, N'Иванов Иван Иванович', N'ул. Ленинна 12, д. 5', 89121543287)
INSERT [dbo].[Clients] ([ID], [fullName], [address], [phoneNumber]) VALUES (4, N'АГА', N'ул. НеПридумал, д. 76', 82356534896)
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Components] ON 

INSERT [dbo].[Components] ([ID], [supplies_ID], [type_ID], [name], [price]) VALUES (1, 1, 1, N'Fifine A8', 5099)
INSERT [dbo].[Components] ([ID], [supplies_ID], [type_ID], [name], [price]) VALUES (2, 1, 1, N'Fifinte AM8', 6099)
INSERT [dbo].[Components] ([ID], [supplies_ID], [type_ID], [name], [price]) VALUES (4, 3, 2, N'G PRO 2', 15000)
INSERT [dbo].[Components] ([ID], [supplies_ID], [type_ID], [name], [price]) VALUES (5, 3, 2, N'FLSports DF54A', 5890)
INSERT [dbo].[Components] ([ID], [supplies_ID], [type_ID], [name], [price]) VALUES (6, 4, 7, N'Офисник (i3 12, gtx1050)', 30000)
SET IDENTITY_INSERT [dbo].[Components] OFF
GO
SET IDENTITY_INSERT [dbo].[Components_type] ON 

INSERT [dbo].[Components_type] ([ID], [name]) VALUES (1, N'Микрофон')
INSERT [dbo].[Components_type] ([ID], [name]) VALUES (2, N'Клавиатура')
INSERT [dbo].[Components_type] ([ID], [name]) VALUES (3, N'Мышь')
INSERT [dbo].[Components_type] ([ID], [name]) VALUES (4, N'Коврик для мыши')
INSERT [dbo].[Components_type] ([ID], [name]) VALUES (5, N'Монитор')
INSERT [dbo].[Components_type] ([ID], [name]) VALUES (7, N'Компьютеры')
SET IDENTITY_INSERT [dbo].[Components_type] OFF
GO
SET IDENTITY_INSERT [dbo].[Ordered_components] ON 

INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1, 19, 1, 19120520241537)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (2, 20, 2, 20120520242007)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (9, 22, 1, 22120520242207)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (10, 22, 2, 22120520242207)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1003, 1021, 1, 1021140520241630)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1004, 1021, 2, 1021140520241630)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1005, 1021, 1, 1021140520241630)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1006, 1021, 2, 1021140520241630)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1016, 1029, 4, 1029260520242030)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1017, 1029, 1, 1029260520242030)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1018, 1028, 4, 1028260520242000)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1019, 1028, 2, 1028260520242000)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1020, 1028, 1, 1028260520242000)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1024, 1033, 1, 1033010620241841)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1025, 1033, 5, 1033010620241841)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1032, 1034, 1, 1034010620241853)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1038, 1036, 2, 1036020620240139)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1039, 1036, 5, 1036020620240139)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (1040, 1036, 4, 1036020620240139)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (2021, 2031, 1, 2031050620241941)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (2022, 1035, 1, 1035010620241859)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (2023, 1035, 2, 1035010620241859)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (2024, 2033, 1, 2033080620242333)
INSERT [dbo].[Ordered_components] ([ID], [orders_ID], [components_ID], [checkCode]) VALUES (2025, 2033, 5, 2033080620242333)
SET IDENTITY_INSERT [dbo].[Ordered_components] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (19, 1, CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-05-15' AS Date), 6599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (20, 1, CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-05-15' AS Date), 35094, 1)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (22, 1, CAST(N'2024-05-12T22:07:30.347' AS DateTime), CAST(N'2024-05-15' AS Date), 12698, 2)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1021, 1, CAST(N'2024-05-14T16:30:10.397' AS DateTime), CAST(N'2024-05-17' AS Date), 23896, 3)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1022, 3, CAST(N'2024-05-23T00:06:17.970' AS DateTime), CAST(N'2024-05-27' AS Date), 20088, 2)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1023, 3, CAST(N'2024-05-23T00:07:26.953' AS DateTime), CAST(N'2024-05-31' AS Date), 8890, 3)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1024, 3, CAST(N'2024-05-23T00:08:41.700' AS DateTime), CAST(N'2024-05-26' AS Date), 9099, 3)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1028, 3, CAST(N'2024-06-01T18:37:48.690' AS DateTime), CAST(N'2024-06-05' AS Date), 29198, 3)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1029, 1, CAST(N'2024-06-01T18:15:46.420' AS DateTime), CAST(N'2024-06-05' AS Date), 28989, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1031, 1, CAST(N'2024-06-01T18:38:36.380' AS DateTime), CAST(N'2024-06-04' AS Date), 8099, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1032, 3, CAST(N'2024-06-01T18:40:18.237' AS DateTime), CAST(N'2024-06-04' AS Date), 8099, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1033, 1, CAST(N'2024-06-01T18:42:29.127' AS DateTime), CAST(N'2024-06-05' AS Date), 13989, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1034, 1, CAST(N'2024-06-01T18:57:35.650' AS DateTime), CAST(N'2024-06-04' AS Date), 8099, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1035, 3, CAST(N'2024-06-01T18:59:14.620' AS DateTime), CAST(N'2024-06-08' AS Date), 14198, 2)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1036, 1, CAST(N'2024-06-02T01:39:25.817' AS DateTime), CAST(N'2024-06-06' AS Date), 29989, 3)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (2031, 4, CAST(N'2024-06-05T19:41:08.590' AS DateTime), CAST(N'2024-06-08' AS Date), 8099, 4)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (2033, 1, CAST(N'2024-06-08T23:33:37.713' AS DateTime), CAST(N'2024-06-12' AS Date), 13989, 0)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([ID], [roleName]) VALUES (1, N'Оператор')
INSERT [dbo].[Roles] ([ID], [roleName]) VALUES (2, N'Работник склада')
INSERT [dbo].[Roles] ([ID], [roleName]) VALUES (3, N'Администратор')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Supplies] ON 

INSERT [dbo].[Supplies] ([ID], [name], [address], [phoneNumber], [deliveryTime]) VALUES (1, N'doedatel INC', N'г. Люберцы, ул. Автозоводская, д. 56', 84953342387, 3)
INSERT [dbo].[Supplies] ([ID], [name], [address], [phoneNumber], [deliveryTime]) VALUES (3, N'test', N'test', 8912892, 4)
INSERT [dbo].[Supplies] ([ID], [name], [address], [phoneNumber], [deliveryTime]) VALUES (4, N'CompMaster', N'г. Воркута, ул. НеПридумал, д. 2', 89212667904, 6)
SET IDENTITY_INSERT [dbo].[Supplies] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [login], [password], [role_ID]) VALUES (1, N'1', N'1', 1)
INSERT [dbo].[Users] ([ID], [login], [password], [role_ID]) VALUES (2, N'2', N'2', 2)
INSERT [dbo].[Users] ([ID], [login], [password], [role_ID]) VALUES (3, N'3', N'3', 3)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Components]  WITH CHECK ADD  CONSTRAINT [FK_Components_Components_type] FOREIGN KEY([type_ID])
REFERENCES [dbo].[Components_type] ([ID])
GO
ALTER TABLE [dbo].[Components] CHECK CONSTRAINT [FK_Components_Components_type]
GO
ALTER TABLE [dbo].[Components]  WITH CHECK ADD  CONSTRAINT [FK_Components_Supplies] FOREIGN KEY([supplies_ID])
REFERENCES [dbo].[Supplies] ([ID])
GO
ALTER TABLE [dbo].[Components] CHECK CONSTRAINT [FK_Components_Supplies]
GO
ALTER TABLE [dbo].[Ordered_components]  WITH CHECK ADD  CONSTRAINT [FK_Ordered_components_Components] FOREIGN KEY([components_ID])
REFERENCES [dbo].[Components] ([ID])
GO
ALTER TABLE [dbo].[Ordered_components] CHECK CONSTRAINT [FK_Ordered_components_Components]
GO
ALTER TABLE [dbo].[Ordered_components]  WITH CHECK ADD  CONSTRAINT [FK_Ordered_components_Orders] FOREIGN KEY([orders_ID])
REFERENCES [dbo].[Orders] ([ID])
GO
ALTER TABLE [dbo].[Ordered_components] CHECK CONSTRAINT [FK_Ordered_components_Orders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Clients] FOREIGN KEY([client_ID])
REFERENCES [dbo].[Clients] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Clients]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([role_ID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
