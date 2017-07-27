Use DAMS_01
Go


CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);



CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);


CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);



CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);


CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);



Create table AdminAccount
(
     ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	 AdminID NVARCHAR(50) NOT NULL,
     Name NVARCHAR(50) NOT NULL,
     Email NVARCHAR(50) NOT NULL,
     SecurityLevel NVARCHAR(50) NOT NULL,
	 AspNetID NVARCHAR (128) FOREIGN KEY REFERENCES AspNetUsers(Id) NOT NULL
)

Create table ClinicHospital
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	ClinicHospitalID NVARCHAR(50) NOT NULL,
    ClinicHospitalName NVARCHAR(50) NOT NULL,
	ClinicHospitalAddress NVARCHAR(350) NOT NULL,
	ClinicHospitalOpenHours NVARCHAR(100) NOT NULL,
	ClinicHospitalTel NVARCHAR(50) NOT NULL,
	ClinicHospitalEmail NVARCHAR(100) NOT NULL,
	IsStringOpenHours bit NOT NULL
)

Create table DoctorDentist
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	DoctorDentistID NVARCHAR(50) NOT NULL,
    Name NVARCHAR(50) NOT NULL,
	MaxBookings INT NOT NULL,
	ClinicHospitalID INT FOREIGN KEY REFERENCES ClinicHospital(ID) NOT NULL
)

Create table DoctorDentistDateBooking
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
    DateOfBookings Date NOT NULL,
	Bookings INT NOT NULL,
	DoctorDentistID INT FOREIGN KEY REFERENCES DoctorDentist(ID) NOT NULL
)

Create table AdminAccountClinicHospital
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	ClinicHospitalID INT FOREIGN KEY REFERENCES ClinicHospital(ID) NOT NULL,
	AdminID INT FOREIGN KEY REFERENCES AdminAccount(ID) NOT NULL
)

Create table Bookings
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	BookingDate DateTime NOT NULL,
	IsFullyBooked bit NOT NULL,
	ClinicHospitalID INT FOREIGN KEY REFERENCES ClinicHospital(ID) NOT NULL
)

Create table OpeningHours
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	OpeningHoursDay INT NOT NULL,
	TimeRangeStart TIME NOT NULL,
	TimeRangeEnd TIME NOT NULL,
	ClinicHospitalID INT FOREIGN KEY REFERENCES ClinicHospital(ID) NOT NULL
)

Create table Advertisement
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	AdvImage VARBINARY NOT NULL,
	AdvDesc NVARCHAR(50) NOT NULL
)

Create table ClinicHospitalAdvertisement
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	AdvertisementID INT FOREIGN KEY REFERENCES Advertisement(ID) NOT NULL,
	ClinicHospitalID INT FOREIGN KEY REFERENCES ClinicHospital(ID) NOT NULL
)

Create table UserAccount
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	NRIC NVARCHAR(50) NOT NULL,
	Name NVARCHAR(50) NOT NULL,
	DOB NVARCHAR(50) NOT NULL,
	Gender NVARCHAR(50) NOT NULL,
	Mobile NVARCHAR(50) NOT NULL,
	Addrress NVARCHAR(50) NOT NULL,
	AspNetID NVARCHAR (128) FOREIGN KEY REFERENCES AspNetUsers(Id) NOT NULL,
	IsDeleted BIT NOT NULL
)

Create table Appointment
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	AppointmentID NVARCHAR(50) NOT NULL,
	UserID INT FOREIGN KEY REFERENCES UserAccount(ID) NOT NULL,
	ClinicHospitalID INT FOREIGN KEY REFERENCES ClinicHospital(ID) NOT NULL,
	ApprovalState INT NOT NULL,
	PreferredDate DATE NOT NULL,
	PreferredTime INT NOT NULL,
	DoctorDentistID INT FOREIGN KEY REFERENCES DoctorDentist(ID) NULL,
	RequestDoctorDentistID INT FOREIGN KEY REFERENCES DoctorDentist(ID) NULL,
	Remarks NVARCHAR(500) NOT NULL,
	AppointmentDate DATE NULL,
	AppointmentTime INT NULL
)

Create table Treatment
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	TreatmentID NVARCHAR(50) NOT NULL,
	TreatmentName NVARCHAR(50) NOT NULL,
	TreatmentDesc NVARCHAR(1000) NOT NULL,
	IsFollowUp BIT NOT NULL
)



Create table ClinicHospitalTreatment
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	TreatmentID INT FOREIGN KEY REFERENCES Treatment(ID) NOT NULL,
	ClinicHospitalID INT FOREIGN KEY REFERENCES ClinicHospital(ID) NOT NULL,
	PriceLow MONEY NOT NULL,
	PriceHigh MONEY NOT NULL
)

Create table AppointmentTreatment
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	AppointmentID INT FOREIGN KEY REFERENCES Appointment(ID) NOT NULL,
	TreatmentID INT FOREIGN KEY REFERENCES Treatment(ID) NOT NULL
)


INSERT INTO [dbo].[AspNetRoles]
Values (1, 'User');

INSERT INTO [dbo].[AspNetRoles] (Id, Name)
Values (2, 'SysAdmin')

INSERT INTO [dbo].[AspNetRoles]
Values (3, 'HospAdmin');

INSERT INTO [dbo].[AspNetRoles]
Values (4, 'ClerkAdmin');

INSERT INTO [dbo].[AspNetUsers]
Values ('38ddb20f-d142-437b-ab96-403a7c4949b6', 
'Default@Admin.com',
0,
'AGUn6XB/MLNcRM+kQOJNcdusPu6tTnOWzIQ7VP/4/S/eK0bRR5Sn82uUIcMk9Zjwwg==',
'932a25a4-8fe6-4aa3-a644-bc7d731b6211',
NULL,
0,
0,
NULL,
0,
0,
'Admin');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('38ddb20f-d142-437b-ab96-403a7c4949b6', 2);

INSERT INTO [dbo].[AdminAccount]
Values ('DefaultAdminID',
'DefaultAdminIDName',
'Default@Admin.com',
1,
'38ddb20f-d142-437b-ab96-403a7c4949b6');
