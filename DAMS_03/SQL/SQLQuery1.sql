Use DAMS_01
Go

Create table AdminAccount
(
     ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	 AdminID NVARCHAR(50) NOT NULL,
     Name NVARCHAR(50) NOT NULL,
     Email NVARCHAR(50) NOT NULL,
     SecurityLevel NVARCHAR(50) NOT NULL
)

Create table DoctorDentist
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	DoctorDentistID NVARCHAR(50) NOT NULL,
    Name NVARCHAR(50) NOT NULL
)

Create table AdminAccountDoctorDentist
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	DoctorDentistID INT FOREIGN KEY REFERENCES DoctorDentist(ID) NOT NULL,
	AdminID INT FOREIGN KEY REFERENCES AdminAccount(ID) NOT NULL
)

Create table ClinicHospital
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	ClinicHospitalID NVARCHAR(50) NOT NULL,
    ClinicHospitalName NVARCHAR(50) NOT NULL,
	ClinicHospitalAddress NVARCHAR(50) NOT NULL,
	ClinicHospitalOpenHours NVARCHAR(50) NOT NULL,
	ClinicHospitalTel NVARCHAR(50) NOT NULL,
	ClinicHospitalEmail NVARCHAR(50) NOT NULL,
	MaxBookings NVARCHAR(50) NOT NULL
)

Create table ClinicHospitalDoctorDentist
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	DoctorDentistID INT FOREIGN KEY REFERENCES DoctorDentist(ID) NOT NULL,
	ClinicHospitalID INT FOREIGN KEY REFERENCES ClinicHospital(ID) NOT NULL
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
	TimeRangeENd TIME NOT NULL
)

Create table ClinicHospitalOpeningHours
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	OpeningHoursID INT FOREIGN KEY REFERENCES OpeningHours(ID) NOT NULL,
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
	Passwordd NVARCHAR(50) NOT NULL,
	PasswordHash NVARCHAR(50) NOT NULL,
	DOB NVARCHAR(50) NOT NULL,
	Gender NVARCHAR(50) NOT NULL,
	Mobile NVARCHAR(50) NOT NULL,
	Addrress NVARCHAR(50) NOT NULL
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
	DoctorDentistID INT FOREIGN KEY REFERENCES DoctorDentist(ID) NOT NULL,
	RequestDoctorDentistID INT FOREIGN KEY REFERENCES DoctorDentist(ID) NOT NULL,
	Remarks NVARCHAR(50) NOT NULL,
	AppointmentDate DATE NOT NULL,
	AppointmentTime INT NOT NULL
)

Create table Treatment
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	TreatmentID NVARCHAR(50) NOT NULL,
	TreatmentName NVARCHAR(50) NOT NULL,
	TreatmentDesc NVARCHAR(50) NOT NULL,
	IsFollowUp BIT NOT NULL
)



Create table ClinicHospitalTreatment
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	TreatmentID INT FOREIGN KEY REFERENCES Treatment(ID) NOT NULL,
	ClinicHospitalID INT FOREIGN KEY REFERENCES ClinicHospital(ID) NOT NULL,
	Price MONEY NOT NULL
)

Create table AppointmentTreatment
(
	ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	AppointmentID INT FOREIGN KEY REFERENCES Appointment(ID) NOT NULL,
	TreatmentID INT FOREIGN KEY REFERENCES Treatment(ID) NOT NULL
)

