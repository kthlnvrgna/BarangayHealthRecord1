CREATE DATABASE PatientData
GO

USE [PatientData]
GO 

CREATE TABLE [dbo].[tbPatientMaster](
	[PatientID] [varchar] (10) NOT NULL,
	[FirstName] [varchar](250) NULL,
	[MiddleName] [varchar](250) NULL,
	[Lastname] [varchar](250) NULL,
	[Sex] [varchar](10) NULL,
	[BirthDate] [datetime] NULL,
	[Address] [varchar](500) NULL,
	[CivilStatus] [varchar](100) NULL,
	[Nationality] [varchar](100) NULL,
	[Religion] [varchar](100) NULL,
	[Category] [varchar](100) NULL,
	PRIMARY KEY (PatientID)
) ON [PRIMARY] 
GO 

CREATE TABLE [dbo].[tbPatientRegistration](
	[PatientID] [varchar](10) NOT NULL,
	[RegNum] [varchar](10) NOT NULL,
	[RegDate] [datetime] NULL,
	[DcrDate] [datetime] NULL
	PRIMARY KEY (RegNum)
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[tbRegistrationDetails](
	[PatientID] [varchar](10) NOT NULL,
	[RegNum] [varchar](10) NOT NULL,
	[FamilyRecord] [varchar](max) NULL,
	[Medicines] [varchar](max) NULL,
	[Allergies] [varchar](250) NULL,
	[ChiefComplaint] [varchar](max) NULL,
	[Consultation] [varchar](max) NULL,
	[Treatment] [varchar](max) NULL,
	[Diagnosis] [varchar](max) NULL
	PRIMARY KEY (RegNum)
)  
GO 

CREATE TABLE Logs(
ID INTEGER IDENTITY(1,1),
Remarks VARCHAR(MAX) NOT NULL,
RemarksDate DATETIME NOT NULL,
UserID VARCHAR(5) NOT NULL
PRIMARY KEY(ID)
)
GO 
