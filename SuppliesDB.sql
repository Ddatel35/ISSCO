USE [SuppliesDB]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 29.05.2024 0:11:00 ******/
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
/****** Object:  Table [dbo].[Components]    Script Date: 29.05.2024 0:11:01 ******/
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
/****** Object:  Table [dbo].[Components_type]    Script Date: 29.05.2024 0:11:01 ******/
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
/****** Object:  Table [dbo].[Ordered_components]    Script Date: 29.05.2024 0:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ordered_components](
	[orders_ID] [int] NOT NULL,
	[components_ID] [int] NOT NULL,
	[checkCode] [bigint] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Ordered_components_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 29.05.2024 0:11:01 ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 29.05.2024 0:11:01 ******/
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
/****** Object:  Table [dbo].[Supplies]    Script Date: 29.05.2024 0:11:01 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 29.05.2024 0:11:01 ******/
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
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Components] ON 

INSERT [dbo].[Components] ([ID], [supplies_ID], [type_ID], [name], [price]) VALUES (1, 1, 1, N'Fifine A8', 5099)
INSERT [dbo].[Components] ([ID], [supplies_ID], [type_ID], [name], [price]) VALUES (2, 1, 1, N'Fifinte AM8', 6099)
INSERT [dbo].[Components] ([ID], [supplies_ID], [type_ID], [name], [price]) VALUES (4, 3, 2, N'G PRO 2', 15000)
INSERT [dbo].[Components] ([ID], [supplies_ID], [type_ID], [name], [price]) VALUES (5, 3, 2, N'FLSports DF54A', 5890)
SET IDENTITY_INSERT [dbo].[Components] OFF
GO
SET IDENTITY_INSERT [dbo].[Components_type] ON 

INSERT [dbo].[Components_type] ([ID], [name]) VALUES (1, N'Микрофон')
INSERT [dbo].[Components_type] ([ID], [name]) VALUES (2, N'Клавиатура')
INSERT [dbo].[Components_type] ([ID], [name]) VALUES (3, N'Мышь')
INSERT [dbo].[Components_type] ([ID], [name]) VALUES (4, N'Коврик для мыши')
INSERT [dbo].[Components_type] ([ID], [name]) VALUES (5, N'Монитор')
SET IDENTITY_INSERT [dbo].[Components_type] OFF
GO
SET IDENTITY_INSERT [dbo].[Ordered_components] ON 

INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (19, 1, 19120520241537, 1)
INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (20, 2, 20120520242007, 2)
INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (22, 1, 22120520242207, 9)
INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (22, 2, 22120520242207, 10)
INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (1021, 1, 1021140520241630, 1003)
INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (1021, 2, 1021140520241630, 1004)
INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (1021, 1, 1021140520241630, 1005)
INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (1021, 2, 1021140520241630, 1006)
INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (1028, 4, 1028260520242000, 1014)
INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (1028, 2, 1028260520242000, 1015)
INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (1029, 4, 1029260520242030, 1016)
INSERT [dbo].[Ordered_components] ([orders_ID], [components_ID], [checkCode], [ID]) VALUES (1029, 1, 1029260520242030, 1017)
SET IDENTITY_INSERT [dbo].[Ordered_components] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1, 1, CAST(N'2024-05-06T00:00:00.000' AS DateTime), NULL, 5099, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (4, 1, CAST(N'2024-05-11T00:00:00.000' AS DateTime), CAST(N'2024-05-14' AS Date), 6599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (6, 1, CAST(N'2024-05-11T00:00:00.000' AS DateTime), CAST(N'2024-05-14' AS Date), 7599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (8, 1, CAST(N'2024-05-11T00:00:00.000' AS DateTime), CAST(N'2024-05-14' AS Date), 7599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (9, 1, CAST(N'2024-05-11T00:00:00.000' AS DateTime), CAST(N'2024-05-14' AS Date), 7599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (11, 1, CAST(N'2024-05-11T00:00:00.000' AS DateTime), CAST(N'2024-05-14' AS Date), 7599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (12, 1, CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-05-15' AS Date), 7599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (13, 1, CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-05-15' AS Date), 7599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (14, 1, CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-05-15' AS Date), 6599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (15, 1, CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-05-15' AS Date), 7599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (16, 1, CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-05-15' AS Date), 7599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (17, 1, CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-05-15' AS Date), 7599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (18, 1, CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-05-15' AS Date), 6599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (19, 1, CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-05-15' AS Date), 6599, 0)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (20, 1, CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-05-15' AS Date), 35094, 1)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (22, 1, CAST(N'2024-05-12T22:07:30.347' AS DateTime), CAST(N'2024-05-15' AS Date), 12698, 2)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1021, 1, CAST(N'2024-05-14T16:30:10.397' AS DateTime), CAST(N'2024-05-17' AS Date), 23896, 3)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1022, 3, CAST(N'2024-05-23T00:06:17.970' AS DateTime), CAST(N'2024-05-27' AS Date), 20088, 2)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1023, 3, CAST(N'2024-05-23T00:07:26.953' AS DateTime), CAST(N'2024-05-31' AS Date), 8890, 3)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1024, 3, CAST(N'2024-05-23T00:08:41.700' AS DateTime), CAST(N'2024-05-26' AS Date), 9099, 3)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1028, 3, CAST(N'2024-05-26T20:00:51.827' AS DateTime), CAST(N'2024-05-30' AS Date), 24099, 3)
INSERT [dbo].[Orders] ([ID], [client_ID], [createDate], [deliveryDate], [fullPrice], [orderStatus]) VALUES (1029, 1, CAST(N'2024-05-26T20:30:55.397' AS DateTime), CAST(N'2024-05-30' AS Date), 23099, 3)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([ID], [roleName]) VALUES (1, N'Оператор')
INSERT [dbo].[Roles] ([ID], [roleName]) VALUES (2, N'Работник склада')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Supplies] ON 

INSERT [dbo].[Supplies] ([ID], [name], [address], [phoneNumber], [deliveryTime]) VALUES (1, N'doedatel INC', N'г. Люберцы, ул. Автозоводская, д. 56', 84953342387, 3)
INSERT [dbo].[Supplies] ([ID], [name], [address], [phoneNumber], [deliveryTime]) VALUES (3, N'test', N'test', 8912892, 4)
SET IDENTITY_INSERT [dbo].[Supplies] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [login], [password], [role_ID]) VALUES (1, N'1', N'1', 1)
INSERT [dbo].[Users] ([ID], [login], [password], [role_ID]) VALUES (2, N'2', N'2', 2)
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
