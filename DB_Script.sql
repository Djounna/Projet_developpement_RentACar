USE [master]
GO
/****** Object:  Database [ProjetSGDB]    Script Date: 17-04-22 19:23:07 ******/
CREATE DATABASE [ProjetSGDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjetSGDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLDB2022\MSSQL\DATA\ProjetSGDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjetSGDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLDB2022\MSSQL\DATA\ProjetSGDB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ProjetSGDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjetSGDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjetSGDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjetSGDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjetSGDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjetSGDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjetSGDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjetSGDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjetSGDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjetSGDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjetSGDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjetSGDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjetSGDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjetSGDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjetSGDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjetSGDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjetSGDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjetSGDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjetSGDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjetSGDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjetSGDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjetSGDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjetSGDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjetSGDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjetSGDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ProjetSGDB] SET  MULTI_USER 
GO
ALTER DATABASE [ProjetSGDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjetSGDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjetSGDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjetSGDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjetSGDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProjetSGDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProjetSGDB', N'ON'
GO
ALTER DATABASE [ProjetSGDB] SET QUERY_STORE = OFF
GO
USE [ProjetSGDB]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[IDClient] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](50) NOT NULL,
	[Prenom] [nvarchar](50) NOT NULL,
	[Mail] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[IDClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Depot]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Depot](
	[IDDepot] [int] IDENTITY(1,1) NOT NULL,
	[IDVille] [int] NOT NULL,
	[Inactif] [bit] NULL,
 CONSTRAINT [PK_Depot] PRIMARY KEY CLUSTERED 
(
	[IDDepot] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forfait]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forfait](
	[IDForfait] [int] IDENTITY(1,1) NOT NULL,
	[IDDepot_1] [int] NOT NULL,
	[IDDepot_2] [int] NOT NULL,
	[Prix] [decimal](8, 2) NOT NULL,
	[Date_Debut] [datetime] NOT NULL,
	[Date_Fin] [datetime] NULL,
 CONSTRAINT [PK_Forfait] PRIMARY KEY CLUSTERED 
(
	[IDForfait] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notoriete]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notoriete](
	[IDNotoriete] [int] IDENTITY(1,1) NOT NULL,
	[Libelle] [nvarchar](50) NOT NULL,
	[Coefficient_Multiplicateur] [decimal](4, 2) NOT NULL,
	[Inactif] [bit] NULL,
 CONSTRAINT [PK_Notoriete] PRIMARY KEY CLUSTERED 
(
	[IDNotoriete] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pays]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pays](
	[IDPays] [int] IDENTITY(1,1) NOT NULL,
	[Nom] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Pays] PRIMARY KEY CLUSTERED 
(
	[IDPays] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prix]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prix](
	[IDPrix] [int] IDENTITY(1,1) NOT NULL,
	[IDPays] [int] NOT NULL,
	[Date_Debut] [datetime] NOT NULL,
	[Date_Fin] [datetime] NULL,
	[Prix_Km] [decimal](8, 2) NOT NULL,
 CONSTRAINT [PK_Prix] PRIMARY KEY CLUSTERED 
(
	[IDPrix] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[IDReservation] [int] IDENTITY(1,1) NOT NULL,
	[IDVoiture] [int] NOT NULL,
	[IDClient] [int] NOT NULL,
	[IDDepotDepart] [int] NOT NULL,
	[IDDepotRetour] [int] NULL,
	[IDForfait] [int] NULL,
	[DateReservation] [datetime] NOT NULL,
	[DateDepart] [datetime] NOT NULL,
	[DateRetour] [datetime] NOT NULL,
	[Kilometrage_Depart] [int] NULL,
	[Kilometrage_Retour] [int] NULL,
	[Coefficient_Multiplicateur] [decimal](4, 2) NOT NULL,
	[Penalite] [bit] NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[IDReservation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ville]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ville](
	[IDVille] [int] IDENTITY(1,1) NOT NULL,
	[IDPays] [int] NOT NULL,
	[Nom] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Ville] PRIMARY KEY CLUSTERED 
(
	[IDVille] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voiture]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voiture](
	[IDVoiture] [int] IDENTITY(1,1) NOT NULL,
	[IDNotoriete] [int] NOT NULL,
	[IDDepot] [int] NOT NULL,
	[Immatriculation] [nchar](10) NOT NULL,
	[Marque] [nvarchar](50) NOT NULL,
	[Inactif] [bit] NULL,
 CONSTRAINT [PK_Voiture] PRIMARY KEY CLUSTERED 
(
	[IDVoiture] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1219, N'BOURGEOIS', N'Basile', N'Basile.BOURGEOIS@gmail.com')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1220, N'REY', N'Léon', N'Léon.REY@yahoo.fr')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1221, N'ROBIN', N'ade', N'ade.ROBIN@hotmail.com')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1222, N'MARCHAND', N'Elise', N'Elise.MARCHAND@hotmail.com')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1223, N'DUPONT', N'Noémie', N'Noémie.DUPONT@students.ephec.be')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1224, N'DUMONT', N'Noa', N'Noa.DUMONT@hotmail.com')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1225, N'MARTINEZ', N'Lila', N'Lila.MARTINEZ@students.ephec.be')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1226, N'FRANCOIS', N'Malone', N'Malone.FRANCOIS@students.ephec.be')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1227, N'BERNARD', N'Auguste', N'Auguste.BERNARD@outlook.com')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1228, N'OLIVIER', N'Maxence', N'Maxence.OLIVIER@yahoo.fr')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1229, N'LACROIX', N'Anas', N'Anas.LACROIX@hotmail.com')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1230, N'ROUX', N'Anaïs', N'Anaïs.ROUX@gmail.com')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1231, N'MARTIN', N'Héloïse', N'Héloïse.MARTIN@hotmail.com')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1232, N'ROLLAND', N'Zoé', N'Zoé.ROLLAND@gmail.com')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1233, N'ROY', N'mbre', N'mbre.ROY@hotmail.com')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1234, N'LEFEVRE', N'Charlie', N'Charlie.LEFEVRE@hotmail.com')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1235, N'RICHARD', N'Noa', N'Noa.RICHARD@yahoo.fr')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1236, N'RIVIERE', N'Noam', N'Noam.RIVIERE@students.ephec.be')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1237, N'LEFEBVRE', N'Lyana', N'Lyana.LEFEBVRE@yahoo.fr')
