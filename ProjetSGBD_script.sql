USE [master]
GO
/****** Object:  Database [ProjetSGDB]    Script Date: 09/04/2022 10:58:59 ******/
CREATE DATABASE [ProjetSGDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjetSGDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ProjetSGDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjetSGDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ProjetSGDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[Client]    Script Date: 09/04/2022 10:58:59 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Mail] UNIQUE NONCLUSTERED 
(
	[Mail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Depot]    Script Date: 09/04/2022 10:58:59 ******/
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
/****** Object:  Table [dbo].[Forfait]    Script Date: 09/04/2022 10:58:59 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Prix_DateDebut_DateFin] UNIQUE NONCLUSTERED 
(
	[Prix] ASC,
	[Date_Debut] ASC,
	[Date_Fin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notoriete]    Script Date: 09/04/2022 10:58:59 ******/
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
/****** Object:  Table [dbo].[Pays]    Script Date: 09/04/2022 10:58:59 ******/
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
/****** Object:  Table [dbo].[Prix]    Script Date: 09/04/2022 10:58:59 ******/
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
/****** Object:  Table [dbo].[Reservation]    Script Date: 09/04/2022 10:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[IDReservation] [int] IDENTITY(1,1) NOT NULL,
	[IDVoiture] [int] NOT NULL,
	[IDClient] [int] NOT NULL,
	[IDDepotDepart] [int] NOT NULL,
	[IDDepotRetour] [int] NOT NULL,
	[IDForfait] [int] NULL,
	[DateReservation] [datetime] NOT NULL,
	[DateDepart] [datetime] NOT NULL,
	[DateRetour] [datetime] NULL,
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
/****** Object:  Table [dbo].[Ville]    Script Date: 09/04/2022 10:58:59 ******/
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
/****** Object:  Table [dbo].[Voiture]    Script Date: 09/04/2022 10:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voiture](
	[IDVoiture] [int] IDENTITY(1,1) NOT NULL,
	[IDNotoriete] [int] NOT NULL,
	[IDDepot] [int] NOT NULL,
	[Immatriculation] [nchar](10) NOT NULL,
	[Marque] [nvarchar](50) NULL,
	[Inactif] [bit] NULL,
 CONSTRAINT [PK_Voiture] PRIMARY KEY CLUSTERED 
(
	[IDVoiture] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Immatriculation] UNIQUE NONCLUSTERED 
(
	[Immatriculation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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
/****** Object:  StoredProcedure [dbo].[CloseReservation]    Script Date: 09/04/2022 10:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CloseReservation] @IdReservation int,  @KilometrageRetour int, @IDDepotRetour int, @DateRetour Datetime,@IdForfait int, @Penalite bit

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
/****** Object:  StoredProcedure [dbo].[InsertForfait]    Script Date: 09/04/2022 10:58:59 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertReservation]    Script Date: 09/04/2022 10:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertReservation] @IdClient int ,@IdVoiture int,@IdDepotDepart int, @IdDepotRetour int, @IdForfait int, @DateReservation Datetime,@DateDepart Datetime, @Coefficient_Multiplicateur int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    insert into Reservation (IDVoiture, IDClient, IDDepotDepart, IDDepotRetour, IDForfait, DateReservation, DateDepart, Coefficient_Multiplicateur)values (@IdVoiture, @IdClient, @IdDepotDepart, @IdDepotRetour, @IdForfait, @DateReservation,@DateDepart, @Coefficient_Multiplicateur) ;
END
GO
/****** Object:  StoredProcedure [dbo].[StartReservation]    Script Date: 09/04/2022 10:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[StartReservation] @IdReservation int,  @KilometrageDepart int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Update Reservation
	Set   Kilometrage_Depart=@KilometrageDepart
	Where IDReservation=@IdReservation;

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateForfait]    Script Date: 09/04/2022 10:58:59 ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateReservation]    Script Date: 09/04/2022 10:58:59 ******/
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
