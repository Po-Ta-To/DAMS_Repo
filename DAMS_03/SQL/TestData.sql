Use DAMS_01
Go


INSERT INTO Treatment
Values ('T111', 'Treatment One', 
'Treatment one desc One one', 0)

INSERT INTO Treatment
Values ('T222', 'Treatment Two', 
'Treatment one desc Two two', 1);

INSERT INTO Treatment
Values ('T333', 'Treatment Three', 
'Treatment one desc Three three', 1);

INSERT INTO Treatment
Values ('T444', 'Treatment Four', 
'Treatment one desc Fout four', 0);

INSERT INTO Treatment
Values ('T555', 'Treatment Five', 
'Treatment one desc Five five', 0);

INSERT INTO Treatment
Values ('T666', 'Treatment Six', 
'Treatment one desc Six six', 0);

INSERT INTO ClinicHospital
Values ('H111', 'Hospital One',
'Hospital One Address at Singapore 111111',
'Hospital One opening hrs desc',
'Hosp One Tel: 1234556',
'HospOne@hosp.com',
1);

INSERT INTO OpeningHours
Values(1,
'01:00:00',
'02:00:00',
1)

INSERT INTO OpeningHours
Values(2,
'03:00:00',
'04:00:00',
1)

INSERT INTO OpeningHours
Values(3,
'00:00:00',
'00:00:00',
1)

INSERT INTO ClinicHospital
Values ('H222', 'Hospital Two',
'Hospital Two Address at Singapore 222222',
'Hospital Two opening hrs desc',
'Hosp Two Tel: 1234566',
'HospTwo@hosp.com',
1);

INSERT INTO OpeningHours
Values(1,
'05:00:00',
'06:00:00',
2)

INSERT INTO OpeningHours
Values(2,
'07:00:00',
'08:00:00',
2)

INSERT INTO OpeningHours
Values(3,
'09:00:00',
'10:00:00',
2)

INSERT INTO ClinicHospital
Values ('H333', 'Hospital Three',
'Hospital Three Address at Singapore 333333',
'Hospital Three opening hrs desc',
'Hosp Three Tel: 1234666',
'HospThree@hosp.com',
1);

INSERT INTO OpeningHours
Values(1,
'11:00:00',
'12:00:00',
3)

INSERT INTO OpeningHours
Values(2,
'13:00:00',
'14:00:00',
3)

INSERT INTO OpeningHours
Values(3,
'15:00:00',
'16:00:00',
3)

INSERT INTO [dbo].[AspNetUsers]
Values ('2a6e9769-165b-4ca1-939f-7b32c1402835', 
'ClerkOne@gmail.com',
0,
'ALNA19rwVg69eJTmIFene5eTEJgCmyZyaebAuxycEQyGftAMwkKYUWLjKrfN007ATw==',
'511459dc-bffc-4c14-8207-28aa0c4ddeff',
NULL,
0,
0,
NULL,
0,
0,
'C111');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('2a6e9769-165b-4ca1-939f-7b32c1402835', 4);

INSERT INTO [dbo].[AdminAccount]
Values ('OrgC111',
'ClerkOne',
'ClerkOne@gmail.com',
3,
'2a6e9769-165b-4ca1-939f-7b32c1402835');

INSERT INTO [dbo].[AdminAccountClinicHospital]
Values (
1, 2);


INSERT INTO [dbo].[AspNetUsers]
Values ('4cc453e5-c9e0-4cea-8275-e93807fc92fe', 
'ClerkTwo@gmail.com',
0,
'AM3vvyf9DfFZSZHaFbO3fN6cRQIkKyfAjuof+qa5Qnt053b+XKrB1Ql4k92z8jut1w==',
'fe1abd5b-94bd-4429-b4ad-6c2957645e83',
NULL,
0,
0,
NULL,
0,
0,
'C222');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('4cc453e5-c9e0-4cea-8275-e93807fc92fe', 4);

INSERT INTO [dbo].[AdminAccount]
Values ('OrgC222',
'ClerkTwo',
'ClerkTwo@gmail.com',
3,
'4cc453e5-c9e0-4cea-8275-e93807fc92fe');

INSERT INTO [dbo].[AdminAccountClinicHospital]
Values (
1, 3);

