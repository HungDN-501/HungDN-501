USE [ShopWeb]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2/22/2024 11:39:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2/22/2024 11:39:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[ImageUrl] [nchar](50) NULL,
	[Price] [decimal](8, 0) NULL,
	[Quantity] [int] NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/22/2024 11:39:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Role] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Điện thoại')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'Máy tính')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, N'Máy tính bảng')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (4, N'Máy quay')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Description], [ImageUrl], [Price], [Quantity], [CategoryId]) VALUES (1, N'Điện thoại XiaoMi', N'Là mẫu được bán chạy nhất thị trường năm 2023', N'/img/product01.png                                ', CAST(30 AS Decimal(8, 0)), 25, 1)
INSERT [dbo].[Product] ([Id], [Name], [Description], [ImageUrl], [Price], [Quantity], [CategoryId]) VALUES (2, N'Máy tính DELL', N'Là mẫu được bán chạy nhất thị trường năm 2023', N'/img/product02.png                                ', CAST(45 AS Decimal(8, 0)), 19, 2)
INSERT [dbo].[Product] ([Id], [Name], [Description], [ImageUrl], [Price], [Quantity], [CategoryId]) VALUES (3, N'Máy tính bảng AirPac', N'Là mẫu được bán chạy nhất thị trường năm 2023', N'/img/product03.png                                ', CAST(35 AS Decimal(8, 0)), 28, 3)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Name], [Email], [Password], [Role]) VALUES (1, N'Hung', N'hung@gmail.com', N'123456', N'Admin')
INSERT [dbo].[User] ([Id], [Name], [Email], [Password], [Role]) VALUES (2, N'Hiep', N'hiep@gmail.com', N'123456', N'User')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
