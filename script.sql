USE [ExtraAir_dev]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[Street] [nvarchar](max) NULL,
	[StreetNumber] [int] NOT NULL,
	[PostIndex] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Admins]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Admins] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Airports]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airports](
	[AirportId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[AddressId] [int] NULL,
 CONSTRAINT [PK_dbo.Airports] PRIMARY KEY CLUSTERED 
(
	[AirportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clients]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comforts]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comforts](
	[ComfortId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ComfortType] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PlaneId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Comforts] PRIMARY KEY CLUSTERED 
(
	[ComfortId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CreditCards]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditCards](
	[CreditCardId] [int] IDENTITY(1,1) NOT NULL,
	[CardNumber] [int] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[SecurityCode] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.CreditCards] PRIMARY KEY CLUSTERED 
(
	[CreditCardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Dispatchers]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dispatchers](
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Dispatchers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[FeedbackId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[TourId] [int] NULL,
	[AirportId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_dbo.Feedbacks] PRIMARY KEY CLUSTERED 
(
	[FeedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Paid] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreditCardId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PassengerOrders]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PassengerOrders](
	[Passenger_PassengerId] [int] NOT NULL,
	[Order_OrderId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PassengerOrders] PRIMARY KEY CLUSTERED 
(
	[Passenger_PassengerId] ASC,
	[Order_OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Passengers]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Passengers](
	[PassengerId] [int] IDENTITY(1,1) NOT NULL,
	[PassengerType] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Passengers] PRIMARY KEY CLUSTERED 
(
	[PassengerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PassengerTours]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PassengerTours](
	[Passenger_PassengerId] [int] NOT NULL,
	[Tour_TourId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PassengerTours] PRIMARY KEY CLUSTERED 
(
	[Passenger_PassengerId] ASC,
	[Tour_TourId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Planes]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Planes](
	[PlaneId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[MaxCountPassenger] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Planes] PRIMARY KEY CLUSTERED 
(
	[PlaneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tours]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tours](
	[TourId] [int] IDENTITY(1,1) NOT NULL,
	[DateStart] [datetime] NOT NULL,
	[DateFinish] [datetime] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CurrentCountPassenger] [int] NOT NULL,
	[PlaneId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Airport_AirportId] [int] NULL,
 CONSTRAINT [PK_dbo.Tours] PRIMARY KEY CLUSTERED 
(
	[TourId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/24/2017 10:29:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[Birthday] [datetime] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[AddressId] [int] NULL,
	[UserClaimId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Admins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Admins_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Admins] CHECK CONSTRAINT [FK_dbo.Admins_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[Airports]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Airports_dbo.Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO
ALTER TABLE [dbo].[Airports] CHECK CONSTRAINT [FK_dbo.Airports_dbo.Addresses_AddressId]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Clients_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_dbo.Clients_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[Comforts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Comforts_dbo.Planes_PlaneId] FOREIGN KEY([PlaneId])
REFERENCES [dbo].[Planes] ([PlaneId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comforts] CHECK CONSTRAINT [FK_dbo.Comforts_dbo.Planes_PlaneId]
GO
ALTER TABLE [dbo].[CreditCards]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CreditCards_dbo.Clients_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Clients] ([UserId])
GO
ALTER TABLE [dbo].[CreditCards] CHECK CONSTRAINT [FK_dbo.CreditCards_dbo.Clients_UserId]
GO
ALTER TABLE [dbo].[Dispatchers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Dispatchers_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Dispatchers] CHECK CONSTRAINT [FK_dbo.Dispatchers_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Feedbacks_dbo.Airports_AirportId] FOREIGN KEY([AirportId])
REFERENCES [dbo].[Airports] ([AirportId])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_dbo.Feedbacks_dbo.Airports_AirportId]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Feedbacks_dbo.Tours_TourId] FOREIGN KEY([TourId])
REFERENCES [dbo].[Tours] ([TourId])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_dbo.Feedbacks_dbo.Tours_TourId]
GO
ALTER TABLE [dbo].[Feedbacks]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Feedbacks_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Feedbacks] CHECK CONSTRAINT [FK_dbo.Feedbacks_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orders_dbo.Clients_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Clients] ([UserId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_dbo.Orders_dbo.Clients_UserId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orders_dbo.CreditCards_CreditCardId] FOREIGN KEY([CreditCardId])
REFERENCES [dbo].[CreditCards] ([CreditCardId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_dbo.Orders_dbo.CreditCards_CreditCardId]
GO
ALTER TABLE [dbo].[PassengerOrders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PassengerOrders_dbo.Orders_Order_OrderId] FOREIGN KEY([Order_OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PassengerOrders] CHECK CONSTRAINT [FK_dbo.PassengerOrders_dbo.Orders_Order_OrderId]
GO
ALTER TABLE [dbo].[PassengerOrders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PassengerOrders_dbo.Passengers_Passenger_PassengerId] FOREIGN KEY([Passenger_PassengerId])
REFERENCES [dbo].[Passengers] ([PassengerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PassengerOrders] CHECK CONSTRAINT [FK_dbo.PassengerOrders_dbo.Passengers_Passenger_PassengerId]
GO
ALTER TABLE [dbo].[PassengerTours]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PassengerTours_dbo.Passengers_Passenger_PassengerId] FOREIGN KEY([Passenger_PassengerId])
REFERENCES [dbo].[Passengers] ([PassengerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PassengerTours] CHECK CONSTRAINT [FK_dbo.PassengerTours_dbo.Passengers_Passenger_PassengerId]
GO
ALTER TABLE [dbo].[PassengerTours]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PassengerTours_dbo.Tours_Tour_TourId] FOREIGN KEY([Tour_TourId])
REFERENCES [dbo].[Tours] ([TourId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PassengerTours] CHECK CONSTRAINT [FK_dbo.PassengerTours_dbo.Tours_Tour_TourId]
GO
ALTER TABLE [dbo].[Tours]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tours_dbo.Airports_Airport_AirportId] FOREIGN KEY([Airport_AirportId])
REFERENCES [dbo].[Airports] ([AirportId])
GO
ALTER TABLE [dbo].[Tours] CHECK CONSTRAINT [FK_dbo.Tours_dbo.Airports_Airport_AirportId]
GO
ALTER TABLE [dbo].[Tours]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tours_dbo.Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tours] CHECK CONSTRAINT [FK_dbo.Tours_dbo.Orders_OrderId]
GO
ALTER TABLE [dbo].[Tours]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Tours_dbo.Planes_PlaneId] FOREIGN KEY([PlaneId])
REFERENCES [dbo].[Planes] ([PlaneId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tours] CHECK CONSTRAINT [FK_dbo.Tours_dbo.Planes_PlaneId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.Addresses_AddressId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.UserClaims_UserClaimId] FOREIGN KEY([UserClaimId])
REFERENCES [dbo].[UserClaims] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.UserClaims_UserClaimId]
GO