INSERT INTO [dbo].[AspNetUsers]
Values ('8b107064-b293-4745-9a26-fe3ca10f1d0f', 
'ClerkThree@gmail.com',
0,
'AHRuR0+GgT0mts0VyaYAFlRjdS/ZB00amyZaKQp6CzzlRmmlnof4uICQEkd85fsjdw==',
'b4d89d5d-b81d-4491-981a-0d8d8c68106e',
NULL,
0,
0,
NULL,
0,
0,
'C333');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('8b107064-b293-4745-9a26-fe3ca10f1d0f', 4);

INSERT INTO [dbo].[AdminAccount]
Values ('OrgC333',
'ClerkThree',
'ClerkThree@gmail.com',
3,
'8b107064-b293-4745-9a26-fe3ca10f1d0f');

INSERT INTO [dbo].[AdminAccountClinicHospital]
Values (
2, 4);


INSERT INTO [dbo].[AspNetUsers]
Values ('ea97021a-8eb8-4a9d-af45-f2244634d79c', 
'AdminOne@gmail.com',
0,
'AGJtmqi6/x6r1yjh9wL3YpWL/gjHpN1nTSkeQSGkKpB9E1vc1clxcoAY0GIpNT/K3A==',
'b6746ac9-abf0-4e04-94f5-04d913eada86',
NULL,
0,
0,
NULL,
0,
0,
'A111');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('ea97021a-8eb8-4a9d-af45-f2244634d79c', 3);

INSERT INTO [dbo].[AdminAccount]
Values ('OrgA111',
'AdminOne',
'AdminOne@gmail.com',
2,
'ea97021a-8eb8-4a9d-af45-f2244634d79c');

INSERT INTO [dbo].[AdminAccountClinicHospital]
Values (
1, 5);


INSERT INTO [dbo].[AspNetUsers]
Values ('ceead7d6-e5b7-44a1-8f47-47e003d349a1', 
'AdminTwo@gmail.com',
0,
'ADBUA3xdIVBe3s77NKQyFQzyILs2w6b6RwiNkNfjhAl99Dm/G3bU9RLRa+Qp3LNe3A==',
'dde4d02c-fbe3-48f1-b2f9-54b5225d51ec',
NULL,
0,
0,
NULL,
0,
0,
'A222');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('ceead7d6-e5b7-44a1-8f47-47e003d349a1', 3);

INSERT INTO [dbo].[AdminAccount]
Values ('OrgA222',
'AdminTwo',
'AdminTwo@gmail.com',
2,
'ceead7d6-e5b7-44a1-8f47-47e003d349a1');

INSERT INTO [dbo].[AdminAccountClinicHospital]
Values (
2, 6);



INSERT INTO [dbo].[AspNetUsers]
Values ('82c2540d-ef60-4681-9a0b-abbfad07339f', 
'UserTwo@gmail.com',
0,
'AKxueI098MrJGAhs+W66iW5el55ipTcmRIF0VD3uzHrd/5j5O8OFTkbQP7MdIq2B4Q==',
'ddec61d2-c492-466f-92f8-9f3fd3029621',
NULL,
0,
0,
NULL,
0,
0,
'U111');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('82c2540d-ef60-4681-9a0b-abbfad07339f', 1);

INSERT INTO [dbo].[UserAccount]
Values ('U1111111A',
'UserOneName',
'UserOneDob',
'UserOneGender',
'123456789',
'UserOneAddress',
'82c2540d-ef60-4681-9a0b-abbfad07339f',
0);

INSERT INTO [dbo].[AspNetUsers]
Values ('009115f3-68ed-4215-bba7-d48551dc4e5f', 
'UserTwo@gmail.com',
0,
'ANgyghUp901j/H0PnM4MgFXB+8895RtnDutvyDdRnRtvOxwGSMq4iub0ZpbVSs/+FQ==',
'002ff1ee-5134-4528-b552-acd41f7f470f',
NULL,
0,
0,
NULL,
0,
0,
'U222');

INSERT INTO [dbo].[AspNetUserRoles]
Values ('009115f3-68ed-4215-bba7-d48551dc4e5f', 1);

INSERT INTO [dbo].[UserAccount]
Values ('U2222222A',
'UserTwoName',
'UserTwoDob',
'UserTwoGender',
'223456789',
'UserTwoAddress',
'009115f3-68ed-4215-bba7-d48551dc4e5f',
0);




INSERT INTO [dbo].[DoctorDentist]
Values('D111',
'Dentist One',
10,
1);

INSERT INTO [dbo].[DoctorDentist]
Values('D222',
'Dentist Two',
11,
2);

INSERT INTO [dbo].[DoctorDentist]
Values('D333',
'Dentist Three',
12,
3);