INSERT [dbo].[Client] ([IDClient], [Nom], [Prenom], [Mail]) VALUES (1238, N'GERARD', N'Maya', N'Maya.GERARD@outlook.com')
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Depot] ON 

INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (542, 1695, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (543, 1696, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (544, 1697, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (545, 1698, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (546, 1699, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (547, 1700, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (548, 1701, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (549, 1702, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (550, 1703, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (551, 1704, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (552, 1705, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (553, 1706, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (554, 1707, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (555, 1708, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (556, 1709, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (557, 1710, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (558, 1711, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (559, 1712, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (560, 1713, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (561, 1714, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (562, 1715, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (563, 1716, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (564, 1717, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (565, 1718, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (566, 1719, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (567, 1720, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (568, 1721, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (569, 1722, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (570, 1723, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (571, 1724, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (572, 1725, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (573, 1726, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (574, 1727, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (575, 1728, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (576, 1729, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (577, 1730, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (578, 1731, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (579, 1732, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (580, 1733, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (581, 1734, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (582, 1735, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (583, 1736, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (584, 1737, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (585, 1738, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (586, 1739, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (587, 1740, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (588, 1741, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (589, 1742, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (590, 1743, NULL)
INSERT [dbo].[Depot] ([IDDepot], [IDVille], [Inactif]) VALUES (591, 1744, NULL)
SET IDENTITY_INSERT [dbo].[Depot] OFF
GO
SET IDENTITY_INSERT [dbo].[Forfait] ON 

INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (201, 564, 571, CAST(330.00 AS Decimal(8, 2)), CAST(N'2017-12-13T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (202, 571, 551, CAST(392.00 AS Decimal(8, 2)), CAST(N'2016-01-20T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (203, 569, 567, CAST(831.00 AS Decimal(8, 2)), CAST(N'2018-01-17T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (204, 565, 581, CAST(129.00 AS Decimal(8, 2)), CAST(N'2017-08-21T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (205, 580, 551, CAST(829.00 AS Decimal(8, 2)), CAST(N'2012-11-06T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (206, 589, 544, CAST(676.00 AS Decimal(8, 2)), CAST(N'2012-08-11T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (207, 586, 551, CAST(392.00 AS Decimal(8, 2)), CAST(N'2018-06-28T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (208, 587, 588, CAST(425.00 AS Decimal(8, 2)), CAST(N'2014-09-10T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (209, 568, 562, CAST(502.00 AS Decimal(8, 2)), CAST(N'2022-02-16T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (210, 556, 560, CAST(194.00 AS Decimal(8, 2)), CAST(N'2013-04-06T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (211, 545, 587, CAST(424.00 AS Decimal(8, 2)), CAST(N'2015-08-03T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (212, 552, 565, CAST(254.00 AS Decimal(8, 2)), CAST(N'2014-04-15T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (213, 579, 551, CAST(966.00 AS Decimal(8, 2)), CAST(N'2012-08-10T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (214, 552, 577, CAST(599.00 AS Decimal(8, 2)), CAST(N'2016-03-29T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (215, 587, 566, CAST(419.00 AS Decimal(8, 2)), CAST(N'2011-06-15T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (216, 570, 579, CAST(604.00 AS Decimal(8, 2)), CAST(N'2021-05-07T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (217, 546, 556, CAST(634.00 AS Decimal(8, 2)), CAST(N'2022-08-17T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (218, 558, 570, CAST(470.00 AS Decimal(8, 2)), CAST(N'2018-04-02T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (219, 546, 581, CAST(463.00 AS Decimal(8, 2)), CAST(N'2010-04-13T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (220, 549, 556, CAST(423.00 AS Decimal(8, 2)), CAST(N'2012-08-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (221, 566, 542, CAST(621.00 AS Decimal(8, 2)), CAST(N'2011-01-25T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (222, 544, 544, CAST(852.00 AS Decimal(8, 2)), CAST(N'2013-01-27T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (223, 576, 555, CAST(371.00 AS Decimal(8, 2)), CAST(N'2010-09-09T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (224, 557, 575, CAST(620.00 AS Decimal(8, 2)), CAST(N'2015-11-11T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (225, 543, 549, CAST(242.00 AS Decimal(8, 2)), CAST(N'2017-08-28T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (226, 556, 548, CAST(177.00 AS Decimal(8, 2)), CAST(N'2022-11-27T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (227, 573, 554, CAST(444.00 AS Decimal(8, 2)), CAST(N'2014-07-10T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (228, 588, 582, CAST(348.00 AS Decimal(8, 2)), CAST(N'2011-07-27T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (229, 542, 560, CAST(444.00 AS Decimal(8, 2)), CAST(N'2018-03-09T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (230, 546, 584, CAST(344.00 AS Decimal(8, 2)), CAST(N'2021-02-20T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (231, 553, 562, CAST(285.00 AS Decimal(8, 2)), CAST(N'2015-06-28T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (232, 577, 578, CAST(320.00 AS Decimal(8, 2)), CAST(N'2011-09-06T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (233, 548, 575, CAST(967.00 AS Decimal(8, 2)), CAST(N'2017-04-09T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (234, 572, 578, CAST(800.00 AS Decimal(8, 2)), CAST(N'2021-05-14T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (235, 580, 571, CAST(638.00 AS Decimal(8, 2)), CAST(N'2011-10-20T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (236, 588, 589, CAST(119.00 AS Decimal(8, 2)), CAST(N'2016-08-02T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (237, 564, 585, CAST(374.00 AS Decimal(8, 2)), CAST(N'2018-04-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (238, 542, 542, CAST(754.00 AS Decimal(8, 2)), CAST(N'2018-04-23T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (239, 570, 575, CAST(947.00 AS Decimal(8, 2)), CAST(N'2016-08-11T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (240, 550, 559, CAST(475.00 AS Decimal(8, 2)), CAST(N'2017-03-23T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (241, 554, 573, CAST(891.00 AS Decimal(8, 2)), CAST(N'2016-06-04T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (242, 556, 563, CAST(410.00 AS Decimal(8, 2)), CAST(N'2015-03-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (243, 563, 589, CAST(897.00 AS Decimal(8, 2)), CAST(N'2013-12-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (244, 561, 580, CAST(450.00 AS Decimal(8, 2)), CAST(N'2010-03-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (245, 581, 546, CAST(830.00 AS Decimal(8, 2)), CAST(N'2017-08-13T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (246, 550, 582, CAST(770.00 AS Decimal(8, 2)), CAST(N'2013-09-18T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (247, 543, 546, CAST(314.00 AS Decimal(8, 2)), CAST(N'2010-08-05T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (248, 573, 562, CAST(565.00 AS Decimal(8, 2)), CAST(N'2016-05-12T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (249, 550, 590, CAST(730.00 AS Decimal(8, 2)), CAST(N'2011-02-05T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Forfait] ([IDForfait], [IDDepot_1], [IDDepot_2], [Prix], [Date_Debut], [Date_Fin]) VALUES (250, 542, 578, CAST(816.00 AS Decimal(8, 2)), CAST(N'2014-05-12T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Forfait] OFF
GO
SET IDENTITY_INSERT [dbo].[Notoriete] ON 

INSERT [dbo].[Notoriete] ([IDNotoriete], [Libelle], [Coefficient_Multiplicateur], [Inactif]) VALUES (1147, N'Luxe', CAST(2.90 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Notoriete] ([IDNotoriete], [Libelle], [Coefficient_Multiplicateur], [Inactif]) VALUES (1148, N'Poubelle', CAST(2.54 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Notoriete] ([IDNotoriete], [Libelle], [Coefficient_Multiplicateur], [Inactif]) VALUES (1149, N'SUV', CAST(2.11 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Notoriete] ([IDNotoriete], [Libelle], [Coefficient_Multiplicateur], [Inactif]) VALUES (1150, N'Familiale', CAST(2.42 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Notoriete] ([IDNotoriete], [Libelle], [Coefficient_Multiplicateur], [Inactif]) VALUES (1151, N'Citadine', CAST(2.54 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Notoriete] ([IDNotoriete], [Libelle], [Coefficient_Multiplicateur], [Inactif]) VALUES (1152, N'Campagnarde', CAST(2.53 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Notoriete] ([IDNotoriete], [Libelle], [Coefficient_Multiplicateur], [Inactif]) VALUES (1153, N'Roule-a-peine', CAST(2.11 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Notoriete] ([IDNotoriete], [Libelle], [Coefficient_Multiplicateur], [Inactif]) VALUES (1154, N'Sport', CAST(1.88 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Notoriete] ([IDNotoriete], [Libelle], [Coefficient_Multiplicateur], [Inactif]) VALUES (1155, N'Tank', CAST(1.76 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Notoriete] ([IDNotoriete], [Libelle], [Coefficient_Multiplicateur], [Inactif]) VALUES (1156, N'Fullspeed', CAST(1.15 AS Decimal(4, 2)), NULL)
SET IDENTITY_INSERT [dbo].[Notoriete] OFF
GO
SET IDENTITY_INSERT [dbo].[Pays] ON 

INSERT [dbo].[Pays] ([IDPays], [Nom]) VALUES (1752, N'Allemagne')
INSERT [dbo].[Pays] ([IDPays], [Nom]) VALUES (1750, N'Belgique ')
INSERT [dbo].[Pays] ([IDPays], [Nom]) VALUES (1749, N'Espagne')
INSERT [dbo].[Pays] ([IDPays], [Nom]) VALUES (1751, N'France')
INSERT [dbo].[Pays] ([IDPays], [Nom]) VALUES (1748, N'Italie')
SET IDENTITY_INSERT [dbo].[Pays] OFF
GO
SET IDENTITY_INSERT [dbo].[Prix] ON 

INSERT [dbo].[Prix] ([IDPrix], [IDPays], [Date_Debut], [Date_Fin], [Prix_Km]) VALUES (1630, 1748, CAST(N'2022-04-17T19:17:26.917' AS DateTime), NULL, CAST(3.47 AS Decimal(8, 2)))
INSERT [dbo].[Prix] ([IDPrix], [IDPays], [Date_Debut], [Date_Fin], [Prix_Km]) VALUES (1631, 1749, CAST(N'2022-04-17T19:17:26.950' AS DateTime), NULL, CAST(1.95 AS Decimal(8, 2)))
INSERT [dbo].[Prix] ([IDPrix], [IDPays], [Date_Debut], [Date_Fin], [Prix_Km]) VALUES (1632, 1750, CAST(N'2022-04-17T19:17:26.953' AS DateTime), NULL, CAST(4.80 AS Decimal(8, 2)))
INSERT [dbo].[Prix] ([IDPrix], [IDPays], [Date_Debut], [Date_Fin], [Prix_Km]) VALUES (1633, 1751, CAST(N'2022-04-17T19:17:26.957' AS DateTime), NULL, CAST(3.47 AS Decimal(8, 2)))
INSERT [dbo].[Prix] ([IDPrix], [IDPays], [Date_Debut], [Date_Fin], [Prix_Km]) VALUES (1634, 1752, CAST(N'2022-04-17T19:17:26.960' AS DateTime), NULL, CAST(3.70 AS Decimal(8, 2)))
SET IDENTITY_INSERT [dbo].[Prix] OFF
GO
SET IDENTITY_INSERT [dbo].[Reservation] ON 

INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1527, 9116, 1235, 586, 591, NULL, CAST(N'2021-07-02T00:00:00.000' AS DateTime), CAST(N'2021-08-02T00:00:00.000' AS DateTime), CAST(N'2021-09-02T00:00:00.000' AS DateTime), 46516, 47822, CAST(1.15 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1529, 9114, 1236, 546, 584, 230, CAST(N'2022-07-08T00:00:00.000' AS DateTime), CAST(N'2022-08-08T00:00:00.000' AS DateTime), CAST(N'2022-09-08T00:00:00.000' AS DateTime), 12457, 12486, CAST(2.53 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1531, 9072, 1234, 543, 548, NULL, CAST(N'2021-10-11T00:00:00.000' AS DateTime), CAST(N'2021-11-11T00:00:00.000' AS DateTime), CAST(N'2021-12-11T00:00:00.000' AS DateTime), 38014, 39695, CAST(1.15 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1532, 9065, 1233, 571, 557, NULL, CAST(N'2021-07-01T00:00:00.000' AS DateTime), CAST(N'2021-08-01T00:00:00.000' AS DateTime), CAST(N'2021-09-01T00:00:00.000' AS DateTime), 53475, NULL, CAST(2.11 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1533, 9088, 1230, 548, 575, 233, CAST(N'2023-06-09T00:00:00.000' AS DateTime), CAST(N'2023-07-09T00:00:00.000' AS DateTime), CAST(N'2023-08-09T00:00:00.000' AS DateTime), 21450, NULL, CAST(2.42 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1534, 9110, 1234, 577, 586, NULL, CAST(N'2022-01-08T00:00:00.000' AS DateTime), CAST(N'2022-02-08T00:00:00.000' AS DateTime), CAST(N'2022-03-08T00:00:00.000' AS DateTime), 56896, 58152, CAST(2.11 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1535, 9045, 1219, 556, 548, 226, CAST(N'2021-11-15T00:00:00.000' AS DateTime), CAST(N'2021-12-15T00:00:00.000' AS DateTime), CAST(N'2021-12-16T00:00:00.000' AS DateTime), 7492, 7643, CAST(1.76 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1536, 9095, 1224, 564, 571, 201, CAST(N'2022-01-07T00:00:00.000' AS DateTime), CAST(N'2022-01-08T00:00:00.000' AS DateTime), CAST(N'2022-01-09T00:00:00.000' AS DateTime), 18242, 18456, CAST(1.15 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1537, 9050, 1236, 569, 567, 203, CAST(N'2022-09-11T00:00:00.000' AS DateTime), CAST(N'2022-10-11T00:00:00.000' AS DateTime), CAST(N'2022-11-11T00:00:00.000' AS DateTime), 21038, 21592, CAST(1.15 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1538, 9031, 1219, 577, 572, NULL, CAST(N'2023-01-25T00:00:00.000' AS DateTime), CAST(N'2023-01-26T00:00:00.000' AS DateTime), CAST(N'2023-01-27T00:00:00.000' AS DateTime), 49100, NULL, CAST(2.54 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1541, 9063, 1220, 556, 563, NULL, CAST(N'2022-01-14T00:00:00.000' AS DateTime), CAST(N'2022-02-14T00:00:00.000' AS DateTime), CAST(N'2022-03-14T00:00:00.000' AS DateTime), 15861, 17020, CAST(2.90 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1542, 9024, 1224, 564, 560, NULL, CAST(N'2023-10-04T00:00:00.000' AS DateTime), CAST(N'2023-11-04T00:00:00.000' AS DateTime), CAST(N'2023-12-04T00:00:00.000' AS DateTime), 24448, 25340, CAST(2.54 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1543, 9045, 1224, 543, 581, NULL, CAST(N'2022-01-10T00:00:00.000' AS DateTime), CAST(N'2022-01-11T00:00:00.000' AS DateTime), CAST(N'2022-01-12T00:00:00.000' AS DateTime), 24658, 25264, CAST(1.76 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1544, 9116, 1225, 579, 545, NULL, CAST(N'2022-01-20T00:00:00.000' AS DateTime), CAST(N'2022-02-20T00:00:00.000' AS DateTime), CAST(N'2022-03-20T00:00:00.000' AS DateTime), 24581, 25218, CAST(1.15 AS Decimal(4, 2)), NULL)
INSERT [dbo].[Reservation] ([IDReservation], [IDVoiture], [IDClient], [IDDepotDepart], [IDDepotRetour], [IDForfait], [DateReservation], [DateDepart], [DateRetour], [Kilometrage_Depart], [Kilometrage_Retour], [Coefficient_Multiplicateur], [Penalite]) VALUES (1545, 9101, 1226, 552, 565, 212, CAST(N'2023-10-24T00:00:00.000' AS DateTime), CAST(N'2023-11-24T00:00:00.000' AS DateTime), CAST(N'2023-12-24T00:00:00.000' AS DateTime), 6016, 6810, CAST(1.76 AS Decimal(4, 2)), 1)
SET IDENTITY_INSERT [dbo].[Reservation] OFF
GO
SET IDENTITY_INSERT [dbo].[Ville] ON 

INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1716, 1748, N'Bari')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1718, 1748, N'Bologne')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1715, 1748, N'Catane')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1717, 1748, N'Florence')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1719, 1748, N'Gênes')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1723, 1748, N'Milan')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1722, 1748, N'Naples')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1720, 1748, N'Palerme')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1724, 1748, N'Rome')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1721, 1748, N'Turin')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1743, 1749, N'Barcelone')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1740, 1749, N'Bilbao')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1737, 1749, N'Las Palmas de Gran Canaria')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1744, 1749, N'Madrid')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1739, 1749, N'Malaga')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1736, 1749, N'Murcie')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1735, 1749, N'Palma de Majorque')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1738, 1749, N'Saragosse')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1742, 1749, N'Séville')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1741, 1749, N'Valence')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1698, 1750, N'Anderlecht')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1697, 1750, N'Bruges')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1700, 1750, N'Bruxelles')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1702, 1750, N'Charleroi')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1703, 1750, N'Gand')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1701, 1750, N'Liège')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1695, 1750, N'Louvain')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1704, 1750, N'Mons')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1696, 1750, N'Namur')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1699, 1750, N'Schaerbeek')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1706, 1751, N'Bordeaux')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1705, 1751, N'Lille')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1712, 1751, N'Lyon')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1713, 1751, N'Marseille')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1708, 1751, N'Montpellier')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1709, 1751, N'Nantes')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1710, 1751, N'Nice')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1714, 1751, N'Paris')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1707, 1751, N'Strasbourg')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1711, 1751, N'Toulouse')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1734, 1752, N'Berlin')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1731, 1752, N'Cologne')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1727, 1752, N'Dortmund')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1728, 1752, N'Düsseldorf')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1726, 1752, N'Essen')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1730, 1752, N'Francfort-sur-le-Main')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1733, 1752, N'Hambourg')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1725, 1752, N'Leipzig')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1732, 1752, N'Munich')
INSERT [dbo].[Ville] ([IDVille], [IDPays], [Nom]) VALUES (1729, 1752, N'Stuttgart')
SET IDENTITY_INSERT [dbo].[Ville] OFF
GO
SET IDENTITY_INSERT [dbo].[Voiture] ON 

INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9020, 1150, 546, N'2-OUA-653 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9021, 1149, 577, N'2-MJE-637 ', N'Audi', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9022, 1150, 570, N'2-QJC-132 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9023, 1148, 569, N'2-OMK-673 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9024, 1148, 550, N'1-BUL-306 ', N'Renault', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9025, 1151, 561, N'2-GAV-103 ', N'Skoda', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9026, 1147, 552, N'2-SYE-502 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9027, 1153, 578, N'2-MXR-540 ', N'Volvo', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9028, 1155, 562, N'2-QHY-707 ', N'MINI', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9029, 1149, 553, N'1-KMG-192 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9030, 1148, 545, N'2-PRC-594 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9031, 1151, 581, N'1-HXO-347 ', N'MINI', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9032, 1151, 575, N'1-YJM-181 ', N'Peugeot', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9033, 1153, 577, N'1-YJK-432 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9034, 1152, 586, N'2-TFN-171 ', N'Renault', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9035, 1152, 570, N'1-RAG-253 ', N'BMW', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9036, 1152, 582, N'2-TSO-776 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9037, 1147, 574, N'1-KWO-280 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9038, 1152, 578, N'2-WBA-118 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9039, 1155, 584, N'1-UXD-994 ', N'BMW', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9040, 1152, 554, N'2-PAG-605 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9041, 1147, 556, N'1-RXC-717 ', N'Renault', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9042, 1148, 557, N'2-MHN-366 ', N'BMW', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9043, 1149, 575, N'1-JIV-734 ', N'MINI', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9044, 1147, 567, N'2-BUH-619 ', N'BMW', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9045, 1155, 569, N'2-TUE-956 ', N'Renault', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9046, 1148, 563, N'1-OKW-974 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9047, 1151, 586, N'2-KSY-366 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9048, 1149, 578, N'1-YIL-362 ', N'Skoda', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9049, 1150, 546, N'1-VOG-145 ', N'Audi', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9050, 1156, 576, N'2-VAP-557 ', N'Renault', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9051, 1150, 559, N'2-YTS-230 ', N'Volvo', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9052, 1154, 550, N'2-LEX-348 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9053, 1154, 581, N'1-GNB-894 ', N'Peugeot', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9054, 1154, 576, N'1-RCP-915 ', N'BMW', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9055, 1149, 583, N'1-YFE-372 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9056, 1148, 590, N'2-LAJ-875 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9057, 1156, 581, N'2-KKN-981 ', N'Renault', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9058, 1147, 550, N'1-DNE-713 ', N'Peugeot', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9059, 1154, 558, N'1-IRD-238 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9060, 1147, 550, N'2-OLU-727 ', N'Volvo', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9061, 1156, 549, N'1-UAL-185 ', N'MINI', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9062, 1147, 565, N'1-XPN-320 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9063, 1147, 586, N'1-QUA-276 ', N'Renault', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9064, 1147, 582, N'1-SMG-676 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9065, 1153, 545, N'1-ECX-565 ', N'BMW', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9066, 1152, 569, N'2-ARW-748 ', N'Peugeot', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9067, 1155, 553, N'2-LXU-971 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9068, 1153, 562, N'1-CZO-942 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9069, 1151, 580, N'1-FBM-784 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9070, 1154, 542, N'1-ATX-471 ', N'Audi', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9071, 1151, 546, N'2-RUY-172 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9072, 1156, 542, N'1-LMZ-450 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9073, 1148, 584, N'2-DIE-689 ', N'Peugeot', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9074, 1148, 545, N'2-TGN-236 ', N'Volvo', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9075, 1147, 542, N'2-XIC-996 ', N'Audi', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9076, 1154, 548, N'1-GBO-494 ', N'Volvo', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9077, 1148, 582, N'1-EZD-650 ', N'Audi', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9078, 1156, 566, N'1-SPC-642 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9079, 1150, 557, N'1-VWL-892 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9080, 1154, 548, N'2-NMN-784 ', N'Audi', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9081, 1153, 576, N'1-JAG-217 ', N'MINI', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9082, 1156, 546, N'2-HYJ-673 ', N'Peugeot', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9083, 1149, 575, N'2-ZJT-816 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9084, 1154, 556, N'2-ANQ-651 ', N'MINI', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9085, 1155, 578, N'1-OUU-605 ', N'Audi', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9086, 1153, 566, N'2-OGK-209 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9087, 1150, 566, N'2-QJB-748 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9088, 1150, 575, N'2-YPT-901 ', N'Renault', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9089, 1149, 556, N'1-XMA-208 ', N'BMW', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9090, 1150, 569, N'1-UHO-803 ', N'BMW', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9091, 1155, 555, N'2-KNT-599 ', N'Skoda', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9092, 1148, 562, N'2-AEX-725 ', N'BMW', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9093, 1147, 561, N'1-EQY-178 ', N'Volvo', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9094, 1150, 564, N'1-WWP-315 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9095, 1156, 575, N'1-YBI-251 ', N'BMW', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9096, 1154, 574, N'1-MDM-483 ', N'BMW', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9097, 1148, 560, N'1-CXS-955 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9098, 1154, 570, N'2-DUF-563 ', N'Audi', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9099, 1149, 552, N'2-SET-201 ', N'MINI', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9100, 1156, 564, N'1-VOE-121 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9101, 1155, 590, N'2-FZB-427 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9102, 1147, 579, N'1-NKB-355 ', N'Volvo', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9103, 1153, 590, N'2-CJU-822 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9104, 1153, 565, N'1-PQK-723 ', N'Renault', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9105, 1151, 566, N'2-AWV-357 ', N'Skoda', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9106, 1155, 583, N'2-EAM-990 ', N'MINI', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9107, 1154, 569, N'2-VSI-640 ', N'Volvo', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9108, 1155, 578, N'2-LNC-836 ', N'Audi', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9109, 1151, 590, N'1-ZOM-877 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9110, 1149, 571, N'1-COI-301 ', N'MINI', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9111, 1153, 574, N'2-AJF-825 ', N'Renault', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9112, 1151, 562, N'2-IJV-654 ', N'Ford', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9113, 1148, 545, N'1-KEG-192 ', N'Skoda', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9114, 1152, 561, N'2-YQU-568 ', N'Audi', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9115, 1149, 563, N'1-ESL-675 ', N'Volkswagen', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9116, 1156, 560, N'1-YIA-282 ', N'Renault', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9117, 1153, 572, N'1-LTZ-340 ', N'Fiat', NULL)
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9118, 1154, 589, N'2-IUP-203 ', N'MINI', NULL)
GO
INSERT [dbo].[Voiture] ([IDVoiture], [IDNotoriete], [IDDepot], [Immatriculation], [Marque], [Inactif]) VALUES (9119, 1147, 546, N'2-EIM-825 ', N'BMW', NULL)
SET IDENTITY_INSERT [dbo].[Voiture] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Mail]    Script Date: 17-04-22 19:23:08 ******/
ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [UK_Mail] UNIQUE NONCLUSTERED 
(
	[Mail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UK_Prix_DateDebut_DateFin]    Script Date: 17-04-22 19:23:08 ******/
ALTER TABLE [dbo].[Forfait] ADD  CONSTRAINT [UK_Prix_DateDebut_DateFin] UNIQUE NONCLUSTERED 
(
	[Prix] ASC,
	[Date_Debut] ASC,
	[Date_Fin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Libelle]    Script Date: 17-04-22 19:23:08 ******/
ALTER TABLE [dbo].[Notoriete] ADD  CONSTRAINT [UK_Libelle] UNIQUE NONCLUSTERED 
(
	[Libelle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Nom]    Script Date: 17-04-22 19:23:08 ******/
ALTER TABLE [dbo].[Pays] ADD  CONSTRAINT [UK_Nom] UNIQUE NONCLUSTERED 
(
	[Nom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_IDPays_Nom]    Script Date: 17-04-22 19:23:08 ******/
ALTER TABLE [dbo].[Ville] ADD  CONSTRAINT [UK_IDPays_Nom] UNIQUE NONCLUSTERED 
(
	[IDPays] ASC,
	[Nom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Immatriculation]    Script Date: 17-04-22 19:23:08 ******/
ALTER TABLE [dbo].[Voiture] ADD  CONSTRAINT [UK_Immatriculation] UNIQUE NONCLUSTERED 
(
	[Immatriculation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Depot]  WITH CHECK ADD  CONSTRAINT [FK_Depot_Ville] FOREIGN KEY([IDVille])
REFERENCES [dbo].[Ville] ([IDVille])
GO
ALTER TABLE [dbo].[Depot] CHECK CONSTRAINT [FK_Depot_Ville]
GO
ALTER TABLE [dbo].[Forfait]  WITH CHECK ADD  CONSTRAINT [FK_Forfait_Depot_1] FOREIGN KEY([IDDepot_1])
REFERENCES [dbo].[Depot] ([IDDepot])
GO
ALTER TABLE [dbo].[Forfait] CHECK CONSTRAINT [FK_Forfait_Depot_1]
GO
ALTER TABLE [dbo].[Forfait]  WITH CHECK ADD  CONSTRAINT [FK_Forfait_Depot_2] FOREIGN KEY([IDDepot_2])
REFERENCES [dbo].[Depot] ([IDDepot])
GO
ALTER TABLE [dbo].[Forfait] CHECK CONSTRAINT [FK_Forfait_Depot_2]
GO
ALTER TABLE [dbo].[Prix]  WITH CHECK ADD  CONSTRAINT [FK_Prix_Pays] FOREIGN KEY([IDPays])
REFERENCES [dbo].[Pays] ([IDPays])
GO
ALTER TABLE [dbo].[Prix] CHECK CONSTRAINT [FK_Prix_Pays]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Client] FOREIGN KEY([IDClient])
REFERENCES [dbo].[Client] ([IDClient])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Client]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_DepotD] FOREIGN KEY([IDDepotDepart])
REFERENCES [dbo].[Depot] ([IDDepot])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_DepotD]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_DepotR] FOREIGN KEY([IDDepotRetour])
REFERENCES [dbo].[Depot] ([IDDepot])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_DepotR]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Forfait] FOREIGN KEY([IDForfait])
REFERENCES [dbo].[Forfait] ([IDForfait])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Forfait]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Voiture] FOREIGN KEY([IDVoiture])
REFERENCES [dbo].[Voiture] ([IDVoiture])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Voiture]
GO
ALTER TABLE [dbo].[Ville]  WITH CHECK ADD  CONSTRAINT [FK_Pays_Ville] FOREIGN KEY([IDPays])
REFERENCES [dbo].[Pays] ([IDPays])
GO
ALTER TABLE [dbo].[Ville] CHECK CONSTRAINT [FK_Pays_Ville]
GO
ALTER TABLE [dbo].[Voiture]  WITH CHECK ADD  CONSTRAINT [FK_Voiture_Depot] FOREIGN KEY([IDDepot])
REFERENCES [dbo].[Depot] ([IDDepot])
GO
ALTER TABLE [dbo].[Voiture] CHECK CONSTRAINT [FK_Voiture_Depot]
GO
ALTER TABLE [dbo].[Voiture]  WITH CHECK ADD  CONSTRAINT [FK_Voiture_Notoriete] FOREIGN KEY([IDNotoriete])
REFERENCES [dbo].[Notoriete] ([IDNotoriete])
GO
ALTER TABLE [dbo].[Voiture] CHECK CONSTRAINT [FK_Voiture_Notoriete]
GO
/****** Object:  StoredProcedure [dbo].[CloseReservationWithForfait]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CloseReservationWithForfait] @IdReservation int,  @KilometrageRetour int, @IDDepotRetour int, @DateRetour Datetime,@IdForfait int, @Penalite bit

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Update Reservation
	Set  Kilometrage_Retour=@KilometrageRetour, IDDepotRetour=@IDDepotRetour, DateRetour=@DateRetour,IDForfait=@IdForfait, Penalite=@Penalite
	Where IDReservation=@IdReservation;

END
GO
/****** Object:  StoredProcedure [dbo].[CloseReservationWithoutForfait]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CloseReservationWithoutForfait] @IdReservation int,  @KilometrageRetour int, @IDDepotRetour int, @DateRetour Datetime, @Penalite bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Update Reservation
	Set  Kilometrage_Retour=@KilometrageRetour, IDDepotRetour=@IDDepotRetour, DateRetour=@DateRetour, Penalite=@Penalite
	Where IDReservation=@IdReservation;

END
GO
/****** Object:  StoredProcedure [dbo].[InsertForfait]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertForfait] @IdDep1 int ,@IdDep2 int, @prix decimal,@DateDeb Datetime

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    insert into Forfait (IDDepot_1,IDDepot_2,Prix,Date_Debut)values (@IdDep1,@IdDep2, @prix,@DateDeb) 
END
GO
/****** Object:  StoredProcedure [dbo].[InsertReservation]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[InsertReservation]    Script Date: 12-04-22 18:09:03 ******/


CREATE PROCEDURE [dbo].[InsertReservation] @IdClient int ,@IdVoiture int,@IdDepotDepart int, @IdDepotRetour int, @IdForfait int, @DateReservation Datetime,@DateDepart Datetime, @DateRetour Datetime, @Coefficient_Multiplicateur int



AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;



if(@IdDepotRetour = 0) set @IdDepotRetour = null;
if(@IdForfait = 0) set @IdForfait = null;
if(Convert(nvarchar,@DateRetour) = '01/01/1900 00:00:00') set @DateRetour = null;



insert into Reservation (IDVoiture, IDClient, IDDepotDepart, IDDepotRetour, IDForfait, DateReservation, DateDepart,DateRetour, Coefficient_Multiplicateur)values (@IdVoiture, @IdClient, @IdDepotDepart, @IdDepotRetour, @IdForfait, @DateReservation,@DateDepart, @DateRetour, @Coefficient_Multiplicateur) ;
END
GO
/****** Object:  StoredProcedure [dbo].[InsertReservationWithForfait]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



Create PROCEDURE [dbo].[InsertReservationWithForfait] @IdClient int ,@IdVoiture int,@IdDepotDepart int, @IdDepotRetour int, @IdForfait int, @DateReservation Datetime,@DateDepart Datetime, @DateRetour Datetime, @Coefficient_Multiplicateur int



AS
BEGIN

SET NOCOUNT ON;

insert into Reservation (IDVoiture, IDClient, IDDepotDepart, IDDepotRetour, IDForfait, DateReservation, DateDepart,DateRetour, Coefficient_Multiplicateur)values (@IdVoiture, @IdClient, @IdDepotDepart, @IdDepotRetour, @IdForfait, @DateReservation,@DateDepart, @DateRetour, @Coefficient_Multiplicateur) ;
END
GO
/****** Object:  StoredProcedure [dbo].[InsertReservationWithoutForfait]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



Create PROCEDURE [dbo].[InsertReservationWithoutForfait] @IdClient int ,@IdVoiture int,@IdDepotDepart int, @DateReservation Datetime,@DateDepart Datetime, @DateRetour Datetime, @Coefficient_Multiplicateur int



AS
BEGIN

SET NOCOUNT ON;

insert into Reservation (IDVoiture, IDClient, IDDepotDepart, DateReservation, DateDepart,DateRetour, Coefficient_Multiplicateur)values (@IdVoiture, @IdClient, @IdDepotDepart, @DateReservation,@DateDepart, @DateRetour, @Coefficient_Multiplicateur) ;
END
GO
/****** Object:  StoredProcedure [dbo].[StartReservation]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[StartReservation] @IdReservation int, @DateDepart datetime, @KilometrageDepart int



AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;
Update Reservation
Set Kilometrage_Depart=@KilometrageDepart, DateDepart=@DateDepart
Where IDReservation=@IdReservation;



END
GO
/****** Object:  StoredProcedure [dbo].[UpdateForfait]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateForfait] @dep1 int,@dep2 int,@prix Decimal,@dateDeb Datetime,@DateFin Datetime,@id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	UPDATE Forfait SET IDDepot_1=@dep1,IDDepot_2=@dep2,Prix=@prix,Date_Debut=@dateDeb,Date_Fin=@dateFin
	where IDForfait=@id
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateReservation]    Script Date: 17-04-22 19:23:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateReservation] @IdReservation int, @IdDepotRetour int, @DateRetour Datetime, @KilometrageDepart int, @KilometrageRetour int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Update Reservation
	Set IDDepotRetour=@IdDepotRetour, DateRetour= @DateRetour, Kilometrage_Depart=@KilometrageDepart, Kilometrage_Retour=@KilometrageRetour
	Where IDReservation=@IdReservation;

END
GO
USE [master]
GO
ALTER DATABASE [ProjetSGDB] SET  READ_WRITE 
GO
